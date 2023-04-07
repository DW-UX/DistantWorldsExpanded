// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.TextResolver
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DistantWorlds.Types
{
  public static class TextResolver
  {
    private static Dictionary<string, string> _Text = new Dictionary<string, string>();

    private static void Clear() => TextResolver._Text.Clear();

    public static void LoadText(string filename)
    {
      if (!File.Exists(filename))
        return;
      int num = 0;
      try
      {
        TextResolver.Clear();
        string str1 = ";";
        using (FileStream fileStream = File.OpenRead(filename))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.Unicode))
          {
            while (!streamReader.EndOfStream)
            {
              ++num;
              string str2 = streamReader.ReadLine();
              if (!string.IsNullOrEmpty(str2) && str2.Trim() != string.Empty && str2.Trim().Substring(0, 1) != "'")
              {
                int length = str2.IndexOf(str1);
                if (length >= 0)
                {
                  string key = str2.Substring(0, length).Trim();
                  string str3 = str2.Substring(length + 1, str2.Length - (length + 1)).Trim().Replace("\\n", Environment.NewLine);
                  if (!string.IsNullOrEmpty(key))
                  {
                    if (TextResolver._Text.ContainsKey(key))
                      throw new ApplicationException("Dictionary already contains tag: " + key);
                    TextResolver._Text.Add(key, str3);
                  }
                }
              }
            }
          }
        }
      }
      catch (ApplicationException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error at line " + num.ToString() + " reading file " + filename);
      }
    }

    public static string GetText(string tag) => TextResolver._Text.ContainsKey(tag) ? TextResolver._Text[tag] : "KEY NOT FOUND: '" + tag + "'";
  }
}
