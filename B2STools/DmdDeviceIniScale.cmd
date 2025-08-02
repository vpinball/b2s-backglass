@echo off
setlocal
echo  ___ ___ ___ 
echo ^| _ ^)^_  ^) __^|
echo ^| _ \/ /\__ \
echo ^|___/___^|___/ DmdDeviceIniScale
echo.
echo This script is used to change the scaling and position of entries in a DmdDevice.ini file.
REM 
REM Check if filename was given as first parameter
if "%~1"=="" (
    echo.
    echo Usage^: DmdDeviceIniScale.cmd ^<DmdDevice.ini file^>
    echo Drop a DmdDevice.ini file on this script in windows explorer. 
    echo It will make a backup copy of your file and recalculate all positions and sizes on the basis of a entered scaling factor.
    echo It is also possible to move the positions using an offset horizontal and vertically.
    pause
    exit
) else (
    set INIFILE=%~n1
    set EXTENSION=%~x1
)
echo [33mAfter making a backup, your original file %INIFILE%%EXTENSION% will be overwritten with the new values.[0m
pause

for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"
set "YY=%dt:~2,2%" & set "YYYY=%dt:~0,4%" & set "MM=%dt:~4,2%" & set "DD=%dt:~6,2%"
set "HH=%dt:~8,2%" & set "Min=%dt:~10,2%" & set "Sec=%dt:~12,2%"

set BACKUPFILE=%INIFILE%_%YYYY%%MM%%DD%_%HH%%Min%%Sec%%EXTENSION%
set INIFILE=%INIFILE%%EXTENSION%

echo Backup %INIFILE% to %BACKUPFILE%
copy %INIFILE% %BACKUPFILE% > NUL
REM Run PowerShell code directly
powershell -NoProfile -ExecutionPolicy Bypass -Command ^
    "$BACKUPFILE='%BACKUPFILE%';" ^
    "$INIFILE='%INIFILE%';" ^
    "$iniLines = Get-Content $BACKUPFILE;" ^
    "$hasHeader = $false;" ^
    "$origScaling = 100;" ^
    "$origOffsetLeft = 0;" ^
    "$origOffsetTop = 0;" ^
    "foreach ($l in $iniLines) {" ^
    "  if ($l -match '^\s*;\s*B2S DmdDeviceScale') { $hasHeader = $true }" ^
    "  if ($l -match '^\s*;\s*screenScaling\s*=\s*(\d+)') { $origScaling = [int]$matches[1] }" ^
    "  if ($l -match '^\s*;\s*offsetLeft\s*=\s*(-?\d+)') { $origOffsetLeft = [int]$matches[1] }" ^
    "  if ($l -match '^\s*;\s*offsetTop\s*=\s*(-?\d+)') { $origOffsetTop = [int]$matches[1] }" ^
    "}" ^
    "$inputScaling = Read-Host ('What scaling percent was this file saved with? (default ' + $origScaling + ')');" ^
    "if ([string]::IsNullOrWhiteSpace($inputScaling)) { $inputScaling = $origScaling }" ^
    "$origScaling = [int]$inputScaling;" ^
    "$screenScaling = Read-Host ('Wanted scaling in percent (e.g. 150, default ' + $origScaling + ')'); if ([string]::IsNullOrWhiteSpace($screenScaling)) { $screenScaling = $origScaling }" ^
    "$offsetLeft = Read-Host ('Offset for left (e.g. 0, default ' + $origOffsetLeft + ')'); if ([string]::IsNullOrWhiteSpace($offsetLeft)) { $offsetLeft = $origOffsetLeft }" ^
    "$offsetTop = Read-Host ('Offset for top (e.g. 0, default ' + $origOffsetTop + ')'); if ([string]::IsNullOrWhiteSpace($offsetTop)) { $offsetTop = $origOffsetTop }" ^
    "$factor = [double]$origScaling / [double]$screenScaling;" ^
    "$newLines = @();" ^
    "$newLines += '; B2S DmdDeviceScale';" ^
    "$newLines += '; screenScaling = ' + $screenScaling;" ^
    "$newLines += '; offsetLeft=' + $offsetLeft;" ^
    "$newLines += '; offsetTop=' + $offsetTop;" ^
    "foreach ($line in $iniLines) {" ^
    "  if ($line -match '^\s*;\s*B2S DmdDeviceScale') { continue }" ^
    "  if ($line -match '^\s*;\s*screenScaling\s*=') { continue }" ^
    "  if ($line -match '^\s*;\s*offsetLeft\s*=') { continue }" ^
    "  if ($line -match '^\s*;\s*offsetTop\s*=') { continue }" ^
    "  if ($line -match '(?i)padding') { $newLines += $line; continue }" ^
    "  if ($line.Trim().StartsWith(';') -or $line.Trim() -eq '') { $newLines += $line; continue }" ^
    "  if ($line -match '^\s*([^=]+?)\s*=\s*(-?\d+)\s*$') {" ^
    "    $key = $matches[1].Trim(); $value = [double]$matches[2];" ^
    "    if ($key.ToLower().EndsWith('left')) { $newValue = [math]::Round((($value - $origOffsetLeft) * $factor) + [double]$offsetLeft) }" ^
    "    elseif ($key.ToLower().EndsWith('top')) { $newValue = [math]::Round(($value - $origOffsetTop) * $factor + [double]$offsetTop) }" ^
    "    elseif ($key.ToLower().EndsWith('width') -or $key.ToLower().EndsWith('height')) { $newValue = [math]::Round($value * $factor) }" ^
    "    else { $newValue = $value }" ^
    "    $newLines += \"$key = $newValue\"" ^
    "  } else { $newLines += $line }" ^
    "}" ^
    "$newLines | Set-Content -Encoding UTF8 $INIFILE;" ^
    "Write-Host 'Done!'" 

pause