@echo off
set "restemplate=%~1"
set B2SResFileEndingOverride=.res
for /f "tokens=3" %%a in ('reg query "HKCU\Software\B2S"  /V B2SResFileEndingOverride  ^|findstr /ri "REG_SZ"') do set B2SResFileEndingOverride=%%a

set "resfile=%~dpn2%B2SResFileEndingOverride%"

copy "%~dp0%restemplate%" "%resfile%"