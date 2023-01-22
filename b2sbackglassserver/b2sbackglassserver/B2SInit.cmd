@echo off
echo %0 %* > "%~dp0B2SWindowPunch.log"
REM Check if there is any table specific settings for B2SWindowPunch

setlocal EnableDelayedExpansion

set resfile=%cd%\%~1.res
REM Cut holes in the destination "B2S Backglass Server" & "B2S DMD" forms
set "destination=^B2S Backglass Server$|^B2S DMD$"
REM using "Virtual DMD" and all "PUPSCREEN" forms as regular expressions
set "cutter=^Virtual DMD$|^PUPSCREEN[0-9]+$"

for /f "usebackq eol=# tokens=1,2 delims==" %%G in (`findstr "=" "%resfile%"`) do set "%%G=%%H"

echo destination = "!destination!" >> "%~dp0B2SWindowPunch.log"
echo cutter = "!cutter!" >> "%~dp0B2SWindowPunch.log"

REM Quit If no destination set
if ["!destination!"] == [""] goto :EOF

REM Wait 10 seconds
timeout /t 10 /nobreak > NUL

REM Cut the holes
echo "%~dp0B2SWindowPunch.exe" "!destination!" "!cutter!"  >> "%~dp0B2SWindowPunch.log"
"%~dp0B2SWindowPunch.exe" "!destination!" "!cutter!"  >> "%~dp0B2SWindowPunch.log"
