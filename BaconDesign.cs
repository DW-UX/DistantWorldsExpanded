// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconDesign
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
    public static class BaconDesign
    {
        public static Stopwatch sw = new Stopwatch();
        public static int designRedefineCounter = 0;

        public static int ModifyBoardingStrength(Design ship)
        {
            int num = 1;
            if (ship != null && ship.Empire != null && ship.Empire.Name.Contains("Romulan"))
                num = 1;
            return num;
        }

        public static int SetPictureRef(Design design, int originalValue)
        {
            if (originalValue < 72)
                return originalValue;
            string name = new StackTrace().GetFrame(1).GetMethod().Name;
            if (name == "PrepareDesignForEditor" || name == "Clone")
                return originalValue;
            Empire empire = design.Empire;
            if (empire != null)
                ;
            BuiltObjectSubRole builtObjectSubRole = design.SubRole;
            int num1;
            switch (builtObjectSubRole)
            {
                case BuiltObjectSubRole.EnergyResearchStation:
                case BuiltObjectSubRole.WeaponsResearchStation:
                case BuiltObjectSubRole.HighTechResearchStation:
                    num1 = 1;
                    break;
                default:
                    num1 = builtObjectSubRole == BuiltObjectSubRole.MonitoringStation ? 1 : 0;
                    break;
            }
            if (num1 != 0)
                builtObjectSubRole = BuiltObjectSubRole.GenericBase;
            else if (builtObjectSubRole == BuiltObjectSubRole.DefensiveBase)
                builtObjectSubRole = BuiltObjectSubRole.MediumSpacePort;
            string subRoleAsString = Enum.GetName(typeof(BuiltObjectSubRole), (object)builtObjectSubRole);
            string family = "family";
            int num2 = empire.PirateEmpireBaseHabitat != null ? design.Empire.DominantRace.DesignPictureFamilyIndexPirates : empire.DesignPictureFamilyIndex;
            family += num2.ToString();
            IEnumerable<string> source = BaconBuiltObjectImageCache.shipPictures.Where<string>((Func<string, bool>)(path => path.Contains(family)));
            string str = (string)null;
            if (BaconBuiltObject.myMain == null || BaconBuiltObject.myMain._Game == null || design.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
            {
                str = source.FirstOrDefault<string>((Func<string, bool>)(path2 => Path.GetFileNameWithoutExtension(path2) == subRoleAsString));
            }
            else
            {
                List<string> list = source.Where<string>((Func<string, bool>)(path2 => Path.GetFileNameWithoutExtension(path2) == subRoleAsString)).ToList<string>();
                if (list.Count < 1)
                {
                    int num3 = (int)MessageBox.Show("No image for " + subRoleAsString + " for " + family, "Missing image in mod");
                }
                else
                    str = list[Galaxy.Rnd.Next(0, list.Count - 1)];
            }
            if (str != null)
                originalValue = BaconBuiltObjectImageCache.shipPictures.IndexOf(str);
            return originalValue;
        }

        public static void ctlDesignComponents_SelectionChanged(Main main, object sender, EventArgs e)
        {
            Component selectedComponent = main.ctlDesignComponents.SelectedComponent;
            string empty = string.Empty;
            if (selectedComponent == null)
                return;
            string name = selectedComponent.Name;
            if (name != string.Empty)
            {
                for (int index = 0; index < main.ctlDesignComponentToolbox.Rows.Count; ++index)
                {
                    object obj = main.ctlDesignComponentToolbox.Rows[index].Cells[2].Value;
                    if (obj != null && obj is string && (string)obj == name)
                    {
                        main.ctlDesignComponentToolbox.Grid.Rows[index].Selected = true;
                        main.ctlDesignComponentToolbox.Grid.FirstDisplayedScrollingRowIndex = index;
                        main.ctlDesignComponentToolbox.SelectRow(index, true);
                        break;
                    }
                }
            }
        }

        public static void RedefineAllBases(Main main)
        {
            try
            {
                foreach (Empire empire in (SyncList<Empire>)main._Game.Galaxy.Empires)
                {
                    if (empire != main._Game.Galaxy.IndependentEmpire)
                    {
                        foreach (Design design in (SyncList<Design>)empire.Designs)
                        {
                            if (design.Role == BuiltObjectRole.Base)
                                BaconDesign.Redefine(design);
                        }
                    }
                }
                foreach (Empire pirateEmpire in (SyncList<Empire>)main._Game.Galaxy.PirateEmpires)
                {
                    if (pirateEmpire.Designs != null && pirateEmpire.Designs.Count != 0)
                    {
                        foreach (Design design in (SyncList<Design>)pirateEmpire.Designs)
                        {
                            if (design.Role == BuiltObjectRole.Base)
                                BaconDesign.Redefine(design);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void Redefine(Design design)
        {
            try
            {
                Empire empire = design.Empire;
                if (empire == null)
                    return;
                empire.BuiltObjects.ForEach((Action<BuiltObject>)(x =>
                {
                    if (x.Design != design)
                        return;
                    x.ReDefine();
                }));
                empire.PrivateBuiltObjects.ForEach((Action<BuiltObject>)(x =>
                {
                    if (x.Design != design)
                        return;
                    x.ReDefine();
                }));
            }
            catch (Exception ex)
            {
            }
        }

        public static double CalculateMaintenanceCosts(Design design, Galaxy galaxy, Empire empire)
        {
            double num1 = (empire.PirateEmpireBaseHabitat != null ? (double)((int)(design.CalculateCurrentPurchasePrice(galaxy) / (Galaxy.ShipMarkupFactorPirates * 2.0)) + 1) : (double)((int)(design.CalculateCurrentPurchasePrice(galaxy) / Galaxy.ShipMarkupFactor) + 1)) + Galaxy.ShipMaintenanceCostPerSizeUnit * (double)design.Size;
            double num2 = 0.0;
            if (empire != null && empire.DominantRace != null && empire.DominantRace.ChangePeriodActive && empire.DominantRace.PeriodicRaceEvent == RaceEventType.StrengthInNumbersMaintenanceLowerForSmallShips && design.Size <= 200)
                num2 = 0.25;
            double num3 = (double)Design.GetLeaderMaintenanceBonuses(design, empire) / 100.0;
            double num4 = Math.Min(1.0, design.MaintenanceSavings + num2 + num3) * num1;
            double num5 = 1.0;
            if (empire != null && empire.GovernmentAttributes != null)
                num5 = empire.GovernmentAttributes.MaintenanceCosts;
            if (empire != null && empire.PirateEmpireBaseHabitat != null)
            {
                double num6 = Math.Sqrt((double)galaxy.BaseTechCost / 120000.0);
                num5 *= galaxy.PirateShipMaintenanceFactor * num6;
            }
            return (num1 - num4) * num5;
        }
    }
}
