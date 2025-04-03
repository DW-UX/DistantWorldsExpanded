using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;
using HtmlAgilityPack;
using MimeKit;
using MimeKit.IO;
using MimeKit.Text;

namespace DistantWorlds;

public static class WebBrowserExtensions
{

    static string TranslateMimeUri(string url, MultipartRelated root, string documentPath)
    {
        try
        {
            /*{
              // for debugging
              using var tempContent = root.Open(new(url), out _, out _);
              var tempSr = new StreamReader(tempContent);
              var tempStr = tempSr.ReadToEnd();
            }*/
            using var content = root.Open(new(url), out var mimeType, out var charset);
            /* maybe use this if under 32kb when encoded
            using var b64Tx = new ToBase64Transform();
            using var ctx = new CryptoStream(content, b64Tx, CryptoStreamMode.Read);
            using var sr = new StreamReader(ctx);
            if (!string.IsNullOrEmpty(charset))
              mimeType += $";charset={charset}";
            return $"data:{mimeType};base64,{sr.ReadToEnd()}";
            */
            var subDir = documentPath + ".parts";
            Directory.CreateDirectory(subDir);
            var partPath = Path.Combine(subDir, Path.GetFileName(url));
            using var fs = File.Create(partPath);
            content.CopyTo(fs);
            var newUrl = new Uri(partPath).ToString();
            return newUrl;
        }
        catch
        {
            // not in the mime document
            return null;
        }
    }

    public static unsafe void Navigate2(this WebBrowser browser, string url)
    {
        if (!url.EndsWith(".mht"))
        {
            browser.Navigate(url);
            Synchronize(browser);
            return;
        }

        var uri = new Uri(url);
        var localPath = uri.LocalPath;
        for (; ; )
        {
            var replaced = localPath.Replace(@"\\", @"\");
            if (replaced == localPath) break;

            localPath = replaced;
        }

        var txPath = localPath + ".html";
        var txUri = new Uri(txPath).ToString();
        //if (File.Exists(txPath))
        //{
        //    browser.Navigate(txUri);
        //    Synchronize(browser);
        //    return;
        //}

        var file = new FileInfo(localPath);

        file.WithMappedFileContentStream(ums =>
        {
            var parser = new MimeKit.MimeParser(ums, MimeKit.MimeFormat.Default);
            var message = parser.ParseMessage();
            // build mapping of content location to attachment
            Dictionary<string, MimeEntity> entityMap = new();
            foreach (var attachment in message.BodyParts)
                entityMap[attachment.Headers["Content-Location"]] = attachment;

            // maybe we can get the content length and pre-size memory stream?
            using var ms = new MemoryStream();
            if (message.Body is not MultipartRelated body)
                throw new NotImplementedException();

            var bodyLoc = body.Root.ContentLocation;

            //body.WriteTo(ms, true);
            //ms.Position = 0;
            var doc = new HtmlAgilityPack.HtmlDocument();
            var html = message.HtmlBody;
            using (var sw = new StreamWriter(ms, leaveOpen: true))
                sw.Write(html);
            ms.Position = 0;
            /*using (var sr = new StreamReader(ms, leaveOpen: true))
              if (html != sr.ReadToEnd())
                Debugger.Break();
            ms.Position = 0;*/
            doc.Load(ms);

            var docNode = doc.DocumentNode;
            // scan for urls that match embedded entities and replace with data uris
            var metaLinks = docNode.SelectNodes("//link[@href]");
            if (metaLinks != null)
            {
                foreach (var link in metaLinks)
                {
                    var href = link.Attributes["href"].Value;
                    var absHref = new Uri(bodyLoc, href).ToString();
                    var newHref = TranslateMimeUri(absHref, body, localPath);
                    if (absHref == newHref)
                        continue;

                    link.Attributes["href"].Value = newHref;
                }
            }

            var links = docNode.SelectNodes("//a[@href]");
            if (links != null)
            {
                foreach (var link in links)
                {
                    var href = link.Attributes["href"].Value;
                    var absHref = new Uri(bodyLoc, href).ToString();
                    var newHref = TranslateMimeUri(absHref, body, localPath);
                    if (absHref == newHref)
                        continue;

                    link.Attributes["href"].Value = newHref;
                }
            }

            var images = docNode.SelectNodes("//img[@src]");
            if (images != null)
            {
                foreach (var image in images)
                {
                    var href = image.Attributes["src"].Value;

                    // Check if the href is already a data URI
                    if (href.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                    {
                        // Data URIs don't need to be resolved against the base URI
                        // They can be passed directly to TranslateMimeUri
                        //var newHrefImg = TranslateMimeUri(href, body, localPath);
                        //if (newHrefImg != null)
                        //{
                            image.Attributes["src"].Value = href;
                        //}
                        continue;
                    }

                    // Normal URI processing for non-data URIs
                    var absHref = new Uri(bodyLoc, href).ToString();
                    var newHref = TranslateMimeUri(absHref, body, localPath);
                    if (absHref == newHref)
                        continue;

                    image.Attributes["src"].Value = newHref;
                }
            }

            //using var fixedMs = new MemoryStream();
            //doc.Save(fixedMs);
            //fixedMs.Position = 0;
            //using var streamReader = new StreamReader(fixedMs);
            //var htmlContent = streamReader.ReadToEnd();

            // strip IE conditional comments and VML
            doc.DocumentNode.Descendants()
            .Where(n => n.NodeType == HtmlNodeType.Comment)
            .ToList()
            .ForEach(n => n.Remove());

            doc.Save(txPath);
            //var htmlContent = doc.DocumentNode.OuterHtml;
            //browser.Navigate("about:blank");
            //browser.Document!.Write(htmlContent);
            browser.Navigate(txUri);
            Synchronize(browser);
        });
    }

    private static void Synchronize(WebBrowser browser)
    {
        while (browser.ReadyState
               is WebBrowserReadyState.Uninitialized
               or WebBrowserReadyState.Loading)
            Application.DoEvents();

        var unscaleFactor = 96.0 / browser.DeviceDpi;

        // wait an extra split sec
        var future = DateTime.Now.AddMilliseconds(20);
        while (DateTime.Now < future)
            Application.DoEvents();

        try
        {
            var doc = browser.Document!;
            var scriptElem = doc.CreateElement("script")!;
            scriptElem.OuterHtml = //language=html
              """
        <script type="text/javascript">
        // do nothing but init js ffs
        </script>
        """;
            scriptElem.SetAttribute("type", "text/javascript");
            doc.GetElementsByTagName("head")[0].AppendChild(scriptElem);
            doc.InvokeScript("eval", new object[] {
        $"""
        document.body.style.zoom = {unscaleFactor};
        """
      });
        }
        catch
        {
            // oof
        }
    }

}