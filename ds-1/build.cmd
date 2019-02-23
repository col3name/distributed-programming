@echo off

set /A argc=0
for %%x in (%*) do Set /A argc += 1

if not %argc% == 1 (
  echo "Invalid argument count"
  echo "Usage: build.cmd <MAJOR.MINOR.PATCH>"
  goto Failed
) 

echo %1|findstr /R "^[1-9][0-9]*.[1-9][0-9]*.[1-9][0-9]*$">nul 2>&1 
if %ERRORLEVEL% EQU 1 (
  echo Invalid version format. Usage <MAJOR.MINOR.PATCH>. MAJOR, MINOR and PATCH it's positive number 
  goto Failed
) else (
  echo Valid version format

  mkdir semver_%1\config
  xcopy .\src\config\config.json .\semver_%1\config /Y
  xcopy .\src\run.cmd .\semver_%1 /Y
  xcopy .\src\stop.cmd .\semver_%1 /Y

  cd .\src\Frontend
  dotnet publish -c Release
  xcopy bin\release\netcoreapp2.2 ..\..\semver_%1\Frontend /E /Y
  
  cd ..\Backend
  dotnet publish -c Release
  xcopy bin\release\netcoreapp2.2 ..\..\semver_%1\Backend /E /Y
    
  echo Build success. Output folder: 'version_': %1
  echo "Run instraction: cd semver_%1
  echo "run.cmd"
  echo "Stop instraction: stop.cmd"
  goto Success
) 

:Failed
  echo Build failed;
exit 1

:Success
  echo Build success;
exit 0