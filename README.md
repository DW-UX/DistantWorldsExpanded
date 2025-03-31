# Notes:
Expansion mod shortened to EM and Bacon mod to BM

# Installation:
## First time:
  
1.Unblock zip after download (file properties -> button or [checkbox](https://github.com/user-attachments/assets/e23b50d8-0740-41a3-97e7-3f8e555fcbe6) ) or don't use Windows built-in unpacking as it preserves alternate stream which block game from loading dll

2.Copy "Full" folder to game root folder

Updating to newer version of EM:
1.Copy all files from "Full" folder if you don't care about losing your settings
Or
2.Follow latest update instructions on changes in files to preserve your settings.


# Changes in BM:
## Assign mission:
assign passenger\cargo\mining now use UI instead of hotkeys to set target\
destination. Alt+1,2,3 and Alt+M now obsolete and changed to assign missions (see hotkeys in EM)
## Commands:
Ship\bases\planets name search and ! commands now case insensitive
## Repair order:
see “Repair templates” in new EM features.
## BacontSettings.txt:
EM mod support both . and , deciaml separators in file.

# New features in EM:
## Hotkeys:
all available original and BM hotkeys can now be reassigned in Options → Hotkeys
button. Hotkeys in start menu not available for now.
If you Alt-Tab from game then first hotkey combination will be ignored (Known bug)
## x64:
Latest stable version EM have support of x64 and x86 platforms in single exe. Newer beta version may drop x86 support

## Game startup speed:
EM mod massively improved startup time. Approximate loading time on myPC:

• with DWUR graphic mod:
EM - ~30s
BM - ~220s
Original game - ~240s

• No graphic mods:
EM - ~16-18s
BM - ~52s
Original game - ~47s

## UPS timer:
shows update per second done in game. Used by author to look for game features that massively drops performance. Located near current money (upper right corner).
## Energy collection:
RMB menu now contains “Energy colletion” option to show energy collected in selected point with current tech level

## Repair templates:
BM used random to select component to repair. EM allows setting order for repair for component types. This affects both crew and repair module. New templates could be added in designer page “Select repair priority” button. Player can adjust default templates for player and AI empires in “AdvMods\ExpansionMod\RepairPriorityTemplates.json” file. File support default and user template names created later. If file contains unknown template name then ships will use “Original” template until this template created.

Possible default template names:
1. Original – original game random repair
2. Default – default order, could be viewed in editor. No editing of repair order for this
template.

To reset default templates for empires use this commands in BM ShipFinder window (default
Ctrl+E):

1. !ResetAllRepairTemplates – to reset AI and player designs to respective templates.
2. !ResetAiRepairTemplates – to reset AI only empir designs
3. !ResetPlayerRepairTemplates – to reset player only designs

## Removed limit on various txt file:
1. ShipSets – removed limit of maximum of 50 shipsets. Consecutive numbering not required
now.
2. Race families – removed limit of maximum of 50.
3. Plagues – removed limit of maximum of 50.
4. Governments – removed limit of maximum of 60.
5. Facilities – removed limit of maximum of 100.
6. Resources – removed limit of maximum of 80.
7. Components – removed limit of maximum of 500.
8. Research – removed limit of maximum of 1500.
9. Fighters – removed limit of maximum of 50.

## Resource filter in planner:
player can now set minimal % of selected resource to show. All resources" filter out total % of resources, selected filter by selected resource. Added resource rarity column in planner (C - common, R - rare, VR - very rare)
## Construction ship queue editor:
now player can edit queue to swap order or removing missions.
## Design:
adding new warning to design window. Currently implemented warning about above\below count of miners on ship and bases.

# Latest beta version changes:
1. Some UI change in start menu, adapts to resolution. Work in progres, have problem with checkboxes
2. Bug fix for freeze during game launch
3. Bug with music, only first song plays, problems with pause\continue. Possibly fixed in latest version
4. State Tax upper limit set to 75% for Ai and player
5. New tax algoritm, you can set dessired happiness bassed on colony size (“AdvMods\ExpansionMod\RepairPriorityTemplates.json”)
6. Fixed scraping AI planetary facility playing as pirate  (you could scrap any facility after gaining 1% of control on planet)
7. Game editor can now colonize asteroid, still can't change pop for them
8. Increased year limit for victory condition start and end.
9. Support for multiple mods at same time. New file structure, all files converted to SQLite database on game start. Modders can adjust patch files to change whatever they need, easier to make massive changes with SQL commands. Mooder need knowladge of SQL to make changes in new structure instead of txt file.
  - Convertion tool to convert txt files to new structure
  - New structure removes all limits on maximum count for various thing that original game have. (Government adjectives, resource needed for component, etc) 
