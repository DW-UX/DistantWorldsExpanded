// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconEmpireMessageListView
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Drawing;

namespace BaconDistantWorlds
{
  public static class BaconEmpireMessageListView
  {
    public static Bitmap BindDataMessageImages(
      EmpireMessageListView emlv,
      EmpireMessageList empireMessages,
      int indexer,
      Bitmap original)
    {
      if (original != null)
        return original;
      Bitmap bitmap = (Bitmap) null;
      try
      {
        EmpireMessage empireMessage = empireMessages[indexer];
        switch (empireMessage.Hint)
        {
          case "ransom":
            bitmap = emlv._MessageImages[31];
            empireMessage.Title = "Ransom";
            break;
          case "distance":
            bitmap = emlv._MessageImages[32];
            empireMessage.Title = "Distance Measurement";
            break;
          case "scienceResearchComplete":
            bitmap = emlv._MessageImages[33];
            empireMessage.Title = "Science ship discovery";
            break;
          case "prisonbreak":
            bitmap = emlv._MessageImages[34];
            empireMessage.Title = "Spy Escaped";
            break;
          case "loanPayment":
            bitmap = emlv._MessageImages[35];
            empireMessage.Title = "Loan Payment";
            break;
          case "exploreRuins":
            bitmap = emlv._MessageImages[36];
            empireMessage.Title = "Explore Ruins";
            break;
          case "fighterRepaired":
            bitmap = emlv._MessageImages[37];
            empireMessage.Title = "Fighter Repaired";
            break;
          default:
            bitmap = emlv._MessageImages[30];
            break;
        }
      }
      catch (Exception ex)
      {
        if (BaconBuiltObject.myMain == null)
          ;
        return bitmap;
      }
      return bitmap;
    }
  }
}
