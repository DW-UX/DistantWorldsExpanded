// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Tutorial
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Tutorial
    {
        private string string_0;

        private int int_0;

        private TutorialItemList tutorialItemList_0;

        public string Name
        {
            get
            {
                return string_0;
            }
            set
            {
                string_0 = value;
            }
        }

        public int Index
        {
            get
            {
                return int_0;
            }
            set
            {
                int_0 = value;
            }
        }

        public TutorialItemList Items
        {
            get
            {
                return tutorialItemList_0;
            }
            set
            {
                tutorialItemList_0 = value;
            }
        }

        public bool LastStep
        {
            get
            {
                if (int_0 == tutorialItemList_0.Count - 1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool Finished
        {
            get
            {
                if (int_0 >= tutorialItemList_0.Count)
                {
                    return true;
                }
                return false;
            }
        }

        public TutorialItem PreviousStep
        {
            get
            {
                if (tutorialItemList_0.Count > 1 && int_0 <= tutorialItemList_0.Count && int_0 >= 0)
                {
                    return tutorialItemList_0[int_0];
                }
                return null;
            }
        }

        public TutorialItem CurrentStep
        {
            get
            {
                if (int_0 < tutorialItemList_0.Count)
                {
                    return tutorialItemList_0[int_0];
                }
                return null;
            }
        }

        public void ClearData()
        {
            if (tutorialItemList_0 != null)
            {
                tutorialItemList_0.Clear();
            }
        }

        public void Next()
        {
            int_0++;
        }

        public Tutorial():base()
        {
            Class7.VEFSJNszvZKMZ();
            tutorialItemList_0 = new TutorialItemList();
        }
    }

}
