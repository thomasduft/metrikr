#!/bin/bash

if [ -z "$1" ];
then
  echo No version specified! Please specify a valid version like 1.2.3 or 1.2.3-rc1!
  exit 1
fi

if [ -z "$2" ];
then
  echo No release info specified! For a relase provide the r flag and a valid release verion!
  exit 1
fi

echo ----
echo Starting building version $1

echo ----
echo Cleaning up
rm -r ./dist
dotnet tool uninstall -g metrikr

echo ----
echo Restore solution
dotnet restore src/metrikr

echo ----
if [ $2 = "i" ];
then
  echo Packaging solution with Version = $1
  dotnet pack src/metrikr -c Release -p:PackageVersion=$1 -p:Version=$1 -o ./dist/nupkgs/
else
  echo Packaging solution with PackageVersion = $1
  dotnet pack src/metrikr -c Release -p:PackageVersion=$1 -o ./dist/nupkgs/
fi

echo ----
if [ -z "$3" ];
then
  echo No publish info specified! For publishing metrikr provide the p flag!
  exit 1
fi

echo ----
echo Installing metrikr globally with Version = $1
dotnet tool install --global --add-source ./dist/nupkgs/ metrikr

echo ----
echo Done