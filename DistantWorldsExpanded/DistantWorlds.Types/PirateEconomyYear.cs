// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PirateEconomyYear
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PirateEconomyYear
  {
    private long _YearStartDate;
    private double _ProtectionAgreementIncome;
    private double _MiningIncome;
    private double _LootingIncome;
    private double _MissionIncome;
    private double _SellInfoIncome;
    private double _ControlColonyIncome;
    private double _SmugglingIncome;
    private double _ScrapCapturedShipIncome;
    private double _ResortIncome;
    private double _OtherIncome;
    private double _ShipMaintenanceExpenses;
    private double _ConstructionExpenses;
    private double _PurchaseResourcesExpenses;
    private double _CrashResearchExpenses;
    private double _FacilityConstructionExpenses;
    private double _FuelExpenses;
    private double _OtherExpenses;

    public PirateEconomyYear(long starDate) => this._YearStartDate = Galaxy.CalculateStartOfYear(starDate);

    public long YearStartDate => this._YearStartDate;

    public double TotalIncome => this._ProtectionAgreementIncome + this._MiningIncome + this._LootingIncome + this._MissionIncome + this._SellInfoIncome + this._ControlColonyIncome + this._SmugglingIncome + this._ScrapCapturedShipIncome + this._ResortIncome + this._OtherIncome;

    public double TotalExpenses => this._ShipMaintenanceExpenses + this._ConstructionExpenses + this._PurchaseResourcesExpenses + this._CrashResearchExpenses + this._FacilityConstructionExpenses + this._FuelExpenses + this._OtherExpenses;

    public double StableIncome => this._ProtectionAgreementIncome + this._ControlColonyIncome;

    public double StableExpenses => this._ShipMaintenanceExpenses;

    public double BonusIncome => this._MiningIncome + this._LootingIncome + this._MissionIncome + this._SellInfoIncome + this._SmugglingIncome + this._ScrapCapturedShipIncome + this._ResortIncome + this._OtherIncome;

    public double StableCashflow => this.StableIncome - this.StableExpenses;

    public double TotalCashflow => this.TotalIncome - this.TotalExpenses;

    public void PerformIncome(double amount, PirateIncomeType type)
    {
      switch (type)
      {
        case PirateIncomeType.Undefined:
          this._OtherIncome += amount;
          break;
        case PirateIncomeType.ProtectionAgreement:
          this._ProtectionAgreementIncome += amount;
          break;
        case PirateIncomeType.Mining:
          this._MiningIncome += amount;
          break;
        case PirateIncomeType.Looting:
          this._LootingIncome += amount;
          break;
        case PirateIncomeType.Missions:
          this._MissionIncome += amount;
          break;
        case PirateIncomeType.SellInfo:
          this._SellInfoIncome += amount;
          break;
        case PirateIncomeType.ControlColony:
          this._ControlColonyIncome += amount;
          break;
        case PirateIncomeType.Smuggling:
          this._SmugglingIncome += amount;
          break;
        case PirateIncomeType.ScrapCapturedShips:
          this._ScrapCapturedShipIncome += amount;
          break;
        case PirateIncomeType.Resort:
          this._ResortIncome += amount;
          break;
      }
    }

    public void PerformExpense(double amount, PirateExpenseType type)
    {
      switch (type)
      {
        case PirateExpenseType.Undefined:
          this._OtherExpenses += amount;
          break;
        case PirateExpenseType.ShipMaintenance:
          this._ShipMaintenanceExpenses += amount;
          break;
        case PirateExpenseType.Construction:
          this._ConstructionExpenses += amount;
          break;
        case PirateExpenseType.PurchaseResources:
          this._PurchaseResourcesExpenses += amount;
          break;
        case PirateExpenseType.CrashResearch:
          this._CrashResearchExpenses += amount;
          break;
        case PirateExpenseType.FacilityConstruction:
          this._FacilityConstructionExpenses += amount;
          break;
        case PirateExpenseType.Fuel:
          this._FuelExpenses += amount;
          break;
      }
    }

    public double ProtectionAgreementIncome => this._ProtectionAgreementIncome;

    public double MiningIncome => this._MiningIncome;

    public double LootingIncome => this._LootingIncome;

    public double MissionIncome => this._MissionIncome;

    public double SellInfoIncome => this._SellInfoIncome;

    public double ControlColonyIncome => this._ControlColonyIncome;

    public double SmugglingIncome => this._SmugglingIncome;

    public double ScrapCapturedShipIncome => this._ScrapCapturedShipIncome;

    public double ResortIncome => this._ResortIncome;

    public double OtherIncome => this._OtherIncome;

    public double ShipMaintenanceExpenses => this._ShipMaintenanceExpenses;

    public double ConstructionExpenses => this._ConstructionExpenses;

    public double PurchaseResourcesExpenses => this._PurchaseResourcesExpenses;

    public double CrashResearchExpenses => this._CrashResearchExpenses;

    public double FacilityConstructionExpenses => this._FacilityConstructionExpenses;

    public double FuelExpenses => this._FuelExpenses;

    public double OtherExpenses => this._OtherExpenses;
  }
}
