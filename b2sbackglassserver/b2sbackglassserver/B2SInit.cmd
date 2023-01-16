@echo off
echo %0 %* > "%~dp0\B2SWindowPunch.log"
timeout /t 10 /nobreak > NUL
REM Cut holes in the "B2S Backglass Server" form using "Virtual DMD" and all "PUPSCREEN" forms as regular expressions
"%~dp0\B2SWindowPunch.exe" "^B2S Backglass Server$" "^Virtual DMD$|^PUPSCREEN[0-9]+$" >> "%~dp0\B2SWindowPunch.log"