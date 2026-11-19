// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ShipImageHelper
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll


using System;

namespace DistantWorlds.Types
{
    [Serializable]
    public static class ShipImageHelper
    {
        private static Random _Rnd = new Random((int)DateTime.Now.Ticks);

        public static readonly int ShakturiFamily = 0;

        public static readonly int ShakturiAlliesFamily = 1;

        public static readonly int AncientHelpersFamily = 2;

        public static readonly int FreedomAllianceFamily = 3;

        public static readonly int ShipSetImageCount = 24;

        public static readonly int ShipSetFighterImageCount = 2;

        private static readonly int MinorFamilyCount = 7;

        private static readonly int MinorBaseCount = 2;

        public static readonly int PlanetDestroyer = 0;

        private static readonly int MinorBaseStartIndex = 1;

        private static readonly int MinorShipStartIndex = 3;

        private static readonly int MajorShipStartIndex = 31;

        private static readonly int SuperPiratesStartIndex = 64;

        public static readonly int StandardShipImageStartIndex = 72;

        public static int ResolveStandardShipImageByFamilyAndSubRole(int standardFamily, BuiltObjectSubRole subRole)
        {
            return StandardShipImageStartIndex + standardFamily * (Enum.GetValues(typeof(BuiltObjectSubRole)).Length - 1) + (int)(subRole - 1);
        }

        public static int ResolveNewFighterImageIndex(Race empireRace, bool isPirates)
        {
            int num = 0;
            if (empireRace != null)
            {
                num = empireRace.DesignPictureFamilyIndex;
                if (isPirates && empireRace.DesignPictureFamilyIndexPirates > 0)
                {
                    num = empireRace.DesignPictureFamilyIndexPirates;
                }
            }
            int shipSetFighterImageCount = ShipSetFighterImageCount;
            return num * shipSetFighterImageCount;
        }

        public static int ResolveNewBomberImageIndex(Race empireRace, bool isPirates)
        {
            int num = 0;
            if (empireRace != null)
            {
                num = empireRace.DesignPictureFamilyIndex;
                if (isPirates && empireRace.DesignPictureFamilyIndexPirates > 0)
                {
                    num = empireRace.DesignPictureFamilyIndexPirates;
                }
            }
            int shipSetFighterImageCount = ShipSetFighterImageCount;
            return num * shipSetFighterImageCount + 1;
        }

        public static int ResolveNewShipImageIndex(BuiltObjectSubRole shipSubRole, Race empireRace, bool isPirates)
        {
            int shipSetImageCount = ShipSetImageCount;
            shipSubRole = Galaxy.ResolveLegacySubRole(shipSubRole);
            int num = 0;
            if (empireRace != null)
            {
                num = empireRace.DesignPictureFamilyIndex;
                if (isPirates)
                {
                    num = empireRace.DesignPictureFamilyIndexPirates;
                }
            }
            return StandardShipImageStartIndex + num * shipSetImageCount + (int)(shipSubRole - 1);
        }

        public static int ResolveSuperPirateShipImageIndex(BuiltObjectSubRole subRole)
        {
            int num = 0;
            switch (subRole)
            {
                case BuiltObjectSubRole.Escort:
                    num = 0;
                    break;
                case BuiltObjectSubRole.Frigate:
                    num = 1;
                    break;
                case BuiltObjectSubRole.Destroyer:
                    num = 2;
                    break;
                case BuiltObjectSubRole.Cruiser:
                    num = 3;
                    break;
                case BuiltObjectSubRole.CapitalShip:
                    num = 4;
                    break;
                case BuiltObjectSubRole.Carrier:
                    num = 5;
                    break;
                case BuiltObjectSubRole.DefensiveBase:
                    num = 6;
                    break;
                case BuiltObjectSubRole.GenericBase:
                    num = 7;
                    break;
            }
            int superPiratesStartIndex = SuperPiratesStartIndex;
            return num + superPiratesStartIndex;
        }

        public static int ResolveMajorShipImageIndex(int family, BuiltObjectSubRole subRole, bool aged)
        {
            int num2 = 0;
            switch (subRole)
            {
                case BuiltObjectSubRole.CapitalShip:
                    num2 = 3;
                    break;
                case BuiltObjectSubRole.Cruiser:
                    num2 = 2;
                    break;
                case BuiltObjectSubRole.Destroyer:
                    num2 = 1;
                    break;
                case BuiltObjectSubRole.Frigate:
                    num2 = 0;
                    break;
                case BuiltObjectSubRole.TroopTransport:
                    num2 = 4;
                    break;
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.MediumSpacePort:
                case BuiltObjectSubRole.Outpost:
                    num2 = 6;
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                case BuiltObjectSubRole.MiningStation:
                case BuiltObjectSubRole.GenericBase:
                    num2 = 5;
                    break;
            }
            int num3 = 0;
            if (aged && family == FreedomAllianceFamily)
            {
                num3 = 7;
            }
            int num4 = ResolveMajorFamilyIndex(family);
            return num4 + num2 + num3;
        }

        private static int ResolveMajorFamilyIndex(int majorFamily)
        {
            int majorShipStartIndex = MajorShipStartIndex;
            return majorShipStartIndex + majorFamily * 7;
        }

        private static int ResolveMinorFamilyIndex(int minorFamily)
        {
            int minorShipStartIndex = MinorShipStartIndex;
            return minorShipStartIndex + minorFamily * 4;
        }

        public static int ResolveMinorShipImageIndex(BuiltObjectSubRole subRole, bool largeShips)
        {
            return ResolveMinorShipImageIndex(_Rnd.Next(0, MinorFamilyCount), subRole, largeShips);
        }

        public static int ResolveMinorShipImageIndex(int family, BuiltObjectSubRole subRole, bool largeShips)
        {
            int num = -1;
            int num2 = 0;
            switch (subRole)
            {
                case BuiltObjectSubRole.GasMiningStation:
                case BuiltObjectSubRole.MiningStation:
                case BuiltObjectSubRole.SmallSpacePort:
                case BuiltObjectSubRole.GenericBase:
                case BuiltObjectSubRole.Outpost:
                    if (_Rnd.Next(0, 2) == 1)
                    {
                        int family2 = _Rnd.Next(1, 3);
                        num = ResolveMajorShipImageIndex(family2, subRole, aged: false);
                    }
                    else
                    {
                        num = MinorBaseStartIndex + _Rnd.Next(0, MinorBaseCount);
                    }
                    break;
                case BuiltObjectSubRole.Escort:
                    num2 = 0;
                    break;
                case BuiltObjectSubRole.Frigate:
                    num2 = 1;
                    break;
                case BuiltObjectSubRole.Destroyer:
                    num2 = ((!largeShips) ? 2 : 0);
                    break;
                case BuiltObjectSubRole.Cruiser:
                    num2 = 1;
                    break;
                case BuiltObjectSubRole.CapitalShip:
                    num2 = 2;
                    break;
                case BuiltObjectSubRole.ColonyShip:
                    num2 = 3;
                    break;
            }
            if (num < 0)
            {
                int num3 = ResolveMinorFamilyIndex(family);
                num = num3 + num2;
            }
            return num;
        }
    }
}
