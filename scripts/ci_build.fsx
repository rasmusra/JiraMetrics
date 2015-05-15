    // include Fake lib
#r @"..\tools\FAKE\tools\FakeLib.dll"
open Fake

trace "restoring packages..."


"../.nuget/packages.config"
|> RestorePackage (fun p ->
    { p with
        OutputPath = "../packages"
        ToolPath = "../tools/nuget/nuget.exe"
        Retries = 4 })


"../Olifant.JiraMetrics.sln"
|> RestoreMSSolutionPackages (fun p ->
    { p with
        OutputPath = "../packages"
        ToolPath = "../tools/nuget/nuget.exe"
        Retries = 4 })


#load "core_build.fsx"

RunTargetOrDefault "Test"
