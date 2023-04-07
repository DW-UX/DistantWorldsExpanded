// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryColony
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireSummaryColony : GradientPanel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private Empire empire_0;

        private int int_0;

        private int int_1;

        private int int_2;

        private int int_3;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private SolidBrush solidBrush_3;

        private Font font_0;

        private Font font_1;

        public EmpireSummaryColony() : base()
        {
            Class7.VEFSJNszvZKMZ();
            int_0 = -1;
            int_1 = 15;
            int_2 = 10;
            int_3 = 10;
            solidBrush_0 = new SolidBrush(Color.FromArgb(170, 170, 170));
            solidBrush_1 = new SolidBrush(Color.Black);
            solidBrush_2 = new SolidBrush(Color.Red);
            solidBrush_3 = new SolidBrush(Color.Green);
            Font = new Font("Verdana", 8f);
            SetFont(16.67f);
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public void ClearData()
        {
            galaxy_0 = null;
            empire_0 = null;
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Empire empire, int newGovernmentStyle)
        {
            base.BorderStyle = BorderStyle.None;
            base.BackColor = Color.Transparent;
            base.BackColor2 = Color.Transparent;
            base.BackColor3 = Color.Transparent;
            main_0 = parentForm;
            galaxy_0 = galaxy;
            empire_0 = empire;
            int_0 = newGovernmentStyle;
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (empire_0 != null && empire_0.PirateEmpireBaseHabitat != null)
            {
                method_2(e.Graphics);
            }
            else
            {
                method_3(e.Graphics);
            }
        }

        private int method_0(Graphics graphics_0, string string_0, Font font_2, int int_4)
        {
            int num = (int)graphics_0.MeasureString(string_0, font_2, int_4, StringFormat.GenericTypographic).Width;
            return (int_4 - num) / 2;
        }

        private int method_1(Graphics graphics_0, string string_0, Font font_2, int int_4)
        {
            int num = (int)graphics_0.MeasureString(string_0, font_2, int_4, StringFormat.GenericTypographic).Width;
            return int_4 - num;
        }

        private void method_2(Graphics graphics_0)
        {
            int num = 0;
            int num2 = 20;
            int int_ = 103;
            int num3 = 113;
            int num4 = 17;
            int num5 = 0;
            if (empire_0 == null || empire_0.PirateEmpireBaseHabitat == null)
            {
                return;
            }
            BuiltObject builtObject = null;
            if (empire_0.PirateEmpireBaseHabitat.BasesAtHabitat != null)
            {
                for (int i = 0; i < empire_0.PirateEmpireBaseHabitat.BasesAtHabitat.Count; i++)
                {
                    BuiltObject builtObject2 = empire_0.PirateEmpireBaseHabitat.BasesAtHabitat[i];
                    if (builtObject2.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject2.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject2.SubRole == BuiltObjectSubRole.LargeSpacePort)
                    {
                        builtObject = builtObject2;
                    }
                }
            }
            string string_ = TextResolver.GetText("Home Base");
            int num6 = method_1(graphics_0, string_, Font, int_);
            method_8(graphics_0, string_, Font, new Point(num + num6, num5 + 1));
            if (builtObject != null)
            {
                string string_2 = string.Format(TextResolver.GetText("Pirate Home Base at Location"), builtObject.Name, empire_0.PirateEmpireBaseHabitat.Name);
                method_8(graphics_0, string_2, font_0, new Point(num3, num5));
            }
            else
            {
                method_8(graphics_0, empire_0.PirateEmpireBaseHabitat.Name, font_0, new Point(num3, num5));
            }
            num5 += num4;
            string string_3 = TextResolver.GetText("Corruption");
            num6 = method_1(graphics_0, string_3, Font, int_);
            method_8(graphics_0, string_3, Font, new Point(num + num6, num5 + 1));
            method_8(graphics_0, empire_0.Corruption.ToString("0%"), font_0, new Point(num3, num5));
            num5 += num4;
            num5 += num4;
            HabitatList habitatList = new HabitatList();
            HabitatList habitatList2 = new HabitatList();
            for (int j = 0; j < empire_0.Colonies.Count; j++)
            {
                Habitat habitat = empire_0.Colonies[j];
                if (habitat == null || habitat.HasBeenDestroyed)
                {
                    continue;
                }
                if (habitat.Empire == empire_0)
                {
                    habitatList.Add(habitat);
                    continue;
                }
                PirateColonyControl byFaction = habitat.GetPirateControl().GetByFaction(empire_0);
                if (byFaction != null)
                {
                    habitatList2.Add(habitat);
                }
            }
            string text = string.Empty;
            if (habitatList.Count > 0)
            {
                method_8(graphics_0, TextResolver.GetText("Owned Colonies"), font_0, new Point(num, num5));
                for (int k = 0; k < habitatList.Count; k++)
                {
                    Habitat habitat2 = habitatList[k];
                    if (habitat2 != null && !habitat2.HasBeenDestroyed)
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            text += ", ";
                        }
                        text += habitat2.Name;
                    }
                }
                num5 += num4;
                SizeF sizeF_ = graphics_0.MeasureString(text, Font, base.Width - (num2 + int_3));
                method_10(graphics_0, text, Font, new Point(num2, num5), solidBrush_0, sizeF_);
                num5 += (int)sizeF_.Height;
                num5 += num4;
            }
            string text2 = string.Empty;
            method_8(graphics_0, TextResolver.GetText("Controlled Colonies"), font_0, new Point(num, num5));
            if (habitatList2.Count == 0)
            {
                num5 += num4;
                method_8(graphics_0, "(" + TextResolver.GetText("None") + ")", Font, new Point(num2, num5));
                num5 += num4;
            }
            else
            {
                for (int l = 0; l < habitatList2.Count; l++)
                {
                    Habitat habitat3 = habitatList2[l];
                    if (habitat3 == null || habitat3.HasBeenDestroyed)
                    {
                        continue;
                    }
                    PirateColonyControl byFaction2 = habitat3.GetPirateControl().GetByFaction(empire_0);
                    if (byFaction2 != null)
                    {
                        if (!string.IsNullOrEmpty(text2))
                        {
                            text2 += ", ";
                        }
                        text2 += habitat3.Name;
                    }
                }
                num5 += num4;
                SizeF sizeF_2 = graphics_0.MeasureString(text2, Font, base.Width - (num2 + int_3));
                method_10(graphics_0, text2, Font, new Point(num2, num5), solidBrush_0, sizeF_2);
                num5 += (int)sizeF_2.Height;
                num5 += num4;
            }
            string string_4 = string.Format(TextResolver.GetText("Pirate Playstyle Description"), Galaxy.ResolveDescription(empire_0.PiratePlayStyle));
            method_8(graphics_0, string_4, font_0, new Point(num, num5));
            num5 += num4;
            List<double> factorValues = new List<double>();
            List<bool> modifiersAreBonuses = new List<bool>();
            List<string> list = Galaxy.ResolvePirateFactionModifierDescriptions(empire_0.PiratePlayStyle, out factorValues, out modifiersAreBonuses);
            for (int m = 0; m < list.Count; m++)
            {
                Color color = solidBrush_0.Color;
                if (factorValues.Count > m)
                {
                    if (modifiersAreBonuses[m])
                    {
                        if (factorValues[m] > 1.0)
                        {
                            color = Color.Green;
                        }
                        else if (factorValues[m] < 1.0)
                        {
                            color = Color.Red;
                        }
                    }
                    else if (factorValues[m] > 1.0)
                    {
                        color = Color.Red;
                    }
                    else if (factorValues[m] < 1.0)
                    {
                        color = Color.Green;
                    }
                }
                using SolidBrush solidBrush_ = new SolidBrush(color);
                method_9(graphics_0, list[m], Font, new Point(num2, num5), solidBrush_);
                num5 += num4 - 3;
            }
        }

        private void method_3(Graphics graphics_0)
        {
            int num = 0;
            int int_ = 103;
            int num2 = 113;
            int num3 = 2;
            int num4 = 0;
            Point point = new Point(int_3, 0);
            if (empire_0 == null)
            {
                return;
            }
            EmpirePriorityList empirePriorityList = null;
            int num5 = -1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Capitals"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Capitals"), font_2: Font);
            point = new Point(num2, num4 - num3);
            string text = string.Empty;
            for (int i = 0; i < empire_0.Capitals.Count; i++)
            {
                text = text + empire_0.Capitals[i].Name + ", ";
            }
            if (text.Length >= 3)
            {
                text = text.Substring(0, text.Length - 2);
            }
            method_8(graphics_0, text, font_0, point);
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Territory"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Territory"), font_2: Font);
            HabitatList habitatList = new HabitatList();
            foreach (Habitat colony in empire_0.Colonies)
            {
                Habitat item = Galaxy.DetermineHabitatSystemStar(colony);
                if (!habitatList.Contains(item))
                {
                    habitatList.Add(item);
                }
            }
            point = new Point(num2, num4 - num3);
            string string_ = string.Format(TextResolver.GetText("X colonies in Y systems"), empire_0.Colonies.Count.ToString(), habitatList.Count.ToString());
            SizeF sizeF = graphics_0.MeasureString(string_, font_0, base.Width, StringFormat.GenericTypographic);
            method_8(graphics_0, string_, font_0, point);
            empirePriorityList = galaxy_0.DetermineOrderedKnownEmpires(empire_0, EmpireComparisonType.Territory);
            num5 = empirePriorityList.IndexOf(empire_0);
            if (num5 >= 0)
            {
                string_ = "(" + string.Format(TextResolver.GetText("Xth of Y"), Galaxy.OrderedNumberDescription(num5 + 1), empirePriorityList.Count.ToString()) + ")";
            }
            method_8(point_0: new Point(num2 + (int)sizeF.Width + 10, num4), graphics_0: graphics_0, string_0: string_, font_2: Font);
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Population"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Population"), font_2: Font);
            point = new Point(num2, num4 - num3);
            string string_2 = empire_0.TotalPopulation.ToString("0,,M");
            sizeF = graphics_0.MeasureString(string_2, font_0, base.Width, StringFormat.GenericTypographic);
            method_8(graphics_0, string_2, font_0, point);
            empirePriorityList = galaxy_0.DetermineOrderedKnownEmpires(empire_0, EmpireComparisonType.Population);
            num5 = empirePriorityList.IndexOf(empire_0);
            if (num5 >= 0)
            {
                string_2 = "(" + string.Format(TextResolver.GetText("Xth of Y"), Galaxy.OrderedNumberDescription(num5 + 1), empirePriorityList.Count.ToString()) + ")";
            }
            method_8(point_0: new Point(num2 + (int)sizeF.Width + 10, num4), graphics_0: graphics_0, string_0: string_2, font_2: Font);
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Strategic Value"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Strategic Value"), font_2: Font);
            point = new Point(num2, num4 - num3);
            string string_3 = empire_0.TotalColonyStrategicValue.ToString("0,K");
            sizeF = graphics_0.MeasureString(string_3, font_0, base.Width, StringFormat.GenericTypographic);
            method_8(graphics_0, string_3, font_0, point);
            empirePriorityList = galaxy_0.DetermineOrderedKnownEmpires(empire_0, EmpireComparisonType.StrategicValue);
            num5 = empirePriorityList.IndexOf(empire_0);
            if (num5 >= 0)
            {
                string_3 = "(" + string.Format(TextResolver.GetText("Xth of Y"), Galaxy.OrderedNumberDescription(num5 + 1), empirePriorityList.Count.ToString()) + ")";
            }
            method_8(point_0: new Point(num2 + (int)sizeF.Width + 10, num4), graphics_0: graphics_0, string_0: string_3, font_2: Font);
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Reputation"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Reputation"), font_2: Font);
            point = new Point(num2, num4 - num3);
            string string_4 = empire_0.CivilityDescription();
            Color color = Color.Green;
            if (empire_0.CivilityRating < 0.0)
            {
                color = Color.Red;
            }
            using (SolidBrush solidBrush_ = new SolidBrush(color))
            {
                method_9(graphics_0, string_4, font_0, point, solidBrush_);
            }
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("War weariness"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("War weariness"), font_2: Font);
            point = new Point(num2, num4 - num3);
            Color color2 = solidBrush_0.Color;
            if (empire_0.WarWeariness <= 0.0)
            {
                color2 = solidBrush_0.Color;
            }
            else if (empire_0.WarWeariness > 0.0 && empire_0.WarWeariness <= 6.0)
            {
                color2 = Color.FromArgb(255, 255, 0);
            }
            else if (empire_0.WarWeariness >= 6.0 && empire_0.WarWeariness <= 12.0)
            {
                color2 = Color.FromArgb(255, 205, 0);
            }
            else if (empire_0.WarWeariness >= 12.0 && empire_0.WarWeariness <= 18.0)
            {
                color2 = Color.FromArgb(255, 155, 0);
            }
            else if (empire_0.WarWeariness >= 18.0 && empire_0.WarWeariness <= 26.0)
            {
                color2 = Color.FromArgb(255, 105, 0);
            }
            else if (empire_0.WarWeariness >= 26.0 && empire_0.WarWeariness <= 34.0)
            {
                color2 = Color.FromArgb(255, 55, 0);
            }
            else if (empire_0.WarWeariness > 34.0)
            {
                color2 = Color.FromArgb(255, 0, 0);
            }
            using (SolidBrush solidBrush_2 = new SolidBrush(color2))
            {
                int num6 = 0;
                foreach (DiplomaticRelation diplomaticRelation in empire_0.DiplomaticRelations)
                {
                    if (diplomaticRelation.Type == DiplomaticRelationType.War)
                    {
                        num6++;
                    }
                }
                string text2 = Galaxy.ResolveWarWearinessDescription(empire_0.WarWeariness);
                if (num6 > 0)
                {
                    string text3 = text2;
                    text2 = text3 + " (" + TextResolver.GetText("At war with").ToLower(CultureInfo.InvariantCulture) + " " + num6 + " " + TextResolver.GetText("empires") + ")";
                }
                method_9(graphics_0, text2, font_0, point, solidBrush_2);
            }
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Troops"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Troops"), font_2: Font);
            point = new Point(num2, num4 - num3);
            TroopList troopList = new TroopList();
            troopList.AddRange(empire_0.Troops);
            foreach (Habitat colony2 in empire_0.Colonies)
            {
                if (colony2.TroopsToRecruit == null)
                {
                    continue;
                }
                for (int j = 0; j < colony2.TroopsToRecruit.Count; j++)
                {
                    Troop troop = colony2.TroopsToRecruit[j];
                    if (troop != null && !troopList.Contains(troop))
                    {
                        troopList.Add(troop);
                    }
                }
            }
            method_8(graphics_0, troopList.Count.ToString(), font_0, point);
            num4 += int_1;
            method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Intelligence Agents"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Intelligence Agents"), font_2: Font);
            method_8(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: empire_0.Characters.CountCharactersByRole(CharacterRole.IntelligenceAgent).ToString(), font_2: font_0);
            num4 += int_1;
            num4 += 10;
            num4 += 10;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, 80, 80, 255)))
            {
                Rectangle rect = new Rectangle(0, num4 + 5, 256, 200);
                graphics_0.FillRectangle(brush, rect);
            }
            using (SolidBrush brush2 = new SolidBrush(Color.FromArgb(80, 80, 80, 80)))
            {
                Rectangle rect2 = new Rectangle(257, num4 + 5, base.Width, 200);
                graphics_0.FillRectangle(brush2, rect2);
            }
            num4 += 10;
            method_8(point_0: new Point(num + 4, num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Government"), font_2: font_1);
            method_8(point_0: new Point(num2, num4 - num3 + 4), graphics_0: graphics_0, string_0: empire_0.GovernmentAttributes.Name, font_2: font_0);
            num4 += int_1;
            num4 += 10;
            if (empire_0.GovernmentAttributes != null)
            {
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("War weariness"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("War weariness"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.WarWeariness - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(empire_0.GovernmentAttributes.WarWeariness - 1.0, bool_0: false));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Maintenance costs"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Maintenance costs"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.MaintenanceCosts - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(empire_0.GovernmentAttributes.MaintenanceCosts - 1.0, bool_0: false));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Approval"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Approval"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.ApprovalRating - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(empire_0.GovernmentAttributes.ApprovalRating - 1.0));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Growth rate"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Growth rate"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.PopulationGrowth - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(empire_0.GovernmentAttributes.PopulationGrowth - 1.0));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Research speed"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Research speed"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.ResearchSpeed - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(empire_0.GovernmentAttributes.ResearchSpeed - 1.0));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Troop recruitment"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Troop recruitment"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.TroopRecruitment - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(empire_0.GovernmentAttributes.TroopRecruitment - 1.0));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Corruption"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Corruption"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.Corruption - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(empire_0.GovernmentAttributes.Corruption - 1.0, bool_0: false));
                num4 += int_1;
                method_8(point_0: new Point(num + method_1(graphics_0, TextResolver.GetText("Colony Income"), Font, int_), num4), graphics_0: graphics_0, string_0: TextResolver.GetText("Colony Income"), font_2: Font);
                method_9(point_0: new Point(num2, num4 - num3), graphics_0: graphics_0, string_0: (empire_0.GovernmentAttributes.TradeBonus - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(empire_0.GovernmentAttributes.TradeBonus - 1.0, bool_0: true));
                num4 += int_1;
            }
            if (int_0 != -1)
            {
                num4 = 150;
                GovernmentAttributes governmentAttributes = galaxy_0.Governments[int_0];
                num4 = 150 + int_1;
                num4 += 10;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.WarWeariness - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(governmentAttributes.WarWeariness - 1.0, bool_0: false));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.MaintenanceCosts - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(governmentAttributes.MaintenanceCosts - 1.0, bool_0: false));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.ApprovalRating - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(governmentAttributes.ApprovalRating - 1.0));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.PopulationGrowth - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(governmentAttributes.PopulationGrowth - 1.0));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.ResearchSpeed - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(governmentAttributes.ResearchSpeed - 1.0));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.TroopRecruitment - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_4(governmentAttributes.TroopRecruitment - 1.0));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.Corruption - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(governmentAttributes.Corruption - 1.0, bool_0: false));
                num4 += int_1;
                method_9(point_0: new Point(260, num4 - num3), graphics_0: graphics_0, string_0: (governmentAttributes.TradeBonus - 1.0).ToString("+#0%;-#0%;" + TextResolver.GetText("Normal")), font_2: font_0, solidBrush_4: method_5(governmentAttributes.TradeBonus - 1.0, bool_0: true));
                num4 += int_1;
            }
        }

        private SolidBrush method_4(double double_0)
        {
            return method_5(double_0, bool_0: true);
        }

        private SolidBrush method_5(double double_0, bool bool_0)
        {
            SolidBrush result = solidBrush_0;
            if (double_0 < 0.0)
            {
                result = ((!bool_0) ? solidBrush_3 : solidBrush_2);
            }
            else if (double_0 > 0.0)
            {
                result = ((!bool_0) ? solidBrush_2 : solidBrush_3);
            }
            return result;
        }

        private SolidBrush method_6(double double_0)
        {
            return method_7(double_0, bool_0: true);
        }

        private SolidBrush method_7(double double_0, bool bool_0)
        {
            SolidBrush result = solidBrush_0;
            if (double_0 < 0.0)
            {
                result = solidBrush_2;
            }
            return result;
        }

        private void method_8(Graphics graphics_0, string string_0, Font font_2, Point point_0)
        {
            method_9(graphics_0, string_0, font_2, point_0, solidBrush_0);
        }

        private void method_9(Graphics graphics_0, string string_0, Font font_2, Point point_0, SolidBrush solidBrush_4)
        {
            point_0 = new Point(point_0.X + 1, point_0.Y + 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
            point_0 = new Point(point_0.X - 1, point_0.Y - 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_4, point_0, StringFormat.GenericTypographic);
        }

        private void method_10(Graphics graphics_0, string string_0, Font font_2, Point point_0, Brush brush_0, SizeF sizeF_0)
        {
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                RectangleF layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                StringFormat genericTypographic = StringFormat.GenericTypographic;
                graphics_0.DrawString(string_0, font_2, solidBrush_1, layoutRectangle, genericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_2, brush_0, layoutRectangle, genericTypographic);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_2, brush_0, point_0);
            }
        }
    }
}
