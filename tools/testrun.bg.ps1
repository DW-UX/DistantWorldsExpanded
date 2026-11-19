# Background driving of the test copy: works while the PC is locked, because it captures the
# game's windows directly and posts them messages instead of using the real screen and mouse.
# Dot-sourced by testrun.ps1; replaces Send-Click, Send-GameKeys and Save-Shot.

if (-not ('DwuTest.Bg2' -as [type])) {
    Add-Type -Namespace DwuTest -Name Bg2 -MemberDefinition @"
[DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr hwnd, IntPtr after, int x, int y, int cx, int cy, uint flags);
[DllImport("user32.dll", CharSet = CharSet.Unicode)] public static extern int GetClassName(IntPtr hwnd, System.Text.StringBuilder name, int max);
"@
}

# Windows are moved this far down, below every monitor, so the lock screen is not over them
# and their controls hit-test as themselves.
$script:ParkOffset = 3000

function Get-WinRect([IntPtr]$h) {
    $r = New-Object DwuTest.Bg+RECT
    [void][DwuTest.Bg]::GetWindowRect($h, [ref]$r)
    return $r
}

function Park-Game {
    foreach ($h in (Get-GameWindows)) {
        $r = Get-WinRect $h
        if ($r.Top -lt 2000) {
            [void][DwuTest.Bg2]::SetWindowPos($h, [IntPtr]::Zero, $r.Left, $r.Top + $ParkOffset, 0, 0, 0x0015)
        }
    }
    # Dialogs that opened on another monitor are moved over the main window, so one
    # coordinate space covers everything.
    $o = Get-GameOrigin
    if ($o -eq $null) { return }
    foreach ($h in (Get-GameWindows)) {
        $r = Get-WinRect $h
        $w = $r.Right - $r.Left
        $hgt = $r.Bottom - $r.Top
        if ($w -le 0 -or $hgt -le 0 -or ($w -eq ($o.Right - $o.Left) -and $hgt -eq ($o.Bottom - $o.Top))) { continue }
        if ($r.Left -lt $o.Left -or $r.Right -gt $o.Right -or $r.Top -lt $o.Top -or $r.Bottom -gt $o.Bottom) {
            $x = $o.Left + [int](($o.Right - $o.Left - $w) / 2)
            $y = $o.Top + [int](($o.Bottom - $o.Top - $hgt) / 2)
            [void][DwuTest.Bg2]::SetWindowPos($h, [IntPtr]::Zero, $x, $y, 0, 0, 0x0015)
        }
    }
}

function Unpark-Game {
    foreach ($h in (Get-GameWindows)) {
        $r = Get-WinRect $h
        if ($r.Top -ge 2000) {
            [void][DwuTest.Bg2]::SetWindowPos($h, [IntPtr]::Zero, $r.Left, $r.Top - $ParkOffset, 0, 0, 0x0015)
        }
    }
}

# The rectangle of the game's largest window; test coordinates are relative to its top-left corner.
function Get-GameOrigin {
    $best = $null
    $area = 0
    foreach ($h in (Get-GameWindows)) {
        $r = Get-WinRect $h
        $a = ($r.Right - $r.Left) * ($r.Bottom - $r.Top)
        if ($a -gt $area) { $area = $a; $best = $r }
    }
    return $best
}

function Find-GameTarget([int]$sx, [int]$sy) {
    foreach ($h in (Get-GameWindows)) {
        $r = Get-WinRect $h
        if ($sx -lt $r.Left -or $sx -ge $r.Right -or $sy -lt $r.Top -or $sy -ge $r.Bottom) { continue }
        $cur = $h
        while ($true) {
            $p = New-Object DwuTest.Bg+POINT
            $p.X = $sx; $p.Y = $sy
            [void][DwuTest.Bg]::ScreenToClient($cur, [ref]$p)
            $child = [DwuTest.Bg]::ChildWindowFromPointEx($cur, $p, 1)
            if ($child -eq [IntPtr]::Zero -or $child -eq $cur) { break }
            $cur = $child
        }
        return $cur
    }
    return [IntPtr]::Zero
}

function Get-ClientLParam([IntPtr]$target, [int]$sx, [int]$sy) {
    $p = New-Object DwuTest.Bg+POINT
    $p.X = $sx; $p.Y = $sy
    [void][DwuTest.Bg]::ScreenToClient($target, [ref]$p)
    return [IntPtr](($p.Y -shl 16) -bor ($p.X -band 0xFFFF))
}

function Send-Click([int]$X, [int]$Y, [switch]$Right, [switch]$Double) {
    Park-Game
    $o = Get-GameOrigin
    $sx = $X + $o.Left
    $sy = $Y + $o.Top
    $t = Find-GameTarget $sx $sy
    if ($t -eq [IntPtr]::Zero) { throw "no game window at $X,$Y" }
    $name = New-Object Text.StringBuilder 256
    [void][DwuTest.Bg2]::GetClassName($t, $name, 256)
    $lp = Get-ClientLParam $t $sx $sy
    [void][DwuTest.Bg]::PostMessage($t, 0x0200, [IntPtr]::Zero, $lp)
    Start-Sleep -Milliseconds 80
    if ($Right) {
        [void][DwuTest.Bg]::PostMessage($t, 0x0204, [IntPtr]2, $lp)
        Start-Sleep -Milliseconds 60
        [void][DwuTest.Bg]::PostMessage($t, 0x0205, [IntPtr]::Zero, $lp)
    }
    elseif ($name.ToString() -match 'BUTTON' -and -not $Double) {
        # BM_CLICK: a button's own click, which does not depend on where the real mouse is.
        [void][DwuTest.Bg]::PostMessage($t, 0x00F5, [IntPtr]::Zero, [IntPtr]::Zero)
    }
    else {
        [void][DwuTest.Bg]::PostMessage($t, 0x0201, [IntPtr]1, $lp)
        Start-Sleep -Milliseconds 60
        [void][DwuTest.Bg]::PostMessage($t, 0x0202, [IntPtr]::Zero, $lp)
        if ($Double) {
            [void][DwuTest.Bg]::PostMessage($t, 0x0203, [IntPtr]1, $lp)
            [void][DwuTest.Bg]::PostMessage($t, 0x0202, [IntPtr]::Zero, $lp)
        }
    }
}

function Send-Hover([int]$X, [int]$Y) {
    Park-Game
    $o = Get-GameOrigin
    $sx = $X + $o.Left
    $sy = $Y + $o.Top
    $t = Find-GameTarget $sx $sy
    if ($t -eq [IntPtr]::Zero) { return }
    [void][DwuTest.Bg]::PostMessage($t, 0x0200, [IntPtr]::Zero, (Get-ClientLParam $t $sx $sy))
}

$script:KeyCodes = @{ '{ESC}' = 0x1B; ' ' = 0x20; '{[}' = 0xDB; '{ENTER}' = 0x0D; '{TAB}' = 0x09; '{HOME}' = 0x24; '{END}' = 0x23; '{DEL}' = 0x2E; '{INS}' = 0x2D; '{BS}' = 0x08 }
foreach ($n in 1..12) { $KeyCodes["{F$n}"] = 0x6F + $n }

function Send-GameKeys([string]$Keys) {
    Park-Game
    if ($KeyCodes.ContainsKey($Keys)) { $vk = $KeyCodes[$Keys] }
    elseif ($Keys.Length -eq 1) { $vk = [int][char]$Keys.ToUpper() }
    else { throw "unknown key $Keys" }
    $game = Get-Game | Select-Object -First 1
    $target = Get-GameWindows | Select-Object -First 1
    $info = New-Object DwuTest.Bg+GUITHREADINFO
    $info.cbSize = [Runtime.InteropServices.Marshal]::SizeOf($info)
    $processId = [uint32]0
    $tid = [DwuTest.Bg]::GetWindowThreadProcessId($game.MainWindowHandle, [ref]$processId)
    if ([DwuTest.Bg]::GetGUIThreadInfo($tid, [ref]$info) -and $info.hwndFocus -ne [IntPtr]::Zero) { $target = $info.hwndFocus }
    [void][DwuTest.Bg]::PostMessage($target, 0x0100, [IntPtr]$vk, [IntPtr]1)
    Start-Sleep -Milliseconds 60
    [void][DwuTest.Bg]::PostMessage($target, 0x0101, [IntPtr]$vk, [IntPtr]0xC0000001)
}

function Get-WindowBitmap([IntPtr]$win) {
    $r = Get-WinRect $win
    $ww = $r.Right - $r.Left
    $wh = $r.Bottom - $r.Top
    if ($ww -le 0 -or $wh -le 0) { return $null }
    $bmp = New-Object Drawing.Bitmap $ww, $wh
    $g = [Drawing.Graphics]::FromImage($bmp)
    $hdc = $g.GetHdc()
    [void][DwuTest.Bg]::PrintWindow($win, $hdc, 2)
    $g.ReleaseHdc($hdc)
    $g.Dispose()
    return $bmp
}

# Composite capture of all the game's windows, in game-relative coordinates.
function Save-Shot([string]$Name, [int[]]$Region, [double]$Scale = 1.0) {
    New-Item -ItemType Directory -Force $ShotDir | Out-Null
    Park-Game
    $o = Get-GameOrigin
    $canvas = New-Object Drawing.Bitmap ($o.Right - $o.Left), ($o.Bottom - $o.Top)
    $cg = [Drawing.Graphics]::FromImage($canvas)
    $wins = @(Get-GameWindows)
    [array]::Reverse($wins)
    foreach ($win in $wins) {
        $bmp = Get-WindowBitmap $win
        if ($bmp -eq $null) { continue }
        $r = Get-WinRect $win
        $cg.DrawImage($bmp, ($r.Left - $o.Left), ($r.Top - $o.Top), $bmp.Width, $bmp.Height)
        $bmp.Dispose()
    }
    $cg.Dispose()
    $bitmap = $canvas
    if ($Region) {
        $crop = $bitmap.Clone((New-Object Drawing.Rectangle $Region[0], $Region[1], $Region[2], $Region[3]), $bitmap.PixelFormat)
        $bitmap.Dispose()
        $bitmap = $crop
    }
    if ($Scale -ne 1.0) {
        $small = New-Object Drawing.Bitmap ([int]($bitmap.Width * $Scale)), ([int]($bitmap.Height * $Scale))
        $g2 = [Drawing.Graphics]::FromImage($small)
        $g2.InterpolationMode = 'HighQualityBicubic'
        $g2.DrawImage($bitmap, 0, 0, $small.Width, $small.Height)
        $g2.Dispose()
        $bitmap.Dispose()
        $bitmap = $small
    }
    $path = Join-Path $ShotDir "$Name.png"
    $bitmap.Save($path, [Drawing.Imaging.ImageFormat]::Png)
    $bitmap.Dispose()
    return $path
}

# Scrolls the control under a point. Notches is positive for up, negative for down.
function Send-Wheel([int]$X, [int]$Y, [int]$Notches) {
    Park-Game
    $o = Get-GameOrigin
    $sx = $X + $o.Left
    $sy = $Y + $o.Top
    $t = Find-GameTarget $sx $sy
    if ($t -eq [IntPtr]::Zero) { return }
    $wp = [IntPtr]([int64]($Notches * 120) -shl 16)
    $lp = [IntPtr](($sy -shl 16) -bor ($sx -band 0xFFFF))
    [void][DwuTest.Bg]::PostMessage($t, 0x020A, $wp, $lp)
}

if (-not ('DwuTest.Enum3' -as [type])) {
    Add-Type -Namespace DwuTest -Name Enum3 -MemberDefinition @"
[DllImport("user32.dll")] static extern bool EnumChildWindows(IntPtr parent, EnumProc cb, IntPtr l);
[DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr h);
[DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr h, out RECT r);
[DllImport("user32.dll")] static extern IntPtr GetParent(IntPtr h);
[DllImport("user32.dll", CharSet=CharSet.Unicode)] static extern int GetClassName(IntPtr h, System.Text.StringBuilder s, int n);
delegate bool EnumProc(IntPtr h, IntPtr l);
[StructLayout(LayoutKind.Sequential)] public struct RECT { public int Left, Top, Right, Bottom; }
static bool AllVisible(IntPtr h, IntPtr top) { while (h != IntPtr.Zero && h != top) { if (!IsWindowVisible(h)) return false; h = GetParent(h); } return true; }
// Every visible descendant as "handle|class|left|top|width|height", topmost first.
public static System.Collections.Generic.List<string> Children(IntPtr top) {
  var list = new System.Collections.Generic.List<string>();
  EnumChildWindows(top, (h, l) => { if (AllVisible(h, top)) { var sb = new System.Text.StringBuilder(128); GetClassName(h, sb, 128); RECT r; GetWindowRect(h, out r); list.Add(h + "|" + sb + "|" + r.Left + "|" + r.Top + "|" + (r.Right - r.Left) + "|" + (r.Bottom - r.Top)); } return true; }, IntPtr.Zero);
  return list; }
"@
}

function Get-GameMainWindow {
    $best = [IntPtr]::Zero
    $area = 0
    foreach ($h in (Get-GameWindows)) {
        $r = Get-WinRect $h
        $a = ($r.Right - $r.Left) * ($r.Bottom - $r.Top)
        if ($a -gt $area) { $area = $a; $best = $h }
    }
    return $best
}

# Closes every open screen panel by pressing its close button (a square button 30 unscaled pixels wide).
function Close-Screens([double]$UiScale = 1.75) {
    $side = [int][Math]::Round(30 * $UiScale)
    foreach ($attempt in 1..6) {
        $closed = $false
        foreach ($line in [DwuTest.Enum3]::Children((Get-GameMainWindow))) {
            $p = $line -split '\|'
            if ($p[1] -match 'BUTTON' -and [Math]::Abs([int]$p[4] - $side) -le 1 -and $p[4] -eq $p[5]) {
                [void][DwuTest.Bg]::PostMessage([IntPtr][int64]$p[0], 0x00F5, [IntPtr]::Zero, [IntPtr]::Zero)
                $closed = $true
                break
            }
        }
        if (-not $closed) { return }
        Start-Sleep -Milliseconds 1200
    }
}
