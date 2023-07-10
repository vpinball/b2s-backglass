@echo off
set restemplate="%~1"
set resfile="%~dpn2.res"

copy "%~dp0%restemplate:~1,-1%" "%resfile:~1,-1%"
