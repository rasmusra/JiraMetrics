@echo off

pushd .
cd %~dp0

if NOT EXIST "tools\FAKE*" (
	"tools\nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion"
) ELSE (
	ECHO "FAKE is installed!"
)

cd scripts
%~dp0"tools\FAKE\tools\FAKE.exe" build.fsx %*

popd


