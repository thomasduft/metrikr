#!/bin/bash

TOOLNAME="tomware.MetrikR"
echo $TOOLNAME

if [ -z "$1" ];
then
  echo ----
  echo No version specified! Please specify a valid version like 1.2.3 or 1.2.3-rc1!
  exit 1
fi

echo ----
echo Starting building version $1

echo ----
echo Cleaning up
rm -rf ./artifacts
dotnet tool uninstall -g $TOOLNAME

echo ----
echo Restore
dotnet restore src/metrikr

echo ----
echo Packaging with Version = $1
dotnet pack src/metrikr -c Release -p:PackageVersion=$1 -p:Version=$1 -o ./artifacts/nupkgs/

echo ----
echo Installing globally with Version = $1
dotnet tool install --global --add-source ./artifacts/nupkgs/ $TOOLNAME

echo ----
echo Done
