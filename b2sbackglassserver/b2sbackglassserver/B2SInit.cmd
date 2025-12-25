@echo off
echo %0 %* > "%~dp0B2SWindowPunch.log"
setlocal EnableDelayedExpansion
set B2SResFileEndingOverride=.res
for /f "tokens=3" %%a in ('reg query "HKCU\Software\B2S"  /V B2SResFileEndingOverride  ^|findstr /ri "REG_SZ"') do set B2SResFileEndingOverride=%%a

set "resfile=%cd%\%~1%B2SResFileEndingOverride%"
set "GameName=%~2"

REM If started from explorer, quit
if ["%GameName%"] == [""] echo %0 Exit when started from explorer >> "%~dp0B2SWindowPunch.log"&goto :EOF

REM Cut holes in the destination "B2S Backglass Server" & "B2S DMD" forms
set "B2SWindowPunch=^B2S Backglass$|^B2S Backglass Server$|^B2S DMD$"

REM using "Virtual DMD", "Virtual Alphanumeric" and all "PUPSCREEN" forms as regular expressions
set "cutter=^Virtual DMD$|^Virtual Alphanumeric Display$|^PUPSCREEN[0-9]+$|^VPinMAME:|^PROC:"

REM Default to square non rounded cut out.
set cutterradius=0

REM Default wait time.
set cuttertimeout=10

REM Check if there is any table specific settings for B2SWindowPunch
if exist "%resfile%" for /f "usebackq eol=# tokens=1,2 delims==" %%G in (`findstr /R "^[A-Za-z0-9][A-Za-z0-9]*=" "%resfile%"`) do set "%%G=%%H"

echo B2SWindowPunch = "!B2SWindowPunch!" >> "%~dp0B2SWindowPunch.log"
echo cutter = "!cutter!" >> "%~dp0B2SWindowPunch.log"
echo cutterradius = "!cutterradius!" >> "%~dp0B2SWindowPunch.log"
echo cuttertimeout = "!cuttertimeout!" >> "%~dp0B2SWindowPunch.log"

if ["!B2SWindowPunch!"] == ["off"] echo %0 B2SWindowPunch turned off>> "%~dp0B2SWindowPunch.log"&goto :EOF
REM Quit If no destination set
if ["!B2SWindowPunch!"] == [""] echo %0 No destination set>> "%~dp0B2SWindowPunch.log"&goto :EOF

REM Wait 10 seconds
timeout /t !cuttertimeout! /nobreak > NUL

REM Punch the holes
echo "%~dp0B2SWindowPunch.exe" "!B2SWindowPunch!" "!cutter!" !cutterradius! >> "%~dp0B2SWindowPunch.log"
"%~dp0B2SWindowPunch.exe" "!B2SWindowPunch!" "!cutter!" !cutterradius! >> "%~dp0B2SWindowPunch.log"
