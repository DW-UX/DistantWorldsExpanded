// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DialogSet
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DialogSet
  {
    private DialogPartList _BaseDialogParts;
    private List<DialogPartList> _DialogParts = new List<DialogPartList>();

    public void Initialize(string baseDialogPath, string customDialogPath, RaceList races)
    {
      this._BaseDialogParts = this.ParseDialogFile(baseDialogPath + "base_dialog.txt");
      foreach (Race race in (SyncList<Race>) races)
      {
        string str1 = race.Name.Replace("'", "");
        string str2 = baseDialogPath + str1 + ".txt";
        string str3 = customDialogPath + str1 + ".txt";
        if (File.Exists(str3))
        {
          DialogPartList dialogFile = this.ParseDialogFile(str3);
          dialogFile.Race = race;
          this._DialogParts.Add(dialogFile);
        }
        else if (File.Exists(str2))
        {
          DialogPartList dialogFile = this.ParseDialogFile(str2);
          dialogFile.Race = race;
          this._DialogParts.Add(dialogFile);
        }
      }
    }

    private DialogPartList ParseDialogFile(string filename)
    {
      DialogPartList dialogFile = new DialogPartList();
      string str1 = ";";
      StreamReader streamReader = new StreamReader((Stream) File.OpenRead(filename), Encoding.Default);
      while (!streamReader.EndOfStream)
      {
        string str2 = streamReader.ReadLine();
        if (!string.IsNullOrEmpty(str2) && str2.Trim() != string.Empty && str2.Trim().Substring(0, 1) != "'")
        {
          int length = str2.IndexOf(str1);
          if (length >= 0)
          {
            string code = str2.Substring(0, length).Trim();
            string dialog = str2.Substring(length + 1, str2.Length - (length + 1)).Trim().Replace("\\n", Environment.NewLine);
            DialogPartType type = this.ResolveDialogPartType(code);
            if (type != DialogPartType.Undefined && dialogFile[type] == null)
              dialogFile.Add(new DialogPart(type, dialog));
          }
        }
      }
      return dialogFile;
    }

    private DialogPartType ResolveDialogPartType(string code)
    {
      DialogPartType dialogPartType = DialogPartType.Undefined;
      if (Enum.IsDefined(typeof (DialogPartType), (object) code))
        dialogPartType = (DialogPartType) Enum.Parse(typeof (DialogPartType), code);
      return dialogPartType;
    }

    public string ResolveDialog(DialogPartType type)
    {
      DialogPart baseDialogPart = this._BaseDialogParts[type];
      return baseDialogPart != null ? baseDialogPart.Dialog : string.Empty;
    }

    public string ResolveDialog(DialogPartType type, Race race)
    {
      foreach (DialogPartList dialogPart1 in this._DialogParts)
      {
        if (dialogPart1.Race == race)
        {
          DialogPart dialogPart2 = dialogPart1[type];
          if (dialogPart2 != null)
            return dialogPart2.Dialog;
        }
      }
      DialogPart baseDialogPart = this._BaseDialogParts[type];
      return baseDialogPart != null ? baseDialogPart.Dialog : string.Empty;
    }
  }
}
