using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.HotKeyMapping
{
    internal class MappingJsonFileModel
    {
        public int FormatVersion { get; set; }
        /// <summary>
        /// backspace
        /// </summary>
        public string ZoomToSelected { get; set; }
        //public string Escape { get; set; }
        public string ToglePause { get; set; }
        /// <summary>
        /// end
        /// </summary>
        public string ZoomToGalaxyLevel { get; set; }
        /// <summary>
        /// home
        /// </summary>
        public string ZoomInMaximumLevel { get; set; }
        /// <summary>
        /// insert
        /// </summary>
        public string ZoomToSystemLevel { get; set; }
        /// <summary>
        /// delete
        /// </summary>
        public string ZoomToSectorLevel { get; set; }
        public string ControlGroup0 { get; set; }
        public string ControlGroup1 { get; set; }
        public string ControlGroup2 { get; set; }
        public string ControlGroup3 { get; set; }
        public string ControlGroup4 { get; set; }
        public string ControlGroup5 { get; set; }
        public string ControlGroup6 { get; set; }
        public string ControlGroup7 { get; set; }
        public string ControlGroup8 { get; set; }
        public string ControlGroup9 { get; set; }
        /// <summary>
        /// A
        /// </summary>
        public string EnableAuto { get; set; }
        /// <summary>
        /// B
        /// </summary>
        public string CycleSelectionBackward { get; set; }
        /// <summary>
        /// C
        /// Control modifier probably fake1
        /// </summary>
        public string CycleColonySelectionBackward { get; set; }
        /// <summary>
        /// D
        /// </summary>
        public string CycleMainDisplayTypes { get; set; }
        /// <summary>
        /// E
        /// </summary>
        public string ShipEscapeCommand { get; set; }
        /// <summary>
        /// F
        /// Control modifier probably fake1
        /// </summary>
        public string CycleFleet { get; set; }
        /// <summary>
        /// G
        /// </summary>
        public string OpenGalaxyMap { get; set; }
        /// <summary>
        /// H
        /// </summary>
        public string OpenMessageHistory { get; set; }
        /// <summary>
        /// I
        /// Control modifier probably fake1
        /// </summary>
        public string CycleIdleShip { get; set; }
        /// <summary>
        /// L
        /// </summary>
        public string ToggleViewLock { get; set; }
        /// <summary>
        /// M
        /// Control modifier probably fake1
        /// </summary>
        public string CycleMilitaryShip { get; set; }
        /// <summary>
        /// N
        /// </summary>
        public string CycleSelectionForward { get; set; }
        /// <summary>
        /// O
        /// </summary>
        public string OpenOptions { get; set; }
        /// <summary>
        /// P
        /// Control modifier probably fake1
        /// </summary>
        public string CycleBases { get; set; }
        /// <summary>
        /// R
        /// </summary>
        public string RefuelShip { get; set; }
        /// <summary>
        /// S
        /// Control modifier Obsolete\Redefined Bacon SaveAs
        /// </summary>
        public string StopShip { get; set; }
        /// <summary>
        /// T
        /// </summary>
        public string CyclePanelVisibility { get; set; }
        /// <summary>
        /// V
        /// </summary>
        public string OpenEmpireComparison { get; set; }
        /// <summary>
        /// X
        ///  Control modifier probably fake1
        /// </summary>
        public string CycleColonyOrExplorer { get; set; }
        /// <summary>
        /// Y
        ///  Control modifier probably fake1
        /// </summary>
        public string CycleConstractionShip { get; set; }
        /// <summary>
        /// Z
        /// </summary>
        public string FindNearestMilitaryShip { get; set; }
        /// <summary>
        /// F1
        /// </summary>
        public string OpenHelp { get; set; }
        /// <summary>
        /// F2
        /// </summary>
        public string OpenColoniesScreen{ get; set; }
        /// <summary>
        /// F3
        /// </summary>
        public string OpenExpansionPlannerScreen { get; set; }
        /// <summary>
        /// F4
        /// </summary>
        public string OpenIntelligenceAgentsScreen { get; set; }
        /// <summary>
        /// F5
        /// </summary>
        public string OpenDiplomacyScreen { get; set; }
        /// <summary>
        /// F6
        /// </summary>
        public string OpenEmpireSummaryScreen { get; set; }
        /// <summary>
        /// F7
        /// </summary>
        public string OpenResearchScreen { get; set; }
        /// <summary>
        /// F8
        /// </summary>
        public string OpenDesignsScreen { get; set; }
        /// <summary>
        /// F9
        /// </summary>
        public string OpenBuildOrderScreen { get; set; }
        /// <summary>
        /// F10
        /// </summary>
        public string OpenConstructionYardsScreen { get; set; }
        /// <summary>
        /// F11
        /// </summary>
        public string OpenShipAndBasesScreen { get; set; }
        /// <summary>
        /// F12
        /// </summary>
        public string OpenFleetScreen { get; set; }
        /// <summary>
        /// { OemOpenBrackets
        /// </summary>
        public string OpenGroundInvasionStatusScreen { get; set; }
        /// <summary>
        /// Add and Oemplus
        /// </summary>
        public string IncreaseGameSpeed { get; set; }
        /// <summary>
        /// Oemcomma
        /// </summary>
        public string CycleShipEngagmentRange { get; set; }
        /// <summary>
        /// Minus and OemMinus
        /// </summary>
        public string DecreaseGameSpeed { get; set; }
    }
}
