// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxySummaryList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxySummaryList : List<GalaxySummary>
  {
    public void LoadFromFolder(string folderPath)
    {
      if (!Directory.Exists(folderPath))
        return;
      foreach (FileInfo file in new DirectoryInfo(folderPath).GetFiles("*.dwg"))
      {
        if (file != null)
        {
          using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
          {
            GalaxySummary galaxySummary = GalaxySummary.ReadGalaxySummary((Stream) fileStream);
            galaxySummary.Filename = file.Name;
            galaxySummary.Filepath = file.FullName;
            this.Add(galaxySummary);
          }
        }
      }
      this.Sort();
    }
  }
}
