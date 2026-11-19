# Helpers for driving the test copy of the game. Dot-source this file.
#   . .\tools\testrun.ps1        (from the repo root)
# Paths come from this file's location: the test copy of the game is a sibling folder of the repo.
$script:Repo = Split-Path $PSScriptRoot -Parent
$script:TestDir = Join-Path (Split-Path $script:Repo -Parent) 'DWU-UiScaleTest'
$script:ShotDir = Join-Path $PSScriptRoot 'shots'

Add-Type -AssemblyName System.Drawing, System.Windows.Forms
if (-not ('DwuTest.Native' -as [type])) {
    Add-Type -Namespace DwuTest -Name Native -MemberDefinition @'
[DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
[DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint pid);
[DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
[DllImport("user32.dll")] public static extern IntPtr WindowFromPoint(POINT p);
[StructLayout(LayoutKind.Sequential)] public struct POINT { public int X, Y; }
[DllImport("user32.dll")] public static extern void mouse_event(uint flags, uint dx, uint dy, uint data, UIntPtr extra);
[DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);
[StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left, Top, Right, Bottom; }
'@
}

function Get-Game { Get-Process DistantWorlds -ErrorAction SilentlyContinue | Where-Object { $_.Path -like "$TestDir*" } }

# Copies the freshly built DLL into the test folder.
function Update-TestBuild {
    Copy-Item "$Repo\DistantWorldsExpanded\bin\Release\net9.0-windows\win-x64\DistantWorlds.dll" "$TestDir\DistantWorlds.dll" -Force
}

function Set-TestScale([double]$Factor) {
    $dir = Join-Path ([Environment]::GetFolderPath('Personal')) 'My Games\Distant Worlds Universe'
    New-Item -ItemType Directory -Force $dir | Out-Null
    Set-Content (Join-Path $dir 'UiScale.txt') $Factor.ToString([Globalization.CultureInfo]::InvariantCulture) -Encoding ascii
}

function Start-Game {
    if (Get-Game) { return }
    Start-Process "$TestDir\DistantWorlds.exe" -WorkingDirectory $TestDir
}

function Stop-Game { Get-Game | Stop-Process -Force }

# True when the foreground window belongs to the test copy of the game.
function Test-GameForeground {
    $pid2 = 0
    [void][DwuTest.Native]::GetWindowThreadProcessId([DwuTest.Native]::GetForegroundWindow(), [ref]$pid2)
    $game = Get-Game
    return [bool]($game -and ($game.Id -contains $pid2))
}

function Get-GameWindowRect {
    $rect = New-Object DwuTest.Native+RECT
    [void][DwuTest.Native]::GetWindowRect([DwuTest.Native]::GetForegroundWindow(), [ref]$rect)
    return $rect
}

# Saves a screenshot of the primary screen (optionally a region, optionally downscaled).
function Save-Shot([string]$Name, [int[]]$Region, [double]$Scale = 1.0) {
    New-Item -ItemType Directory -Force $ShotDir | Out-Null
    $bounds = [Windows.Forms.Screen]::PrimaryScreen.Bounds
    if ($Region) { $bounds = New-Object Drawing.Rectangle $Region[0], $Region[1], $Region[2], $Region[3] }
    $bitmap = New-Object Drawing.Bitmap $bounds.Width, $bounds.Height
    $graphics = [Drawing.Graphics]::FromImage($bitmap)
    $graphics.CopyFromScreen($bounds.Location, [Drawing.Point]::Empty, $bounds.Size)
    $graphics.Dispose()
    if ($Scale -ne 1.0) {
        $small = New-Object Drawing.Bitmap ([int]($bounds.Width * $Scale)), ([int]($bounds.Height * $Scale))
        $g2 = [Drawing.Graphics]::FromImage($small)
        $g2.InterpolationMode = 'HighQualityBicubic'
        $g2.DrawImage($bitmap, 0, 0, $small.Width, $small.Height)
        $g2.Dispose(); $bitmap.Dispose(); $bitmap = $small
    }
    $path = Join-Path $ShotDir "$Name.png"
    $bitmap.Save($path, [Drawing.Imaging.ImageFormat]::Png)
    $bitmap.Dispose()
    return $path
}

# Clicks at screen coordinates, but only while the game is the foreground window.
function Send-Click([int]$X, [int]$Y) {
    if (-not (Test-PointIsGame $X $Y)) { throw 'that point is not on the game window; not clicking' }
    [void][DwuTest.Native]::SetCursorPos($X, $Y)
    Start-Sleep -Milliseconds 120
    [DwuTest.Native]::mouse_event(2, 0, 0, 0, [UIntPtr]::Zero)
    Start-Sleep -Milliseconds 60
    [DwuTest.Native]::mouse_event(4, 0, 0, 0, [UIntPtr]::Zero)
}

function Send-GameKeys([string]$Keys) {
    if (-not (Test-GameForeground)) { throw 'game is not the foreground window; not typing' }
    [Windows.Forms.SendKeys]::SendWait($Keys)
}

# Brings the game's main window to the front.
function Focus-Game {
    $game = Get-Game | Select-Object -First 1
    if ($game -and $game.MainWindowHandle -ne [IntPtr]::Zero) {
        [void][DwuTest.Native]::SetForegroundWindow($game.MainWindowHandle)
        Start-Sleep -Milliseconds 400
    }
    return (Test-GameForeground)
}

# True when the window at the given screen point belongs to the test copy of the game.
function Test-PointIsGame([int]$X, [int]$Y) {
    $pid2 = 0
    $point = New-Object DwuTest.Native+POINT
    $point.X = $X; $point.Y = $Y
    $window = [DwuTest.Native]::WindowFromPoint($point)
    [void][DwuTest.Native]::GetWindowThreadProcessId($window, [ref]$pid2)
    $game = Get-Game
    return [bool]($game -and ($game.Id -contains $pid2))
}
# Builds the game the way the release is built. Returns $true on success.
function Build-Game {
    Push-Location $Repo
    [Environment]::CurrentDirectory = $Repo
    dotnet build DistantWorldsExpanded\DistantWorldsExpanded.csproj -c Release -r win-x64 --no-self-contained -v q -nologo -clp:"ErrorsOnly;Summary" > "$env:TEMP\dwx_build.log" 2>&1
    $ok = $LASTEXITCODE -eq 0
    Get-Content "$env:TEMP\dwx_build.log" | Select-String ": error " | Select-Object -First 10 | ForEach-Object { ($_.Line -replace [regex]::Escape("$Repo\"),'' -replace ' \[[A-Za-z]:.*$','') } | Write-Host
    Pop-Location
    if ($ok) { Update-TestBuild }
    return $ok
}

# Launches at 175% and clicks through to a running introductory game (2560x1440 screen).
function Enter-Game175 {
    Stop-Game; Start-Sleep 2
    Set-TestScale 1.75
    Start-Game; Start-Sleep 28
    Send-Click 2100 136; Start-Sleep 2     # close Galactopedia
    Send-Click 1280 513; Start-Sleep 4     # Start New Game
    Send-Click 1280 330; Start-Sleep 50    # Introductory Game
    Send-Click 1280 1250; Start-Sleep 4    # Start Playing
    Send-Click 1250 40; Start-Sleep 1      # give the game keyboard focus
    Send-GameKeys ' '; Start-Sleep 1       # pause, so random events do not pop up mid-test
}

# ---- Driving the game behind a locked screen: capture its window directly and post it messages.
if (-not ('DwuTest.Bg' -as [type])) {
    Add-Type -Namespace DwuTest -Name Bg -MemberDefinition @"
[DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdc, uint flags);
[DllImport("user32.dll")] public static extern bool PostMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
[DllImport("user32.dll")] public static extern IntPtr SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);
[DllImport("user32.dll")] public static extern bool ScreenToClient(IntPtr hwnd, ref POINT p);
[DllImport("user32.dll")] public static extern bool ClientToScreen(IntPtr hwnd, ref POINT p);
[DllImport("user32.dll")] public static extern IntPtr ChildWindowFromPointEx(IntPtr parent, POINT p, uint flags);
[DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);
[DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool GetGUIThreadInfo(uint thread, ref GUITHREADINFO info);
[DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint pid);
[DllImport("user32.dll")] public static extern bool EnumWindows(EnumProc cb, IntPtr l);
public delegate bool EnumProc(IntPtr h, IntPtr l);
[StructLayout(LayoutKind.Sequential)] public struct POINT { public int X, Y; }
[StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left, Top, Right, Bottom; }
[StructLayout(LayoutKind.Sequential)] public struct GUITHREADINFO { public int cbSize; public int flags; public IntPtr hwndActive, hwndFocus, hwndCapture, hwndMenuOwner, hwndMoveSize, hwndCaret; public RECT rcCaret; }
// Visible top-level windows of a process, topmost first.
public static System.Collections.Generic.List<IntPtr> TopWindows(uint pid) {
    var list = new System.Collections.Generic.List<IntPtr>();
    EnumWindows((h, l) => { uint p; GetWindowThreadProcessId(h, out p); if (p == pid && IsWindowVisible(h)) list.Add(h); return true; }, IntPtr.Zero);
    return list;
}
"@
}

function Get-GameWindows { $g = Get-Game | Select-Object -First 1; if ($g) { [DwuTest.Bg]::TopWindows([uint32]$g.Id) } }

# Captures one of the game's windows (default: its main window) without needing it on screen.
function Save-WinShot([string]$Name, [double]$Scale = 1.0, [int[]]$Region, [IntPtr]$Window = [IntPtr]::Zero) {
    New-Item -ItemType Directory -Force $ShotDir | Out-Null
    if ($Window -eq [IntPtr]::Zero) { $Window = (Get-Game | Select-Object -First 1).MainWindowHandle }
    $r = New-Object DwuTest.Bg+RECT
    [void][DwuTest.Bg]::GetWindowRect($Window, [ref]$r)
    $w = $r.Right - $r.Left; $h = $r.Bottom - $r.Top
    $bitmap = New-Object Drawing.Bitmap $w, $h
    $g = [Drawing.Graphics]::FromImage($bitmap)
    $hdc = $g.GetHdc()
    [void][DwuTest.Bg]::PrintWindow($Window, $hdc, 2)
    $g.ReleaseHdc($hdc); $g.Dispose()
    if ($Region) {
        $crop = $bitmap.Clone((New-Object Drawing.Rectangle $Region[0], $Region[1], $Region[2], $Region[3]), $bitmap.PixelFormat)
        $bitmap.Dispose(); $bitmap = $crop
    }
    if ($Scale -ne 1.0) {
        $small = New-Object Drawing.Bitmap ([int]($bitmap.Width * $Scale)), ([int]($bitmap.Height * $Scale))
        $g2 = [Drawing.Graphics]::FromImage($small); $g2.InterpolationMode = 'HighQualityBicubic'
        $g2.DrawImage($bitmap, 0, 0, $small.Width, $small.Height); $g2.Dispose(); $bitmap.Dispose(); $bitmap = $small
    }
    $path = Join-Path $ShotDir "$Name.png"
    $bitmap.Save($path, [Drawing.Imaging.ImageFormat]::Png); $bitmap.Dispose()
    return $path
}

. "$PSScriptRoot\testrun.bg.ps1"

