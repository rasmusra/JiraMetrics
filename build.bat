@echo off

if NOT EXIST "packages\FAKE*" (
	"tools\nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "packages" "-Version" "3.17.3"
) ELSE (
	ECHO "FAKE is installed!"
)

if NOT EXIST "packages\pickles*" (
	"tools\nuget\nuget.exe" "install" "Pickles.CommandLine" "-OutputDirectory" "packages" 	
) ELSE (
	ECHO "Pickles.CommandLine is installed!"
)

if NOT EXIST "packages\nunit.runners*" (
	"tools\nuget\nuget.exe" "install" "NUnit.Runners" "-OutputDirectory" "packages" "-Version" "2.6.3"	
) ELSE (
	ECHO "NUnit.Runners is installed!"
)

call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\Tools\VsDevCmd.bat" 
fsi --exec build.fsx

pause
