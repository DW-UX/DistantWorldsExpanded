// Decompiled with JetBrains decompiler
// Type: DistantWorlds.DiplomaticMessageQueue
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;

namespace DistantWorlds
{
    public class DiplomaticMessageQueue
    {
        public class MessageClickedEventArgs : EventArgs
        {
            public EmpireMessage Message;

            public ConversationOption ConversationOption;

            public MessageClickedEventArgs(EmpireMessage message, ConversationOption conversationOption):base()
            {
                
                Message = message;
                ConversationOption = conversationOption;
            }
        }

        public bool Visible;

        public DateTime LastRefresh;

        public bool NeedsRefresh;

        private EventHandler<MessageClickedEventArgs> eventHandler_0;

        private object object_0;

        private EmpireMessageList empireMessageList_0;

        private ConversationOptionList conversationOptionList_0;

        private List<double> list_0;

        private List<Bitmap> list_1;

        private Main main_0;

        private DateTime dateTime_0;

        private long long_0;

        private Rectangle rectangle_0;

        private int int_0;

        private int int_1;

        private double double_0;

        private EmpireMessage empireMessage_0;

        private EmpireMessage empireMessage_1;

        private double double_1;

        private RaceImageCache raceImageCache_0;

        private Bitmap bitmap_0;

        private Bitmap bitmap_1;

        private Bitmap bitmap_2;

        private Bitmap bitmap_3;

        private Bitmap bitmap_4;

        private Bitmap bitmap_5;

        private Bitmap aXhRobvDvo;

        private Bitmap bitmap_6;

        private Bitmap bitmap_7;

        private Bitmap bitmap_8;

        private Bitmap bitmap_9;

        private Bitmap bitmap_10;

        private Bitmap bitmap_11;

        private Bitmap bitmap_12;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private Font font_0;

        private Font font_1;

        protected IFontCache _FontCache;

        //private float float_0;

        //private bool bool_0;

        private Bitmap bitmap_13;

        public Rectangle Area => rectangle_0;

        public Bitmap PanelImage => bitmap_13;

        public event EventHandler<MessageClickedEventArgs> MessageClicked
        {
            add
            {
                EventHandler<MessageClickedEventArgs> eventHandler = eventHandler_0;
                EventHandler<MessageClickedEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<MessageClickedEventArgs> value2 = (EventHandler<MessageClickedEventArgs>)Delegate.Combine(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
            remove
            {
                EventHandler<MessageClickedEventArgs> eventHandler = eventHandler_0;
                EventHandler<MessageClickedEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<MessageClickedEventArgs> value2 = (EventHandler<MessageClickedEventArgs>)Delegate.Remove(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
        }

        private void method_0()
        {
            if (_FontCache != null)
            {
                font_0 = _FontCache.GenerateFont(16.67f, isBold: false);
                font_1 = _FontCache.GenerateFont(16.67f, isBold: true);
            }
        }

        public void InitializeImages(RaceImageCache raceImageCache, Bitmap buildImage, Bitmap agentImage, Bitmap colonyImage, Bitmap moneyImage, Bitmap constructionImage, Bitmap refuelImage, Bitmap miningImage, Bitmap colonizeImage, Bitmap attackImage, Bitmap bombardImage, Bitmap planetDestroyImage, Bitmap advisorSuggestionImage, Bitmap pirateImage, Bitmap raidImage)
        {
            raceImageCache_0 = raceImageCache;
            bitmap_0 = GraphicsHelper.ScaleImageMaximum(buildImage, 30, 30, 0.67f);
            bitmap_1 = GraphicsHelper.ScaleImageMaximum(agentImage, 30, 30, 0.67f);
            bitmap_2 = GraphicsHelper.ScaleImageMaximum(colonyImage, 30, 30, 0.67f);
            bitmap_3 = GraphicsHelper.ScaleImageMaximum(refuelImage, 30, 30, 0.67f);
            bitmap_4 = GraphicsHelper.ScaleImageMaximum(miningImage, 30, 30, 0.67f);
            bitmap_5 = GraphicsHelper.ScaleImageMaximum(colonizeImage, 30, 30, 0.67f);
            aXhRobvDvo = GraphicsHelper.ScaleImageMaximum(constructionImage, 30, 30, 0.67f);
            bitmap_6 = GraphicsHelper.ScaleImageMaximum(bombardImage, 30, 30, 0.67f);
            bitmap_7 = GraphicsHelper.ScaleImageMaximum(planetDestroyImage, 30, 30, 0.67f);
            bitmap_8 = GraphicsHelper.ScaleImageMaximum(attackImage, 30, 30, 0.67f);
            bitmap_9 = GraphicsHelper.ScaleImageMaximum(moneyImage, 30, 30, 0.67f);
            bitmap_10 = GraphicsHelper.ScaleImageMaximum(advisorSuggestionImage, 30, 30, 0.67f);
            bitmap_11 = GraphicsHelper.ScaleImageMaximum(pirateImage, 30, 30, 0.67f);
            bitmap_12 = GraphicsHelper.ScaleImageMaximum(raidImage, 30, 30, 0.67f);
        }

        private Bitmap method_1(Bitmap bitmap_14, int int_2, int int_3, float float_1)
        {
            if (int_2 < 1)
            {
                int_2 = 1;
            }
            if (int_3 < 1)
            {
                int_3 = 1;
            }
            ImageAttributes imageAttr = main_0.method_20(float_1);
            Bitmap bitmap = new Bitmap(int_2, int_3, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = new Rectangle(0, 0, bitmap_14.Width, bitmap_14.Height);
            Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawImage(bitmap_14, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttr);
            return bitmap;
        }

        public void Initialize(Main parentForm, Rectangle area)
        {
            method_0();
            main_0 = parentForm;
            rectangle_0 = area;
            int_0 = 46;
            double_0 = 20.0;
            int_1 = area.Height / int_0;
            Reset();
        }

        public void Reset()
        {
            empireMessageList_0.Clear();
            conversationOptionList_0.Clear();
            list_0.Clear();
            if (list_1 != null)
            {
                for (int i = 0; i < list_1.Count; i++)
                {
                    if (list_1[i] != null)
                    {
                        list_1[i].Dispose();
                    }
                }
                list_1.Clear();
            }
            long_0 = 0L;
        }

        public bool CheckHover(Point point)
        {
            ConversationOption conversationOption_ = null;
            return method_2(point, out empireMessage_0, out conversationOption_);
        }

        private bool method_2(Point point_0, out EmpireMessage empireMessage_2, out ConversationOption conversationOption_0)
        {
            empireMessage_2 = null;
            conversationOption_0 = null;
            if (!Visible)
            {
                return false;
            }
            if (rectangle_0.Contains(point_0))
            {
                for (int i = 0; i < list_0.Count; i++)
                {
                    int num = (int)list_0[i] + rectangle_0.Top;
                    if (point_0.Y >= num && point_0.Y < num + (int_0 - 5) && conversationOptionList_0[i] != null)
                    {
                        empireMessage_2 = empireMessageList_0[i];
                        conversationOption_0 = conversationOptionList_0[i];
                        NeedsRefresh = true;
                        return true;
                    }
                }
            }
            empireMessage_0 = null;
            return false;
        }

        public bool CheckClick(Point point)
        {
            if (!Visible)
            {
                return false;
            }
            EmpireMessage empireMessage_ = null;
            ConversationOption conversationOption_ = null;
            method_2(point, out empireMessage_, out conversationOption_);
            if (empireMessage_ != null && eventHandler_0 != null)
            {
                if (empireMessage_.MessageType != EmpireMessageType.AdvisorSuggestion || empireMessage_.AdvisorMessageType == AdvisorMessageType.BuildOrder)
                {
                    int num = -1;
                    for (int i = 0; i < empireMessageList_0.Count; i++)
                    {
                        if (empireMessageList_0[i] == empireMessage_)
                        {
                            num = i;
                            break;
                        }
                    }
                    if (num >= 0)
                    {
                        conversationOptionList_0[num] = null;
                    }
                }
                eventHandler_0(this, new MessageClickedEventArgs(empireMessage_, conversationOption_));
                return true;
            }
            return false;
        }

        public void RemoveMessage(EmpireMessage message)
        {
            if (message == null)
            {
                return;
            }
            int num = -1;
            for (int i = 0; i < empireMessageList_0.Count; i++)
            {
                if (empireMessageList_0[i] == message)
                {
                    num = i;
                    break;
                }
            }
            if (num >= 0)
            {
                conversationOptionList_0[num] = null;
            }
        }

        public void AddMessage(EmpireMessage message, ConversationOption conversationOption)
        {
            lock (object_0)
            {
                empireMessageList_0.Add(message);
                conversationOptionList_0.Add(conversationOption);
                list_1.Add(method_1(message.Sender.LargeFlagPicture, 50, 30, 0.5f));
                double item = 0.0;
                if (list_0 != null && list_0.Count > 0)
                {
                    item = list_0[list_0.Count - 1] + (double)int_0;
                }
                list_0.Add(item);
                empireMessage_1 = message;
                double_1 = 1.0;
                if (message.MessageType == EmpireMessageType.AdvisorSuggestion && message.AdvisorMessageType == AdvisorMessageType.BuildOrder)
                {
                    long_0 = 0L;
                }
                dateTime_0 = main_0._Game.Galaxy.CurrentDateTime;
            }
        }

        public bool ExpireDiplomacyMessagesForEmpire(Empire empire)
        {
            lock (object_0)
            {
                bool result = false;
                if (empire != null)
                {
                    for (int i = 0; i < empireMessageList_0.Count; i++)
                    {
                        EmpireMessage empireMessage = empireMessageList_0[i];
                        bool flag = false;
                        switch (empireMessage.MessageType)
                        {
                            case EmpireMessageType.AdvisorSuggestion:
                                switch (empireMessage.AdvisorMessageType)
                                {
                                    case AdvisorMessageType.IntelligenceMission:
                                    case AdvisorMessageType.EnemyAttack:
                                    case AdvisorMessageType.EnemyBombard:
                                    case AdvisorMessageType.EnemyBlockade:
                                    case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                                    case AdvisorMessageType.PrepareRaid:
                                    case AdvisorMessageType.DiplomaticGift:
                                    case AdvisorMessageType.TreatyOffer:
                                    case AdvisorMessageType.WarTradeSanctions:
                                    case AdvisorMessageType.OfferMilitaryRefueling:
                                    case AdvisorMessageType.CancelMilitaryRefueling:
                                    case AdvisorMessageType.OfferMiningRights:
                                    case AdvisorMessageType.CancelMiningRights:
                                    case AdvisorMessageType.AllowTradeRestrictedResources:
                                    case AdvisorMessageType.DisallowTradeRestrictedResources:
                                    case AdvisorMessageType.PirateRaid:
                                        if (empireMessage.ResolveTargetEmpireFromSubject() == empire)
                                        {
                                            flag = true;
                                        }
                                        break;
                                }
                                break;
                            case EmpireMessageType.DiplomaticRelationChange:
                            case EmpireMessageType.ProposeDiplomaticRelation:
                            case EmpireMessageType.RefuseDiplomaticRelation:
                            case EmpireMessageType.OfferTrade:
                                if (empireMessage.Sender == empire)
                                {
                                    flag = true;
                                }
                                break;
                        }
                        if (flag && conversationOptionList_0.Count > i)
                        {
                            conversationOptionList_0[i] = null;
                            result = true;
                        }
                    }
                }
                return result;
            }
        }

        public bool ExpireInvalidMessages(EmpireMessage newMessage)
        {
            lock (object_0)
            {
                bool result = false;
                if (newMessage != null)
                {
                    for (int i = 0; i < empireMessageList_0.Count; i++)
                    {
                        EmpireMessage empireMessage = empireMessageList_0[i];
                        bool flag = false;
                        if (empireMessage != newMessage)
                        {
                            switch (newMessage.MessageType)
                            {
                                case EmpireMessageType.AdvisorSuggestion:
                                    switch (newMessage.AdvisorMessageType)
                                    {
                                        case AdvisorMessageType.BuildOrder:
                                            if (empireMessage.AdvisorMessageType == newMessage.AdvisorMessageType)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.BuildOneOff:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.Colonization:
                                            if (empireMessage.CheckAdvisorMessageEquivalenceAbbreviated(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.IntelligenceMission:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.EnemyAttack:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.EnemyBombard:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.EnemyBlockade:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.InvadeIndependent:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.PrepareRaid:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.TreatyOffer:
                                            if ((empireMessage.AdvisorMessageType == AdvisorMessageType.TreatyOffer || empireMessage.AdvisorMessageType == AdvisorMessageType.WarTradeSanctions) && empireMessage.ResolveTargetEmpireFromSubject() == newMessage.ResolveTargetEmpireFromSubject())
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.WarTradeSanctions:
                                            if ((empireMessage.AdvisorMessageType == AdvisorMessageType.TreatyOffer || empireMessage.AdvisorMessageType == AdvisorMessageType.WarTradeSanctions) && empireMessage.ResolveTargetEmpireFromSubject() == newMessage.ResolveTargetEmpireFromSubject())
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.ColonyFacility:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.OfferMilitaryRefueling:
                                            if (empireMessage.AdvisorMessageType == AdvisorMessageType.OfferMilitaryRefueling || empireMessage.AdvisorMessageType == AdvisorMessageType.CancelMilitaryRefueling)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.CancelMilitaryRefueling:
                                            if (empireMessage.AdvisorMessageType == AdvisorMessageType.CancelMilitaryRefueling || empireMessage.AdvisorMessageType == AdvisorMessageType.OfferMilitaryRefueling)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.OfferMiningRights:
                                            if (empireMessage.AdvisorMessageType == AdvisorMessageType.OfferMiningRights || empireMessage.AdvisorMessageType == AdvisorMessageType.CancelMiningRights)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.CancelMiningRights:
                                            if (empireMessage.AdvisorMessageType == AdvisorMessageType.CancelMiningRights || empireMessage.AdvisorMessageType == AdvisorMessageType.OfferMiningRights)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.AllowTradeRestrictedResources:
                                            if (empireMessage.AdvisorMessageType == AdvisorMessageType.AllowTradeRestrictedResources || empireMessage.AdvisorMessageType == AdvisorMessageType.DisallowTradeRestrictedResources)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.DisallowTradeRestrictedResources:
                                            if (empireMessage.AdvisorMessageType == AdvisorMessageType.DisallowTradeRestrictedResources || empireMessage.AdvisorMessageType == AdvisorMessageType.AllowTradeRestrictedResources)
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.Retrofit:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.PirateRaid:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.PirateFacilityEradicate:
                                            if (empireMessage.CheckAdvisorMessageEquivalence(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                        case AdvisorMessageType.OfferPirateAttackMission:
                                        case AdvisorMessageType.OfferPirateDefendMission:
                                        case AdvisorMessageType.OfferPirateSmuggleMission:
                                        case AdvisorMessageType.AcceptPirateSmugglingMission:
                                        case AdvisorMessageType.DefendTarget:
                                            if (empireMessage.CheckAdvisorMessageEquivalenceAbbreviated(newMessage))
                                            {
                                                flag = true;
                                            }
                                            break;
                                    }
                                    break;
                                case EmpireMessageType.MilitaryRefuelingAllowed:
                                case EmpireMessageType.MilitaryRefuelingBlocked:
                                    if ((empireMessage.MessageType == EmpireMessageType.MilitaryRefuelingAllowed || empireMessage.MessageType == EmpireMessageType.MilitaryRefuelingBlocked) && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.MiningRightsAllowed:
                                case EmpireMessageType.MiningRightsBlocked:
                                    if ((empireMessage.MessageType == EmpireMessageType.MiningRightsAllowed || empireMessage.MessageType == EmpireMessageType.MiningRightsBlocked) && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.SellInfoUnmetEmpire:
                                    if (empireMessage.MessageType == EmpireMessageType.SellInfoUnmetEmpire && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.SellInfoIndependentColony:
                                    if (empireMessage.MessageType == EmpireMessageType.SellInfoIndependentColony && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.SellInfoSystemMap:
                                    if (empireMessage.MessageType == EmpireMessageType.SellInfoSystemMap && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.SellInfoRuins:
                                case EmpireMessageType.SellInfoDebrisField:
                                case EmpireMessageType.SellInfoRestrictedArea:
                                case EmpireMessageType.SellInfoPlanetDestroyer:
                                    if ((empireMessage.MessageType == EmpireMessageType.SellInfoDebrisField || empireMessage.MessageType == EmpireMessageType.SellInfoPlanetDestroyer || empireMessage.MessageType == EmpireMessageType.SellInfoRestrictedArea || empireMessage.MessageType == EmpireMessageType.SellInfoRuins) && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.PirateOfferProtection:
                                case EmpireMessageType.CancelPirateProtection:
                                    if ((empireMessage.MessageType == EmpireMessageType.PirateOfferProtection || empireMessage.MessageType == EmpireMessageType.CancelPirateProtection) && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.RestrictedResourceTradingAllowed:
                                case EmpireMessageType.RestrictedResourceTradingBlocked:
                                    if ((empireMessage.MessageType == EmpireMessageType.RestrictedResourceTradingAllowed || empireMessage.MessageType == EmpireMessageType.RestrictedResourceTradingBlocked) && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.OfferTrade:
                                    if (empireMessage.MessageType == EmpireMessageType.OfferTrade && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.RemoveForcesFromSystem:
                                    if (empireMessage.MessageType == EmpireMessageType.RemoveForcesFromSystem && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.DiplomaticRelationChange:
                                case EmpireMessageType.ProposeDiplomaticRelation:
                                    if ((empireMessage.MessageType == EmpireMessageType.DiplomaticRelationChange || empireMessage.MessageType == EmpireMessageType.ProposeDiplomaticRelation) && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.RefuseDiplomaticRelation:
                                    if (empireMessage.MessageType == EmpireMessageType.RefuseDiplomaticRelation && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.StopMissionsAgainstUs:
                                    if (empireMessage.MessageType == EmpireMessageType.StopMissionsAgainstUs && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                                case EmpireMessageType.StopAttacks:
                                    if (empireMessage.MessageType == EmpireMessageType.StopAttacks && empireMessage.Sender == newMessage.Sender)
                                    {
                                        flag = true;
                                    }
                                    break;
                            }
                        }
                        if (flag && conversationOptionList_0.Count > i)
                        {
                            conversationOptionList_0[i] = null;
                            result = true;
                        }
                    }
                }
                return result;
            }
        }

        private void method_3(long long_1)
        {
            long num = long_1 - (long)(250.0 * (double)Galaxy.RealSecondsInGalacticYear);
            lock (object_0)
            {
                for (int i = 0; i < empireMessageList_0.Count; i++)
                {
                    if (empireMessageList_0[i] != null && empireMessageList_0[i].StarDate < num)
                    {
                        conversationOptionList_0[i] = null;
                    }
                }
            }
        }

        private void method_4(long long_1)
        {
            long num = long_1 - long_0;
            if (num < 10000L)
            {
                return;
            }
            lock (object_0)
            {
                Empire empire = null;
                if (main_0 != null && main_0._Game != null)
                {
                    empire = main_0._Game.PlayerEmpire;
                }
                if (empire != null)
                {
                    for (int i = 0; i < empireMessageList_0.Count; i++)
                    {
                        EmpireMessage empireMessage = empireMessageList_0[i];
                        if (empireMessage != null && empireMessage.MessageType == EmpireMessageType.AdvisorSuggestion && empireMessage.AdvisorMessageType == AdvisorMessageType.BuildOrder)
                        {
                            double annualSupportCosts = 0.0;
                            ForceStructureProjectionList forceStructureProjectionList = empire.CurrentStateForceStructure(out annualSupportCosts);
                            ForceStructureProjectionList forceStructureProjectionList2 = new ForceStructureProjectionList();
                            if (empire.StateForceStructureProjections != null)
                            {
                                forceStructureProjectionList2 = ((empire.PirateEmpireBaseHabitat == null) ? empire.StateForceStructureProjections.Diff(forceStructureProjectionList) : empire.StateForceStructureProjections.Clone());
                            }
                            if (empire.PirateEmpireBaseHabitat != null && empire.PrivateForceStructureProjections != null)
                            {
                                forceStructureProjectionList2.AddRange(empire.PrivateForceStructureProjections.Clone());
                            }
                            forceStructureProjectionList2.Sort();
                            double totalSupportCosts = 0.0;
                            double totalPurchaseCosts = 0.0;
                            empire.RefactorForceStructureProjectionsToCosts(forceStructureProjectionList2, includeCashflowCheck: false, out totalSupportCosts, out totalPurchaseCosts, randomizedOrder: false);
                            empireMessage.Description = string.Format(TextResolver.GetText("Build new ships for X credits"), totalPurchaseCosts.ToString("###,###,###,##0"));
                            if (totalPurchaseCosts <= 0.0)
                            {
                                conversationOptionList_0[i] = null;
                            }
                            NeedsRefresh = true;
                        }
                    }
                }
            }
            long_0 = long_1;
        }

        private void method_5(DateTime dateTime_1)
        {
            TimeSpan timeSpan = dateTime_1.Subtract(dateTime_0);
            dateTime_0 = dateTime_1;
            if (double_1 > 0.0)
            {
                double_1 += timeSpan.TotalSeconds * 67.0;
                if (double_1 > 200.0)
                {
                    double_1 = 0.0;
                }
                NeedsRefresh = true;
            }
            bool flag = false;
            for (int i = 0; i < conversationOptionList_0.Count; i++)
            {
                if (conversationOptionList_0[i] == null)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag && empireMessageList_0.Count <= int_1)
            {
                return;
            }
            double num = timeSpan.TotalSeconds * double_0;
            NeedsRefresh = true;
            lock (object_0)
            {
                double val = num;
                if (empireMessageList_0.Count > int_1)
                {
                    int num2 = empireMessageList_0.Count - int_1;
                    val = Math.Min(val, (double)int_0 + list_0[0] + (double)(num2 - 1) * (double)int_0);
                }
                else
                {
                    val = 0.0;
                }
                double num3 = 0.0;
                List<int> list = new List<int>();
                for (int j = 0; j < list_0.Count; j++)
                {
                    bool flag2 = false;
                    if (j > 0 && conversationOptionList_0[j - 1] == null)
                    {
                        flag2 = true;
                        num3 += num;
                    }
                    list_0[j] -= val + num3;
                    if (flag2 && list_0[j] <= list_0[j - 1])
                    {
                        list_0[j] = list_0[j - 1];
                        if (list_0[j] < 0.0)
                        {
                            list_0[j] = 0.0;
                        }
                        list.Add(j - 1);
                        num3 -= num;
                    }
                }
                if (list.Count > 0)
                {
                    for (int k = 0; k < list.Count; k++)
                    {
                        int num4 = list[k];
                        if (empireMessageList_0.Count > num4)
                        {
                            empireMessageList_0.RemoveAt(num4);
                        }
                        if (conversationOptionList_0.Count > num4)
                        {
                            conversationOptionList_0.RemoveAt(num4);
                        }
                        if (list_0.Count > num4)
                        {
                            list_0.RemoveAt(num4);
                        }
                        if (list_1.Count > num4)
                        {
                            if (list_1[num4] != null)
                            {
                                list_1[num4].Dispose();
                            }
                            list_1.RemoveAt(num4);
                        }
                    }
                }
                if (list_0.Count > 0 && list_0[0] <= (double)int_0 * -1.0)
                {
                    list_0.RemoveAt(0);
                    empireMessageList_0.RemoveAt(0);
                    conversationOptionList_0.RemoveAt(0);
                    if (list_1[0] != null)
                    {
                        list_1[0].Dispose();
                    }
                    list_1.RemoveAt(0);
                }
            }
        }

        public Bitmap DrawMessagesToImage()
        {
            if (bitmap_13 == null || bitmap_13.PixelFormat == PixelFormat.Undefined)
            {
                bitmap_13 = new Bitmap(rectangle_0.Width, rectangle_0.Height + 3, PixelFormat.Format32bppPArgb);
            }
            using (Graphics graphics = Graphics.FromImage(bitmap_13))
            {
                graphics.Clear(Color.Transparent);
                DrawMessages(graphics, new Rectangle(0, 0, rectangle_0.Width, rectangle_0.Height + 3));
            }
            return bitmap_13;
        }

        public void DrawMessages(Graphics graphics)
        {
            DrawMessages(graphics, new Rectangle(rectangle_0.X, rectangle_0.Y - 3, rectangle_0.Width, rectangle_0.Height + 3));
        }

        public void DrawMessages(Graphics graphics, Rectangle area)
        {
            if (main_0 == null || main_0._Game == null)
            {
                return;
            }
            long currentStarDate = main_0._Game.Galaxy.CurrentStarDate;
            method_3(currentStarDate);
            method_4(currentStarDate);
            method_5(main_0._Game.Galaxy.CurrentDateTime);
            if (!Visible)
            {
                return;
            }
            graphics.SetClip(new Rectangle(area.X, area.Y, area.Width, area.Height));
            for (int i = 0; i < empireMessageList_0.Count; i++)
            {
                if (conversationOptionList_0[i] == null)
                {
                    continue;
                }
                main_0.method_112(graphics, GraphicsQuality.Low);
                int num = 80;
                if (empireMessageList_0[i] == empireMessage_0)
                {
                    num += 80;
                }
                else if (double_1 > 0.0 && empireMessageList_0[i] == empireMessage_1)
                {
                    double num2 = double_1;
                    if (num2 > 100.0)
                    {
                        num2 = 200.0 - num2;
                    }
                    num += (int)(num2 * 1.7);
                }
                num = Math.Max(0, Math.Min(255, num));
                Rectangle rect = new Rectangle(area.X, area.Y + (int)list_0[i], area.Width - 1, int_0 - 5);
                Color color = Color.FromArgb(num, empireMessageList_0[i].Sender.MainColor);
                if (empireMessageList_0[i].Sender.MainColor == Color.FromArgb(1, 1, 1))
                {
                    color = Color.FromArgb(num, 64, 64, 64);
                }
                Color color2 = empireMessageList_0[i].Sender.MainColor;
                if (empireMessageList_0[i].Sender.PirateEmpireBaseHabitat != null)
                {
                    color = Color.FromArgb(num, 96, 96, 96);
                    color2 = Color.FromArgb(170, 170, 170);
                }
                else if (empireMessageList_0[i].MessageType == EmpireMessageType.AdvisorSuggestion)
                {
                    color = Color.FromArgb(num, 144, 144, 144);
                    color2 = Color.FromArgb(170, 170, 170);
                }
                using (SolidBrush brush = new SolidBrush(color))
                {
                    graphics.FillRectangle(brush, rect);
                }
                if (empireMessageList_0[i] == empireMessage_0)
                {
                    int alpha = Math.Min(255, num + 64);
                    if (empireMessageList_0[i].MessageType == EmpireMessageType.AdvisorSuggestion)
                    {
                        using Pen pen = new Pen(Color.FromArgb(alpha, Color.FromArgb(170, 170, 170)));
                        graphics.DrawRectangle(pen, rect);
                    }
                    else
                    {
                        using Pen pen2 = new Pen(Color.FromArgb(alpha, color));
                        graphics.DrawRectangle(pen2, rect);
                    }
                }
                if (empireMessageList_0[i].MessageType == EmpireMessageType.AdvisorSuggestion)
                {
                    Bitmap bitmap = null;
                    switch (empireMessageList_0[i].AdvisorMessageType)
                    {
                        case AdvisorMessageType.BuildOrder:
                            bitmap = bitmap_0;
                            break;
                        case AdvisorMessageType.Colonization:
                            bitmap = bitmap_5;
                            break;
                        case AdvisorMessageType.IntelligenceMission:
                            bitmap = bitmap_1;
                            break;
                        case AdvisorMessageType.EnemyBombard:
                            bitmap = bitmap_6;
                            break;
                        case AdvisorMessageType.EnemyAttackPlanetDestroyer:
                            bitmap = bitmap_7;
                            break;
                        case AdvisorMessageType.DiplomaticGift:
                            bitmap = bitmap_9;
                            break;
                        case AdvisorMessageType.OfferMilitaryRefueling:
                        case AdvisorMessageType.CancelMilitaryRefueling:
                            bitmap = bitmap_3;
                            break;
                        case AdvisorMessageType.OfferMiningRights:
                        case AdvisorMessageType.CancelMiningRights:
                            bitmap = bitmap_4;
                            break;
                        case AdvisorMessageType.EnemyAttack:
                        case AdvisorMessageType.InvadeIndependent:
                        case AdvisorMessageType.DefendTerritory:
                            bitmap = bitmap_8;
                            break;
                        case AdvisorMessageType.BuildOneOff:
                        case AdvisorMessageType.Retrofit:
                            bitmap = aXhRobvDvo;
                            break;
                        case AdvisorMessageType.PirateRaid:
                            bitmap = bitmap_12;
                            break;
                        default:
                            bitmap = bitmap_10;
                            break;
                        case AdvisorMessageType.OfferPirateAttackMission:
                        case AdvisorMessageType.OfferPirateDefendMission:
                        case AdvisorMessageType.OfferPirateSmuggleMission:
                        case AdvisorMessageType.PirateFacilityEradicate:
                        case AdvisorMessageType.DefendTarget:
                            bitmap = bitmap_11;
                            break;
                    }
                    Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                    int num3 = Math.Max(0, (30 - bitmap.Width) / 2);
                    int num4 = Math.Max(0, (30 - bitmap.Height) / 2);
                    Rectangle destRect = new Rectangle(rect.X + 15 + num3, rect.Y + 5 + num4, bitmap.Width, bitmap.Height);
                    graphics.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
                    main_0.method_112(graphics, GraphicsQuality.Medium);
                    Point point_ = new Point(rect.X + 59, rect.Y + 3);
                    using SolidBrush brush_ = new SolidBrush(color2);
                    SizeF sizeF_ = new SizeF(238f, 20f);
                    string text = Galaxy.ResolveDescription(empireMessageList_0[i].AdvisorMessageType, empireMessageList_0[i]);
                    if (empireMessageList_0[i].AdvisorMessageType == AdvisorMessageType.ColonyFacility && empireMessageList_0[i].AdvisorMessageData != null && empireMessageList_0[i].AdvisorMessageData is PlanetaryFacilityDefinition)
                    {
                        PlanetaryFacilityDefinition planetaryFacility = (PlanetaryFacilityDefinition)empireMessageList_0[i].AdvisorMessageData;
                        if (Galaxy.CalculatePlanetaryFacilityCost(planetaryFacility, main_0._Game.PlayerEmpire) > main_0._Game.PlayerEmpire.StateMoney)
                        {
                            text = TextResolver.GetText("Advisor Message ColonyFacility Need Funds");
                        }
                    }
                    string string_ = TextResolver.GetText("Advisors") + ": " + text;
                    method_7(graphics, string_, font_1, point_, brush_, sizeF_);
                    Point point_2 = new Point(rect.X + 59, rect.Y + 21);
                    string description = empireMessageList_0[i].Description;
                    method_7(graphics, description, font_0, point_2, brush_, sizeF_);
                }
                else
                {
                    graphics.DrawImage(list_1[i], rect.X + 5, rect.Y + 5);
                    Bitmap empireDominantRaceImageSize = raceImageCache_0.GetEmpireDominantRaceImageSize30(empireMessageList_0[i].Sender, useTransparent: true);
                    graphics.DrawImage(empireDominantRaceImageSize, rect.X + 59, rect.Y + 5);
                    main_0.method_112(graphics, GraphicsQuality.Medium);
                    Point point_3 = new Point(rect.X + 93, rect.Y + 3);
                    using SolidBrush brush_2 = new SolidBrush(color2);
                    SizeF sizeF_2 = new SizeF(204f, 20f);
                    method_7(graphics, empireMessageList_0[i].Sender.Name, font_1, point_3, brush_2, sizeF_2);
                    Point point_4 = new Point(rect.X + 93, rect.Y + 21);
                    string title = empireMessageList_0[i].Title;
                    method_7(graphics, title, font_0, point_4, brush_2, sizeF_2);
                }
            }
            graphics.SetClip(main_0.ClientRectangle);
            main_0.method_112(graphics, GraphicsQuality.High);
        }

        private void method_6(Graphics graphics_0, string string_0, Font font_2, Point point_0)
        {
            WcaRlwvBtx(graphics_0, string_0, font_2, point_0, solidBrush_0);
        }

        private void WcaRlwvBtx(Graphics graphics_0, string string_0, Font font_2, Point point_0, Brush brush_0)
        {
            method_7(graphics_0, string_0, font_2, point_0, brush_0, SizeF.Empty);
        }

        private void method_7(Graphics graphics_0, string string_0, Font font_2, Point point_0, Brush brush_0, SizeF sizeF_0)
        {
            Color color = Color.Black;
            if (brush_0 is SolidBrush)
            {
                color = Galaxy.DetermineContrastDropShadowColor(((SolidBrush)brush_0).Color);
            }
            using SolidBrush brush = new SolidBrush(color);
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                RectangleF layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_2, brush, layoutRectangle, StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                layoutRectangle = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_2, brush_0, layoutRectangle, StringFormat.GenericTypographic);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_2, brush, point_0);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_2, brush_0, point_0);
            }
        }

        public DiplomaticMessageQueue():base()
        {
            
            Visible = true;
            LastRefresh = DateTime.MinValue;
            object_0 = new object();
            empireMessageList_0 = new EmpireMessageList();
            conversationOptionList_0 = new ConversationOptionList();
            list_0 = new List<double>();
            list_1 = new List<Bitmap>();
            dateTime_0 = DateTime.MinValue;
            rectangle_0 = new Rectangle(10, 100, 200, 50);
            int_0 = 46;
            int_1 = 4;
            double_0 = 20.0;
            solidBrush_0 = new SolidBrush(Color.White);
            solidBrush_1 = new SolidBrush(Color.Black);
            font_0 = new Font("Verdana", 8f);
            font_1 = new Font("Verdana", 10f, FontStyle.Bold);
        }
    }
}
