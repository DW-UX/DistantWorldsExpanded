// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// DistantWorlds.Main
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Management;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using BaconDistantWorlds;
using ExpansionMod;
using DistantWorlds.Controls;
using DistantWorlds.Types;
//using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
//using SlimDX.DirectSound;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using System.Collections.Concurrent;

namespace DistantWorlds {

  public partial class Main {


        private void method_238(ConversationOption conversationOption_0)
        {
            ConversationOptionList conversationOptionList = new ConversationOptionList();
            DialogPartType type = conversationOption_0.Type;
            object relatedInfo = conversationOption_0.RelatedInfo;
            double cost = conversationOption_0.Cost;
            _ = conversationOption_0.Initiator.DominantRace;
            new Random((int)(_Game.Galaxy.CurrentStarDate / 90000L));
            Empire empire = ctlDiplomacyTradeThem.Empire;
            DiplomaticRelation diplomaticRelation = null;
            bool flag = false;
            bool flag2 = false;
            if (empire != null && empire.PirateEmpireBaseHabitat != null)
            {
                flag = true;
                if (empire.PirateEmpireSuperPirates)
                {
                    flag2 = true;
                }
            }
            if (empire != null && !flag)
            {
                diplomaticRelation = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire);
            }
            Empire empire2 = null;
            switch (type)
            {
                case DialogPartType.INFO_OFFER_UNMETEMPIRE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_UNMETEMPIRE, string.Format(TextResolver.GetText("Tell me about this empire"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_INDEPENDENTCOLONY:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_INDEPENDENTCOLONY, string.Format(TextResolver.GetText("Where is this colony?"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_SYSTEMMAPS:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_EXPLORATION, string.Format(TextResolver.GetText("Show me the system maps"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_RUINS:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_RUINS, string.Format(TextResolver.GetText("What is this discovery?"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_RESTRICTEDAREA:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_RESTRICTEDAREA, string.Format(TextResolver.GetText("What is this discovery?"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_DEBRISFIELD:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_DEBRISFIELD, string.Format(TextResolver.GetText("What is this discovery?"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_PLANETDESTROYER:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_PLANETDESTROYER, string.Format(TextResolver.GetText("What is this discovery?"), cost.ToString("###,###,###,##0")), relatedInfo, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.PIRATE_BUYINFO:
                    {
                        _Game.PlayerEmpire.GenerateSaleableInfoForEmpire(empire, _Game.PlayerEmpire, out var unmetEmpires, out var unexploredSystems, out var independentColonies, out var ruinHabitats, out var debrisFieldLocations, out var planetDestroyerLocations, out var restrictedAreaLocations);
                        if (unmetEmpires.Count > 0)
                        {
                            cost = (double)unmetEmpires[0].TotalColonyStrategicValue / 300.0;
                            cost = Math.Round(cost, 0);
                            cost = Math.Min(cost, 10000.0);
                            conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_UNMETEMPIRE, string.Format(TextResolver.GetText("We can put you in contact with another empire"), cost.ToString("###,###,###,##0")), unmetEmpires[0], cost, _Game.PlayerEmpire));
                        }
                        if (unexploredSystems.Count > 0)
                        {
                            cost = 2000.0;
                            conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_EXPLORATION, string.Format(TextResolver.GetText("We have maps of an unexplored system"), cost.ToString("###,###,###,##0")), unexploredSystems[0], cost, _Game.PlayerEmpire));
                        }
                        if (independentColonies.Count > 0)
                        {
                            cost = 20000.0;
                            conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_INDEPENDENTCOLONY, string.Format(TextResolver.GetText("We can reveal the location of an independent colony"), cost.ToString("###,###,###,##0")), independentColonies[0], cost, _Game.PlayerEmpire));
                        }
                        if (ruinHabitats.Count > 0 || debrisFieldLocations.Count > 0 || planetDestroyerLocations.Count > 0 || restrictedAreaLocations.Count > 0)
                        {
                            if (ruinHabitats.Count > 0)
                            {
                                cost = 30000.0;
                                conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_RUINS, string.Format(TextResolver.GetText("We have made an intriguing discovery that we will share"), cost.ToString("###,###,###,##0")), ruinHabitats[0], cost, _Game.PlayerEmpire));
                            }
                            else if (restrictedAreaLocations.Count > 0)
                            {
                                cost = 30000.0;
                                conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_RESTRICTEDAREA, string.Format(TextResolver.GetText("We have made an intriguing discovery that we will share"), cost.ToString("###,###,###,##0")), restrictedAreaLocations[0], cost, _Game.PlayerEmpire));
                            }
                            else if (debrisFieldLocations.Count > 0)
                            {
                                cost = 30000.0;
                                conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_DEBRISFIELD, string.Format(TextResolver.GetText("We have made an intriguing discovery that we will share"), cost.ToString("###,###,###,##0")), debrisFieldLocations[0], cost, _Game.PlayerEmpire));
                            }
                            else if (planetDestroyerLocations.Count > 0)
                            {
                                cost = 30000.0;
                                conversationOptionList.Add(new ConversationOption(DialogPartType.INFO_PLANETDESTROYER, string.Format(TextResolver.GetText("We have made an intriguing discovery that we will share"), cost.ToString("###,###,###,##0")), planetDestroyerLocations[0], cost, _Game.PlayerEmpire));
                            }
                        }
                        break;
                    }
                case DialogPartType.PIRATE_ATTACKOFFER_EMPIRES:
                    {
                        DistantWorlds.Types.EmpireList empiresWillingToAttack = new DistantWorlds.Types.EmpireList();
                        List<double> attackCosts = new List<double>();
                        _Game.PlayerEmpire.DetermineEmpiresPirateWillingToAttack(empire, _Game.PlayerEmpire, onlyUnfriendlyEmpires: false, out empiresWillingToAttack, out attackCosts);
                        for (int i = 0; i < empiresWillingToAttack.Count; i++)
                        {
                            string text2 = string.Format(TextResolver.GetText("Attack X for Y"), empiresWillingToAttack[i].Name, attackCosts[i].ToString("###,###,###,##0"));
                            conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_ATTACKCOMMENCE, text2, empiresWillingToAttack[i], attackCosts[i], _Game.PlayerEmpire));
                        }
                        break;
                    }
                case DialogPartType.PIRATE_ATTACKOFFER_SINGLEEMPIRE:
                    if (relatedInfo is Empire)
                    {
                        empire2 = (Empire)relatedInfo;
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_ATTACKCOMMENCE, string.Format(TextResolver.GetText("Commence the attacks!"), cost.ToString("###,###,###,##0")), empire2, cost, _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.PIRATE_ALLIANCEPROPOSE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_ALLIANCEACCEPT, string.Format(TextResolver.GetText("We accept your offer of an alliance"), cost.ToString("###,###,###,##0")), null, cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_ALLIANCEREJECT, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.PIRATE_ALLIANCEACCEPTRESPONSE:
                case DialogPartType.PIRATE_ALLIANCEREJECTRESPONSE:
                case DialogPartType.PIRATE_ALLIANCECANCELRESPONSE:
                case DialogPartType.GREETING_INTRODUCTION:
                case DialogPartType.GREETING_FRIENDLY:
                case DialogPartType.GREETING_NEUTRAL:
                case DialogPartType.GREETING_ANGRY:
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null && empire != null)
                    {
                        PirateRelation pirateRelation = _Game.PlayerEmpire.ObtainPirateRelation(empire);
                        if (pirateRelation.Type == PirateRelationType.None)
                        {
                            if (empire.PirateEmpireBaseHabitat != null)
                            {
                                conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER, TextResolver.GetText("Propose Pirate Protection Pirates"), null, 0.0, _Game.PlayerEmpire));
                            }
                            else
                            {
                                double cost2 = _Game.PlayerEmpire.CalculatePirateProtectionPricePerMonth(empire);
                                conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONPROPOSE_OFFER, string.Format(TextResolver.GetText("Propose Pirate Protection"), cost2.ToString("0")), null, cost2, _Game.PlayerEmpire));
                            }
                        }
                        else
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.CANCELPIRATEPROTECTION, TextResolver.GetText("Cancel Pirate Protection Option"), _Game.PlayerEmpire));
                        }
                        conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_BEGIN, TextResolver.GetText("Negotiate a trade proposal..."), null, 0.0, _Game.PlayerEmpire));
                    }
                    else if (flag && !flag2 && empire != null)
                    {
                        PirateRelation pirateRelation2 = _Game.PlayerEmpire.ObtainPirateRelation(empire);
                        if (pirateRelation2.Type == PirateRelationType.None)
                        {
                            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                            {
                                conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_TRUCEPROPOSE, TextResolver.GetText("Propose Pirate Protection Pirates"), _Game.PlayerEmpire));
                            }
                            else
                            {
                                conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONPROPOSE, TextResolver.GetText("Request Pirate Protection"), _Game.PlayerEmpire));
                            }
                        }
                        else
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.CANCELPIRATEPROTECTION, TextResolver.GetText("Cancel Pirate Protection Option"), _Game.PlayerEmpire));
                        }
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_BUYINFO, TextResolver.GetText("Buy information"), _Game.PlayerEmpire));
                    }
                    else if (diplomaticRelation != null)
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.TREATY_PROPOSAL, TextResolver.GetText("Change relationship"), null, 0.0, _Game.PlayerEmpire));
                        if (_Game.PlayerEmpire.StateMoney >= 1000.0)
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.GIFT_PROPOSE, TextResolver.GetText("Send a gift"), null, 0.0, _Game.PlayerEmpire));
                        }
                        if (diplomaticRelation.Type != DiplomaticRelationType.War)
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.WARNING, TextResolver.GetText("Send a warning"), null, 0.0, _Game.PlayerEmpire));
                        }
                        if (diplomaticRelation.Type == DiplomaticRelationType.War && !diplomaticRelation.Locked)
                        {
                            object[] array = new object[6];
                            TradeableItemList tradeableItemList2 = new TradeableItemList();
                            TradeableItemList tradeableItemList3 = new TradeableItemList();
                            int value = _Game.Galaxy.ValueEndWarAgainstUs(empire, _Game.PlayerEmpire);
                            value = _Game.Galaxy.RefactorValueForEmpire(value, _Game.PlayerEmpire, empire);
                            tradeableItemList2.Add(new TradeableItem(TradeableItemType.EndWar, empire, value));
                            tradeableItemList3.Add(new TradeableItem(TradeableItemType.EndWar, _Game.PlayerEmpire, 0));
                            array[3] = tradeableItemList2;
                            array[4] = tradeableItemList3;
                            conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_BEGIN, TextResolver.GetText("Negotiate an end to this war..."), array, value, _Game.PlayerEmpire));
                        }
                        else if (diplomaticRelation.Type == DiplomaticRelationType.TradeSanctions && !diplomaticRelation.Locked)
                        {
                            object[] array2 = new object[6];
                            TradeableItemList tradeableItemList4 = new TradeableItemList();
                            TradeableItemList tradeableItemList5 = new TradeableItemList();
                            int value2 = _Game.Galaxy.ValueLiftTradeSanctionsAgainstUs(empire, _Game.PlayerEmpire);
                            value2 = _Game.Galaxy.RefactorValueForEmpire(value2, _Game.PlayerEmpire, empire);
                            tradeableItemList4.Add(new TradeableItem(TradeableItemType.LiftTradeSanctions, empire, value2));
                            tradeableItemList5.Add(new TradeableItem(TradeableItemType.LiftTradeSanctions, _Game.PlayerEmpire, 0));
                            array2[3] = tradeableItemList4;
                            array2[4] = tradeableItemList5;
                            conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_BEGIN, TextResolver.GetText("Negotiate lifting trade sanctions..."), array2, value2, _Game.PlayerEmpire));
                        }
                        else if (empire != null && !empire.Reclusive)
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.OFFER_DEAL, TextResolver.GetText("Swap maps or tech"), null, 0.0, _Game.PlayerEmpire));
                            conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_BEGIN, TextResolver.GetText("Negotiate a trade proposal..."), null, 0.0, _Game.PlayerEmpire));
                        }
                    }
                    break;
                case DialogPartType.OFFER_FREETRADE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.FREETRADE_ACCEPT, TextResolver.GetText("Yes, a Free Trade Agreement sounds like a great idea!"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.FREETRADE_REJECT, TextResolver.GetText("Not at the moment, thanks"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.OFFER_PROTECTORATE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.PROTECTORATE_ACCEPT, TextResolver.GetText("Yes, we accept your offer of a Protectorate!"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.PROTECTORATE_REJECT, TextResolver.GetText("Not at the moment, thanks"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.OFFER_MUTUALDEFENSE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.MUTUALDEFENSE_ACCEPT, TextResolver.GetText("Yes, we join you in a Mutual Defense Pact!"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.MUTUALDEFENSE_REJECT, TextResolver.GetText("Not at the moment, thanks"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.OFFER_DEAL_RESPONSE:
                    {
                        int num4 = _Game.Galaxy.ValueTerritoryMapForEmpire(_Game.PlayerEmpire, empire);
                        int num5 = _Game.Galaxy.ValueGalaxyMapForEmpire(_Game.PlayerEmpire, empire);
                        TradeableItemList tradeableItemList = _Game.Galaxy.ResolveTradeableItems(_Game.PlayerEmpire, empire, includeNearestColony: false, refactorValuesForEmpire: false);
                        conversationOptionList.Add(new ConversationOption(DialogPartType.OFFER_DEAL_TERRITORYMAP, TextResolver.GetText("Swap Territory maps (empire systems)"), new TradeableItem(TradeableItemType.TerritoryMap, null, num4), num4, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.OFFER_DEAL_GALAXYMAP, TextResolver.GetText("Swap Galaxy maps (all exploration)"), new TradeableItem(TradeableItemType.GalaxyMap, null, num5), num5, _Game.PlayerEmpire));
                        int num6 = 0;
                        foreach (TradeableItem item6 in tradeableItemList)
                        {
                            if (num6 < 3 && item6.Type == TradeableItemType.ResearchProject && item6.Item is ResearchNode)
                            {
                                ResearchNode researchNode = (ResearchNode)item6.Item;
                                string text = string.Format(TextResolver.GetText("Sell TECH for X credits"), researchNode.Name, item6.Value.ToString("###,###,###,##0"));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.OFFER_DEAL_COMPONENT, text, item6, item6.Value, _Game.PlayerEmpire));
                                num6++;
                            }
                        }
                        break;
                    }
                case DialogPartType.OFFER_DEAL_TERRITORYMAP:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_ACCEPT, TextResolver.GetText("We accept your proposal"), conversationOption_0.RelatedInfo, conversationOption_0.Cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.OFFER_DEAL_GALAXYMAP:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_ACCEPT, TextResolver.GetText("We accept your proposal"), conversationOption_0.RelatedInfo, conversationOption_0.Cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.OFFER_DEAL_COMPONENT:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_ACCEPT, TextResolver.GetText("We accept your proposal"), conversationOption_0.RelatedInfo, conversationOption_0.Cost, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.DEAL_BEGIN:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_OFFER, TextResolver.GetText("Would you accept this trade?"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.DEAL_OFFER:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_ACCEPT, TextResolver.GetText("We accept this proposal"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_REJECT, TextResolver.GetText("We reject this proposal"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_IMPROVE, TextResolver.GetText("Let's make a different deal..."), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.DEAL_DEMAND:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_ACCEPTCOMPLAIN, TextResolver.GetText("We accede to your outrageous demands"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_REJECTCOMPLAIN, TextResolver.GetText("This proposal is unfair"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.DEAL_THREAT:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_ACCEPTCOMPLAIN, TextResolver.GetText("We accept your demands"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_REJECTCOMPLAIN, TextResolver.GetText("We reject your demands"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.DEAL_IMPROVE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_OFFER, TextResolver.GetText("How about this trade then?"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.DEAL_REJECT:
                case DialogPartType.DEAL_REJECTCOMPLAIN:
                    if (conversationOption_0.ReroutedType != DialogPartType.OFFER_DEAL_TERRITORYMAP && conversationOption_0.ReroutedType != DialogPartType.OFFER_DEAL_GALAXYMAP && conversationOption_0.ReroutedType != DialogPartType.OFFER_DEAL_COMPONENT)
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.DEAL_OFFER, TextResolver.GetText("Would you accept this trade?"), _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.MUTUALDEFENSE_REQUESTHELP:
                    {
                        Empire empire3 = empire_2;
                        if (conversationOption_0.RelatedInfo != null && conversationOption_0.RelatedInfo is Empire)
                        {
                            empire3 = (Empire)conversationOption_0.RelatedInfo;
                        }
                        conversationOptionList.Add(new ConversationOption(DialogPartType.MUTUALDEFENSE_HONORREQUESTHELP, string.Format(TextResolver.GetText("We stand alongside our friends and allies"), empire3.Name), empire3, 0.0, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.MUTUALDEFENSE_DECLINEREQUESTHELP, TextResolver.GetText("Sorry, we can't help you right now..."), _Game.PlayerEmpire));
                        break;
                    }
                case DialogPartType.TREATY_PROPOSAL:
                    {
                        double num7 = (double)_Game.PlayerEmpire.MilitaryPotency / (double)empire.MilitaryPotency;
                        ConversationOption item = new ConversationOption(DialogPartType.OFFER_MUTUALDEFENSE, TextResolver.GetText("Propose Mutual Defense Pact"), empire, 0.0, _Game.PlayerEmpire);
                        if (num7 > 5.0)
                        {
                            item = new ConversationOption(DialogPartType.OFFER_PROTECTORATE, TextResolver.GetText("Propose Protectorate"), empire, 0.0, _Game.PlayerEmpire);
                        }
                        ConversationOption item2 = new ConversationOption(DialogPartType.MILITARYREFUELING_OFFER, TextResolver.GetText("Allow Military Refueling"), _Game.PlayerEmpire);
                        ConversationOption item3 = new ConversationOption(DialogPartType.MILITARYREFUELING_CANCEL, TextResolver.GetText("Cancel Military Refueling"), _Game.PlayerEmpire);
                        ConversationOption item4 = new ConversationOption(DialogPartType.MININGRIGHTS_OFFER, TextResolver.GetText("Allow Mining Rights"), _Game.PlayerEmpire);
                        ConversationOption item5 = new ConversationOption(DialogPartType.MININGRIGHTS_CANCEL, TextResolver.GetText("Cancel Mining Rights"), _Game.PlayerEmpire);
                        empire.ObtainDiplomaticRelation(_Game.PlayerEmpire);
                        switch (diplomaticRelation.Type)
                        {
                            case DiplomaticRelationType.None:
                                conversationOptionList.Add(new ConversationOption(DialogPartType.OFFER_FREETRADE, TextResolver.GetText("Propose Free Trade Agreement"), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(item);
                                conversationOptionList.Add(new ConversationOption(DialogPartType.TRADESANCTIONS_IMPOSE, TextResolver.GetText("Impose Trade Sanctions"), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE, TextResolver.GetText("Declare War!"), empire, 0.0, _Game.PlayerEmpire));
                                if (diplomaticRelation.MilitaryRefuelingToOther)
                                {
                                    conversationOptionList.Add(item3);
                                }
                                else
                                {
                                    conversationOptionList.Add(item2);
                                }
                                if (diplomaticRelation.MiningRightsToOther)
                                {
                                    conversationOptionList.Add(item5);
                                }
                                else
                                {
                                    conversationOptionList.Add(item4);
                                }
                                break;
                            case DiplomaticRelationType.FreeTradeAgreement:
                                conversationOptionList.Add(new ConversationOption(DialogPartType.CANCELTREATY, string.Format(TextResolver.GetText("Cancel our current treaty"), Galaxy.ResolveDescription(diplomaticRelation.Type)), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(item);
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE, TextResolver.GetText("Declare War!"), empire, 0.0, _Game.PlayerEmpire));
                                if (diplomaticRelation.MilitaryRefuelingToOther)
                                {
                                    conversationOptionList.Add(item3);
                                }
                                else
                                {
                                    conversationOptionList.Add(item2);
                                }
                                if (diplomaticRelation.MiningRightsToOther)
                                {
                                    conversationOptionList.Add(item5);
                                }
                                else
                                {
                                    conversationOptionList.Add(item4);
                                }
                                break;
                            case DiplomaticRelationType.SubjugatedDominion:
                                if (diplomaticRelation.Initiator == _Game.PlayerEmpire)
                                {
                                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATION_RELEASE, TextResolver.GetText("We set you free from Subjugation"), relatedInfo, 0.0, _Game.PlayerEmpire));
                                    if (diplomaticRelation.MilitaryRefuelingToOther)
                                    {
                                        conversationOptionList.Add(item3);
                                    }
                                    else
                                    {
                                        conversationOptionList.Add(item2);
                                    }
                                    if (diplomaticRelation.MiningRightsToOther)
                                    {
                                        conversationOptionList.Add(item5);
                                    }
                                    else
                                    {
                                        conversationOptionList.Add(item4);
                                    }
                                }
                                else
                                {
                                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATION_REQUESTRELEASE, TextResolver.GetText("We beg for release from Subjugation"), relatedInfo, 0.0, _Game.PlayerEmpire));
                                    if (diplomaticRelation.MilitaryRefuelingToOther)
                                    {
                                        conversationOptionList.Add(item3);
                                    }
                                    else
                                    {
                                        conversationOptionList.Add(item2);
                                    }
                                    if (diplomaticRelation.MiningRightsToOther)
                                    {
                                        conversationOptionList.Add(item5);
                                    }
                                    else
                                    {
                                        conversationOptionList.Add(item4);
                                    }
                                }
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE, TextResolver.GetText("Declare War!"), empire, 0.0, _Game.PlayerEmpire));
                                break;
                            case DiplomaticRelationType.MutualDefensePact:
                            case DiplomaticRelationType.Protectorate:
                                conversationOptionList.Add(new ConversationOption(DialogPartType.CANCELTREATY, string.Format(TextResolver.GetText("Cancel our current treaty"), Galaxy.ResolveDescription(diplomaticRelation.Type)), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.OFFER_FREETRADE, TextResolver.GetText("Propose Free Trade Agreement"), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE, TextResolver.GetText("Declare War!"), empire, 0.0, _Game.PlayerEmpire));
                                if (diplomaticRelation.MiningRightsToOther)
                                {
                                    conversationOptionList.Add(item5);
                                }
                                else
                                {
                                    conversationOptionList.Add(item4);
                                }
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                if (diplomaticRelation.Initiator == _Game.PlayerEmpire)
                                {
                                    conversationOptionList.Add(new ConversationOption(DialogPartType.TRADESANCTIONS_LIFT, TextResolver.GetText("Lift Trade Sanctions"), relatedInfo, 0.0, _Game.PlayerEmpire));
                                }
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE, TextResolver.GetText("Declare War!"), relatedInfo, 0.0, _Game.PlayerEmpire));
                                break;
                            case DiplomaticRelationType.War:
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END, TextResolver.GetText("Propose an end to War"), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_SUBJUGATIONDEMAND, TextResolver.GetText("Propose an end to War if you agree to become our Subjugated Dominion"), empire, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_SUBJUGATIONOFFER, TextResolver.GetText("We agree to become your Subjugated Dominion to end this war"), _Game.PlayerEmpire, 0.0, _Game.PlayerEmpire));
                                break;
                            case DiplomaticRelationType.Truce:
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END, TextResolver.GetText("Propose an end to War"), relatedInfo, 0.0, _Game.PlayerEmpire));
                                conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE, TextResolver.GetText("Declare War!"), relatedInfo, 0.0, _Game.PlayerEmpire));
                                break;
                        }
                        break;
                    }
                case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER:
                    if (relatedInfo is Empire)
                    {
                        empire2 = (Empire)relatedInfo;
                        conversationOptionList.Add(new ConversationOption(DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER_ACCEPT, string.Format(TextResolver.GetText("Lift Trade Sanctions against X"), empire2.Name), relatedInfo, cost, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER_REJECT, TextResolver.GetText("No, our Trade Sanctions will continue"), relatedInfo, cost, _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT:
                    if (relatedInfo is Empire)
                    {
                        empire2 = (Empire)relatedInfo;
                        conversationOptionList.Add(new ConversationOption(DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT_ACCEPT, string.Format(TextResolver.GetText("Impose Trade Sanctions on X"), empire2.Name), relatedInfo, cost, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT_REJECT, TextResolver.GetText("No, we see no need for Trade Sanctions"), relatedInfo, cost, _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.WAR_DECLARE_REQUESTJOINT:
                    if (relatedInfo is Empire)
                    {
                        empire2 = (Empire)relatedInfo;
                        conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE_REQUESTJOINT_ACCEPT, string.Format(TextResolver.GetText("Ok, we declare war on the X"), empire2.Name), relatedInfo, 0.0, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_DECLARE_REQUESTJOINT_REJECT, TextResolver.GetText("Sorry, this is not our war"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.WAR_END:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_ACCEPT, TextResolver.GetText("We agree - this war ends now"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_SUBJUGATIONDEMAND, TextResolver.GetText("We agree to end this war only if you agree to become our Subjugated Dominion"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_REJECT, TextResolver.GetText("No, we will fight on"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.WAR_END_SUBJUGATIONDEMAND:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATIONDEMAND_ACCEPT, TextResolver.GetText("Yes, we accept defeat and acknowledge your status as our ruler"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATIONDEMAND_REJECT, TextResolver.GetText("No, we will not become your slaves!"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.WAR_END_SUBJUGATIONOFFER:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATIONOFFER_ACCEPT, TextResolver.GetText("Yes, we accept your acknowledgement of defeat and look forward to receiving our regular tribute"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATIONOFFER_REJECT, TextResolver.GetText("No, you must suffer further before this war will end"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    break;
                case DialogPartType.WAR_END_REQUESTOTHER:
                    if (relatedInfo is Empire)
                    {
                        empire2 = (Empire)relatedInfo;
                        conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_REQUESTOTHER_ACCEPT, string.Format(TextResolver.GetText("Ok, we will end our war with the X"), empire2.Name), relatedInfo, 0.0, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.WAR_END_REQUESTOTHER_REJECT, TextResolver.GetText("No, we will continue our fight"), relatedInfo, 0.0, _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.SUBJUGATION_REQUESTRELEASE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATION_RELEASE, TextResolver.GetText("Alright, we set you free from subjugation"), _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.SUBJUGATION_REFUSERELEASE, TextResolver.GetText("No, you must remain our slaves"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.GIFT_GIVE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.GIFT_THANKS, TextResolver.GetText("Thanks!"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.GIFT_PROPOSE:
                    {
                        double num = _Game.PlayerEmpire.StateMoney / 8.0;
                        double num2 = num / 2.0;
                        double num3 = num / 4.0;
                        if (num3 > 0.0)
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.GIFT_GIVE, string.Format(TextResolver.GetText("Send small gift"), num3.ToString("###,###,###,##0")), null, num3, _Game.PlayerEmpire));
                        }
                        if (num2 > 0.0)
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.GIFT_GIVE, string.Format(TextResolver.GetText("Send medium gift"), num2.ToString("###,###,###,##0")), null, num2, _Game.PlayerEmpire));
                        }
                        if (num > 0.0)
                        {
                            conversationOptionList.Add(new ConversationOption(DialogPartType.GIFT_GIVE, string.Format(TextResolver.GetText("Send large gift"), num.ToString("###,###,###,##0")), null, num, _Game.PlayerEmpire));
                        }
                        break;
                    }
                case DialogPartType.WARNING:
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.WARNING_INTELLIGENCEMISSIONS, TextResolver.GetText("End your treacherous covert missions against us"), _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.WARNING_ATTACKS, TextResolver.GetText("Stop your military attacks against us"), _Game.PlayerEmpire));
                        DiplomaticRelation diplomaticRelation2 = _Game.PlayerEmpire.ObtainDiplomaticRelation(empire);
                        if (!diplomaticRelation2.MilitaryRefuelingToOther)
                        {
                            HabitatList habitatList = _Game.PlayerEmpire.DetermineEmpireSystemsWithOtherMilitaryForcesPresent(empire);
                            Habitat relatedInfo2 = null;
                            if (habitatList.Count > 0)
                            {
                                relatedInfo2 = habitatList[0];
                            }
                            conversationOptionList.Add(new ConversationOption(DialogPartType.WARNING_REMOVEFORCESSYSTEM, TextResolver.GetText("Remove your military forces from our territory"), relatedInfo2, 0.0, _Game.PlayerEmpire));
                        }
                        break;
                    }
                case DialogPartType.WARNING_REMOVEFORCESSYSTEM:
                    if (relatedInfo is Habitat)
                    {
                        Habitat habitat = (Habitat)relatedInfo;
                        Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                        conversationOptionList.Add(new ConversationOption(DialogPartType.GOTO_TARGET, string.Format(TextResolver.GetText("Go to X system"), habitat2.Name), habitat2, 0.0, _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.HISTORY_OFFER_LOCATIONHINT:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.HISTORY_OFFER_LOCATIONHINT_ACCEPT, TextResolver.GetText("Tell us more"), _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.HISTORY_OFFER_LOCATIONHINT_REJECT, TextResolver.GetText("We are not interested"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.HISTORY_OFFER_STORYCLUE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.HISTORY_OFFER_STORYCLUE_ACCEPT, TextResolver.GetText("Tell us more"), _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.HISTORY_OFFER_STORYCLUE_REJECT, TextResolver.GetText("We are not interested"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.HISTORY_OFFER_STORYMESSAGE:
                    conversationOptionList.Add(new ConversationOption(DialogPartType.HISTORY_OFFER_STORYMESSAGE_ACCEPT, TextResolver.GetText("Tell us more"), _Game.PlayerEmpire));
                    conversationOptionList.Add(new ConversationOption(DialogPartType.HISTORY_OFFER_STORYMESSAGE_REJECT, TextResolver.GetText("We are not interested"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.PIRATE_PROTECTIONPROPOSE:
                case DialogPartType.PIRATE_TRUCEPROPOSE:
                    cost = empire.CalculatePirateProtectionPricePerMonth(_Game.PlayerEmpire);
                    if (cost <= 0.0)
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_TRUCEACCEPTRESPONSE, TextResolver.GetText("We accept a truce"), null, cost, _Game.PlayerEmpire));
                    }
                    else
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONACCEPTRESPONSE, string.Format(TextResolver.GetText("We accept your protection PER MONTH"), cost.ToString("###,###,###,##0")), null, cost, _Game.PlayerEmpire));
                    }
                    if (cost <= 0.0)
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_TRUCEREJECTRESPONSE, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    }
                    else
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONREJECTRESPONSE, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    }
                    break;
                case DialogPartType.PIRATE_PROTECTIONPROPOSEINITIATE:
                case DialogPartType.PIRATE_EXTORTPROTECTION:
                case DialogPartType.PIRATE_TRUCEPROPOSEINITIATE:
                    cost = conversationOption_0.Cost;
                    if (cost <= 0.0)
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_TRUCEACCEPTRESPONSE, TextResolver.GetText("We accept a truce"), null, cost, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_TRUCEREJECTRESPONSE, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    }
                    else
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONACCEPTRESPONSE, string.Format(TextResolver.GetText("We accept your protection"), cost.ToString("###,###,###,##0")), null, cost, _Game.PlayerEmpire));
                        conversationOptionList.Add(new ConversationOption(DialogPartType.PIRATE_PROTECTIONREJECTRESPONSE, TextResolver.GetText("No thanks"), _Game.PlayerEmpire));
                    }
                    break;
            }
            switch (type)
            {
                default:
                    if (type != DialogPartType.GREETING_ANGRY && type != DialogPartType.GREETING_FRIENDLY && type != DialogPartType.GREETING_INTRODUCTION && type != DialogPartType.GREETING_NEUTRAL)
                    {
                        conversationOptionList.Add(new ConversationOption(DialogPartType.GREETING_NEUTRAL, TextResolver.GetText("Let's discuss something else..."), _Game.PlayerEmpire));
                    }
                    conversationOptionList.Add(new ConversationOption(DialogPartType.Exit, TextResolver.GetText("Goodbye"), _Game.PlayerEmpire));
                    break;
                case DialogPartType.INFO_OFFER_UNMETEMPIRE:
                case DialogPartType.INFO_OFFER_INDEPENDENTCOLONY:
                case DialogPartType.INFO_OFFER_SYSTEMMAPS:
                case DialogPartType.INFO_OFFER_RUINS:
                case DialogPartType.INFO_OFFER_RESTRICTEDAREA:
                case DialogPartType.INFO_OFFER_DEBRISFIELD:
                case DialogPartType.INFO_OFFER_PLANETDESTROYER:
                case DialogPartType.PIRATE_PROTECTIONPROPOSE:
                case DialogPartType.PIRATE_PROTECTIONPROPOSEINITIATE:
                case DialogPartType.OFFER_FREETRADE:
                case DialogPartType.OFFER_PROTECTORATE:
                case DialogPartType.OFFER_MUTUALDEFENSE:
                case DialogPartType.OFFER_DEAL:
                case DialogPartType.DEAL_OFFER:
                case DialogPartType.DEAL_DEMAND:
                case DialogPartType.DEAL_THREAT:
                case DialogPartType.MUTUALDEFENSE_REQUESTHELP:
                case DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER:
                case DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT:
                case DialogPartType.WAR_DECLARE_REQUESTJOINT:
                case DialogPartType.WAR_END:
                case DialogPartType.WAR_END_SUBJUGATIONDEMAND:
                case DialogPartType.WAR_END_SUBJUGATIONOFFER:
                case DialogPartType.WAR_END_REQUESTOTHER:
                case DialogPartType.SUBJUGATION_REQUESTRELEASE:
                case DialogPartType.HISTORY_OFFER_LOCATIONHINT:
                case DialogPartType.HISTORY_OFFER_STORYCLUE:
                case DialogPartType.HISTORY_OFFER_STORYMESSAGE:
                case DialogPartType.PIRATE_EXTORTPROTECTION:
                case DialogPartType.PIRATE_TRUCEPROPOSE:
                case DialogPartType.PIRATE_TRUCEPROPOSEINITIATE:
                    break;
            }
            SuspendLayout();
            switch (type)
            {
                default:
                    method_297(empire);
                    break;
                case DialogPartType.DEAL_BEGIN:
                case DialogPartType.DEAL_OFFER:
                case DialogPartType.DEAL_DEMAND:
                case DialogPartType.DEAL_THREAT:
                case DialogPartType.DEAL_ACCEPT:
                case DialogPartType.DEAL_ACCEPTCOMPLAIN:
                case DialogPartType.DEAL_IMPROVE:
                case DialogPartType.DEAL_REJECT:
                case DialogPartType.DEAL_REJECTCOMPLAIN:
                case DialogPartType.DEAL_REJECT_RESPONSE:
                case DialogPartType.DEAL_REJECTDEMAND_RESPONSE:
                case DialogPartType.DEAL_ACCEPT_RESPONSE:
                    break;
            }
            ctlDiplomacyConversation.SuspendLayout();
            ctlDiplomacyConversation.Visible = false;
            ctlDiplomacyConversation.KickStart(this, _Game.Galaxy, cursor_0);
            foreach (ConversationOption item7 in conversationOptionList)
            {
                ctlDiplomacyConversation.AddItem(item7.Text, item7);
            }
            ctlDiplomacyConversation.Visible = true;
            ctlDiplomacyConversation.ResumeLayout();
            ResumeLayout();
        }

        internal void method_239(object sender, EventArgs e)
        {
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            LinkLabel linkLabel = ctlDiplomacyConversation.ResolveHoveredLink(point);
            if (linkLabel == null || linkLabel.Links == null || linkLabel.Links.Count <= 0 || linkLabel.Links[0].LinkData == null || !(linkLabel.Links[0].LinkData is ConversationOption))
            {
                return;
            }
            ConversationOption conversationOption = (ConversationOption)linkLabel.Links[0].LinkData;
            if (conversationOption.RelatedInfo != null && conversationOption.RelatedInfo is TradeableItem)
            {
                TradeableItem tradeableItem = (TradeableItem)conversationOption.RelatedInfo;
                if (tradeableItem.Type == TradeableItemType.ResearchProject && tradeableItem.Item is ResearchNode)
                {
                    ResearchNode object_ = (ResearchNode)tradeableItem.Item;
                    Rectangle rectangle = ctlDiplomacyConversation.ResolveLinkBounds(linkLabel);
                    method_298(rectangle.Right, rectangle.Y, object_, bool_28: false);
                }
            }
        }

        internal void method_240(object sender, EventArgs e)
        {
            method_299();
        }

        internal void method_241(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctlDiplomacyConversation.Clearing)
            {
                return;
            }
            object linkData = e.Link.LinkData;
            method_226();
            if (linkData is ConversationOption)
            {
                ConversationOption conversationOption_ = (ConversationOption)linkData;
                conversationOption_ = method_237(conversationOption_);
                if (conversationOption_.Type != DialogPartType.Exit && conversationOption_.Type != DialogPartType.GOTO_TARGET && (conversationOption_.Type != DialogPartType.WAR_END_REJECT || conversationOption_.Initiator != _Game.PlayerEmpire) && conversationOption_.Type != DialogPartType.HISTORY_OFFER_LOCATIONHINT_REJECT && conversationOption_.Type != DialogPartType.HISTORY_OFFER_STORYCLUE_REJECT && conversationOption_.Type != DialogPartType.HISTORY_OFFER_STORYMESSAGE_REJECT && conversationOption_.Type != DialogPartType.HISTORY_OFFER_LOCATIONHINT_ACCEPT && conversationOption_.Type != DialogPartType.HISTORY_OFFER_STORYCLUE_ACCEPT && conversationOption_.Type != DialogPartType.HISTORY_OFFER_STORYMESSAGE_ACCEPT)
                {
                    method_230(conversationOption_);
                    method_238(conversationOption_);
                }
            }
        }

        private void method_242(EmpireMessage empireMessage_1)
        {
            object obj = null;
            obj = empireMessage_1.Subject;
            if (obj is DiplomaticRelationType)
            {
                obj = empireMessage_1.Sender;
            }
            if (empireMessage_1.Money > 0)
            {
                obj = empireMessage_1.Sender;
            }
            if (obj is Empire)
            {
                obj = empireMessage_1.Location;
            }
            method_243(obj);
        }

        internal void method_243(object object_7)
        {
            if (object_7 != null)
            {
                if (object_7 is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)object_7;
                    EventPing item = new EventPing((int)builtObject.Xpos, (int)builtObject.Ypos, builtObject);
                    mainView.EventLocations.Add(item);
                }
                else if (object_7 is Habitat)
                {
                    Habitat habitat = (Habitat)object_7;
                    EventPing item2 = new EventPing((int)habitat.Xpos, (int)habitat.Ypos, habitat);
                    mainView.EventLocations.Add(item2);
                }
                else if (object_7 is Point)
                {
                    Point point = (Point)object_7;
                    EventPing item3 = new EventPing(point.X, point.Y, point);
                    mainView.EventLocations.Add(item3);
                }
            }
        }

        internal void method_244(EmpireMessage empireMessage_1)
        {
            object obj = null;
            obj = empireMessage_1.Subject;
            if (obj is DiplomaticRelationType)
            {
                obj = empireMessage_1.Sender;
            }
            if (empireMessage_1.Money > 0)
            {
                obj = empireMessage_1.Sender;
            }
            if (obj is Empire)
            {
                obj = empireMessage_1.Location;
            }
            method_245(obj);
        }

        internal void method_245(object object_7)
        {
            if (object_7 == null)
            {
                return;
            }
            if (object_7 is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)object_7;
                List<EventPing> list = new List<EventPing>();
                foreach (EventPing eventLocation in mainView.EventLocations)
                {
                    if (eventLocation.Object is BuiltObject && (BuiltObject)eventLocation.Object == builtObject)
                    {
                        list.Add(eventLocation);
                    }
                }
                {
                    foreach (EventPing item in list)
                    {
                        mainView.EventLocations.Remove(item);
                    }
                    return;
                }
            }
            if (object_7 is Habitat)
            {
                Habitat habitat = (Habitat)object_7;
                List<EventPing> list2 = new List<EventPing>();
                foreach (EventPing eventLocation2 in mainView.EventLocations)
                {
                    if (eventLocation2.Object is Habitat && (Habitat)eventLocation2.Object == habitat)
                    {
                        list2.Add(eventLocation2);
                    }
                }
                {
                    foreach (EventPing item2 in list2)
                    {
                        mainView.EventLocations.Remove(item2);
                    }
                    return;
                }
            }
            if (!(object_7 is Point))
            {
                return;
            }
            Point point = (Point)object_7;
            List<EventPing> list3 = new List<EventPing>();
            foreach (EventPing eventLocation3 in mainView.EventLocations)
            {
                if (eventLocation3.Object is Point)
                {
                    Point point2 = (Point)eventLocation3.Object;
                    if (point2.X == point.X && point2.Y == point.Y)
                    {
                        list3.Add(eventLocation3);
                    }
                }
            }
            foreach (EventPing item3 in list3)
            {
                mainView.EventLocations.Remove(item3);
            }
        }

        internal void method_246(BuiltObjectList builtObjectList_1)
        {
            if (builtObjectList_1 != null)
            {
                mainView.SpecialHighlightBuiltObjects = builtObjectList_1;
            }
            else
            {
                mainView.SpecialHighlightBuiltObjects = new BuiltObjectList();
            }
        }

        internal void method_247(object sender, EventArgs e)
        {
            if (sender is LinkLabel)
            {
                LinkLabel linkLabel = (LinkLabel)sender;
                object linkData = linkLabel.Links[0].LinkData;
                if (linkData is EmpireMessage)
                {
                    EmpireMessage empireMessage_ = (EmpireMessage)linkData;
                    method_242(empireMessage_);
                }
            }
        }

        internal void method_248(object sender, EventArgs e)
        {
            if (sender is LinkLabel)
            {
                LinkLabel linkLabel = (LinkLabel)sender;
                object linkData = linkLabel.Links[0].LinkData;
                if (linkData is EmpireMessage)
                {
                    EmpireMessage empireMessage_ = (EmpireMessage)linkData;
                    method_244(empireMessage_);
                }
            }
        }

        internal void method_249(object sender, LinkLabelLinkClickedEventArgs e)
        {
            object linkData = e.Link.LinkData;
            object obj = null;
            method_226();
            EmpireMessageType empireMessageType = EmpireMessageType.GeneralNeutralEvent;
            if (linkData is EmpireMessage)
            {
                EmpireMessage empireMessage = (EmpireMessage)linkData;
                obj = empireMessage.Subject;
                empireMessageType = empireMessage.MessageType;
                if (obj is DiplomaticRelationType)
                {
                    obj = empireMessage.Sender;
                }
                if (empireMessage.Money > 0)
                {
                    obj = empireMessage.Sender;
                }
            }
            if (obj != null)
            {
                if (obj is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)obj;
                    method_157(builtObject);
                    method_4(1.0);
                    method_208(builtObject);
                    if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed >= (float)builtObject.WarpSpeed && builtObject.WarpSpeed > 0)
                    {
                        UhvLmNjli7 = true;
                    }
                }
                else if (obj is ShipGroup)
                {
                    ShipGroup object_ = (ShipGroup)obj;
                    method_157(object_);
                    method_4(3000.0);
                    method_208(object_);
                }
                else if (obj is Habitat)
                {
                    Habitat habitat = (Habitat)obj;
                    switch (empireMessageType)
                    {
                        case EmpireMessageType.NewColony:
                            method_208(habitat);
                            method_166(habitat);
                            break;
                        case EmpireMessageType.BattleUnderAttack:
                            method_157(habitat);
                            method_4(1.0);
                            method_208(habitat);
                            if (habitat.Population != null && habitat.Population.Count > 0)
                            {
                                method_164(habitat);
                            }
                            break;
                        default:
                            method_157(obj);
                            method_4(1.0);
                            method_208(obj);
                            break;
                    }
                }
                else if (obj is Character)
                {
                    Character character = (Character)obj;
                    if (character.Empire == _Game.PlayerEmpire)
                    {
                        method_424(character);
                    }
                    else
                    {
                        method_195(character.Empire);
                    }
                }
                else if (obj is ResearchNode)
                {
                    ResearchNode researchNode = (ResearchNode)obj;
                    if (researchNode.Components != null && researchNode.Components.Count > 0)
                    {
                        method_394(researchNode.Industry, researchNode);
                        method_543(researchNode.Components[0]);
                    }
                    else if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0)
                    {
                        method_394(researchNode.Industry, researchNode);
                        method_543(researchNode.ComponentImprovements[0].ImprovedComponent);
                    }
                    else if (researchNode.Abilities != null && researchNode.Abilities.Count > 0)
                    {
                        method_394(researchNode.Industry, researchNode);
                    }
                    else if (researchNode.Fighters != null && researchNode.Fighters.Count > 0)
                    {
                        method_394(researchNode.Industry, researchNode);
                    }
                    else if (researchNode.PlanetaryFacility != null)
                    {
                        method_394(researchNode.Industry, researchNode);
                    }
                }
                else if (obj is Resource)
                {
                    Resource resource = (Resource)obj;
                    method_456(resource.Name);
                }
                else if (obj is Empire)
                {
                    method_195((Empire)obj);
                }
                else if (obj is DistantWorlds.Types.Component)
                {
                    method_393(((DistantWorlds.Types.Component)obj).Industry);
                    method_543((DistantWorlds.Types.Component)obj);
                }
                else if (obj is Point point)
                {
                    method_156(point.X, point.Y);
                    method_4(1.0);
                }
            }
            Focus();
        }

        private string method_250(EmpireMessage empireMessage_1)
        {
            string text = string.Empty;
            if (empireMessage_1.Sender != _Game.PlayerEmpire && empireMessage_1.MessageType != EmpireMessageType.GalacticNewsNet)
            {
                text = string.Format(TextResolver.GetText("X says"), empireMessage_1.Sender.Name) + ": ";
            }
            return text + empireMessage_1.Description;
        }

        public void PromptForAuthorization(string message, object target, EmpireMessage empireMessage)
        {
            Delegate4 method = PromptForAuthorizationInternal;
            BeginInvoke(method, message, target, empireMessage);
            Application.DoEvents();
        }

        public void PromptForAuthorizationInternal(string message, object target, EmpireMessage empireMessage)
        {
            diplomaticMessageQueue_0.AddMessage(empireMessage, new ConversationOption(DialogPartType.Undefined, "", null));
            diplomaticMessageQueue_0.ExpireInvalidMessages(empireMessage);
        }

        private void method_251(EmpireMessage empireMessage_1)
        {
            if (string.IsNullOrEmpty(empireMessage_1.Title))
            {
                switch (empireMessage_1.MessageType)
                {
                    case EmpireMessageType.DiplomaticRelationChange:
                        empireMessage_1.Title = TextResolver.GetText("Diplomatic Relation Change");
                        if (empireMessage_1.Subject is DiplomaticRelationType)
                        {
                            switch ((DiplomaticRelationType)empireMessage_1.Subject)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                    empireMessage_1.Title = TextResolver.GetText("Free Trade Agreement Offered");
                                    break;
                                case DiplomaticRelationType.MutualDefensePact:
                                    empireMessage_1.Title = TextResolver.GetText("Mutual Defense Pact Offered");
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    empireMessage_1.Title = TextResolver.GetText("Subjugation proposed");
                                    break;
                                case DiplomaticRelationType.Protectorate:
                                    empireMessage_1.Title = TextResolver.GetText("Protectorate Offered");
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    empireMessage_1.Title = TextResolver.GetText("Trade Sanctions Imposed");
                                    break;
                                case DiplomaticRelationType.War:
                                    empireMessage_1.Title = TextResolver.GetText("War Declared!");
                                    break;
                            }
                        }
                        break;
                    case EmpireMessageType.ProposeDiplomaticRelation:
                        empireMessage_1.Title = TextResolver.GetText("Treaty proposed");
                        if (empireMessage_1.Subject is DiplomaticRelationType)
                        {
                            switch ((DiplomaticRelationType)empireMessage_1.Subject)
                            {
                                case DiplomaticRelationType.FreeTradeAgreement:
                                    empireMessage_1.Title = TextResolver.GetText("Free Trade Agreement Offered");
                                    break;
                                case DiplomaticRelationType.MutualDefensePact:
                                    empireMessage_1.Title = TextResolver.GetText("Mutual Defense Pact Offered");
                                    break;
                                case DiplomaticRelationType.SubjugatedDominion:
                                    empireMessage_1.Title = TextResolver.GetText("Subjugation proposed");
                                    break;
                                case DiplomaticRelationType.Protectorate:
                                    empireMessage_1.Title = TextResolver.GetText("Protectorate Offered");
                                    break;
                                case DiplomaticRelationType.TradeSanctions:
                                    empireMessage_1.Title = TextResolver.GetText("Trade Sanctions Imposed");
                                    break;
                                case DiplomaticRelationType.War:
                                    empireMessage_1.Title = TextResolver.GetText("War Declared!");
                                    break;
                            }
                        }
                        break;
                    case EmpireMessageType.AcceptDiplomaticRelation:
                        empireMessage_1.Title = TextResolver.GetText("Treaty accepted");
                        break;
                    case EmpireMessageType.RefuseDiplomaticRelation:
                        empireMessage_1.Title = TextResolver.GetText("Treaty refused");
                        break;
                    case EmpireMessageType.RemoveColoniesFromSystem:
                        empireMessage_1.Title = TextResolver.GetText("Request to remove presence from system");
                        break;
                    case EmpireMessageType.StopMissionsAgainstUs:
                        empireMessage_1.Title = TextResolver.GetText("Warning to stop intelligence missions");
                        break;
                    case EmpireMessageType.StopAttacks:
                        empireMessage_1.Title = TextResolver.GetText("Warning to stop attacks");
                        break;
                    case EmpireMessageType.LeaveSystem:
                        empireMessage_1.Title = TextResolver.GetText("Request to Leave System");
                        break;
                    case EmpireMessageType.RequestJointWar:
                        empireMessage_1.Title = TextResolver.GetText("Request to Declare War");
                        break;
                    case EmpireMessageType.RequestJointTradeSanctions:
                        empireMessage_1.Title = TextResolver.GetText("Request to impose Trade Sanctions");
                        break;
                    case EmpireMessageType.RequestStopWar:
                        empireMessage_1.Title = TextResolver.GetText("Request to end War");
                        break;
                    case EmpireMessageType.RequestLiftTradeSanctions:
                        empireMessage_1.Title = TextResolver.GetText("Request to lift Trade Sanctions");
                        break;
                    case EmpireMessageType.GiveGift:
                        empireMessage_1.Title = TextResolver.GetText("Monetary Gift");
                        break;
                    case EmpireMessageType.Informational:
                        empireMessage_1.Title = TextResolver.GetText("General Information");
                        break;
                    case EmpireMessageType.ShipBaseCompleted:
                        empireMessage_1.Title = TextResolver.GetText("Ship completed");
                        break;
                    case EmpireMessageType.ShipBasePurchased:
                        empireMessage_1.Title = TextResolver.GetText("Ship purchased");
                        break;
                    case EmpireMessageType.NewColony:
                        empireMessage_1.Title = TextResolver.GetText("New colony established");
                        break;
                    case EmpireMessageType.NewColonyFailed:
                        empireMessage_1.Title = TextResolver.GetText("Colonization failed");
                        break;
                    case EmpireMessageType.ResearchBreakthrough:
                        empireMessage_1.Title = TextResolver.GetText("Research Breakthrough");
                        break;
                    case EmpireMessageType.BattleUnderAttack:
                        empireMessage_1.Title = TextResolver.GetText("Under attack!");
                        if (empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject3 = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject3.Role != BuiltObjectRole.Base || builtObject3.ParentHabitat == null)
                            {
                                Point point2 = (empireMessage_1.Location = method_530(empireMessage_1));
                            }
                        }
                        break;
                    case EmpireMessageType.BattleAttacking:
                        empireMessage_1.Title = TextResolver.GetText("Attacking enemy");
                        if (empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject4 = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject4.Role != BuiltObjectRole.Base || builtObject4.ParentHabitat == null)
                            {
                                Point point4 = (empireMessage_1.Location = method_530(empireMessage_1));
                            }
                        }
                        break;
                    case EmpireMessageType.IncomingEnemyFleet:
                        empireMessage_1.Title = TextResolver.GetText("Incoming Enemy Fleet!");
                        break;
                    case EmpireMessageType.CharacterAppearance:
                        empireMessage_1.Title = TextResolver.GetText("Character Appears");
                        break;
                    case EmpireMessageType.CharacterDeath:
                        empireMessage_1.Title = TextResolver.GetText("Character Killed");
                        break;
                    case EmpireMessageType.CharacterMissionAccomplished:
                        empireMessage_1.Title = TextResolver.GetText("Agent mission succeeds");
                        break;
                    case EmpireMessageType.CharacterMissionFailure:
                        empireMessage_1.Title = TextResolver.GetText("Agent mission fails");
                        break;
                    case EmpireMessageType.EmpireDiscovered:
                        empireMessage_1.Title = TextResolver.GetText("New empire encountered");
                        break;
                    case EmpireMessageType.ColonyGained:
                        empireMessage_1.Title = TextResolver.GetText("Colony gained");
                        break;
                    case EmpireMessageType.ColonyLost:
                        empireMessage_1.Title = TextResolver.GetText("Colony lost!");
                        break;
                    case EmpireMessageType.ColonyDefended:
                        empireMessage_1.Title = TextResolver.GetText("Colony defends against invasion!");
                        break;
                    case EmpireMessageType.ColonyRebelling:
                        empireMessage_1.Title = TextResolver.GetText("Colony rebelling!");
                        break;
                    case EmpireMessageType.EmpireDefeated:
                        empireMessage_1.Title = TextResolver.GetText("Empire Defeated!");
                        break;
                    case EmpireMessageType.RequestHonorMutualDefense:
                        empireMessage_1.Title = TextResolver.GetText("Request to honor Mutual Defense Pact");
                        break;
                    case EmpireMessageType.BlockadeInitiated:
                        empireMessage_1.Title = TextResolver.GetText("Blockade begins");
                        break;
                    case EmpireMessageType.BlockadeCancelled:
                        empireMessage_1.Title = TextResolver.GetText("Blockade ends");
                        break;
                    case EmpireMessageType.ExplorationRuins:
                        empireMessage_1.Title = TextResolver.GetText("Ruins discovered");
                        break;
                    case EmpireMessageType.ExplorationBuiltObject:
                        empireMessage_1.Title = TextResolver.GetText("Ship discovered");
                        break;
                    case EmpireMessageType.ExplorationHabitat:
                        empireMessage_1.Title = TextResolver.GetText("Planet discovered");
                        break;
                    case EmpireMessageType.ExplorationLocation:
                        empireMessage_1.Title = TextResolver.GetText("Location discovered");
                        break;
                    case EmpireMessageType.GalacticHistory:
                        empireMessage_1.Title = TextResolver.GetText("Galactic History revealed");
                        break;
                    case EmpireMessageType.SellInfoUnmetEmpire:
                        empireMessage_1.Title = TextResolver.GetText("Pirates Offer Empire Contact");
                        break;
                    case EmpireMessageType.SellInfoIndependentColony:
                        empireMessage_1.Title = TextResolver.GetText("Pirates Offer Colony Location");
                        break;
                    case EmpireMessageType.SellInfoSystemMap:
                        empireMessage_1.Title = TextResolver.GetText("Pirates Offer System Map");
                        break;
                    case EmpireMessageType.SellInfoRuins:
                    case EmpireMessageType.SellInfoDebrisField:
                    case EmpireMessageType.SellInfoRestrictedArea:
                    case EmpireMessageType.SellInfoPlanetDestroyer:
                        empireMessage_1.Title = TextResolver.GetText("Pirates Offer Discovery");
                        break;
                    case EmpireMessageType.PirateOfferProtection:
                        if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                        {
                            empireMessage_1.Title = TextResolver.GetText("Pirates Offer Truce");
                        }
                        else
                        {
                            empireMessage_1.Title = TextResolver.GetText("Pirates Offer Protection");
                        }
                        break;
                    case EmpireMessageType.CancelPirateProtection:
                        empireMessage_1.Title = TextResolver.GetText("Cancel Pirate Protection Title");
                        break;
                    case EmpireMessageType.Revolution:
                        empireMessage_1.Title = TextResolver.GetText("Revolution! Our government has changed");
                        break;
                    case EmpireMessageType.RestrictedResourceDiscovered:
                        empireMessage_1.Title = TextResolver.GetText("Valuable Resource Discovered");
                        break;
                    case EmpireMessageType.RestrictedResourceTradingAllowed:
                        empireMessage_1.Title = TextResolver.GetText("Valuable Resource Traded with Us");
                        break;
                    case EmpireMessageType.RestrictedResourceTradingBlocked:
                        empireMessage_1.Title = TextResolver.GetText("Valuable Resource Trading Terminated");
                        break;
                    case EmpireMessageType.OfferTrade:
                        {
                            string title = string.Empty;
                            if (empireMessage_1.Subject is object[])
                            {
                                title = TextResolver.GetText("Trade Deal offered");
                            }
                            else if (empireMessage_1.Subject is TradeableItem)
                            {
                                TradeableItem tradeableItem = (TradeableItem)empireMessage_1.Subject;
                                switch (tradeableItem.Type)
                                {
                                    case TradeableItemType.ResearchProject:
                                        {
                                            ResearchNode researchNode = (ResearchNode)tradeableItem.Item;
                                            title = string.Format(TextResolver.GetText("Technology offered"), researchNode.Name);
                                            break;
                                        }
                                    case TradeableItemType.TerritoryMap:
                                        title = TextResolver.GetText("Territory Map swap offered");
                                        break;
                                    case TradeableItemType.GalaxyMap:
                                        title = TextResolver.GetText("Galaxy Map swap offered");
                                        break;
                                }
                            }
                            empireMessage_1.Title = title;
                            break;
                        }
                    case EmpireMessageType.ShipMissionComplete:
                        empireMessage_1.Title = TextResolver.GetText("Ship Mission Complete");
                        break;
                    case EmpireMessageType.ShipNeedsRefuelling:
                        empireMessage_1.Title = TextResolver.GetText("Ship needs Refuelling");
                        break;
                    case EmpireMessageType.ShipNeedsRepair:
                        empireMessage_1.Title = TextResolver.GetText("Ship Stranded");
                        break;
                    case EmpireMessageType.RemoveForcesFromSystem:
                        empireMessage_1.Title = TextResolver.GetText("Remove Military forces from System");
                        break;
                    case EmpireMessageType.GeneralWarning:
                        empireMessage_1.Title = TextResolver.GetText("You've been warned!");
                        break;
                    case EmpireMessageType.GeneralBadEvent:
                        empireMessage_1.Title = TextResolver.GetText("Catastrophe!");
                        break;
                    case EmpireMessageType.GeneralNeutralEvent:
                        empireMessage_1.Title = "";
                        break;
                    case EmpireMessageType.GeneralGoodEvent:
                        empireMessage_1.Title = TextResolver.GetText("Celebration!");
                        break;
                    case EmpireMessageType.GeneralDecision:
                        empireMessage_1.Title = TextResolver.GetText("You must decide...");
                        break;
                    case EmpireMessageType.HistoryOfferLocationHint:
                        empireMessage_1.Title = TextResolver.GetText("Secret location offered");
                        break;
                    case EmpireMessageType.HistoryOfferStoryClue:
                        empireMessage_1.Title = TextResolver.GetText("Secret history offered");
                        break;
                    case EmpireMessageType.ColonyFacilityCompleted:
                        empireMessage_1.Title = TextResolver.GetText("Planetary Facility completed");
                        break;
                    case EmpireMessageType.ColonyFacilityCancelled:
                        empireMessage_1.Title = TextResolver.GetText("Wonder cancelled");
                        break;
                    case EmpireMessageType.ColonyWonderBegun:
                        empireMessage_1.Title = TextResolver.GetText("Wonder begun");
                        break;
                    case EmpireMessageType.ColonyShipMissionCancelled:
                        empireMessage_1.Title = TextResolver.GetText("Colony ship failed to colonize");
                        break;
                    case EmpireMessageType.StoryMessage:
                        empireMessage_1.Title = TextResolver.GetText("Secret Warning offered");
                        break;
                    case EmpireMessageType.AdvisorSuggestion:
                        empireMessage_1.Title = TextResolver.GetText("Advisor Suggestion");
                        break;
                    case EmpireMessageType.ColonyDestroyed:
                        empireMessage_1.Title = TextResolver.GetText("Colony Destroyed") + "!";
                        break;
                    case EmpireMessageType.MilitaryRefuelingAllowed:
                        empireMessage_1.Title = TextResolver.GetText("Military Refueling Allowed with Us");
                        break;
                    case EmpireMessageType.MilitaryRefuelingBlocked:
                        empireMessage_1.Title = TextResolver.GetText("Military Refueling Blocked with Us");
                        break;
                    case EmpireMessageType.MiningRightsAllowed:
                        empireMessage_1.Title = TextResolver.GetText("Mining Rights Allowed with Us");
                        break;
                    case EmpireMessageType.MiningRightsBlocked:
                        empireMessage_1.Title = TextResolver.GetText("Mining Rights Blocked with Us");
                        break;
                    case EmpireMessageType.CharacterSkillTraitChange:
                        empireMessage_1.Title = TextResolver.GetText("Character promotion");
                        break;
                    case EmpireMessageType.ResearchCriticalBreakthrough:
                        empireMessage_1.Title = TextResolver.GetText("Research Critical Breakthrough");
                        break;
                    case EmpireMessageType.ResearchCriticalFailure:
                        empireMessage_1.Title = TextResolver.GetText("Research Critical Failure");
                        break;
                    case EmpireMessageType.GalacticNewsNet:
                        empireMessage_1.Title = TextResolver.GetText("Galactic NewsNet");
                        break;
                    case EmpireMessageType.ShipBaseBoardedCaptured:
                        if (empireMessage_1.Subject != null && empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject6 = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject6.Role == BuiltObjectRole.Base)
                            {
                                empireMessage_1.Title = TextResolver.GetText("Enemy Base captured") + "!";
                            }
                            else
                            {
                                empireMessage_1.Title = TextResolver.GetText("Enemy Ship captured") + "!";
                            }
                        }
                        else
                        {
                            empireMessage_1.Title = TextResolver.GetText("Enemy Ship captured") + "!";
                        }
                        break;
                    case EmpireMessageType.ShipBaseBoardedLost:
                        if (empireMessage_1.Subject != null && empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject5 = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject5.Role == BuiltObjectRole.Base)
                            {
                                empireMessage_1.Title = TextResolver.GetText("Base boarded and lost") + "!";
                            }
                            else
                            {
                                empireMessage_1.Title = TextResolver.GetText("Ship boarded and lost") + "!";
                            }
                        }
                        else
                        {
                            empireMessage_1.Title = TextResolver.GetText("Ship boarded and lost") + "!";
                        }
                        break;
                    case EmpireMessageType.PirateAttackMissionAvailable:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Attack Mission Available Title");
                        break;
                    case EmpireMessageType.PirateAttackMissionCompleted:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Attack Mission Completed Title");
                        break;
                    case EmpireMessageType.PirateAttackMissionFailed:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Attack Mission Failed Title");
                        break;
                    case EmpireMessageType.PirateDefendMissionFailed:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Defend Mission Failed Title");
                        break;
                    case EmpireMessageType.PirateDefendMissionAvailable:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Defend Mission Available Title");
                        break;
                    case EmpireMessageType.PirateDefendMissionCompleted:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Defend Mission Completed Title");
                        break;
                    case EmpireMessageType.PirateSmugglingMissionAvailable:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Smuggle Mission Available Title");
                        break;
                    case EmpireMessageType.PirateSmugglingMissionCompleted:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Smuggle Mission Completed Title");
                        break;
                    case EmpireMessageType.PirateSmugglerDetected:
                        empireMessage_1.Title = TextResolver.GetText("Pirate Smuggler Detected") + "!";
                        break;
                    case EmpireMessageType.PlanetaryFacilityDestroyed:
                        empireMessage_1.Title = TextResolver.GetText("Planetary Facility destroyed");
                        break;
                    case EmpireMessageType.ShipBaseScrapped:
                        if (empireMessage_1.Subject != null && empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject2.Role == BuiltObjectRole.Base)
                            {
                                empireMessage_1.Title = TextResolver.GetText("Captured Base Scrapped") + "!";
                            }
                            else
                            {
                                empireMessage_1.Title = TextResolver.GetText("Captured Ship Scrapped") + "!";
                            }
                        }
                        else
                        {
                            empireMessage_1.Title = TextResolver.GetText("Captured Ship Scrapped") + "!";
                        }
                        break;
                    case EmpireMessageType.ConstructionResourceShortage:
                        if (empireMessage_1.Subject != null && empireMessage_1.Subject is BuiltObject)
                        {
                            BuiltObject builtObject = (BuiltObject)empireMessage_1.Subject;
                            if (builtObject != null)
                            {
                                empireMessage_1.Title = string.Format(TextResolver.GetText("Construction Resource Shortage at X"), builtObject.Name);
                            }
                        }
                        else if (empireMessage_1.Subject != null && empireMessage_1.Subject is Habitat)
                        {
                            Habitat habitat = (Habitat)empireMessage_1.Subject;
                            if (habitat != null)
                            {
                                empireMessage_1.Title = string.Format(TextResolver.GetText("Construction Resource Shortage at X"), habitat.Name);
                            }
                        }
                        break;
                    case EmpireMessageType.RaidBonuses:
                        empireMessage_1.Title = TextResolver.GetText("Raid Bonuses Title");
                        break;
                    case EmpireMessageType.RaidVictim:
                        empireMessage_1.Title = TextResolver.GetText("Raid Victim Title");
                        break;
                    case EmpireMessageType.PlanetaryFacilityDamaged:
                        empireMessage_1.Title = TextResolver.GetText("Planetary Facility damaged");
                        break;
                }
            }
            bool flag = true;
            EmpireMessageType messageType = empireMessage_1.MessageType;
            if (messageType == EmpireMessageType.Informational)
            {
                flag = false;
            }
            if (flag)
            {
                _Game.PlayerEmpire.AddHistoryMessage(empireMessage_1);
            }
        }

        private string method_252(EmpireMessage empireMessage_1, bool bool_28, bool bool_29, ref bool bool_30, ref bool bool_31)
        {
            string result = method_250(empireMessage_1);
            if (bool_28)
            {
                bool_30 = true;
            }
            if (bool_29)
            {
                bool_31 = true;
            }
            return result;
        }

        public void ReceiveMessage(EmpireMessage message)
        {
            if (delegate5_0 == null)
            {
                delegate5_0 = ReceiveMessageInternal;
            }
            BeginInvoke(delegate5_0, message);
        }

        private void method_253()
        {
            if (conversationOptionList_0 == null || conversationOptionList_0.Count <= 0)
            {
                return;
            }
            lock (object_5)
            {
                if (!bool_21)
                {
                    ConversationOption conversationOption = conversationOptionList_0[0];
                    conversationOptionList_0.RemoveAt(0);
                    method_0(effectsPlayer_0.ResolveImportantMessage());
                    method_154();
                    bool_11 = true;
                    method_296(conversationOption.Initiator, conversationOption);
                }
            }
        }

        private void method_254(ConversationOption conversationOption_0)
        {
            lock (object_5)
            {
                conversationOptionList_0.Add(conversationOption_0);
            }
        }

        public void ReceiveMessageInternal(EmpireMessage message)
        {
            string empty = string.Empty;
            bool bool_ = false;
            bool bool_2 = false;
            ConversationOption conversationOption = null;
            //DiplomaticRelationType diplomaticRelationType = DiplomaticRelationType.NotMet;
            empty = method_250(message);
            switch (message.MessageType)
            {
                default:
                    empty = message.Description;
                    bool_2 = true;
                    break;
                case EmpireMessageType.DiplomaticRelationChange:
                    empty = method_250(message);
                    //diplomaticRelationType = DiplomaticRelationType.NotMet;
                    if (message.Subject is DiplomaticRelationType)
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        switch ((DiplomaticRelationType)message.Subject)
                        {
                            case DiplomaticRelationType.None:
                                {
                                    DiplomaticRelation diplomaticRelation3 = _Game.PlayerEmpire.DiplomaticRelations[message.Sender];
                                    if (diplomaticRelation3 != null)
                                    {
                                        switch (diplomaticRelation3.Type)
                                        {
                                            default:
                                                if (message.Hint.ToLower(CultureInfo.InvariantCulture).Contains("trade sanctions"))
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_LIFT, message.Description, null, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                }
                                                else if (message.Hint.ToLower(CultureInfo.InvariantCulture).Contains("war"))
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.WAR_END, message.Description, null, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                }
                                                else if (message.Hint.ToLower(CultureInfo.InvariantCulture).Contains("free trade"))
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.CANCELTREATY, message.Description, DiplomaticRelationType.FreeTradeAgreement, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                }
                                                else if (message.Hint.ToLower(CultureInfo.InvariantCulture).Contains("mutual defense"))
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.CANCELTREATY, message.Description, DiplomaticRelationType.MutualDefensePact, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                }
                                                else if (message.Hint.ToLower(CultureInfo.InvariantCulture).Contains("protectorate"))
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.CANCELTREATY, message.Description, DiplomaticRelationType.Protectorate, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                }
                                                else if (message.Hint.ToLower(CultureInfo.InvariantCulture).Contains("subjugated"))
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.SUBJUGATION_RELEASE, message.Description, DiplomaticRelationType.SubjugatedDominion, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                }
                                                else
                                                {
                                                    conversationOption = new ConversationOption(DialogPartType.CANCELTREATY, message.Description, diplomaticRelation3.Type, 0.0, message.Sender);
                                                    empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                                }
                                                break;
                                            case DiplomaticRelationType.TradeSanctions:
                                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_LIFT, message.Description, message.Hint, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.War:
                                                conversationOption = new ConversationOption(DialogPartType.WAR_END, message.Description, message.Hint, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.Truce:
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                    }
                                    break;
                                }
                            case DiplomaticRelationType.FreeTradeAgreement:
                                conversationOption = new ConversationOption(DialogPartType.FREETRADE_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.MutualDefensePact:
                                conversationOption = new ConversationOption(DialogPartType.MUTUALDEFENSE_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.SubjugatedDominion:
                                conversationOption = new ConversationOption(DialogPartType.SUBJUGATIONDEMAND_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Protectorate:
                                conversationOption = new ConversationOption(DialogPartType.PROTECTORATE_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_IMPOSE, message.Description, message.Hint, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.War:
                                conversationOption = new ConversationOption(DialogPartType.WAR_DECLARE, message.Description, message.Hint, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Truce:
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                        }
                    }
                    else
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //DiplomaticRelationType diplomaticRelationType6 = DiplomaticRelationType.NotMet;
                    }
                    break;
                case EmpireMessageType.ProposeDiplomaticRelation:
                    empty = method_250(message);
                    //diplomaticRelationType = DiplomaticRelationType.NotMet;
                    if (message.Subject is DiplomaticRelationType)
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        switch ((DiplomaticRelationType)message.Subject)
                        {
                            case DiplomaticRelationType.None:
                                {
                                    DiplomaticRelation diplomaticRelation2 = _Game.PlayerEmpire.DiplomaticRelations[message.Sender];
                                    if (diplomaticRelation2 != null)
                                    {
                                        switch (diplomaticRelation2.Type)
                                        {
                                            case DiplomaticRelationType.SubjugatedDominion:
                                                conversationOption = ((diplomaticRelation2.Initiator != _Game.PlayerEmpire) ? new ConversationOption(DialogPartType.SUBJUGATION_RELEASE, message.Description, null, 0.0, message.Sender) : new ConversationOption(DialogPartType.SUBJUGATION_REQUESTRELEASE, message.Description, null, 0.0, message.Sender));
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            default:
                                                conversationOption = new ConversationOption(DialogPartType.CANCELTREATY, message.Description, diplomaticRelation2.Type, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.TradeSanctions:
                                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_LIFT, message.Description, null, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.War:
                                                conversationOption = new ConversationOption(DialogPartType.WAR_END, message.Description, null, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.Truce:
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                    }
                                    break;
                                }
                            case DiplomaticRelationType.FreeTradeAgreement:
                                conversationOption = new ConversationOption(DialogPartType.OFFER_FREETRADE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.MutualDefensePact:
                                conversationOption = new ConversationOption(DialogPartType.OFFER_MUTUALDEFENSE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.SubjugatedDominion:
                                conversationOption = new ConversationOption(DialogPartType.WAR_END_SUBJUGATIONDEMAND, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Protectorate:
                                conversationOption = new ConversationOption(DialogPartType.OFFER_PROTECTORATE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_IMPOSE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.War:
                                conversationOption = new ConversationOption(DialogPartType.WAR_DECLARE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Truce:
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                        }
                    }
                    else
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //DiplomaticRelationType diplomaticRelationType3 = DiplomaticRelationType.NotMet;
                    }
                    break;
                case EmpireMessageType.AcceptDiplomaticRelation:
                    empty = method_250(message);
                    //diplomaticRelationType = DiplomaticRelationType.NotMet;
                    if (message.Subject is DiplomaticRelationType)
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        switch ((DiplomaticRelationType)message.Subject)
                        {
                            case DiplomaticRelationType.None:
                                {
                                    _ = _Game.PlayerEmpire.DiplomaticRelations[message.Sender];
                                    DiplomaticRelationType diplomaticRelationType4 = DiplomaticRelationType.War;
                                    if (message.Description.ToLower(CultureInfo.InvariantCulture).Contains(TextResolver.GetText("Subjugation").ToLower(CultureInfo.InvariantCulture)))
                                    {
                                        diplomaticRelationType4 = DiplomaticRelationType.SubjugatedDominion;
                                    }
                                    switch (diplomaticRelationType4)
                                    {
                                        case DiplomaticRelationType.War:
                                            conversationOption = new ConversationOption(DialogPartType.WAR_END_ACCEPT, message.Description, null, 0.0, message.Sender);
                                            empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                            break;
                                        case DiplomaticRelationType.SubjugatedDominion:
                                            conversationOption = new ConversationOption(DialogPartType.SUBJUGATION_RELEASE, message.Description, null, 0.0, message.Sender);
                                            empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                            break;
                                    }
                                    break;
                                }
                            case DiplomaticRelationType.FreeTradeAgreement:
                                conversationOption = new ConversationOption(DialogPartType.FREETRADE_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.MutualDefensePact:
                                conversationOption = new ConversationOption(DialogPartType.MUTUALDEFENSE_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.SubjugatedDominion:
                                conversationOption = new ConversationOption(DialogPartType.SUBJUGATIONDEMAND_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Protectorate:
                                conversationOption = new ConversationOption(DialogPartType.PROTECTORATE_ACCEPT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_IMPOSE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.War:
                                conversationOption = new ConversationOption(DialogPartType.WAR_DECLARE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Truce:
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                        }
                    }
                    else
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //DiplomaticRelationType diplomaticRelationType5 = DiplomaticRelationType.NotMet;
                    }
                    break;
                case EmpireMessageType.RefuseDiplomaticRelation:
                    empty = method_250(message);
                    //diplomaticRelationType = DiplomaticRelationType.NotMet;
                    if (message.Subject is DiplomaticRelationType)
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        switch ((DiplomaticRelationType)message.Subject)
                        {
                            case DiplomaticRelationType.None:
                                {
                                    DiplomaticRelation diplomaticRelation = _Game.PlayerEmpire.DiplomaticRelations[message.Sender];
                                    if (diplomaticRelation != null)
                                    {
                                        switch (diplomaticRelation.Type)
                                        {
                                            case DiplomaticRelationType.SubjugatedDominion:
                                                conversationOption = new ConversationOption(DialogPartType.SUBJUGATION_REFUSERELEASE, message.Description, null, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            default:
                                                conversationOption = new ConversationOption(DialogPartType.CANCELTREATY, message.Description, diplomaticRelation.Type, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.TradeSanctions:
                                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_LIFT, message.Description, null, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.War:
                                                conversationOption = new ConversationOption(DialogPartType.WAR_END_REJECT, message.Description, null, 0.0, message.Sender);
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                            case DiplomaticRelationType.Truce:
                                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                    }
                                    break;
                                }
                            case DiplomaticRelationType.FreeTradeAgreement:
                                conversationOption = new ConversationOption(DialogPartType.FREETRADE_REJECT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.MutualDefensePact:
                                conversationOption = new ConversationOption(DialogPartType.MUTUALDEFENSE_REJECT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.SubjugatedDominion:
                                conversationOption = new ConversationOption(DialogPartType.SUBJUGATIONDEMAND_REJECT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Protectorate:
                                conversationOption = new ConversationOption(DialogPartType.PROTECTORATE_REJECT, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.TradeSanctions:
                                conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_IMPOSE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.War:
                                conversationOption = new ConversationOption(DialogPartType.WAR_DECLARE, message.Description, null, 0.0, message.Sender);
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                            case DiplomaticRelationType.Truce:
                                empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                                break;
                        }
                    }
                    else
                    {
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //diplomaticRelationType = DiplomaticRelationType.NotMet;
                        //DiplomaticRelationType diplomaticRelationType2 = DiplomaticRelationType.NotMet;
                    }
                    break;
                case EmpireMessageType.StopMissionsAgainstUs:
                    conversationOption = new ConversationOption(DialogPartType.WARNING_INTELLIGENCEMISSIONS, message.Description, null, 0.0, message.Sender);
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.StopAttacks:
                    conversationOption = new ConversationOption(DialogPartType.WARNING_ATTACKS, message.Description, null, 0.0, message.Sender);
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RemoveColoniesFromSystem:
                case EmpireMessageType.LeaveSystem:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RequestJointWar:
                    if (_Game.PlayerEmpire.ControlDiplomacyOffense != AutomationLevel.FullyAutomated)
                    {
                        conversationOption = new ConversationOption(DialogPartType.WAR_DECLARE_REQUESTJOINT, message.Description, message.Subject, 0.0, message.Sender);
                    }
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RequestJointTradeSanctions:
                    if (_Game.PlayerEmpire.ControlDiplomacyOffense != AutomationLevel.FullyAutomated)
                    {
                        conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_REQUESTIMPOSEJOINT, message.Description, message.Subject, 0.0, message.Sender);
                    }
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RequestStopWar:
                    if (_Game.PlayerEmpire.ControlDiplomacyOffense != AutomationLevel.FullyAutomated)
                    {
                        conversationOption = new ConversationOption(DialogPartType.WAR_END_REQUESTOTHER, message.Description, message.Subject, 0.0, message.Sender);
                    }
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RequestLiftTradeSanctions:
                    if (_Game.PlayerEmpire.ControlDiplomacyOffense != AutomationLevel.FullyAutomated)
                    {
                        conversationOption = new ConversationOption(DialogPartType.TRADESANCTIONS_REQUESTLIFTOTHER, message.Description, message.Subject, 0.0, message.Sender);
                    }
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.GiveGift:
                    {
                        conversationOption = new ConversationOption(DialogPartType.GIFT_GIVE, message.Description, null, message.Money, message.Sender);
                        empty = method_252(message, _Game.DisplayPopupDiplomacyGift, _Game.DisplayMessageDiplomacyGift, ref bool_, ref bool_2);
                        double num3 = _Game.PlayerEmpire.ValueMoneyGiftFromEmpire(message.Sender, conversationOption.Cost);
                        EmpireEvaluation empireEvaluation = _Game.PlayerEmpire.ObtainEmpireEvaluation(message.Sender);
                        empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + num3;
                        message.Sender.CivilityRating += num3 * 0.1;
                        message.Sender.StateMoney -= conversationOption.Cost;
                        message.Sender.PirateEconomy.PerformExpense(conversationOption.Cost, PirateExpenseType.Undefined, _Game.Galaxy.CurrentStarDate);
                        _Game.PlayerEmpire.StateMoney += conversationOption.Cost;
                        _Game.PlayerEmpire.PirateEconomy.PerformIncome(conversationOption.Cost, PirateIncomeType.Undefined, _Game.Galaxy.CurrentStarDate);
                        DiplomaticRelation diplomaticRelation4 = message.Sender.ObtainDiplomaticRelation(_Game.PlayerEmpire);
                        diplomaticRelation4.LastGiftDate = _Game.Galaxy.CurrentStarDate;
                        break;
                    }
                case EmpireMessageType.Informational:
                    empty = method_250(message);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.ShipBasePurchased:
                    return;
                case EmpireMessageType.NewColony:
                case EmpireMessageType.NewColonyFailed:
                    empty = method_252(message, _Game.DisplayPopupNewColony, _Game.DisplayMessageNewColony, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.BattleAttacking:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.IncomingEnemyFleet:
                    empty = method_252(message, _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases, _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.EmpireDiscovered:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyEmpireMetDestroyed, _Game.DisplayMessageDiplomacyEmpireMetDestroyed, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.EmpireDefeated:
                    {
                        empty = method_252(message, _Game.DisplayPopupDiplomacyEmpireMetDestroyed, _Game.DisplayMessageDiplomacyEmpireMetDestroyed, ref bool_, ref bool_2);
                        if (!(message.Subject is Empire))
                        {
                            break;
                        }
                        Empire empire2 = (Empire)message.Subject;
                        if (empire2 != _Game.PlayerEmpire)
                        {
                            break;
                        }
                        Empire victorEmpire = null;
                        int num2 = 0;
                        for (int j = 0; j < _Game.Galaxy.Empires.Count; j++)
                        {
                            Empire empire3 = _Game.Galaxy.Empires[j];
                            int totalColonyStrategicValue = empire3.TotalColonyStrategicValue;
                            if (totalColonyStrategicValue > num2)
                            {
                                victorEmpire = empire3;
                                num2 = totalColonyStrategicValue;
                            }
                        }
                        Galaxy_GameEnd(null, new GameEndEventArgs(victorEmpire, GameEndOutcome.Defeat, TextResolver.GetText("Your empire has been completely wiped out!"), 0));
                        break;
                    }
                case EmpireMessageType.RequestHonorMutualDefense:
                    conversationOption = new ConversationOption(DialogPartType.MUTUALDEFENSE_REQUESTHELP, message.Description, message.Subject, 0.0, message.Sender);
                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                    if (message.Subject is Empire)
                    {
                        Empire empire = (empire_2 = (Empire)message.Subject);
                    }
                    break;
                case EmpireMessageType.BlockadeInitiated:
                case EmpireMessageType.BlockadeCancelled:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyWarTradeSanctions, _Game.DisplayMessageDiplomacyWarTradeSanctions, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ExplorationRuins:
                case EmpireMessageType.ExplorationBuiltObject:
                case EmpireMessageType.ExplorationHabitat:
                case EmpireMessageType.ExplorationLocation:
                case EmpireMessageType.GalacticHistory:
                    empty = method_252(message, _Game.DisplayPopupExploration, _Game.DisplayMessageExploration, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.SellInfoUnmetEmpire:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_UNMETEMPIRE, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.SellInfoIndependentColony:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_INDEPENDENTCOLONY, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.SellInfoSystemMap:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_SYSTEMMAPS, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.SellInfoRuins:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_RUINS, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.SellInfoDebrisField:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_DEBRISFIELD, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.SellInfoRestrictedArea:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_RESTRICTEDAREA, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.SellInfoPlanetDestroyer:
                    conversationOption = new ConversationOption(DialogPartType.INFO_OFFER_PLANETDESTROYER, message.Description, message.Subject, message.Money, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.PirateOfferProtection:
                    {
                        double num4 = message.Sender.CalculatePirateProtectionPricePerMonth(_Game.PlayerEmpire);
                        if (!string.IsNullOrEmpty(message.Hint) && message.Hint.ToLower(CultureInfo.InvariantCulture) == "extort")
                        {
                            message.Description = string.Format(message.Description, num4.ToString("###,##0"));
                            empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                            conversationOption = new ConversationOption(DialogPartType.PIRATE_EXTORTPROTECTION, message.Description, null, num4, message.Sender);
                            bool_ = true;
                        }
                        else
                        {
                            conversationOption = ((!(num4 <= 0.0)) ? new ConversationOption(DialogPartType.PIRATE_PROTECTIONPROPOSEINITIATE, message.Description, null, num4, message.Sender) : new ConversationOption(DialogPartType.PIRATE_TRUCEPROPOSEINITIATE, message.Description, null, num4, message.Sender));
                            bool_ = false;
                        }
                        bool_2 = true;
                        break;
                    }
                case EmpireMessageType.CancelPirateProtection:
                    conversationOption = ((message.Sender == null || message.Sender.PirateEmpireBaseHabitat == null) ? new ConversationOption(DialogPartType.CANCELPIRATEPROTECTION, message.Description, null, 0.0, message.Sender) : new ConversationOption(DialogPartType.CANCELPIRATEPROTECTIONPIRATE, message.Description, null, 0.0, message.Sender));
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.Revolution:
                    empty = method_252(message, _Game.DisplayPopupColonyInvaded, _Game.DisplayMessageColonyInvaded, ref bool_, ref bool_2);
                    empty = message.Description;
                    break;
                case EmpireMessageType.RestrictedResourceDiscovered:
                    empty = method_252(message, _Game.DisplayPopupExploration, _Game.DisplayMessageExploration, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RestrictedResourceTradingAllowed:
                case EmpireMessageType.RestrictedResourceTradingBlocked:
                    {
                        string text = string.Empty;
                        int num = 0;
                        ResourceList resourceList = message.Sender.DetermineResourcesEmpireSupplies();
                        for (int i = 0; i < _Game.Galaxy.ResourceSystem.SuperLuxuryResources.Count; i++)
                        {
                            ResourceDefinition resourceDefinition = _Game.Galaxy.ResourceSystem.SuperLuxuryResources[i];
                            if (resourceDefinition != null && resourceList.Contains(new Resource(resourceDefinition.ResourceID)))
                            {
                                text = text + resourceDefinition.Name + ", ";
                                message.Subject = new Resource(resourceDefinition.ResourceID);
                                num++;
                            }
                        }
                        if (text.Length > 0)
                        {
                            text = text.Substring(0, text.Length - 2);
                        }
                        string text2 = string.Empty;
                        if (message.MessageType == EmpireMessageType.RestrictedResourceTradingAllowed)
                        {
                            text2 += string.Format(TextResolver.GetText("We have agreed to trade the rare resource X"), text);
                        }
                        else if (message.MessageType == EmpireMessageType.RestrictedResourceTradingBlocked)
                        {
                            text2 += string.Format(TextResolver.GetText("We have terminated trade of the rare resource X"), text);
                        }
                        message.Description = text2;
                        empty = method_252(message, _Game.DisplayPopupExploration, _Game.DisplayMessageExploration, ref bool_, ref bool_2);
                        break;
                    }
                case EmpireMessageType.OfferTrade:
                    if (message.Subject is object[])
                    {
                        object[] array = (object[])message.Subject;
                        TradeableItemList tradeableItemList = (TradeableItemList)array[0];
                        TradeableItemList tradeableItemList2 = (TradeableItemList)array[1];
                        DialogPartType dialogPartType = DialogPartType.DEAL_OFFER;
                        dialogPartType = ((tradeableItemList2.ContainsType(TradeableItemType.ThreatenWar) || tradeableItemList2.ContainsType(TradeableItemType.ThreatenTradeSanctions)) ? DialogPartType.DEAL_THREAT : ((tradeableItemList.Count != 0) ? DialogPartType.DEAL_OFFER : DialogPartType.DEAL_DEMAND));
                        conversationOption = new ConversationOption(dialogPartType, message.Description, array, 0.0, message.Sender);
                        bool_ = false;
                        bool_2 = true;
                    }
                    else if (message.Subject is TradeableItem)
                    {
                        TradeableItem tradeableItem = (TradeableItem)message.Subject;
                        DialogPartType type = DialogPartType.OFFER_DEAL_TERRITORYMAP;
                        switch (tradeableItem.Type)
                        {
                            case TradeableItemType.ResearchProject:
                                type = DialogPartType.OFFER_DEAL_COMPONENT;
                                break;
                            case TradeableItemType.TerritoryMap:
                                type = DialogPartType.OFFER_DEAL_TERRITORYMAP;
                                break;
                            case TradeableItemType.GalaxyMap:
                                type = DialogPartType.OFFER_DEAL_GALAXYMAP;
                                break;
                        }
                        conversationOption = new ConversationOption(type, message.Description, tradeableItem, tradeableItem.Value, message.Sender);
                        bool_ = false;
                        bool_2 = true;
                    }
                    break;
                case EmpireMessageType.ShipMissionComplete:
                    empty = method_252(message, _Game.DisplayPopupShipMissionComplete, _Game.DisplayMessageShipMissionComplete, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ShipNeedsRefuelling:
                case EmpireMessageType.ShipNeedsRepair:
                    empty = method_252(message, _Game.DisplayPopupShipNeedsRefuelling, _Game.DisplayMessageShipNeedsRefuelling, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RemoveForcesFromSystem:
                    {
                        empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                        Habitat relatedInfo = (Habitat)message.Subject;
                        conversationOption = new ConversationOption(DialogPartType.WARNING_REMOVEFORCESSYSTEM, message.Description, relatedInfo, 0.0, message.Sender);
                        bool_ = false;
                        bool_2 = true;
                        break;
                    }
                case EmpireMessageType.GeneralWarning:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    conversationOption = new ConversationOption(DialogPartType.WARNING_GENERAL, message.Description, null, 0.0, message.Sender);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.GeneralBadEvent:
                case EmpireMessageType.GeneralNeutralEvent:
                case EmpireMessageType.GeneralGoodEvent:
                case EmpireMessageType.GeneralDecision:
                    empty = method_252(message, _Game.DisplayPopupExploration, _Game.DisplayMessageExploration, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.HistoryOfferLocationHint:
                    conversationOption = new ConversationOption(DialogPartType.HISTORY_OFFER_LOCATIONHINT, message.Description, null, 0.0, message.Sender);
                    empty = method_250(message);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.HistoryOfferStoryClue:
                    conversationOption = new ConversationOption(DialogPartType.HISTORY_OFFER_STORYCLUE, message.Description, null, 0.0, message.Sender);
                    empty = method_250(message);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.ColonyFacilityCompleted:
                case EmpireMessageType.ColonyFacilityCancelled:
                case EmpireMessageType.ColonyWonderBegun:
                    empty = method_250(message);
                    empty = method_252(message, _Game.DisplayPopupNewColony, _Game.DisplayMessageNewColony, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ColonyShipMissionCancelled:
                    empty = method_252(message, _Game.DisplayPopupNewColony, _Game.DisplayMessageNewColony, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.StoryMessage:
                    conversationOption = new ConversationOption(DialogPartType.HISTORY_OFFER_STORYMESSAGE, message.Description, null, 0.0, message.Sender);
                    empty = method_250(message);
                    bool_ = false;
                    bool_2 = true;
                    break;
                case EmpireMessageType.AdvisorSuggestion:
                    diplomaticMessageQueue_0.AddMessage(message, new ConversationOption(DialogPartType.Undefined, string.Empty, message.Sender));
                    diplomaticMessageQueue_0.ExpireInvalidMessages(message);
                    bool_ = false;
                    bool_2 = false;
                    break;
                case EmpireMessageType.MilitaryRefuelingAllowed:
                case EmpireMessageType.MilitaryRefuelingBlocked:
                case EmpireMessageType.MiningRightsAllowed:
                case EmpireMessageType.MiningRightsBlocked:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.CharacterAppearance:
                case EmpireMessageType.CharacterDeath:
                case EmpireMessageType.CharacterMissionAccomplished:
                case EmpireMessageType.CharacterMissionFailure:
                case EmpireMessageType.CharacterSkillTraitChange:
                    empty = method_252(message, _Game.DisplayPopupIntelligenceMissions, _Game.DisplayMessageIntelligenceMissions, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ResearchBreakthrough:
                case EmpireMessageType.ResearchCriticalBreakthrough:
                case EmpireMessageType.ResearchCriticalFailure:
                    empty = method_252(message, _Game.DisplayPopupResearchNewComponent, _Game.DisplayMessageResearchNewComponent, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.GalacticNewsNet:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyTreaty, _Game.DisplayMessageDiplomacyTreaty, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.BattleUnderAttack:
                case EmpireMessageType.ShipBaseBoardedCaptured:
                case EmpireMessageType.ShipBaseBoardedLost:
                    if (message.Subject is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)message.Subject;
                        switch (builtObject.SubRole)
                        {
                            case BuiltObjectSubRole.Escort:
                            case BuiltObjectSubRole.Frigate:
                            case BuiltObjectSubRole.Destroyer:
                            case BuiltObjectSubRole.Cruiser:
                            case BuiltObjectSubRole.CapitalShip:
                            case BuiltObjectSubRole.TroopTransport:
                            case BuiltObjectSubRole.Carrier:
                            case BuiltObjectSubRole.ResupplyShip:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackMilitaryShips, _Game.DisplayMessageUnderAttackMilitaryShips, ref bool_, ref bool_2);
                                break;
                            case BuiltObjectSubRole.ExplorationShip:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackExplorationShips, _Game.DisplayMessageUnderAttackExplorationShips, ref bool_, ref bool_2);
                                break;
                            case BuiltObjectSubRole.ColonyShip:
                            case BuiltObjectSubRole.ConstructionShip:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackColonyConstructionShips, _Game.DisplayMessageUnderAttackColonyConstructionShips, ref bool_, ref bool_2);
                                break;
                            case BuiltObjectSubRole.SmallFreighter:
                            case BuiltObjectSubRole.MediumFreighter:
                            case BuiltObjectSubRole.LargeFreighter:
                            case BuiltObjectSubRole.PassengerShip:
                            case BuiltObjectSubRole.GasMiningShip:
                            case BuiltObjectSubRole.MiningShip:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackCivilianShips, _Game.DisplayMessageUnderAttackCivilianShips, ref bool_, ref bool_2);
                                break;
                            case BuiltObjectSubRole.GasMiningStation:
                            case BuiltObjectSubRole.MiningStation:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackCivilianBases, _Game.DisplayMessageUnderAttackCivilianBases, ref bool_, ref bool_2);
                                break;
                            case BuiltObjectSubRole.ResortBase:
                            case BuiltObjectSubRole.GenericBase:
                            case BuiltObjectSubRole.EnergyResearchStation:
                            case BuiltObjectSubRole.WeaponsResearchStation:
                            case BuiltObjectSubRole.HighTechResearchStation:
                            case BuiltObjectSubRole.MonitoringStation:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackOtherStateBases, _Game.DisplayMessageUnderAttackOtherStateBases, ref bool_, ref bool_2);
                                break;
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.DefensiveBase:
                                empty = method_252(message, _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases, _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases, ref bool_, ref bool_2);
                                break;
                        }
                    }
                    else if (message.Subject is Habitat)
                    {
                        empty = method_252(message, _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases, _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases, ref bool_, ref bool_2);
                    }
                    break;
                case EmpireMessageType.PirateAttackMissionAvailable:
                case EmpireMessageType.PirateAttackMissionCompleted:
                case EmpireMessageType.PirateAttackMissionFailed:
                case EmpireMessageType.PirateDefendMissionFailed:
                case EmpireMessageType.PirateDefendMissionAvailable:
                case EmpireMessageType.PirateDefendMissionCompleted:
                case EmpireMessageType.PirateSmugglingMissionAvailable:
                case EmpireMessageType.PirateSmugglingMissionCompleted:
                    empty = method_252(message, _Game.DisplayPopupDiplomacyRequestWarning, _Game.DisplayMessageDiplomacyRequestWarning, ref bool_, ref bool_2);
                    method_76(itemListCollectionPanel_0.ActivePanel);
                    break;
                case EmpireMessageType.PirateSmugglerDetected:
                    empty = method_252(message, _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases, _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ShipBaseCompleted:
                case EmpireMessageType.ShipBaseScrapped:
                    empty = method_250(message);
                    empty = method_252(message, _Game.DisplayPopupBuiltObjectBuilt, _Game.DisplayMessageBuiltObjectBuilt, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ConstructionResourceShortage:
                    empty = method_250(message);
                    empty = method_252(message, _Game.DisplayPopupConstructionResourceShortage, _Game.DisplayMessageConstructionResourceShortage, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.RaidBonuses:
                case EmpireMessageType.RaidVictim:
                    empty = method_250(message);
                    empty = method_252(message, _Game.DisplayPopupColonyInvaded, _Game.DisplayMessageColonyInvaded, ref bool_, ref bool_2);
                    break;
                case EmpireMessageType.ColonyGained:
                case EmpireMessageType.ColonyLost:
                case EmpireMessageType.ColonyDefended:
                case EmpireMessageType.ColonyRebelling:
                case EmpireMessageType.ColonyDestroyed:
                case EmpireMessageType.PlanetaryFacilityDestroyed:
                case EmpireMessageType.PlanetaryFacilityDamaged:
                    empty = method_252(message, _Game.DisplayPopupColonyInvaded, _Game.DisplayMessageColonyInvaded, ref bool_, ref bool_2);
                    break;
            }
            bool flag = true;
            if (gameOptions_0 != null && gameOptions_0.SuppressAllPopups)
            {
                flag = false;
            }
            if (flag && ((bool_ && !message.SupressPopup) || bool_2))
            {
                method_0(effectsPlayer_0.ResolveMessage(message.MessageType));
            }
            if (conversationOption != null)
            {
                method_0(effectsPlayer_0.ResolveImportantMessage());
                message.StarDate = _Game.Galaxy.CurrentStarDate;
                switch (conversationOption.Type)
                {
                    default:
                        diplomaticMessageQueue_0.AddMessage(message, conversationOption);
                        diplomaticMessageQueue_0.ExpireInvalidMessages(message);
                        break;
                    case DialogPartType.DEAL_DEMAND:
                    case DialogPartType.DEAL_THREAT:
                    case DialogPartType.MUTUALDEFENSE_REQUESTHELP:
                    case DialogPartType.WAR_DECLARE:
                    case DialogPartType.WAR_END:
                    case DialogPartType.PIRATE_EXTORTPROTECTION:
                        if (flag)
                        {
                            method_254(conversationOption);
                        }
                        break;
                }
                bool_ = false;
                bool_2 = true;
            }
            if (bool_ && !message.SupressPopup)
            {
                if (timer_1.Enabled)
                {
                    timer_1.Stop();
                }
                if (pnlMessagePopup.Message != null)
                {
                    method_244(pnlMessagePopup.Message);
                }
                int_4 = 0;
                pnlMessagePopup.Size = new Size(335, 280);
                List<Bitmap> list = method_396();
                pnlMessagePopup.Ignite(_Game.Galaxy, characterImageCache_0, builtObjectImageCache_0, habitatImageCache_0, bitmap_28, bitmap_29, raceImageCache_0, bitmap_21, _uiResourcesBitmaps, bitmap_2, builtObjectImageCache_0.GetImagesSmall(), bitmap_6, bitmap_8, list.ToArray(), bitmap_73, _Game.PlayerEmpire, message, bitmap_55, bitmap_81, bitmap_82, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27);
                pnlMessagePopup.Location = new Point(mainView.ClientRectangle.Width, 0);
                pnlMessagePopup.Visible = true;
                pnlMessagePopup.BringToFront();
                timer_1.Interval = 100.0;
                timer_1.Start();
            }
            if (bool_2)
            {
                lstMessages.AddItem(empty, message);
                string newLine = Environment.NewLine;
                empty = empty.Replace("\n", newLine);
                message.StarDate = _Game.Galaxy.CurrentStarDate;
                message.Description = empty;
                method_251(message);
            }
            Focus();
        }

        private void timer_1_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer_1.Enabled = false;
            int_4 += 25;
            int num = int_4;
            int num2 = pnlMessagePopup.Width + 10;
            int num3 = num2 + 2000;
            int num4 = num3 + pnlMessagePopup.Width + 10;
            if (num > num2)
            {
                num = num2;
            }
            if (int_4 > num3)
            {
                num = -10;
            }
            int num5 = mainView.ClientRectangle.Width - num;
            int num6 = (mainView.ClientRectangle.Height - pnlMessagePopup.Height) / 2;
            if (SetMessagePopupPosition != null)
            {
                BeginInvoke(SetMessagePopupPosition, num5, num6);
            }
            if (int_4 >= num4)
            {
                timer_1.Stop();
                int_4 = 0;
                if (pnlMessagePopup.Message != null)
                {
                    method_244(pnlMessagePopup.Message);
                }
            }
            else
            {
                timer_1.Enabled = true;
            }
        }

        internal void method_255()
        {
            string path = GetGameFilesFolderCreateIfNeeded() + "GameSummaries";
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, gameSummaryList_0);
                }
                finally
                {
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        internal void method_256()
        {
            string path = GetGameFilesFolderCreateIfNeeded() + "GameSummaries";
            if (File.Exists(path))
            {
                try
                {
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    try
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        object obj = binaryFormatter.Deserialize(fileStream);
                        if (obj is GameSummaryList)
                        {
                            gameSummaryList_0 = (GameSummaryList)obj;
                        }
                        return;
                    }
                    catch (Exception)
                    {
                        gameSummaryList_0 = new GameSummaryList();
                        return;
                    }
                    finally
                    {
                        fileStream.Close();
                    }
                }
                catch (Exception)
                {
                    gameSummaryList_0 = new GameSummaryList();
                    return;
                }
            }
            gameSummaryList_0 = new GameSummaryList();
        }

        internal void method_257()
        {
            string path = GetGameFilesFolderCreateIfNeeded() + "defaultOptions";
            try
            {
                FileStream fileStream = new FileStream(path, FileMode.Create);
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, gameOptions_0);
                }
                finally
                {
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        internal void YxwyUefOyQ()
        {
            if (_Game != null)
            {
                gameOptions_0.AutoPauseWhenInPopupWindow = _Game.AutoPauseWhenInPopupWindow;
                if (_Game.PlayerEmpire != null)
                {
                    gameOptions_0.ControlAgentAssignmentDefault = _Game.PlayerEmpire.ControlAgentAssignment;
                    gameOptions_0.ControlAttacksOnEnemiesDefault = _Game.PlayerEmpire.ControlMilitaryAttacks;
                    gameOptions_0.ControlColonizationDefault = _Game.PlayerEmpire.ControlColonization;
                    gameOptions_0.ControlColonyTaxRatesDefault = _Game.PlayerEmpire.ControlColonyTaxRates;
                    gameOptions_0.ControlDiplomaticGiftsDefault = _Game.PlayerEmpire.ControlDiplomacyGifts;
                    gameOptions_0.ControlFleetFormationDefault = _Game.PlayerEmpire.ControlMilitaryFleets;
                    gameOptions_0.ControlShipBuildingDefault = _Game.PlayerEmpire.ControlStateConstruction;
                    gameOptions_0.ControlShipDesignDefault = _Game.PlayerEmpire.ControlDesigns;
                    gameOptions_0.ControlTreatyNegotiationDefault = _Game.PlayerEmpire.ControlDiplomacyTreaties;
                    gameOptions_0.ControlTroopRecruitmentDefault = _Game.PlayerEmpire.ControlTroopGeneration;
                    gameOptions_0.ControlCharacterLocationsDefault = _Game.PlayerEmpire.ControlCharacterLocations;
                    gameOptions_0.ControlWarTradeSanctionsDefault = _Game.PlayerEmpire.ControlDiplomacyOffense;
                    gameOptions_0.ControlResearchDefault = _Game.PlayerEmpire.ControlResearch;
                    gameOptions_0.ControlColonyFacilitiesDefault = _Game.PlayerEmpire.ControlColonyFacilities;
                    gameOptions_0.ControlPopulationPolicyDefault = _Game.PlayerEmpire.ControlPopulationPolicy;
                    gameOptions_0.ControlOfferPirateMissionsDefault = _Game.PlayerEmpire.ControlOfferPirateMissions;
                    gameOptions_0.AttackRangePatrol = _Game.PlayerEmpire.AttackRangePatrol;
                    gameOptions_0.AttackRangeEscort = _Game.PlayerEmpire.AttackRangeEscort;
                    gameOptions_0.AttackRangeAttack = _Game.PlayerEmpire.AttackRangeAttack;
                    gameOptions_0.AttackRangeOther = _Game.PlayerEmpire.AttackRangeOther;
                    gameOptions_0.AttackRangePatrolManual = _Game.PlayerEmpire.AttackRangePatrolManual;
                    gameOptions_0.AttackRangeEscortManual = _Game.PlayerEmpire.AttackRangeEscortManual;
                    gameOptions_0.AttackRangeAttackManual = _Game.PlayerEmpire.AttackRangeAttackManual;
                    gameOptions_0.AttackRangeOtherManual = _Game.PlayerEmpire.AttackRangeOtherManual;
                    gameOptions_0.AttackOverMatchFactor = _Game.PlayerEmpire.AttackOvermatchFactor;
                    gameOptions_0.FleetAttackRefuelPortion = _Game.PlayerEmpire.FleetAttackRefuelPortion;
                    gameOptions_0.FleetAttackGatherPortion = _Game.PlayerEmpire.FleetAttackGatherPortion;
                    gameOptions_0.DiscoveryActionRuin = _Game.PlayerEmpire.DiscoveryActionRuin;
                    gameOptions_0.DiscoveryActionAbandonedShipBase = _Game.PlayerEmpire.DiscoveryActionAbandonedShipBase;
                    gameOptions_0.NewShipsAutomated = _Game.PlayerEmpire.NewShipsAutomated;
                }
                gameOptions_0.DisplayMessageBuiltObjectBuilt = _Game.DisplayMessageBuiltObjectBuilt;
                gameOptions_0.DisplayMessageColonyInvaded = _Game.DisplayMessageColonyInvaded;
                gameOptions_0.DisplayMessageDiplomacyEmpireMetDestroyed = _Game.DisplayMessageDiplomacyEmpireMetDestroyed;
                gameOptions_0.DisplayMessageDiplomacyGift = _Game.DisplayMessageDiplomacyGift;
                gameOptions_0.DisplayMessageDiplomacyRequestWarning = _Game.DisplayMessageDiplomacyRequestWarning;
                gameOptions_0.DisplayMessageDiplomacyTreaty = _Game.DisplayMessageDiplomacyTreaty;
                gameOptions_0.DisplayMessageDiplomacyWarTradeSanctions = _Game.DisplayMessageDiplomacyWarTradeSanctions;
                gameOptions_0.DisplayMessageNewColony = _Game.DisplayMessageNewColony;
                gameOptions_0.DisplayMessageResearchNewComponent = _Game.DisplayMessageResearchNewComponent;
                gameOptions_0.DisplayMessageIntelligenceMissions = _Game.DisplayMessageIntelligenceMissions;
                gameOptions_0.DisplayMessageExploration = _Game.DisplayMessageExploration;
                gameOptions_0.DisplayMessageShipMissionComplete = _Game.DisplayMessageShipMissionComplete;
                gameOptions_0.DisplayMessageShipNeedsRefuelling = _Game.DisplayMessageShipNeedsRefuelling;
                gameOptions_0.DisplayMessageConstructionResourceShortage = _Game.DisplayMessageConstructionResourceShortage;
                gameOptions_0.DisplayPopupBuiltObjectBuilt = _Game.DisplayPopupBuiltObjectBuilt;
                gameOptions_0.DisplayPopupColonyInvaded = _Game.DisplayPopupColonyInvaded;
                gameOptions_0.DisplayPopupDiplomacyEmpireMetDestroyed = _Game.DisplayPopupDiplomacyEmpireMetDestroyed;
                gameOptions_0.DisplayPopupDiplomacyGift = _Game.DisplayPopupDiplomacyGift;
                gameOptions_0.DisplayPopupDiplomacyRequestWarning = _Game.DisplayPopupDiplomacyRequestWarning;
                gameOptions_0.DisplayPopupDiplomacyTreaty = _Game.DisplayPopupDiplomacyTreaty;
                gameOptions_0.DisplayPopupDiplomacyWarTradeSanctions = _Game.DisplayPopupDiplomacyWarTradeSanctions;
                gameOptions_0.DisplayPopupNewColony = _Game.DisplayPopupNewColony;
                gameOptions_0.DisplayPopupResearchNewComponent = _Game.DisplayPopupResearchNewComponent;
                gameOptions_0.DisplayPopupIntelligenceMissions = _Game.DisplayPopupIntelligenceMissions;
                gameOptions_0.DisplayPopupExploration = _Game.DisplayPopupExploration;
                gameOptions_0.DisplayPopupShipMissionComplete = _Game.DisplayPopupShipMissionComplete;
                gameOptions_0.DisplayPopupShipNeedsRefuelling = _Game.DisplayPopupShipNeedsRefuelling;
                gameOptions_0.DisplayPopupConstructionResourceShortage = _Game.DisplayPopupConstructionResourceShortage;
                gameOptions_0.DisplayMessageUnderAttackCivilianShips = _Game.DisplayMessageUnderAttackCivilianShips;
                gameOptions_0.DisplayMessageUnderAttackCivilianBases = _Game.DisplayMessageUnderAttackCivilianBases;
                gameOptions_0.DisplayMessageUnderAttackExplorationShips = _Game.DisplayMessageUnderAttackExplorationShips;
                gameOptions_0.DisplayMessageUnderAttackColonyConstructionShips = _Game.DisplayMessageUnderAttackColonyConstructionShips;
                gameOptions_0.DisplayMessageUnderAttackMilitaryShips = _Game.DisplayMessageUnderAttackMilitaryShips;
                gameOptions_0.DisplayMessageUnderAttackOtherStateBases = _Game.DisplayMessageUnderAttackOtherStateBases;
                gameOptions_0.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases = _Game.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases;
                gameOptions_0.DisplayPopupUnderAttackCivilianShips = _Game.DisplayPopupUnderAttackCivilianShips;
                gameOptions_0.DisplayPopupUnderAttackCivilianBases = _Game.DisplayPopupUnderAttackCivilianBases;
                gameOptions_0.DisplayPopupUnderAttackExplorationShips = _Game.DisplayPopupUnderAttackExplorationShips;
                gameOptions_0.DisplayPopupUnderAttackColonyConstructionShips = _Game.DisplayPopupUnderAttackColonyConstructionShips;
                gameOptions_0.DisplayPopupUnderAttackMilitaryShips = _Game.DisplayPopupUnderAttackMilitaryShips;
                gameOptions_0.DisplayPopupUnderAttackOtherStateBases = _Game.DisplayPopupUnderAttackOtherStateBases;
                gameOptions_0.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases = _Game.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases;
                gameOptions_0.MainViewScrollSpeed = _Game.MainViewScrollSpeed;
                gameOptions_0.MainViewZoomSpeed = _Game.MainViewZoomSpeed;
                gameOptions_0.MusicVolume = _Game.MusicVolume;
                gameOptions_0.SoundEffectsVolume = _Game.SoundEffectsVolume;
                gameOptions_0.StarFieldSize = _Game.StarFieldSize;
                gameOptions_0.ShowSystemNebulae = _Game.ShowSystemNebulae;
                gameOptions_0.MouseScrollWheelBehaviour = _Game.MouseScrollWheelBehaviour;
            }
        }

        internal void method_258(GameOptions gameOptions_1, Empire empire_5)
        {
            if (empire_5 != null)
            {
                empire_5.ControlAgentAssignment = gameOptions_0.ControlAgentAssignmentDefault;
                empire_5.ControlMilitaryAttacks = gameOptions_0.ControlAttacksOnEnemiesDefault;
                empire_5.ControlColonization = gameOptions_0.ControlColonizationDefault;
                empire_5.ControlColonyTaxRates = gameOptions_0.ControlColonyTaxRatesDefault;
                empire_5.ControlDiplomacyGifts = gameOptions_0.ControlDiplomaticGiftsDefault;
                empire_5.ControlMilitaryFleets = gameOptions_0.ControlFleetFormationDefault;
                empire_5.ControlStateConstruction = gameOptions_0.ControlShipBuildingDefault;
                empire_5.ControlDesigns = gameOptions_0.ControlShipDesignDefault;
                empire_5.ControlDiplomacyTreaties = gameOptions_0.ControlTreatyNegotiationDefault;
                empire_5.ControlTroopGeneration = gameOptions_0.ControlTroopRecruitmentDefault;
                empire_5.ControlCharacterLocations = gameOptions_0.ControlCharacterLocationsDefault;
                empire_5.ControlDiplomacyOffense = gameOptions_0.ControlWarTradeSanctionsDefault;
                empire_5.ControlResearch = gameOptions_0.ControlResearchDefault;
                empire_5.ControlColonyFacilities = gameOptions_0.ControlColonyFacilitiesDefault;
                empire_5.ControlPopulationPolicy = gameOptions_0.ControlPopulationPolicyDefault;
                empire_5.ControlOfferPirateMissions = gameOptions_0.ControlOfferPirateMissionsDefault;
                empire_5.AttackRangePatrol = gameOptions_0.AttackRangePatrol;
                empire_5.AttackRangeEscort = gameOptions_0.AttackRangeEscort;
                empire_5.AttackRangeAttack = gameOptions_0.AttackRangeAttack;
                empire_5.AttackRangeOther = gameOptions_0.AttackRangeOther;
                empire_5.AttackRangePatrolManual = gameOptions_0.AttackRangePatrolManual;
                empire_5.AttackRangeEscortManual = gameOptions_0.AttackRangeEscortManual;
                empire_5.AttackRangeAttackManual = gameOptions_0.AttackRangeAttackManual;
                empire_5.AttackRangeOtherManual = gameOptions_0.AttackRangeOtherManual;
                empire_5.AttackOvermatchFactor = gameOptions_0.AttackOverMatchFactor;
                empire_5.FleetAttackRefuelPortion = gameOptions_0.FleetAttackRefuelPortion;
                empire_5.FleetAttackGatherPortion = gameOptions_0.FleetAttackGatherPortion;
                empire_5.DiscoveryActionRuin = gameOptions_0.DiscoveryActionRuin;
                empire_5.DiscoveryActionAbandonedShipBase = gameOptions_0.DiscoveryActionAbandonedShipBase;
                empire_5.NewShipsAutomated = gameOptions_0.NewShipsAutomated;
            }
        }

        internal StartGameOptions method_259()
        {
            StartGameOptions startGameOptions = new StartGameOptions();
            startGameOptions.GalaxyAggression = 1;
            startGameOptions.GalaxyHabitatQuality = 2;
            startGameOptions.GalaxyAlienLifePrevalence = 2;
            startGameOptions.GalaxyExpansion = 0;
            startGameOptions.GalaxyPirates = 3;
            startGameOptions.GalaxyPirateStrength = 2;
            startGameOptions.GalaxyPirateProximity = 1;
            startGameOptions.GalaxyResearchSpeed = 120;
            startGameOptions.GalaxyShape = GalaxyShape.Elliptical;
            startGameOptions.GalaxySize = 3;
            startGameOptions.GalaxyDimensions = 3;
            startGameOptions.GalaxySpaceCreatures = 2;
            startGameOptions.ColonizationInfluenceRangeFactor = 1f;
            startGameOptions.ColonizationRangeEnforceLimit = true;
            startGameOptions.ColonizationRange = 2f;
            startGameOptions.OtherEmpires = new EmpireStartList();
            startGameOptions.OtherEmpiresAllowNewEmpiresFromIndependentColonies = true;
            startGameOptions.OtherEmpiresAutoGen = true;
            startGameOptions.OtherEmpiresAutoGenAmount = 11;
            startGameOptions.YourEmpireExpansion = 1;
            startGameOptions.YourEmpireFlagShape = 18;
            startGameOptions.YourEmpireGalaxyStartLocation = 0;
            startGameOptions.YourEmpireGovernmentStyle = 0;
            startGameOptions.YourEmpireHomeSystem = 2;
            startGameOptions.YourEmpireMainColor = 1;
            startGameOptions.YourEmpireName = "";
            startGameOptions.YourEmpireRace = 0;
            startGameOptions.YourEmpireSecondaryColor = 20;
            startGameOptions.YourEmpireTechLevel = 0;
            startGameOptions.YourEmpireCorruption = 1;
            startGameOptions.VictoryConditionsApplyWhen = true;
            startGameOptions.VictoryConditionsApplyWhenYears = 20;
            startGameOptions.VictoryConditionsEconomy = true;
            startGameOptions.VictoryConditionsEconomyPercent = 33;
            startGameOptions.VictoryConditionsPopulation = true;
            startGameOptions.VictoryConditionsPopulationPercent = 33;
            startGameOptions.VictoryConditionsTerritory = true;
            startGameOptions.VictoryConditionsTerritoryPercent = 33;
            startGameOptions.VictoryConditionsTimeLimit = false;
            startGameOptions.VictoryConditionsTimeLimitYears = 30;
            startGameOptions.VictoryConditionsStoryEvents = true;
            startGameOptions.VictoryConditionsStoryEventsOriginal = true;
            startGameOptions.VictoryConditionsDisasterEvents = true;
            startGameOptions.VictoryConditionsRaceSpecific = true;
            startGameOptions.GalaxyDifficulty = 1;
            startGameOptions.VictoryConditionsRaceSpecificEvents = true;
            startGameOptions.VictoryConditionsVictoryThresholdPercent = 1;
            return startGameOptions;
        }

        internal void method_260()
        {
            gameOptions_0 = new GameOptions();
            gameOptions_0.AutoPauseWhenInPopupWindow = true;
            gameOptions_0.ShowEncyclopediaAtStart = true;
            gameOptions_0.ControlAgentAssignmentDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.ControlAttacksOnEnemiesDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.ControlColonizationDefault = AutomationLevel.FullyAutomated;
            gameOptions_0.ControlColonyTaxRatesDefault = true;
            gameOptions_0.ControlDiplomaticGiftsDefault = AutomationLevel.Manual;
            gameOptions_0.ControlFleetFormationDefault = true;
            gameOptions_0.ControlShipBuildingDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.ControlShipDesignDefault = true;
            gameOptions_0.ControlTreatyNegotiationDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.ControlTroopRecruitmentDefault = true;
            gameOptions_0.ControlWarTradeSanctionsDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.ControlResearchDefault = true;
            gameOptions_0.ControlColonyFacilitiesDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.ControlPopulationPolicyDefault = true;
            gameOptions_0.ControlCharacterLocationsDefault = true;
            gameOptions_0.ControlOfferPirateMissionsDefault = AutomationLevel.SemiAutomated;
            gameOptions_0.DisplayMessageBuiltObjectBuilt = true;
            gameOptions_0.DisplayMessageColonyInvaded = true;
            gameOptions_0.DisplayMessageDiplomacyEmpireMetDestroyed = true;
            gameOptions_0.DisplayMessageDiplomacyGift = true;
            gameOptions_0.DisplayMessageDiplomacyRequestWarning = true;
            gameOptions_0.DisplayMessageDiplomacyTreaty = true;
            gameOptions_0.DisplayMessageDiplomacyWarTradeSanctions = true;
            gameOptions_0.DisplayMessageNewColony = true;
            gameOptions_0.DisplayMessageResearchNewComponent = true;
            gameOptions_0.DisplayMessageIntelligenceMissions = true;
            gameOptions_0.DisplayMessageExploration = true;
            gameOptions_0.DisplayMessageShipMissionComplete = true;
            gameOptions_0.DisplayMessageShipNeedsRefuelling = true;
            gameOptions_0.DisplayMessageConstructionResourceShortage = true;
            gameOptions_0.DisplayPopupBuiltObjectBuilt = false;
            gameOptions_0.DisplayPopupColonyInvaded = true;
            gameOptions_0.DisplayPopupDiplomacyEmpireMetDestroyed = true;
            gameOptions_0.DisplayPopupDiplomacyGift = true;
            gameOptions_0.DisplayPopupDiplomacyRequestWarning = true;
            gameOptions_0.DisplayPopupDiplomacyTreaty = true;
            gameOptions_0.DisplayPopupDiplomacyWarTradeSanctions = true;
            gameOptions_0.DisplayPopupNewColony = true;
            gameOptions_0.DisplayPopupResearchNewComponent = true;
            gameOptions_0.DisplayPopupIntelligenceMissions = true;
            gameOptions_0.DisplayPopupExploration = true;
            gameOptions_0.DisplayPopupShipMissionComplete = false;
            gameOptions_0.DisplayPopupShipNeedsRefuelling = false;
            gameOptions_0.DisplayPopupConstructionResourceShortage = true;
            gameOptions_0.DisplayMessageUnderAttackCivilianShips = true;
            gameOptions_0.DisplayMessageUnderAttackCivilianBases = true;
            gameOptions_0.DisplayMessageUnderAttackExplorationShips = true;
            gameOptions_0.DisplayMessageUnderAttackColonyConstructionShips = true;
            gameOptions_0.DisplayMessageUnderAttackMilitaryShips = true;
            gameOptions_0.DisplayMessageUnderAttackOtherStateBases = true;
            gameOptions_0.DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases = true;
            gameOptions_0.DisplayPopupUnderAttackCivilianShips = true;
            gameOptions_0.DisplayPopupUnderAttackCivilianBases = true;
            gameOptions_0.DisplayPopupUnderAttackExplorationShips = true;
            gameOptions_0.DisplayPopupUnderAttackColonyConstructionShips = true;
            gameOptions_0.DisplayPopupUnderAttackMilitaryShips = true;
            gameOptions_0.DisplayPopupUnderAttackOtherStateBases = true;
            gameOptions_0.DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases = true;
            gameOptions_0.MainViewScrollSpeed = 10;
            gameOptions_0.MainViewZoomSpeed = 12;
            gameOptions_0.MusicVolume = 0.75;
            gameOptions_0.SoundEffectsVolume = 0.75;
            gameOptions_0.StarFieldSize = 1000;
            gameOptions_0.ShowSystemNebulae = true;
            gameOptions_0.MouseScrollWheelBehaviour = 2;
            gameOptions_0.AutoSaveInterval = 30;
            gameOptions_0.AttackOverMatchFactor = 2f;
            gameOptions_0.AttackRangePatrol = 48000;
            gameOptions_0.AttackRangeEscort = 2000;
            gameOptions_0.AttackRangeAttack = 2000;
            gameOptions_0.AttackRangeOther = 48000;
            gameOptions_0.AttackRangePatrolManual = -1;
            gameOptions_0.AttackRangeEscortManual = -1;
            gameOptions_0.AttackRangeAttackManual = -1;
            gameOptions_0.AttackRangeOtherManual = -1;
            gameOptions_0.FleetAttackRefuelPortion = 0.3f;
            gameOptions_0.FleetAttackGatherPortion = 0.3f;
            gameOptions_0.GalaxyViewDisplayFleets = true;
            gameOptions_0.GalaxyViewDisplayResupplyShips = true;
            gameOptions_0.GalaxyViewDisplayMilitaryShips = true;
            gameOptions_0.GalaxyViewDisplaySpacePorts = true;
            gameOptions_0.GalaxyViewDisplayOtherBases = true;
            gameOptions_0.GalaxyViewDisplayExplorationShips = true;
            gameOptions_0.GalaxyViewDisplayColonyShips = true;
            gameOptions_0.GalaxyViewDisplayConstructionShips = true;
            gameOptions_0.GalaxyViewDisplayCivilianShips = false;
            gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets = true;
            gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips = true;
            gameOptions_0.GalaxyViewDisplayAlwaysPirates = true;
            gameOptions_0.DiscoveryActionRuin = 0;
            gameOptions_0.DiscoveryActionAbandonedShipBase = 0;
            gameOptions_0.LoadedGamesPaused = true;
            gameOptions_0.NewShipsAutomated = true;
            gameOptions_0.StartGameOptions = method_259();
        }

        internal void method_261(Game game_0)
        {
            if (MusicPlayer != null)
            {
                MusicPlayer.SetVolume(game_0.MusicVolume);
            }
            if (EffectsPlayer != null)
            {
                EffectsPlayer.Volume = game_0.SoundEffectsVolume;
            }
            GlassButton.Volume = game_0.SoundEffectsVolume;
            GlassButton.Volume = game_0.SoundEffectsVolume;
            CloseButton.Volume = game_0.SoundEffectsVolume;
            ListViewBase.Volume = game_0.SoundEffectsVolume;
            HoverButton.Volume = game_0.SoundEffectsVolume;
            HoverMenuItem.Volume = game_0.SoundEffectsVolume;
        }

        internal void method_262(GameOptions gameOptions_1)
        {
            if (MusicPlayer != null)
            {
                MusicPlayer.SetVolume(gameOptions_1.MusicVolume);
            }
            if (EffectsPlayer != null)
            {
                EffectsPlayer.Volume = gameOptions_1.SoundEffectsVolume;
            }
            GlassButton.Volume = gameOptions_1.SoundEffectsVolume;
            GlassButton.Volume = gameOptions_1.SoundEffectsVolume;
            CloseButton.Volume = gameOptions_1.SoundEffectsVolume;
            ListViewBase.Volume = gameOptions_1.SoundEffectsVolume;
            HoverButton.Volume = gameOptions_1.SoundEffectsVolume;
            HoverMenuItem.Volume = gameOptions_1.SoundEffectsVolume;
        }

        internal static GameOptions smethod_0()
        {
            GameOptions result = null;
            string path = GetGameFilesFolderCreateIfNeeded() + "defaultOptions";
            if (File.Exists(path))
            {
                try
                {
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    try
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        object obj = binaryFormatter.Deserialize(fileStream);
                        if (!(obj is GameOptions))
                        {
                            return result;
                        }
                        result = (GameOptions)obj;
                        return result;
                    }
                    catch (Exception)
                    {
                        return result;
                    }
                    finally
                    {
                        fileStream.Close();
                    }
                }
                catch (Exception)
                {
                    return result;
                }
            }
            return result;
        }

        internal void method_263()
        {
            gameOptions_0 = smethod_0();
            if (gameOptions_0 == null)
            {
                method_260();
            }
            method_262(gameOptions_0);
        }

        private void btnCycleColonies_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (habitat_0 != null)
            {
                num = _Game.PlayerEmpire.Colonies.IndexOf(habitat_0);
                num++;
                if (num >= _Game.PlayerEmpire.Colonies.Count)
                {
                    num = 0;
                }
            }
            if (_Game.PlayerEmpire.Colonies.Count > num)
            {
                habitat_0 = _Game.PlayerEmpire.Colonies[num];
            }
            else
            {
                habitat_0 = null;
            }
            if (habitat_0 != null)
            {
                method_208(habitat_0);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleConstruction_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Build);
            BuiltObjectList builtObjectsByRole = builtObjectList.GetBuiltObjectsByRole(list);
            BuiltObjectList builtObjectsBySubRole = builtObjectList.GetBuiltObjectsBySubRole(BuiltObjectSubRole.ResupplyShip);
            if (builtObjectsBySubRole.Count > 0)
            {
                builtObjectsByRole.AddRange(builtObjectsBySubRole);
            }
            int num = 0;
            if (builtObject_2 != null)
            {
                num = builtObjectsByRole.IndexOf(builtObject_2);
                num++;
                if (num >= builtObjectsByRole.Count)
                {
                    num = 0;
                }
            }
            if (builtObjectsByRole.Count > num)
            {
                builtObject_2 = builtObjectsByRole[num];
            }
            else
            {
                builtObject_2 = null;
            }
            if (builtObject_2 != null)
            {
                method_208(builtObject_2);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleMilitary_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Military);
            BuiltObjectList builtObjectsByRole = builtObjectList.GetBuiltObjectsByRole(list);
            int num = 0;
            if (builtObject_1 != null)
            {
                num = builtObjectsByRole.IndexOf(builtObject_1);
                num++;
                if (num >= builtObjectsByRole.Count)
                {
                    num = 0;
                }
            }
            if (builtObjectsByRole.Count > num)
            {
                builtObject_1 = builtObjectsByRole[num];
            }
            else
            {
                builtObject_1 = null;
            }
            if (builtObject_1 != null)
            {
                method_208(builtObject_1);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleBases_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(BuiltObjectSubRole.SmallSpacePort);
            list.Add(BuiltObjectSubRole.MediumSpacePort);
            list.Add(BuiltObjectSubRole.LargeSpacePort);
            list.Add(BuiltObjectSubRole.GenericBase);
            list.Add(BuiltObjectSubRole.EnergyResearchStation);
            list.Add(BuiltObjectSubRole.WeaponsResearchStation);
            list.Add(BuiltObjectSubRole.HighTechResearchStation);
            list.Add(BuiltObjectSubRole.MonitoringStation);
            list.Add(BuiltObjectSubRole.DefensiveBase);
            BuiltObjectList builtObjectsBySubRole = builtObjectList.GetBuiltObjectsBySubRole(list);
            int num = 0;
            if (builtObject_0 != null)
            {
                num = builtObjectsBySubRole.IndexOf(builtObject_0);
                num++;
                if (num >= builtObjectsBySubRole.Count)
                {
                    num = 0;
                }
            }
            if (builtObjectsBySubRole.Count > num)
            {
                builtObject_0 = builtObjectsBySubRole[num];
            }
            else
            {
                builtObject_0 = null;
            }
            if (builtObject_0 != null)
            {
                method_208(builtObject_0);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleOther_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Colony);
            list.Add(BuiltObjectRole.Exploration);
            BuiltObjectList builtObjectsByRole = builtObjectList.GetBuiltObjectsByRole(list);
            int num = 0;
            if (builtObject_3 != null)
            {
                num = builtObjectsByRole.IndexOf(builtObject_3);
                num++;
                if (num >= builtObjectsByRole.Count)
                {
                    num = 0;
                }
            }
            if (builtObjectsByRole.Count > num)
            {
                builtObject_3 = builtObjectsByRole[num];
            }
            else
            {
                builtObject_3 = null;
            }
            if (builtObject_3 != null)
            {
                method_208(builtObject_3);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void tbtnColonies_Click(object sender, EventArgs e)
        {
            if (pnlColonyInfo.Visible)
            {
                method_186();
            }
            else
            {
                method_166(null);
            }
        }

        private void tbtnBuiltObjects_Click(object sender, EventArgs e)
        {
            if (pnlBuiltObjectInfo.Visible)
            {
                method_185();
            }
            else
            {
                method_177(null);
            }
        }

        private void tbtnEmpires_Click(object sender, EventArgs e)
        {
            if (pnlEmpireInfo.Visible)
            {
                method_194();
            }
            else
            {
                method_195(null);
            }
        }

        private void tbtnTroops_Click(object sender, EventArgs e)
        {
            if (pnlTroopInfo.Visible)
            {
                method_184();
            }
            else
            {
                method_172(null);
            }
        }

        private void tbtnGalaxyMap_Click(object sender, EventArgs e)
        {
            if (CaLkaMyrMQ.Visible)
            {
                method_132();
            }
            else
            {
                method_131(null);
            }
        }

        private void tbtnShipGroups_Click(object sender, EventArgs e)
        {
            if (pnlShipGroupInfo.Visible)
            {
                method_271();
            }
            else
            {
                method_268(null);
            }
        }

        private void btnLockView_Click(object sender, EventArgs e)
        {
            if (UhvLmNjli7)
            {
                UhvLmNjli7 = false;
                string_20 = string.Empty;
            }
            else
            {
                UhvLmNjli7 = true;
                bool_20 = true;
            }
            WuVtIlwpRt();
        }

        private void btnZoomColony_Click(object sender, EventArgs e)
        {
            method_4(1.0);
            WuVtIlwpRt();
        }

        private void btnZoomSystem_Click(object sender, EventArgs e)
        {
            method_4(50.0);
            WuVtIlwpRt();
        }

        private void jQaYpdpkDs_Click(object sender, EventArgs e)
        {
            method_4(3000.0);
            WuVtIlwpRt();
        }

        private void JqLykZtpp1(object sender, EventArgs e)
        {
            method_4(double_5);
            WuVtIlwpRt();
        }

        private void ctlShipGroupListView_SelectionChanged(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            chkShipGroupUseTroopLoadouts.CheckedChanged -= uuGypgjgrb;
            numShipGroupTroopLoadoutInfantry.ValueChanged -= numShipGroupTroopLoadoutInfantry_ValueChanged;
            numShipGroupTroopLoadoutArtillery.ValueChanged -= numShipGroupTroopLoadoutArtillery_ValueChanged;
            numShipGroupTroopLoadoutArmored.ValueChanged -= numShipGroupTroopLoadoutArmored_ValueChanged;
            numShipGroupTroopLoadoutSpecialForces.ValueChanged -= numShipGroupTroopLoadoutSpecialForces_ValueChanged;
            method_266();
            method_265();
            if (selectedShipGroup != null)
            {
                pnlDetailInfoShipGroup.SetData(_Game, _Game.Galaxy, selectedShipGroup);
                txtShipGroupName.Text = selectedShipGroup.Name;
                gmapShipGroupInfo.SetPosition(selectedShipGroup.LeadShip.Xpos, selectedShipGroup.LeadShip.Ypos);
                gmapShipGroupInfo.SetSystem(null);
                gmapShipGroupInfo.Invalidate();
                if (selectedShipGroup.TroopLoadoutInfantry == byte.MaxValue && selectedShipGroup.TroopLoadoutArtillery == byte.MaxValue && selectedShipGroup.TroopLoadoutArmored == byte.MaxValue && selectedShipGroup.TroopLoadoutSpecialForces == byte.MaxValue)
                {
                    grpShipGroupUseTroopLoadouts.Enabled = false;
                    numShipGroupTroopLoadoutInfantry.Value = 0m;
                    numShipGroupTroopLoadoutArtillery.Value = 0m;
                    numShipGroupTroopLoadoutArmored.Value = 0m;
                    numShipGroupTroopLoadoutSpecialForces.Value = 0m;
                    chkShipGroupUseTroopLoadouts.Checked = false;
                }
                else
                {
                    grpShipGroupUseTroopLoadouts.Enabled = true;
                    numShipGroupTroopLoadoutInfantry.Value = selectedShipGroup.TroopLoadoutInfantry;
                    numShipGroupTroopLoadoutArtillery.Value = selectedShipGroup.TroopLoadoutArtillery;
                    numShipGroupTroopLoadoutArmored.Value = selectedShipGroup.TroopLoadoutArmored;
                    numShipGroupTroopLoadoutSpecialForces.Value = selectedShipGroup.TroopLoadoutSpecialForces;
                    chkShipGroupUseTroopLoadouts.Checked = true;
                }
                lblShipGroupTroopLoadoutDescription.Text = string.Format(TextResolver.GetText("Fleet Troop Capacity Description"), selectedShipGroup.TotalTroopCapacity.ToString("0"));
            }
            else
            {
                pnlDetailInfoShipGroup.ClearData();
                txtShipGroupName.Text = string.Empty;
                gmapShipGroupInfo.SetPosition(0.0, 0.0);
                gmapShipGroupInfo.SetSystem(null);
                gmapShipGroupInfo.Invalidate();
                grpShipGroupUseTroopLoadouts.Enabled = false;
                numShipGroupTroopLoadoutInfantry.Value = 0m;
                numShipGroupTroopLoadoutArtillery.Value = 0m;
                numShipGroupTroopLoadoutArmored.Value = 0m;
                numShipGroupTroopLoadoutSpecialForces.Value = 0m;
                chkShipGroupUseTroopLoadouts.Checked = false;
                lblShipGroupTroopLoadoutDescription.Text = string.Empty;
            }
            method_265();
            chkShipGroupUseTroopLoadouts.CheckedChanged += uuGypgjgrb;
            numShipGroupTroopLoadoutInfantry.ValueChanged += numShipGroupTroopLoadoutInfantry_ValueChanged;
            numShipGroupTroopLoadoutArtillery.ValueChanged += numShipGroupTroopLoadoutArtillery_ValueChanged;
            numShipGroupTroopLoadoutArmored.ValueChanged += numShipGroupTroopLoadoutArmored_ValueChanged;
            numShipGroupTroopLoadoutSpecialForces.ValueChanged += numShipGroupTroopLoadoutSpecialForces_ValueChanged;
        }

        private int method_264()
        {
            int num = 0;
            num = 0 + (int)numShipGroupTroopLoadoutInfantry.Value;
            num += (int)numShipGroupTroopLoadoutArmored.Value;
            num += (int)numShipGroupTroopLoadoutArtillery.Value;
            return num + (int)numShipGroupTroopLoadoutSpecialForces.Value;
        }

        private void method_265()
        {
            int num = method_264();
            int num2 = 100 - num;
            numShipGroupTroopLoadoutInfantry.Maximum = Math.Min(100, (int)numShipGroupTroopLoadoutInfantry.Value + num2);
            numShipGroupTroopLoadoutArmored.Maximum = Math.Min(100, (int)numShipGroupTroopLoadoutArmored.Value + num2);
            numShipGroupTroopLoadoutArtillery.Maximum = Math.Min(100, (int)numShipGroupTroopLoadoutArtillery.Value + num2);
            numShipGroupTroopLoadoutSpecialForces.Maximum = Math.Min(100, (int)numShipGroupTroopLoadoutSpecialForces.Value + num2);
        }

        private void method_266()
        {
            numShipGroupTroopLoadoutInfantry.Value = 0m;
            numShipGroupTroopLoadoutInfantry.Minimum = 0m;
            numShipGroupTroopLoadoutInfantry.Maximum = 100m;
            numShipGroupTroopLoadoutArmored.Value = 0m;
            numShipGroupTroopLoadoutArmored.Minimum = 0m;
            numShipGroupTroopLoadoutArmored.Maximum = 100m;
            numShipGroupTroopLoadoutArtillery.Value = 0m;
            numShipGroupTroopLoadoutArtillery.Minimum = 0m;
            numShipGroupTroopLoadoutArtillery.Maximum = 100m;
            numShipGroupTroopLoadoutSpecialForces.Value = 0m;
            numShipGroupTroopLoadoutSpecialForces.Minimum = 0m;
            numShipGroupTroopLoadoutSpecialForces.Maximum = 100m;
        }

        private void numShipGroupTroopLoadoutInfantry_ValueChanged(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                selectedShipGroup.TroopLoadoutInfantry = (byte)numShipGroupTroopLoadoutInfantry.Value;
                method_267(selectedShipGroup);
            }
            method_265();
        }

        private void numShipGroupTroopLoadoutArmored_ValueChanged(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                selectedShipGroup.TroopLoadoutArmored = (byte)numShipGroupTroopLoadoutArmored.Value;
                method_267(selectedShipGroup);
            }
            method_265();
        }

        private void numShipGroupTroopLoadoutArtillery_ValueChanged(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                selectedShipGroup.TroopLoadoutArtillery = (byte)numShipGroupTroopLoadoutArtillery.Value;
                method_267(selectedShipGroup);
            }
            method_265();
        }

        private void numShipGroupTroopLoadoutSpecialForces_ValueChanged(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                selectedShipGroup.TroopLoadoutSpecialForces = (byte)numShipGroupTroopLoadoutSpecialForces.Value;
                method_267(selectedShipGroup);
            }
            method_265();
        }

        private void uuGypgjgrb(object sender, EventArgs e)
        {
            chkShipGroupUseTroopLoadouts.CheckedChanged -= uuGypgjgrb;
            numShipGroupTroopLoadoutInfantry.ValueChanged -= numShipGroupTroopLoadoutInfantry_ValueChanged;
            numShipGroupTroopLoadoutArtillery.ValueChanged -= numShipGroupTroopLoadoutArtillery_ValueChanged;
            numShipGroupTroopLoadoutArmored.ValueChanged -= numShipGroupTroopLoadoutArmored_ValueChanged;
            numShipGroupTroopLoadoutSpecialForces.ValueChanged -= numShipGroupTroopLoadoutSpecialForces_ValueChanged;
            if (!chkShipGroupUseTroopLoadouts.Checked)
            {
                grpShipGroupUseTroopLoadouts.Enabled = false;
                numShipGroupTroopLoadoutInfantry.Value = 0m;
                numShipGroupTroopLoadoutArmored.Value = 0m;
                numShipGroupTroopLoadoutArtillery.Value = 0m;
                numShipGroupTroopLoadoutSpecialForces.Value = 0m;
                ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
                if (selectedShipGroup != null)
                {
                    selectedShipGroup.TroopLoadoutInfantry = byte.MaxValue;
                    selectedShipGroup.TroopLoadoutArmored = byte.MaxValue;
                    selectedShipGroup.TroopLoadoutArtillery = byte.MaxValue;
                    selectedShipGroup.TroopLoadoutSpecialForces = byte.MaxValue;
                }
                method_267(null);
                if (selectedShipGroup != null)
                {
                    lblShipGroupTroopLoadoutDescription.Text = string.Format(TextResolver.GetText("Fleet Troop Capacity Description"), selectedShipGroup.TotalTroopCapacity.ToString("0"));
                }
            }
            else
            {
                grpShipGroupUseTroopLoadouts.Enabled = true;
                ShipGroup selectedShipGroup2 = ctlShipGroupListView.SelectedShipGroup;
                if (selectedShipGroup2 != null)
                {
                    numShipGroupTroopLoadoutInfantry.Maximum = 100m;
                    numShipGroupTroopLoadoutInfantry.Value = 100m;
                    numShipGroupTroopLoadoutArmored.Value = 0m;
                    numShipGroupTroopLoadoutArtillery.Value = 0m;
                    numShipGroupTroopLoadoutSpecialForces.Value = 0m;
                    selectedShipGroup2.TroopLoadoutInfantry = 100;
                    selectedShipGroup2.TroopLoadoutArmored = 0;
                    selectedShipGroup2.TroopLoadoutArtillery = 0;
                    selectedShipGroup2.TroopLoadoutSpecialForces = 0;
                }
                method_265();
                method_267(selectedShipGroup2);
            }
            chkShipGroupUseTroopLoadouts.CheckedChanged += uuGypgjgrb;
            numShipGroupTroopLoadoutInfantry.ValueChanged += numShipGroupTroopLoadoutInfantry_ValueChanged;
            numShipGroupTroopLoadoutArtillery.ValueChanged += numShipGroupTroopLoadoutArtillery_ValueChanged;
            numShipGroupTroopLoadoutArmored.ValueChanged += numShipGroupTroopLoadoutArmored_ValueChanged;
            numShipGroupTroopLoadoutSpecialForces.ValueChanged += numShipGroupTroopLoadoutSpecialForces_ValueChanged;
        }

        private void method_267(ShipGroup shipGroup_3)
        {
            if (shipGroup_3 != null)
            {
                int infantryAmount = 0;
                int artilleryAmount = 0;
                int armorAmount = 0;
                int specialForcesAmount = 0;
                shipGroup_3.GetTroopLoadoutTargetAmounts(refactorForDisabledTroopTypes: false, out infantryAmount, out artilleryAmount, out armorAmount, out specialForcesAmount);
                lblShipGroupTroopLoadoutInfantry.Text = "% " + TextResolver.GetText("TroopType Infantry") + "  (" + string.Format(TextResolver.GetText("equals X units"), infantryAmount) + ")";
                lblShipGroupTroopLoadoutArtillery.Text = "% " + TextResolver.GetText("TroopType Artillery") + "  (" + string.Format(TextResolver.GetText("equals X units"), artilleryAmount) + ")";
                lblShipGroupTroopLoadoutArmored.Text = "% " + TextResolver.GetText("TroopType Armored") + "  (" + string.Format(TextResolver.GetText("equals X units"), armorAmount) + ")";
                lblShipGroupTroopLoadoutSpecialForces.Text = "% " + TextResolver.GetText("TroopType SpecialForces") + "  (" + string.Format(TextResolver.GetText("equals X units"), specialForcesAmount) + ")";
                lblShipGroupTroopLoadoutDescription.Text = string.Format(TextResolver.GetText("Fleet Troop Capacity Description"), shipGroup_3.TotalTroopCapacity.ToString("0"));
            }
            else
            {
                lblShipGroupTroopLoadoutInfantry.Text = "% " + TextResolver.GetText("TroopType Infantry") + "  (" + string.Format(TextResolver.GetText("equals X units"), 0) + ")";
                lblShipGroupTroopLoadoutArtillery.Text = "% " + TextResolver.GetText("TroopType Artillery") + "  (" + string.Format(TextResolver.GetText("equals X units"), 0) + ")";
                lblShipGroupTroopLoadoutArmored.Text = "% " + TextResolver.GetText("TroopType Armored") + "  (" + string.Format(TextResolver.GetText("equals X units"), 0) + ")";
                lblShipGroupTroopLoadoutSpecialForces.Text = "% " + TextResolver.GetText("TroopType SpecialForces") + "  (" + string.Format(TextResolver.GetText("equals X units"), 0) + ")";
                lblShipGroupTroopLoadoutDescription.Text = string.Format(TextResolver.GetText("Fleet Troop Capacity Description"), "0");
            }
        }

        private void method_268(ShipGroup shipGroup_3)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlShipGroupInfo.Size = new Size(988, 768);
            pnlShipGroupInfo.Location = new Point((mainView.Width - pnlShipGroupInfo.Width) / 2, (mainView.Height - pnlShipGroupInfo.Height) / 2);
            pnlShipGroupInfo.DoLayout();
            lnkFleets.Location = new Point(10, 8);
            lnkFleets.Text = TextResolver.GetText("Learn about Fleets") + "...";
            ctlShipGroupListView.Height = 283;
            ctlShipGroupListView.Width = 950;
            ctlShipGroupListView.Location = new Point(10, 27);
            ctlShipGroupListView.BindData(_Game.PlayerEmpire.ShipGroups, characterImageCache_0);
            ctlShipGroupListView.BringToFront();
            ctlShipGroupListView.Grid.Columns["Name"].Width = 170;
            ctlShipGroupListView.Grid.Columns["Ships"].Width = 50;
            ctlShipGroupListView.Grid.Columns["Home colony"].Width = 140;
            ctlShipGroupListView.Grid.Columns["Mission"].Width = 300;
            ctlShipGroupListView.Grid.Columns["Firepower"].Width = 60;
            ctlShipGroupListView.Grid.Columns["Troops"].Width = 60;
            ctlShipGroupListView.Grid.Columns["Current system"].Width = 140;
            lblShipGroupGalaxyMapTitle.Location = new Point(650, 365);
            gmapShipGroupInfo.BringToFront();
            gmapShipGroupInfo.Size = new Size(310, 310);
            gmapShipGroupInfo.Location = new Point(650, 380);
            gmapShipGroupInfo.SetLocations(_Game.PlayerEmpire.ShipGroups);
            gmapShipGroupInfo.ShowFleetPostures = true;
            lblShipGroupName.Text = TextResolver.GetText("Name");
            lblShipGroupName.Font = font_2;
            lblShipGroupName.ForeColor = color_1;
            lblShipGroupName.BackColor = Color.Transparent;
            lblShipGroupName.Location = new Point(10, 330);
            txtShipGroupName.Font = font_7;
            txtShipGroupName.BackColor = Color.FromArgb(48, 48, 64);
            txtShipGroupName.ForeColor = Color.FromArgb(170, 170, 170);
            txtShipGroupName.Size = new Size(280, 20);
            txtShipGroupName.Location = new Point(60, 327);
            txtShipGroupName.BringToFront();
            btnShipGroupSelect.Location = new Point(350, 320);
            btnShipGroupSelect.Size = new Size(140, 42);
            btnShipGroupGoto.Location = new Point(500, 320);
            btnShipGroupGoto.Size = new Size(140, 42);
            cmbShipGroupInfoHomeColony.Size = new Size(157, 21);
            cmbShipGroupInfoHomeColony.Location = new Point(652, 330);
            cmbShipGroupInfoHomeColony.BringToFront();
            cmbShipGroupInfoHomeColony.Visible = true;
            List<string> list = new List<string>();
            foreach (Habitat colony in _Game.PlayerEmpire.Colonies)
            {
                list.Add(colony.Name);
            }
            list.Sort();
            cmbShipGroupInfoHomeColony.Items.Clear();
            cmbShipGroupInfoHomeColony.Items.Add("(" + TextResolver.GetText("Select new home colony") + ")");
            cmbShipGroupInfoHomeColony.Items.AddRange(list.ToArray());
            if (cmbShipGroupInfoHomeColony.Items.Count > 0)
            {
                cmbShipGroupInfoHomeColony.SelectedIndex = 0;
            }
            btnShipGroupInfoSetHomeColony.Location = new Point(815, 320);
            btnShipGroupInfoSetHomeColony.Size = new Size(145, 42);
            btnShipGroupRepairAndRefuel.Location = new Point(350, 380);
            btnShipGroupRepairAndRefuel.Size = new Size(90, 80);
            btnShipGroupRetrofit.Location = new Point(450, 380);
            btnShipGroupRetrofit.Size = new Size(90, 80);
            XxYlcNpSu4.Location = new Point(550, 380);
            XxYlcNpSu4.Size = new Size(90, 80);
            pnlDetailInfoShipGroup.Size = new Size(328, 310);
            pnlDetailInfoShipGroup.Location = new Point(11, 380);
            pnlDetailInfoShipGroup.Font = font_3;
            pnlDetailInfoShipGroup.CurveMode = CornerCurveMode.BottomRight_TopLeft;
            pnlDetailInfoShipGroup.ShowExtendedInfo = true;
            pnlDetailInfoShipGroup.Reset();
            lblShipGroupUngarrisonedTroopReport.Location = new Point(350, 470);
            lblShipGroupUngarrisonedTroopReport.MaximumSize = new Size(290, 45);
            lblShipGroupUngarrisonedTroopReport.Size = new Size(290, 45);
            lblShipGroupUngarrisonedTroopReport.Font = font_6;
            grpShipGroupUseTroopLoadouts.Size = new Size(290, 175);
            grpShipGroupUseTroopLoadouts.Location = new Point(350, 515);
            chkShipGroupUseTroopLoadouts.Location = new Point(360, 512);
            chkShipGroupUseTroopLoadouts.Font = font_7;
            chkShipGroupUseTroopLoadouts.BringToFront();
            numShipGroupTroopLoadoutInfantry.Size = new Size(40, 25);
            numShipGroupTroopLoadoutArmored.Size = new Size(40, 25);
            numShipGroupTroopLoadoutArtillery.Size = new Size(40, 25);
            numShipGroupTroopLoadoutSpecialForces.Size = new Size(40, 25);
            numShipGroupTroopLoadoutInfantry.Location = new Point(10, 23);
            numShipGroupTroopLoadoutArmored.Location = new Point(10, 53);
            numShipGroupTroopLoadoutArtillery.Location = new Point(10, 83);
            numShipGroupTroopLoadoutSpecialForces.Location = new Point(10, 113);
            lblShipGroupTroopLoadoutInfantry.Location = new Point(50, 28);
            lblShipGroupTroopLoadoutArmored.Location = new Point(50, 58);
            lblShipGroupTroopLoadoutArtillery.Location = new Point(50, 88);
            lblShipGroupTroopLoadoutSpecialForces.Location = new Point(50, 118);
            lblShipGroupTroopLoadoutDescription.Location = new Point(10, 148);
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string text = "% " + TextResolver.GetText("TroopType Infantry") + " (" + string.Format(TextResolver.GetText("equals X units"), num.ToString("0")) + ")";
            string text2 = "% " + TextResolver.GetText("TroopType Armored") + " (" + string.Format(TextResolver.GetText("equals X units"), num2.ToString("0")) + ")";
            string text3 = "% " + TextResolver.GetText("TroopType Artillery") + " (" + string.Format(TextResolver.GetText("equals X units"), num3.ToString("0")) + ")";
            string text4 = "% " + TextResolver.GetText("TroopType SpecialForces") + " (" + string.Format(TextResolver.GetText("equals X units"), num4.ToString("0")) + ")";
            lblShipGroupTroopLoadoutInfantry.Text = text;
            lblShipGroupTroopLoadoutArmored.Text = text2;
            lblShipGroupTroopLoadoutArtillery.Text = text3;
            lblShipGroupTroopLoadoutSpecialForces.Text = text4;
            numShipGroupTroopLoadoutInfantry.Font = font_6;
            numShipGroupTroopLoadoutArmored.Font = font_6;
            numShipGroupTroopLoadoutArtillery.Font = font_6;
            numShipGroupTroopLoadoutSpecialForces.Font = font_6;
            lblShipGroupTroopLoadoutInfantry.Font = font_6;
            lblShipGroupTroopLoadoutArmored.Font = font_6;
            lblShipGroupTroopLoadoutArtillery.Font = font_6;
            lblShipGroupTroopLoadoutSpecialForces.Font = font_6;
            lblShipGroupTroopLoadoutDescription.Font = font_6;
            if (shipGroup_3 != null)
            {
                ctlShipGroupListView.SelectShipGroup(shipGroup_3);
            }
            method_267(ctlShipGroupListView.SelectedShipGroup);
            lblShipGroupUngarrisonedTroopReport.Text = method_269(ctlShipGroupListView.SelectedShipGroup);
            ctlShipGroupListView_SelectionChanged(null, null);
            if (ctlShipGroupListView.SelectedShipGroup != null)
            {
                txtShipGroupName.Text = ctlShipGroupListView.SelectedShipGroup.Name;
            }
            method_270();
            pnlShipGroupInfo.Visible = true;
            pnlShipGroupInfo.BringToFront();
            ctlShipGroupListView.Focus();
        }

        private void ctlShipGroupListView_SelectionChanged_1(object sender, EventArgs e)
        {
            method_270();
            method_267(ctlShipGroupListView.SelectedShipGroup);
            lblShipGroupUngarrisonedTroopReport.Text = method_269(ctlShipGroupListView.SelectedShipGroup);
        }

        private string method_269(ShipGroup shipGroup_3)
        {
            string text = string.Empty;
            if (shipGroup_3 != null)
            {
                TroopList troopsNotGarrisonedAtColony = _Game.PlayerEmpire.Troops.GetTroopsNotGarrisonedAtColony();
                text = TextResolver.GetText("Ungarrisoned Troops At Colonies");
                text += "\n";
                text = text + troopsNotGarrisonedAtColony.Count + " " + TextResolver.GetText("troops");
                int infantryCount = 0;
                int artilleryCount = 0;
                int armorCount = 0;
                int specialForcesCount = 0;
                troopsNotGarrisonedAtColony.GetTroopCountsByType(out infantryCount, out artilleryCount, out armorCount, out specialForcesCount);
                string text2 = Galaxy.ResolveTroopCompositionDescription(infantryCount, artilleryCount, armorCount, specialForcesCount);
                if (!string.IsNullOrEmpty(text2))
                {
                    text = text + ": " + text2;
                }
            }
            return text;
        }

        private void method_270()
        {
            bool enabled = false;
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                enabled = true;
            }
            txtShipGroupName.Enabled = enabled;
            btnShipGroupSelect.Enabled = enabled;
            btnShipGroupGoto.Enabled = enabled;
            cmbShipGroupInfoHomeColony.Enabled = enabled;
            btnShipGroupInfoSetHomeColony.Enabled = enabled;
            btnShipGroupRepairAndRefuel.Enabled = enabled;
            btnShipGroupRetrofit.Enabled = enabled;
            XxYlcNpSu4.Enabled = enabled;
        }

        private void method_271()
        {
            pnlShipGroupInfo.SendToBack();
            pnlShipGroupInfo.Visible = false;
            if (_Game.PlayerEmpire.ShipGroups != null)
            {
                _Game.PlayerEmpire.ShipGroups.ClearSortTags();
                _Game.PlayerEmpire.ShipGroups.Sort();
            }
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void btnShipGroupGoto_Click(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null)
            {
                method_157(selectedShipGroup);
            }
            method_271();
        }

        private void txtShipGroupName_Leave(object sender, EventArgs e)
        {
            ShipGroup selectedShipGroup = ctlShipGroupListView.SelectedShipGroup;
            if (selectedShipGroup != null && !string.IsNullOrEmpty(txtShipGroupName.Text.Trim()))
            {
                selectedShipGroup.Name = txtShipGroupName.Text;
                ctlShipGroupListView.Grid.SelectedRows[0].Cells[1].Value = txtShipGroupName.Text;
            }
            pnlShipGroupInfo.Refresh();
            if (!pnlBuiltObjectInfo.Visible)
            {
                return;
            }
            foreach (DataGridViewRow selectedRow in ctlBuiltObjectList.Grid.SelectedRows)
            {
                BuiltObject builtObject = ctlBuiltObjectList.ResolveBuiltObject(selectedRow);
                if (builtObject != null && builtObject.Role == BuiltObjectRole.Military)
                {
                    if (builtObject.ShipGroup != null)
                    {
                        selectedRow.Cells[9].Value = builtObject.ShipGroup.Name;
                    }
                    else
                    {
                        selectedRow.Cells[9].Value = "(" + TextResolver.GetText("None") + ")";
                    }
                }
            }
        }

        private void cmbGalaxyMapViewMode_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (cmbGalaxyMapViewMode.SelectedIndex)
            {
                default:
                    habitatList_1 = null;
                    habitatList_2 = null;
                    omjYcxcvXH.Hide();
                    cmbGalaxyMapHabitatType.Hide();
                    break;
                case 0:
                    habitatList_1 = null;
                    habitatList_2 = null;
                    omjYcxcvXH.Hide();
                    cmbGalaxyMapHabitatType.Hide();
                    break;
                case 1:
                    {
                        HabitatList habitatList5 = new HabitatList();
                        for (int num3 = 0; num3 < _Game.PlayerEmpire.Colonies.Count; num3++)
                        {
                            Habitat habitat10 = _Game.PlayerEmpire.Colonies[num3];
                            Habitat item3 = Galaxy.DetermineHabitatSystemStar(habitat10);
                            if (!habitatList5.Contains(item3))
                            {
                                habitatList5.Add(item3);
                            }
                        }
                        habitatList_1 = habitatList5;
                        habitatList_2 = _Game.PlayerEmpire.Colonies;
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        break;
                    }
                case 2:
                    {
                        HabitatType habitatType = HabitatType.Undefined;
                        string text = cmbGalaxyMapHabitatType.SelectedItem.ToString();
                        if (text == Galaxy.ResolveDescription(HabitatType.Continental))
                        {
                            habitatType = HabitatType.Continental;
                        }
                        else if (text == Galaxy.ResolveDescription(HabitatType.MarshySwamp))
                        {
                            habitatType = HabitatType.MarshySwamp;
                        }
                        else if (text == Galaxy.ResolveDescription(HabitatType.Desert))
                        {
                            habitatType = HabitatType.Desert;
                        }
                        else if (text == Galaxy.ResolveDescription(HabitatType.Ocean))
                        {
                            habitatType = HabitatType.Ocean;
                        }
                        else if (text == Galaxy.ResolveDescription(HabitatType.Ice))
                        {
                            habitatType = HabitatType.Ice;
                        }
                        else if (text == Galaxy.ResolveDescription(HabitatType.Volcanic))
                        {
                            habitatType = HabitatType.Volcanic;
                        }
                        HabitatList habitatList = new HabitatList();
                        HabitatList habitatList2 = new HabitatList();
                        HabitatPrioritizationList habitatPrioritizationList = _Game.PlayerEmpire.IdentifyColonizationTargets(_Game.Galaxy, filterOutDangerousTargets: false, 0, 500);
                        for (int j = 0; j < habitatPrioritizationList.Count; j++)
                        {
                            HabitatPrioritization habitatPrioritization = habitatPrioritizationList[j];
                            if (habitatType == HabitatType.Undefined || habitatType == habitatPrioritization.Habitat.Type)
                            {
                                habitatList2.Add(habitatPrioritization.Habitat);
                                Habitat item = Galaxy.DetermineHabitatSystemStar(habitatPrioritization.Habitat);
                                if (!habitatList.Contains(item))
                                {
                                    habitatList.Add(item);
                                }
                            }
                        }
                        habitatList_1 = habitatList;
                        habitatList_2 = habitatList2;
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Show();
                        break;
                    }
                case 3:
                    if (omjYcxcvXH.SelectedItem != null)
                    {
                        int value = ((KeyValuePair<string, int>)omjYcxcvXH.SelectedItem).Value;
                        HabitatList habitatList3 = new HabitatList();
                        HabitatList habitatList4 = new HabitatList();
                        for (int l = 0; l < _Game.Galaxy.Habitats.Count; l++)
                        {
                            Habitat habitat5 = _Game.Galaxy.Habitats[l];
                            if (_Game.PlayerEmpire.ResourceMap == null || !_Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat5))
                            {
                                continue;
                            }
                            HabitatResourceList habitatResourceList = habitat5.Resources.Clone();
                            for (int m = 0; m < habitatResourceList.Count; m++)
                            {
                                HabitatResource habitatResource = habitatResourceList[m];
                                if (habitatResource.ResourceID == value)
                                {
                                    habitatList4.Add(habitat5);
                                    Habitat item2 = Galaxy.DetermineHabitatSystemStar(habitat5);
                                    if (!habitatList3.Contains(item2))
                                    {
                                        habitatList3.Add(item2);
                                    }
                                    break;
                                }
                            }
                        }
                        habitatList_1 = habitatList3;
                        habitatList_2 = habitatList4;
                    }
                    cmbGalaxyMapHabitatType.Hide();
                    omjYcxcvXH.Show();
                    break;
                case 4:
                    {
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        habitatList_1 = new HabitatList();
                        habitatList_2 = new HabitatList();
                        for (int num4 = 0; num4 < _Game.Galaxy.Habitats.Count; num4++)
                        {
                            Habitat habitat12 = _Game.Galaxy.Habitats[num4];
                            SystemVisibilityStatus systemVisibilityStatus6 = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat12.SystemIndex);
                            if (systemVisibilityStatus6 == SystemVisibilityStatus.Visible || systemVisibilityStatus6 == SystemVisibilityStatus.Explored)
                            {
                                Habitat habitat13 = Galaxy.DetermineHabitatSystemStar(habitat12);
                                if (habitat13 != null && !habitatList_1.Contains(habitat13))
                                {
                                    habitatList_1.Add(habitat13);
                                }
                                if (_Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat12))
                                {
                                    habitatList_2.Add(habitat12);
                                }
                            }
                        }
                        break;
                    }
                case 5:
                    omjYcxcvXH.Hide();
                    cmbGalaxyMapHabitatType.Hide();
                    habitatList_1 = new HabitatList();
                    habitatList_2 = new HabitatList();
                    foreach (Habitat habitat14 in _Game.Galaxy.Habitats)
                    {
                        if (habitat14.Population.TotalAmount <= 0L || (habitat14.Empire != _Game.Galaxy.IndependentEmpire && habitat14.Empire != null))
                        {
                            continue;
                        }
                        SystemVisibilityStatus systemVisibilityStatus5 = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat14.SystemIndex);
                        if (systemVisibilityStatus5 == SystemVisibilityStatus.Visible || systemVisibilityStatus5 == SystemVisibilityStatus.Explored)
                        {
                            Habitat habitat11 = Galaxy.DetermineHabitatSystemStar(habitat14);
                            if (habitat11 != null && !habitatList_1.Contains(habitat11))
                            {
                                habitatList_1.Add(habitat11);
                            }
                            habitatList_2.Add(habitat14);
                        }
                    }
                    break;
                case 6:
                    {
                        habitatList_1 = new HabitatList();
                        habitatList_2 = new HabitatList();
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        for (int num2 = 0; num2 < _Game.PlayerEmpire.DiplomaticRelations.Count; num2++)
                        {
                            DiplomaticRelation diplomaticRelation = _Game.PlayerEmpire.DiplomaticRelations[num2];
                            if (diplomaticRelation.Type != DiplomaticRelationType.War)
                            {
                                continue;
                            }
                            Empire otherEmpire = diplomaticRelation.OtherEmpire;
                            if (otherEmpire == null || otherEmpire == _Game.PlayerEmpire)
                            {
                                continue;
                            }
                            foreach (Habitat colony in otherEmpire.Colonies)
                            {
                                Habitat habitat9 = Galaxy.DetermineHabitatSystemStar(colony);
                                SystemVisibilityStatus systemVisibilityStatus4 = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat9.SystemIndex);
                                if (systemVisibilityStatus4 == SystemVisibilityStatus.Explored || systemVisibilityStatus4 == SystemVisibilityStatus.Visible)
                                {
                                    if (!habitatList_1.Contains(habitat9))
                                    {
                                        habitatList_1.Add(habitat9);
                                    }
                                    habitatList_2.Add(colony);
                                }
                            }
                        }
                        break;
                    }
                case 7:
                    {
                        habitatList_1 = new HabitatList();
                        habitatList_2 = new HabitatList();
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        for (int num = 0; num < _Game.PlayerEmpire.KnownPirateBases.Count; num++)
                        {
                            BuiltObject builtObject = _Game.PlayerEmpire.KnownPirateBases[num];
                            if (builtObject.ParentHabitat != null)
                            {
                                Habitat habitat8 = Galaxy.DetermineHabitatSystemStar(builtObject.ParentHabitat);
                                if (habitat8 != null && !habitatList_1.Contains(habitat8))
                                {
                                    habitatList_1.Add(habitat8);
                                }
                                habitatList_2.Add(builtObject.ParentHabitat);
                            }
                        }
                        break;
                    }
                case 8:
                    {
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        habitatList_1 = new HabitatList();
                        habitatList_2 = new HabitatList();
                        for (int n = 0; n < _Game.Galaxy.Habitats.Count; n++)
                        {
                            Habitat habitat6 = _Game.Galaxy.Habitats[n];
                            if (habitat6.Ruin == null)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus3 = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat6.SystemIndex);
                            if (systemVisibilityStatus3 == SystemVisibilityStatus.Visible || systemVisibilityStatus3 == SystemVisibilityStatus.Explored)
                            {
                                Habitat habitat7 = Galaxy.DetermineHabitatSystemStar(habitat6);
                                if (habitat7 != null && !habitatList_1.Contains(habitat7))
                                {
                                    habitatList_1.Add(habitat7);
                                }
                                habitatList_2.Add(habitat6);
                            }
                        }
                        break;
                    }
                case 9:
                    {
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        habitatList_1 = new HabitatList();
                        habitatList_2 = new HabitatList();
                        for (int k = 0; k < _Game.Galaxy.Habitats.Count; k++)
                        {
                            Habitat habitat3 = _Game.Galaxy.Habitats[k];
                            if (!(habitat3.ScenicFactor > 0f))
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus2 = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat3.SystemIndex);
                            if (systemVisibilityStatus2 == SystemVisibilityStatus.Visible || systemVisibilityStatus2 == SystemVisibilityStatus.Explored)
                            {
                                Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat3);
                                if (habitat4 != null && !habitatList_1.Contains(habitat4))
                                {
                                    habitatList_1.Add(habitat4);
                                }
                                habitatList_2.Add(habitat3);
                            }
                        }
                        break;
                    }
                case 10:
                    {
                        omjYcxcvXH.Hide();
                        cmbGalaxyMapHabitatType.Hide();
                        habitatList_1 = new HabitatList();
                        habitatList_2 = new HabitatList();
                        for (int i = 0; i < _Game.Galaxy.Habitats.Count; i++)
                        {
                            Habitat habitat = _Game.Galaxy.Habitats[i];
                            if (habitat.ResearchBonus <= 0)
                            {
                                continue;
                            }
                            SystemVisibilityStatus systemVisibilityStatus = _Game.PlayerEmpire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                            if (systemVisibilityStatus == SystemVisibilityStatus.Visible || systemVisibilityStatus == SystemVisibilityStatus.Explored)
                            {
                                Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(habitat);
                                if (habitat2 != null && !habitatList_1.Contains(habitat2))
                                {
                                    habitatList_1.Add(habitat2);
                                }
                                habitatList_2.Add(habitat);
                            }
                        }
                        break;
                    }
            }
            gmapMain.SetSystems(habitatList_1);
            picSystemMap.SetSelectedHabitats(habitatList_2);
            gmapMain.Invalidate();
            picSystemMap.Invalidate();
        }

        private void cmbGalaxyMapHabitatType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbGalaxyMapHabitatType.SelectedItem != null)
            {
                HabitatType habitatType = HabitatType.Undefined;
                string text = cmbGalaxyMapHabitatType.SelectedItem.ToString();
                if (text == Galaxy.ResolveDescription(HabitatType.Continental))
                {
                    habitatType = HabitatType.Continental;
                }
                else if (text == Galaxy.ResolveDescription(HabitatType.MarshySwamp))
                {
                    habitatType = HabitatType.MarshySwamp;
                }
                else if (text == Galaxy.ResolveDescription(HabitatType.Desert))
                {
                    habitatType = HabitatType.Desert;
                }
                else if (text == Galaxy.ResolveDescription(HabitatType.Ocean))
                {
                    habitatType = HabitatType.Ocean;
                }
                else if (text == Galaxy.ResolveDescription(HabitatType.Ice))
                {
                    habitatType = HabitatType.Ice;
                }
                else if (text == Galaxy.ResolveDescription(HabitatType.Volcanic))
                {
                    habitatType = HabitatType.Volcanic;
                }
                HabitatList habitatList = new HabitatList();
                HabitatList habitatList2 = new HabitatList();
                HabitatPrioritizationList habitatPrioritizationList = _Game.PlayerEmpire.IdentifyColonizationTargets(_Game.Galaxy, filterOutDangerousTargets: false, 0, 500);
                for (int i = 0; i < habitatPrioritizationList.Count; i++)
                {
                    HabitatPrioritization habitatPrioritization = habitatPrioritizationList[i];
                    if (habitatType == HabitatType.Undefined || habitatType == habitatPrioritization.Habitat.Type)
                    {
                        habitatList2.Add(habitatPrioritization.Habitat);
                        Habitat item = Galaxy.DetermineHabitatSystemStar(habitatPrioritization.Habitat);
                        if (!habitatList.Contains(item))
                        {
                            habitatList.Add(item);
                        }
                    }
                }
                habitatList_1 = habitatList;
                habitatList_2 = habitatList2;
            }
            gmapMain.SetSystems(habitatList_1);
            picSystemMap.SetSelectedHabitats(habitatList_2);
            gmapMain.Invalidate();
            picSystemMap.Invalidate();
        }

        private void omjYcxcvXH_SelectedValueChanged(object sender, EventArgs e)
        {
            if (omjYcxcvXH.SelectedItem != null)
            {
                int value = ((KeyValuePair<string, int>)omjYcxcvXH.SelectedItem).Value;
                HabitatList habitatList = new HabitatList();
                HabitatList habitatList2 = new HabitatList();
                for (int i = 0; i < _Game.Galaxy.Habitats.Count; i++)
                {
                    Habitat habitat = _Game.Galaxy.Habitats[i];
                    if (_Game.PlayerEmpire.ResourceMap == null || !_Game.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat))
                    {
                        continue;
                    }
                    HabitatResourceList habitatResourceList = habitat.Resources.Clone();
                    for (int j = 0; j < habitatResourceList.Count; j++)
                    {
                        HabitatResource habitatResource = habitatResourceList[j];
                        if (habitatResource.ResourceID == value)
                        {
                            habitatList2.Add(habitat);
                            Habitat item = Galaxy.DetermineHabitatSystemStar(habitat);
                            if (!habitatList.Contains(item))
                            {
                                habitatList.Add(item);
                            }
                            break;
                        }
                    }
                }
                habitatList_1 = habitatList;
                habitatList_2 = habitatList2;
            }
            gmapMain.SetSystems(habitatList_1);
            picSystemMap.SetSelectedHabitats(habitatList_2);
            gmapMain.Invalidate();
            picSystemMap.Invalidate();
        }

        private void txtHabitatSearch_Enter(object sender, EventArgs e)
        {
            txtHabitatSearch.Text = string.Empty;
            txtHabitatSearch.ForeColor = Color.White;
        }

        private void btnTroopDisband_Click(object sender, EventArgs e)
        {
            if (_Game.PlayerEmpire.ControlTroopGeneration && GenerateAutomationMessageBox(TextResolver.GetText("Troop Recruitment")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlTroopGeneration = false;
            }
            TroopList selectedTroops = ctlTroopList.SelectedTroops;
            if (selectedTroops == null || selectedTroops.Count <= 0)
            {
                return;
            }
            int num = int.MaxValue;
            if (_Game.PlayerEmpire.Troops != null)
            {
                for (int i = 0; i < selectedTroops.Count; i++)
                {
                    int num2 = _Game.PlayerEmpire.Troops.IndexOf(selectedTroops[i]);
                    if (num2 < num)
                    {
                        num = num2;
                    }
                }
                num--;
            }
            for (int j = 0; j < selectedTroops.Count; j++)
            {
                Troop troop = selectedTroops[j];
                if (troop == null || troop.Empire != _Game.PlayerEmpire)
                {
                    continue;
                }
                if (troop.Colony != null && troop.Colony.Troops != null && troop.Colony.TroopsToRecruit != null)
                {
                    if (troop.Colony.Troops.Contains(troop))
                    {
                        troop.Colony.Troops.Remove(troop);
                    }
                    else if (troop.Colony.TroopsToRecruit.Contains(troop))
                    {
                        troop.Colony.TroopsToRecruit.Remove(troop);
                    }
                }
                if (troop.BuiltObject != null && troop.BuiltObject.Troops != null && troop.BuiltObject.Troops.Contains(troop))
                {
                    troop.BuiltObject.Troops.Remove(troop);
                }
                if (troop.Empire.Troops.Contains(troop))
                {
                    troop.Empire.Troops.Remove(troop);
                }
                troop.BuiltObject = null;
                troop.Colony = null;
                troop.AwaitingPickup = false;
                troop.Empire = null;
            }
            TroopList troopList = new TroopList();
            troopList.AddRange(_Game.PlayerEmpire.Troops);
            ctlTroopList.BindData(troopList);
            lblTroopSummary.Text = method_171(troopList);
            if (num >= 0 && num < troopList.Count)
            {
                ctlTroopList.SelectTroop(troopList[num]);
            }
            Troop selectedTroop = ctlTroopList.SelectedTroop;
            if (selectedTroop != null)
            {
                txtTroopInfoName.Text = selectedTroop.Name;
            }
        }

        private void IgqymUpftW(object sender, EventArgs e)
        {
            Troop selectedTroop = ctlTroopList.SelectedTroop;
            if (selectedTroop != null && !string.IsNullOrEmpty(txtTroopInfoName.Text.Trim()))
            {
                selectedTroop.Name = txtTroopInfoName.Text;
                ctlTroopList.Grid.SelectedRows[0].Cells[1].Value = txtTroopInfoName.Text;
            }
        }

        private void btnTroopGoto_Click(object sender, EventArgs e)
        {
            Troop selectedTroop = ctlTroopList.SelectedTroop;
            if (selectedTroop != null)
            {
                if (selectedTroop.AtColony)
                {
                    int_13 = (int)selectedTroop.Colony.Xpos;
                    int_14 = (int)selectedTroop.Colony.Ypos;
                }
                else if (selectedTroop.BuiltObject != null)
                {
                    int_13 = (int)selectedTroop.BuiltObject.Xpos;
                    int_14 = (int)selectedTroop.BuiltObject.Ypos;
                }
                method_149();
                bool_20 = true;
            }
            method_184();
        }

        private void method_272()
        {
        }

        private void method_273()
        {
            lblGodData.Visible = false;
            lblGodData.SendToBack();
        }

        private void method_274()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlEmpireSummary.Size = new Size(1027, 770);
            pnlEmpireSummary.Location = new Point((mainView.Width - pnlEmpireSummary.Width) / 2, (mainView.Height - pnlEmpireSummary.Height) / 2);
            string text = TextResolver.GetText("Empire Summary");
            if (_Game.PlayerEmpire != null)
            {
                text = text + ": " + _Game.PlayerEmpire.Name;
                pnlEmpireSummary.HeaderIcon = _Game.PlayerEmpire.LargeFlagPicture;
            }
            pnlEmpireSummary.HeaderTitle = text;
            pnlEmpireSummary.DoLayout();
            lblEmpireSummaryName.Text = TextResolver.GetText("Name");
            lblEmpireSummaryName.Font = font_2;
            lblEmpireSummaryName.ForeColor = color_1;
            lblEmpireSummaryName.BackColor = Color.Transparent;
            lblEmpireSummaryName.Location = new Point(10, 11);
            lblEmpireSummaryName.Visible = false;
            lblEmpireSummaryName.SendToBack();
            txtEmpireSummaryName.Font = font_7;
            txtEmpireSummaryName.BackColor = Color.FromArgb(48, 48, 64);
            txtEmpireSummaryName.ForeColor = Color.FromArgb(170, 170, 170);
            txtEmpireSummaryName.Size = new Size(308, 20);
            txtEmpireSummaryName.Location = new Point(120, 10);
            txtEmpireSummaryName.BringToFront();
            txtEmpireSummaryName.Text = _Game.PlayerEmpire.Name;
            btnEmpireSummaryShowExpansionPlanner.Visible = false;
            btnEmpireSummaryShowExpansionPlanner.SendToBack();
            btnEmpireSummaryShowEmpirePolicy.Size = new Size(300, 35);
            btnEmpireSummaryShowEmpirePolicy.Location = new Point(700, 10);
            lblEmpireSummaryTESTINGSilvermistTriggerLocation.Location = new Point(470, 10);
            lblEmpireSummaryTESTINGSilvermistTriggerLocation.MaximumSize = new Size(170, 60);
            lblEmpireSummaryTESTINGSilvermistTriggerLocation.BackColor = Color.FromArgb(128, 32, 64);
            string text2 = "SilverMist Trigger Location: ";
            if (_Game != null && _Game.Galaxy != null && _Game.Galaxy.SilverMistCreatureRuinsHabitat != null && !_Game.Galaxy.SilverMistCreatureRuinsHabitat.HasBeenDestroyed)
            {
                text2 = text2 + _Game.Galaxy.SilverMistCreatureRuinsHabitat.Name + " in the ";
                text2 = text2 + Galaxy.DetermineHabitatSystemStar(_Game.Galaxy.SilverMistCreatureRuinsHabitat).Name + " system in ";
                text2 = text2 + "sector " + _Game.Galaxy.ResolveSectorDescription(_Game.Galaxy.SilverMistCreatureRuinsHabitat.Xpos, _Game.Galaxy.SilverMistCreatureRuinsHabitat.Ypos);
            }
            lblEmpireSummaryTESTINGSilvermistTriggerLocation.Text = text2;
            lblEmpireSummaryTESTINGSilvermistTriggerLocation.Visible = false;
            pnlEmpireSummaryColony.Size = new Size(420, 300);
            pnlEmpireSummaryColony.Location = new Point(10, 40);
            pnlEmpireSummaryColony.Ignite(this, _Game.Galaxy, _Game.PlayerEmpire, -1);
            pnlEmpireSummaryColony.BringToFront();
            if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
            {
                cmbEmpireSummaryChangeGovernmentType.Visible = true;
                btnEmpireSummaryChangeGovernment.Visible = true;
                lnkEmpireSummaryGovernmentType.Visible = true;
                cmbEmpireSummaryChangeGovernmentType.BringToFront();
                cmbEmpireSummaryChangeGovernmentType.Size = new Size(155, 21);
                cmbEmpireSummaryChangeGovernmentType.Font = font_6;
                cmbEmpireSummaryChangeGovernmentType.Location = new Point(271, 189);
                cmbEmpireSummaryChangeGovernmentType.Items.Clear();
                cmbEmpireSummaryChangeGovernmentType.Items.Add("(" + TextResolver.GetText("Select government...") + ")");
                for (int i = 0; i < _Game.PlayerEmpire.AllowableGovernmentTypes.Count; i++)
                {
                    GovernmentAttributes governmentAttributes = _Game.Galaxy.Governments[_Game.PlayerEmpire.AllowableGovernmentTypes[i]];
                    if (governmentAttributes != null)
                    {
                        cmbEmpireSummaryChangeGovernmentType.Items.Add(governmentAttributes.Name);
                    }
                }
                cmbEmpireSummaryChangeGovernmentType.SelectedIndex = 0;
                cmbEmpireSummaryChangeGovernmentType.Visible = true;
                btnEmpireSummaryChangeGovernment.Location = new Point(10, 345);
                btnEmpireSummaryChangeGovernment.Size = new Size(420, 35);
                btnEmpireSummaryChangeGovernment.Enabled = true;
                if (_Game.PlayerEmpire.DominantRace != null && !_Game.PlayerEmpire.DominantRace.CanChangeGovernment)
                {
                    btnEmpireSummaryChangeGovernment.Enabled = false;
                }
                lnkEmpireSummaryGovernmentType.BringToFront();
                lnkEmpireSummaryGovernmentType.Location = new Point(271, 164);
                lnkEmpireSummaryGovernmentType.Text = TextResolver.GetText("About this type of Government...");
                lnkEmpireSummaryGovernmentType.BackColor = Color.Transparent;
                cmbEmpireSummaryChangeGovernmentType.BringToFront();
            }
            else
            {
                cmbEmpireSummaryChangeGovernmentType.Visible = false;
                cmbEmpireSummaryChangeGovernmentType.SendToBack();
                btnEmpireSummaryChangeGovernment.Visible = false;
                lnkEmpireSummaryGovernmentType.Visible = false;
            }
            btnEmpireSummaryChangeGovernment.Size = new Size(420, 50);
            pnlEmpireSummaryBuiltObject.Size = new Size(632, 282);
            pnlEmpireSummaryBuiltObject.Location = new Point(368, 412);
            pnlEmpireSummaryBuiltObject.Ignite(this, _Game.Galaxy, _Game.PlayerEmpire);
            pnlEmpireSummaryBuiltObject.BringToFront();
            pnlEmpireSummaryEconomy.Size = new Size(550, 340);
            pnlEmpireSummaryEconomy.Location = new Point(450, 54);
            pnlEmpireSummaryEconomy.Ignite(this, _Game.Galaxy, _Game.PlayerEmpire);
            pnlEmpireSummaryEconomy.BringToFront();
            pnlEmpireSummaryBonus.Size = new Size(350, 282);
            pnlEmpireSummaryBonus.Location = new Point(10, 412);
            pnlEmpireSummaryBonus.Ignite(this, _Game.Galaxy, _Game.PlayerEmpire, characterImageCache_0);
            pnlEmpireSummaryBonus.BringToFront();
            pnlEmpireSummary.Visible = true;
            pnlEmpireSummary.BringToFront();
        }

        private void method_275()
        {
            pnlEmpireSummary.SendToBack();
            pnlEmpireSummary.Visible = false;
            if (pnlEmpirePolicy.Visible)
            {
                method_596();
            }
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void txtEmpireSummaryName_Leave(object sender, EventArgs e)
        {
            if (_Game.PlayerEmpire != null && !string.IsNullOrEmpty(txtEmpireSummaryName.Text.Trim()))
            {
                _Game.PlayerEmpire.Name = txtEmpireSummaryName.Text;
                pnlEmpireSummary.HeaderTitle = TextResolver.GetText("Empire Summary") + ": " + txtEmpireSummaryName.Text;
            }
        }

        private void btnGalaxyMapGoto_Click(object sender, EventArgs e)
        {
            if (habitat_8 != null)
            {
                method_157(habitat_8);
                method_132();
                dateTime_1 = _Game.Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, 60));
            }
        }

        private void tbtnResearch_Click(object sender, EventArgs e)
        {
            if (pnlResearch.Visible)
            {
                method_399();
                return;
            }
            if (_Game.PlayerEmpire.ControlResearch && GenerateAutomationMessageBox(TextResolver.GetText("Research")).Show(this).ToLower(CultureInfo.InvariantCulture) == "off")
            {
                _Game.PlayerEmpire.ControlResearch = false;
            }
            method_393(IndustryType.Weapon);
        }

        private void tbtnDesigns_Click(object sender, EventArgs e)
        {
            if (pnlDesigns.Visible)
            {
                method_308();
            }
            else
            {
                method_307(null);
            }
        }

        private void ctlDesignsList_SelectionChanged(object sender, EventArgs e)
        {
            Design selectedDesign = ctlDesignsList.SelectedDesign;
            ctlDesignComponents.SoundsEnabled = false;
            if (selectedDesign != null)
            {
                ctlDesignComponents.BindData(selectedDesign.Components, bitmap_21);
            }
            else
            {
                ctlDesignComponents.BindData(null, bitmap_21);
            }
            ctlDesignComponents.SoundsEnabled = true;
        }

        private BuiltObjectFleeWhen gxQyEgZoWT(string string_30)
        {
            BuiltObjectFleeWhen result = BuiltObjectFleeWhen.Undefined;
            if (string_30 == Galaxy.ResolveDescription(BuiltObjectFleeWhen.EnemyMilitarySighted))
            {
                result = BuiltObjectFleeWhen.EnemyMilitarySighted;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectFleeWhen.Attacked))
            {
                result = BuiltObjectFleeWhen.Attacked;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectFleeWhen.Shields50))
            {
                result = BuiltObjectFleeWhen.Shields50;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectFleeWhen.Shields20))
            {
                result = BuiltObjectFleeWhen.Shields20;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectFleeWhen.Armor50))
            {
                result = BuiltObjectFleeWhen.Armor50;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectFleeWhen.Never))
            {
                result = BuiltObjectFleeWhen.Never;
            }
            return result;
        }

        private BuiltObjectStance method_276(string string_30)
        {
            BuiltObjectStance result = BuiltObjectStance.Undefined;
            if (string_30 == Galaxy.ResolveDescription(BuiltObjectStance.AttackUnallied))
            {
                result = BuiltObjectStance.AttackUnallied;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectStance.AttackEnemies))
            {
                result = BuiltObjectStance.AttackEnemies;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectStance.AttackIfAttacked))
            {
                result = BuiltObjectStance.AttackIfAttacked;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectStance.DoNotAttack))
            {
                result = BuiltObjectStance.DoNotAttack;
            }
            return result;
        }

        private BattleTactics method_277(string string_30)
        {
            BattleTactics result = BattleTactics.Undefined;
            if (string_30 == Galaxy.ResolveDescription(BattleTactics.Evade))
            {
                result = BattleTactics.Evade;
            }
            else if (string_30 == Galaxy.ResolveDescription(BattleTactics.Standoff))
            {
                result = BattleTactics.Standoff;
            }
            else if (string_30 == Galaxy.ResolveDescription(BattleTactics.AllWeapons))
            {
                result = BattleTactics.AllWeapons;
            }
            else if (string_30 == Galaxy.ResolveDescription(BattleTactics.PointBlank))
            {
                result = BattleTactics.PointBlank;
            }
            return result;
        }

        private bool method_278(string string_30)
        {
            bool result = true;
            if (string_30 == TextResolver.GetText("Auto Retrofit (including advisor suggestions)"))
            {
                result = true;
            }
            else if (string_30 == TextResolver.GetText("Only Retrofit When Manually Ordered"))
            {
                result = false;
            }
            return result;
        }

        private InvasionTactics NtLyzCjnsr(string string_30)
        {
            InvasionTactics result = InvasionTactics.Undefined;
            if (string_30 == Galaxy.ResolveDescription(InvasionTactics.DoNotInvade))
            {
                result = InvasionTactics.DoNotInvade;
            }
            else if (string_30 == Galaxy.ResolveDescription(InvasionTactics.InvadeWhenClear))
            {
                result = InvasionTactics.InvadeWhenClear;
            }
            else if (string_30 == Galaxy.ResolveDescription(InvasionTactics.InvadeImmediately))
            {
                result = InvasionTactics.InvadeImmediately;
            }
            return result;
        }

        private BuiltObjectSubRole zirgUhrfvq(string string_30)
        {
            BuiltObjectSubRole result = BuiltObjectSubRole.Undefined;
            if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.CapitalShip))
            {
                result = BuiltObjectSubRole.CapitalShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.Carrier))
            {
                result = BuiltObjectSubRole.Carrier;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.ColonyShip))
            {
                result = BuiltObjectSubRole.ColonyShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.ConstructionShip))
            {
                result = BuiltObjectSubRole.ConstructionShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.Cruiser))
            {
                result = BuiltObjectSubRole.Cruiser;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.DefensiveBase))
            {
                result = BuiltObjectSubRole.DefensiveBase;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.Destroyer))
            {
                result = BuiltObjectSubRole.Destroyer;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.EnergyResearchStation))
            {
                result = BuiltObjectSubRole.EnergyResearchStation;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.Escort))
            {
                result = BuiltObjectSubRole.Escort;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.ExplorationShip))
            {
                result = BuiltObjectSubRole.ExplorationShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.Frigate))
            {
                result = BuiltObjectSubRole.Frigate;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.GasMiningShip))
            {
                result = BuiltObjectSubRole.GasMiningShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.GasMiningStation))
            {
                result = BuiltObjectSubRole.GasMiningStation;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.GenericBase))
            {
                result = BuiltObjectSubRole.GenericBase;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.HighTechResearchStation))
            {
                result = BuiltObjectSubRole.HighTechResearchStation;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.LargeFreighter))
            {
                result = BuiltObjectSubRole.LargeFreighter;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.LargeSpacePort))
            {
                result = BuiltObjectSubRole.LargeSpacePort;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.MediumFreighter))
            {
                result = BuiltObjectSubRole.MediumFreighter;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.MediumSpacePort))
            {
                result = BuiltObjectSubRole.MediumSpacePort;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.MiningShip))
            {
                result = BuiltObjectSubRole.MiningShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.MiningStation))
            {
                result = BuiltObjectSubRole.MiningStation;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.MonitoringStation))
            {
                result = BuiltObjectSubRole.MonitoringStation;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.PassengerShip))
            {
                result = BuiltObjectSubRole.PassengerShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.ResortBase))
            {
                result = BuiltObjectSubRole.ResortBase;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.ResupplyShip))
            {
                result = BuiltObjectSubRole.ResupplyShip;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.SmallFreighter))
            {
                result = BuiltObjectSubRole.SmallFreighter;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.SmallSpacePort))
            {
                result = BuiltObjectSubRole.SmallSpacePort;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.TroopTransport))
            {
                result = BuiltObjectSubRole.TroopTransport;
            }
            else if (string_30 == Galaxy.ResolveDescription(BuiltObjectSubRole.WeaponsResearchStation))
            {
                result = BuiltObjectSubRole.WeaponsResearchStation;
            }
            return result;
        }

        private int method_279(BuiltObjectSubRole builtObjectSubRole_0)
        {
            int result = 0;
            switch (builtObjectSubRole_0)
            {
                case BuiltObjectSubRole.Undefined:
                    result = -1;
                    break;
                case BuiltObjectSubRole.Escort:
                    result = 0;
                    break;
                case BuiltObjectSubRole.Frigate:
                    result = 1;
                    break;
                case BuiltObjectSubRole.Destroyer:
                    result = 2;
                    break;
                case BuiltObjectSubRole.Cruiser:
                    result = 3;
                    break;
                case BuiltObjectSubRole.CapitalShip:
                    result = 4;
                    break;
                case BuiltObjectSubRole.TroopTransport:
                    result = 5;
                    break;
                case BuiltObjectSubRole.Carrier:
                    result = 6;
                    break;
                case BuiltObjectSubRole.ResupplyShip:
                    result = 7;
                    break;
                case BuiltObjectSubRole.ExplorationShip:
                    result = 8;
                    break;
                case BuiltObjectSubRole.SmallFreighter:
                    result = 9;
                    break;
                case BuiltObjectSubRole.MediumFreighter:
                    result = 10;
                    break;
                case BuiltObjectSubRole.LargeFreighter:
                    result = 11;
                    break;
                case BuiltObjectSubRole.ColonyShip:
                    result = 12;
                    break;
                case BuiltObjectSubRole.PassengerShip:
                    result = 13;
                    break;
                case BuiltObjectSubRole.ConstructionShip:
                    result = 14;
                    break;
                case BuiltObjectSubRole.GasMiningShip:
                    result = 15;
                    break;
                case BuiltObjectSubRole.MiningShip:
                    result = 16;
                    break;
                case BuiltObjectSubRole.GasMiningStation:
                    result = 17;
                    break;
                case BuiltObjectSubRole.MiningStation:
                    result = 18;
                    break;
                case BuiltObjectSubRole.SmallSpacePort:
                    result = 19;
                    break;
                case BuiltObjectSubRole.MediumSpacePort:
                    result = 20;
                    break;
                case BuiltObjectSubRole.LargeSpacePort:
                    result = 21;
                    break;
                case BuiltObjectSubRole.ResortBase:
                    result = 22;
                    break;
                case BuiltObjectSubRole.GenericBase:
                    result = 23;
                    break;
                case BuiltObjectSubRole.EnergyResearchStation:
                    result = 24;
                    break;
                case BuiltObjectSubRole.WeaponsResearchStation:
                    result = 25;
                    break;
                case BuiltObjectSubRole.HighTechResearchStation:
                    result = 26;
                    break;
                case BuiltObjectSubRole.MonitoringStation:
                    result = 27;
                    break;
                case BuiltObjectSubRole.DefensiveBase:
                    result = 28;
                    break;
            }
            return result;
        }

        private int method_280(BuiltObjectStance builtObjectStance_0)
        {
            int result = 0;
            switch (builtObjectStance_0)
            {
                case BuiltObjectStance.Undefined:
                    result = -1;
                    break;
                case BuiltObjectStance.AttackUnallied:
                    result = 0;
                    break;
                case BuiltObjectStance.AttackEnemies:
                    result = 1;
                    break;
                case BuiltObjectStance.AttackIfAttacked:
                    result = 2;
                    break;
                case BuiltObjectStance.DoNotAttack:
                    result = 3;
                    break;
            }
            return result;
        }

        private int method_281(BattleTactics battleTactics_0)
        {
            int result = 0;
            switch (battleTactics_0)
            {
                case BattleTactics.Undefined:
                    result = -1;
                    break;
                case BattleTactics.Evade:
                    result = 0;
                    break;
                case BattleTactics.Standoff:
                    result = 1;
                    break;
                case BattleTactics.AllWeapons:
                    result = 2;
                    break;
                case BattleTactics.PointBlank:
                    result = 3;
                    break;
            }
            return result;
        }

        private int method_282(InvasionTactics invasionTactics_0)
        {
            int result = 0;
            switch (invasionTactics_0)
            {
                case InvasionTactics.Undefined:
                    result = -1;
                    break;
                case InvasionTactics.DoNotInvade:
                    result = 0;
                    break;
                case InvasionTactics.InvadeWhenClear:
                    result = 1;
                    break;
                case InvasionTactics.InvadeImmediately:
                    result = 2;
                    break;
            }
            return result;
        }

        private int method_283(BuiltObjectFleeWhen builtObjectFleeWhen_0)
        {
            int result = 0;
            switch (builtObjectFleeWhen_0)
            {
                case BuiltObjectFleeWhen.Undefined:
                    result = -1;
                    break;
                case BuiltObjectFleeWhen.EnemyMilitarySighted:
                    result = 0;
                    break;
                case BuiltObjectFleeWhen.Attacked:
                    result = 1;
                    break;
                case BuiltObjectFleeWhen.Shields50:
                    result = 2;
                    break;
                case BuiltObjectFleeWhen.Shields20:
                    result = 3;
                    break;
                case BuiltObjectFleeWhen.Never:
                    result = 5;
                    break;
                case BuiltObjectFleeWhen.Armor50:
                    result = 4;
                    break;
            }
            return result;
        }

        private int method_284(bool bool_28)
        {
            //int num = 0;
            if (bool_28)
            {
                return 0;
            }
            return 1;
        }

        private ComponentList method_285(ComponentCategoryType componentCategoryType_0)
        {
            ComponentList componentList = new ComponentList();
            ResearchNodeList techTree = _Game.PlayerEmpire.Research.TechTree;
            for (int i = 0; i < techTree.Count; i++)
            {
                ResearchNode researchNode = techTree[i];
                if (!researchNode.IsResearched || researchNode.Category != componentCategoryType_0 || researchNode.AllowedRaces == null || researchNode.AllowedRaces.Count <= 0)
                {
                    continue;
                }
                if (researchNode.Components != null && researchNode.Components.Count > 0)
                {
                    for (int j = 0; j < researchNode.Components.Count; j++)
                    {
                        if (!componentList.Contains(researchNode.Components[j]))
                        {
                            componentList.Add(researchNode.Components[j]);
                        }
                    }
                }
                if (researchNode.ComponentImprovements == null || researchNode.ComponentImprovements.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < researchNode.ComponentImprovements.Count; k++)
                {
                    if (!componentList.Contains(researchNode.ComponentImprovements[k].ImprovedComponent))
                    {
                        componentList.Add(researchNode.ComponentImprovements[k].ImprovedComponent);
                    }
                }
            }
            return componentList;
        }

        private ComponentList method_286(ComponentType componentType_0)
        {
            ComponentList componentList = new ComponentList();
            ResearchNodeList techTree = _Game.PlayerEmpire.Research.TechTree;
            for (int i = 0; i < techTree.Count; i++)
            {
                ResearchNode researchNode = techTree[i];
                if (!researchNode.IsResearched || researchNode.AllowedRaces == null || researchNode.AllowedRaces.Count <= 0)
                {
                    continue;
                }
                if (researchNode.Components != null && researchNode.Components.Count > 0)
                {
                    for (int j = 0; j < researchNode.Components.Count; j++)
                    {
                        if (researchNode.Components[j].Type == componentType_0 && !componentList.Contains(researchNode.Components[j]))
                        {
                            componentList.Add(researchNode.Components[j]);
                        }
                    }
                }
                if (researchNode.ComponentImprovements == null || researchNode.ComponentImprovements.Count <= 0)
                {
                    continue;
                }
                for (int k = 0; k < researchNode.ComponentImprovements.Count; k++)
                {
                    if (researchNode.ComponentImprovements[k].ImprovedComponent.Type == componentType_0 && !componentList.Contains(researchNode.ComponentImprovements[k].ImprovedComponent))
                    {
                        componentList.Add(researchNode.ComponentImprovements[k].ImprovedComponent);
                    }
                }
            }
            return componentList;
        }

        private ComponentList method_287(ComponentList componentList_0, ComponentList componentList_1)
        {
            for (int i = 0; i < componentList_1.Count; i++)
            {
                if (!componentList_0.Contains(componentList_1[i]))
                {
                    componentList_0.Add(componentList_1[i]);
                }
            }
            return componentList_0;
        }

        private ComponentList method_288(ComponentList componentList_0, ComponentCategoryType componentCategoryType_0)
        {
            ComponentList componentList_ = method_285(componentCategoryType_0);
            componentList_0 = method_287(componentList_0, componentList_);
            return componentList_0;
        }

        private ComponentList method_289(ComponentList componentList_0, ComponentType componentType_0)
        {
            ComponentList componentList_ = method_286(componentType_0);
            componentList_0 = method_287(componentList_0, componentList_);
            return componentList_0;
        }

        private ComponentList Kdxguwronl()
        {
            ComponentList componentList = new ComponentList();
            ComponentType[] array = (ComponentType[])Enum.GetValues(typeof(ComponentType));
            List<ComponentCategoryType> list = new List<ComponentCategoryType>();
            list.Add(ComponentCategoryType.Armor);
            list.Add(ComponentCategoryType.HyperDrive);
            list.Add(ComponentCategoryType.HyperDisrupt);
            list.Add(ComponentCategoryType.Reactor);
            list.Add(ComponentCategoryType.Shields);
            list.Add(ComponentCategoryType.ShieldRecharge);
            list.Add(ComponentCategoryType.WeaponBeam);
            list.Add(ComponentCategoryType.WeaponTorpedo);
            foreach (ComponentCategoryType item in list)
            {
                componentList.AddRange(_Game.PlayerEmpire.Research.GetLatestComponents(item));
                method_288(componentList, item);
            }
            DistantWorlds.Types.Component latestComponent = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.EnergyCollector);
            if (latestComponent != null && !componentList.Contains(latestComponent))
            {
                componentList.Add(latestComponent);
            }
            DistantWorlds.Types.Component latestComponent2 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.EnergyToFuel);
            if (latestComponent2 != null && !componentList.Contains(latestComponent2))
            {
                componentList.Add(latestComponent2);
            }
            DistantWorlds.Types.Component component = _Game.PlayerEmpire.ResolveLatestBombardWeapon();
            if (component != null && !componentList.Contains(component))
            {
                componentList.Add(component);
            }
            DistantWorlds.Types.Component component2 = _Game.PlayerEmpire.ResolveLatestStandardTorpedoWeapon();
            if (component2 != null && !componentList.Contains(component2))
            {
                componentList.Add(component2);
            }
            DistantWorlds.Types.Component component3 = _Game.PlayerEmpire.ResolveLatestMissileWeapon();
            if (component3 != null && !componentList.Contains(component3))
            {
                componentList.Add(component3);
            }
            DistantWorlds.Types.Component latestComponent3 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponIonCannon);
            if (latestComponent3 != null && !componentList.Contains(latestComponent3))
            {
                componentList.Add(latestComponent3);
            }
            DistantWorlds.Types.Component latestComponent4 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponIonPulse);
            if (latestComponent4 != null && !componentList.Contains(latestComponent4))
            {
                componentList.Add(latestComponent4);
            }
            DistantWorlds.Types.Component latestComponent5 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponIonDefense);
            if (latestComponent5 != null && !componentList.Contains(latestComponent5))
            {
                componentList.Add(latestComponent5);
            }
            DistantWorlds.Types.Component latestComponent6 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponTractorBeam);
            if (latestComponent6 != null && !componentList.Contains(latestComponent6))
            {
                componentList.Add(latestComponent6);
            }
            DistantWorlds.Types.Component latestComponent7 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponGravityBeam);
            if (latestComponent7 != null && !componentList.Contains(latestComponent7))
            {
                componentList.Add(latestComponent7);
            }
            DistantWorlds.Types.Component latestComponent8 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponAreaGravity);
            if (latestComponent8 != null && !componentList.Contains(latestComponent8))
            {
                componentList.Add(latestComponent8);
            }
            DistantWorlds.Types.Component latestComponent9 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.WeaponPointDefense);
            if (latestComponent9 != null && !componentList.Contains(latestComponent9))
            {
                componentList.Add(latestComponent9);
            }
            DistantWorlds.Types.Component latestComponent10 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.FighterBay);
            if (latestComponent10 != null && !componentList.Contains(latestComponent10))
            {
                componentList.Add(latestComponent10);
            }
            DistantWorlds.Types.Component latestComponent11 = _Game.PlayerEmpire.Research.GetLatestComponent(ComponentType.AssaultPod);
            if (latestComponent11 != null && !componentList.Contains(latestComponent11))
            {
                componentList.Add(latestComponent11);
            }
            ComponentType[] array2 = array;
            foreach (ComponentType componentType in array2)
            {
                switch (componentType)
                {
                    case ComponentType.WeaponBeam:
                    case ComponentType.WeaponTorpedo:
                    case ComponentType.Armor:
                    case ComponentType.Shields:
                    case ComponentType.HyperDrive:
                    case ComponentType.Reactor:
                        continue;
                }
                if (componentType == ComponentType.Undefined)
                {
                    continue;
                }
                ComponentList latestComponents = _Game.PlayerEmpire.Research.GetLatestComponents(componentType);
                for (int j = 0; j < latestComponents.Count; j++)
                {
                    if (!componentList.Contains(latestComponents[j]))
                    {
                        componentList.Add(latestComponents[j]);
                    }
                }
                method_289(componentList, componentType);
            }
            return componentList;
        }

        private ComponentList method_290()
        {
            ComponentList componentList = new ComponentList();
            componentList.AddRange(_Game.PlayerEmpire.Research.ResearchedComponents);
            return componentList;
        }

        private void method_291(Design design_3)
        {
            method_292(design_3, bool_28: true);
        }

        private void method_292(Design design_3, bool bool_28)
        {
            design_0 = design_3;
            if (bool_28)
            {
                ctlDesignComponentToolbox.SoundsEnabled = false;
                if (chkDesignComponentsShowLatest.Checked)
                {
                    ctlDesignComponentToolbox.BindData(Kdxguwronl(), bitmap_21, _Game.Galaxy);
                }
                else
                {
                    ctlDesignComponentToolbox.BindData(method_290(), bitmap_21, _Game.Galaxy);
                }
                ctlDesignComponentToolbox.SoundsEnabled = true;
            }
            method_385(design_0);
            ctlDesignComponents.SoundsEnabled = false;
            if (design_0 != null)
            {
                int pictureRef = design_0.PictureRef;
                DesignImageScalingMode imageScalingType = design_0.ImageScalingType;
                float imageScalingFactor = design_0.ImageScalingFactor;
                ctlDesignComponents.BindData(design_3.Components, bitmap_21);
                txtDesignName.Text = design_0.Name;
                chkDesignObsolete.Checked = design_0.IsObsolete;
                pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, new List<string>(), new List<string>());
                pnlDesignEnergy.Ignite(_Game.Galaxy, design_0);
                pnlDesignMovement.Ignite(_Game.Galaxy, design_0);
                pnlDesignDefense.Ignite(_Game.Galaxy, design_0);
                pnlDesignIndustry.Ignite(_Game.Galaxy, design_0);
                lblDesignsSizeValue.Text = design_0.Size.ToString();
                cmbDesignsSubRole.SelectedIndex = method_279(design_0.SubRole);
                cmbDesignTacticsStrongerShips.SelectedIndex = method_281(design_0.TacticsStrongerShips);
                cmbDesignTacticsWeakerShips.SelectedIndex = method_281(design_0.TacticsWeakerShips);
                cmbDesignTacticsInvasion.SelectedIndex = method_282(design_0.TacticsInvasion);
                cmbDesignsFleeWhen.SelectedIndex = method_283(design_0.FleeWhen);
                cmbDesignsPicture.SelectedIndex = 0;
                cmbDesignsPicture.SelectedIndex = pictureRef;
                cmbDesignImageScalingMode.SetSelectedScalingMode(imageScalingType);
                numDesignImageScalingAmount.Value = (decimal)imageScalingFactor;
                if (imageScalingType == DesignImageScalingMode.None)
                {
                    numDesignImageScalingAmount.Enabled = false;
                }
                else
                {
                    numDesignImageScalingAmount.Enabled = true;
                }
                cmbDesignDetailAutoRetrofit.SelectedIndex = method_284(design_0.AllowAutoRetrofit);
                ctlDesignWeapons.BindData(design_0.Weapons, bitmap_21);
                lblDesignWeaponFirepowerValue.Text = design_0.FirepowerRaw.ToString();
                string text = design_0.MinimumWeaponsRange + "/" + design_0.MaximumWeaponsRange;
                lblDesignWeaponRangeMinimumValue.Text = text;
                string text2 = "(" + TextResolver.GetText("None") + ")";
                int num = 0;
                if (design_0.FighterCapacity > 0)
                {
                    num = design_0.FighterCapacity / 10;
                    text2 = num.ToString();
                }
                int num2 = 0;
                if (_Game.PlayerEmpire != null && _Game.PlayerEmpire.Research != null)
                {
                    FighterSpecification fighterSpecification = _Game.PlayerEmpire.Research.IdentifyLatestBomberSpecification();
                    FighterSpecification fighterSpecification2 = _Game.PlayerEmpire.Research.IdentifyLatestFighterSpecification();
                    int num3 = 0;
                    int num4 = 0;
                    int num5 = 0;
                    if (fighterSpecification != null)
                    {
                        num3 = fighterSpecification.WeaponDamage;
                        num5++;
                    }
                    if (fighterSpecification2 != null)
                    {
                        num4 += fighterSpecification2.WeaponDamage;
                        num5++;
                    }
                    if (num5 > 0)
                    {
                        int num6 = num / num5;
                        if (fighterSpecification != null)
                        {
                            num2 += num3 * num6;
                        }
                        if (fighterSpecification2 != null)
                        {
                            num2 += num4 * num6;
                        }
                    }
                }
                lblDesignWeaponRangeMaximumValue.Text = num2.ToString();
                lblDesignWeaponFightersValue.Text = text2;
                string text3 = "(" + TextResolver.GetText("None") + ")";
                if (design_0.TargettingModifier > 0)
                {
                    text3 = "+" + design_0.TargettingModifier + "%";
                }
                lblDesignWeaponTargettingValue.Text = text3;
                string text4 = "(" + TextResolver.GetText("None") + ")";
                if (design_0.FleetTargettingModifier > 0)
                {
                    text4 = "+" + design_0.FleetTargettingModifier + "%";
                }
                lblDesignWeaponFleetTargettingValue.Text = text4;
                lblDesignWeaponTroopCapacityValue.Text = design_0.TroopCapacity.ToString();
                lblDesignWeaponBoardingAssaultValue.Text = design_0.CalculateBoardingAssaultValue(_Game.Galaxy.PlayerEmpire.DominantRace).ToString();
                lblDesignWeaponHyperDenyValue.Text = design_0.WeaponHyperDenyRange.ToString();
                lblDesignWeaponHyperStopValue.Text = design_0.HyperStopRange.ToString();
                int num7 = 0;
                if (design_0.Weapons != null)
                {
                    for (int i = 0; i < design_0.Weapons.Count; i++)
                    {
                        if (design_0.Weapons[i].Component.Type == ComponentType.WeaponPointDefense)
                        {
                            num7 += design_0.Weapons[i].RawDamage;
                        }
                    }
                }
                lblDesignWeaponPointDefenseValue.Text = num7.ToString();
                lblDesignWeaponBombardPowerValue.Text = design_0.BombardPower.ToString();
                lblDesignWeaponTotalEnergyValue.Text = Design.CalculateTotalWeaponsEnergyUsePerSecond(design_0, _Game.PlayerEmpire.Research).ToString("0");
                List<string> list_ = new List<string>();
                List<string> list_2 = new List<string>();
                GetDesignWarningMessages(design_0, out list_, out list_2);
                pnlDesignWarnings.Ignite(_Game.Galaxy, design_0, list_, list_2);
            }
            else
            {
                ctlDesignComponents.BindData(null, bitmap_21);
                txtDesignName.Text = string.Empty;
                chkDesignObsolete.Checked = false;
                pnlDesignWarnings.Ignite(_Game.Galaxy, null, new List<string>(), new List<string>());
                pnlDesignEnergy.Ignite(_Game.Galaxy, null);
                pnlDesignMovement.Ignite(_Game.Galaxy, null);
                pnlDesignDefense.Ignite(_Game.Galaxy, null);
                pnlDesignIndustry.Ignite(_Game.Galaxy, null);
                lblDesignsSizeValue.Text = "0";
                cmbDesignsSubRole.SelectedIndex = 0;
                cmbDesignTacticsStrongerShips.SelectedIndex = 0;
                cmbDesignTacticsWeakerShips.SelectedIndex = 0;
                cmbDesignTacticsInvasion.SelectedIndex = 0;
                cmbDesignsFleeWhen.SelectedIndex = 0;
                cmbDesignsPicture.SelectedIndex = 0;
                picDesignPicture.Image = null;
                ctlDesignWeapons.BindData(null, bitmap_21);
                lblDesignWeaponFirepowerValue.Text = "0";
                lblDesignWeaponTargettingValue.Text = "0";
                lblDesignWeaponFleetTargettingValue.Text = "0";
                lblDesignWeaponFightersValue.Text = "(" + TextResolver.GetText("None") + ")";
                lblDesignWeaponTroopCapacityValue.Text = "0";
                lblDesignWeaponBoardingAssaultValue.Text = "0";
                lblDesignWeaponHyperDenyValue.Text = "0";
                lblDesignWeaponHyperStopValue.Text = "0";
                lblDesignWeaponPointDefenseValue.Text = "0";
                lblDesignWeaponBombardPowerValue.Text = "0";
                lblDesignWeaponTotalEnergyValue.Text = "0";
            }
            ctlDesignComponents.SoundsEnabled = true;
        }


  }

}