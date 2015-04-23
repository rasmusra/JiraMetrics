@echo off

if NOT EXIST "tools\FAKE*" (
	"tools\nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion"
) ELSE (
	ECHO "FAKE is installed!"
)

tools/FAKE/tools/FAKE.exe build.fsx


