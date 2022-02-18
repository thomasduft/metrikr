@echo off
set /p version=Enter Releaseversion:

if [%version%] == [] (
  echo No version specified! Please specify a valid version like 1.2.3 or 1.2.3-rc1!
  goto Done
)
if [%version%] == [""] (
  echo No version specified! Please specify a valid version like 1.2.3 or 1.2.3-rc1!
  goto Done
)

echo ----
echo Starting building version %version%

echo ----
echo Cleaning up
RD /S /Q ./dist
dotnet tool uninstall -g metrikr

echo ----
echo Restore solution
dotnet restore src/metrikr

echo ----
echo Packaging scaffy
echo Packaging solution with Version = %version%
dotnet pack src/metrikr -c Release -p:PackageVersion=%version% -p:Version=%version% -o ./dist/nupkgs

echo ----
set /p isInstallation=Install scaffy (y/n):

echo %isInstallation%
if "%isInstallation%" == "y" (
  echo Installing metrikr globally with Version = %version%
  dotnet tool install --global --add-source ./dist/nupkgs metrikr
)

@REM timeout 5

echo ----
echo Done