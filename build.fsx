// include Fake lib
#r @"packages\FAKE\tools\FakeLib.dll"
open Fake

// Properties
let srcRoot = "src"
let outputDllFiles = !! "**/bin/**/*.dll"
let projectFiles = !! "**/*.csproj"
let buildArtifacts = "buildArtifacts"
let testResultFile = buildArtifacts + @"/testResults.xml"
let featuresDir = buildArtifacts + @"/features"
let featuresWithTestResultsDir = buildArtifacts + @"/featuresWithTestResults"

// Targets
Target "Clean" (fun _ ->
    DeleteFiles outputDllFiles
    CleanDir buildArtifacts
)

Target "Publish specs" (fun _ ->
    let args = "-f src/HM.JiraMetrics.Test.Acceptance/Features" + @" -o " + featuresDir
    let errorCode = Shell.Exec("packages/Pickles.CommandLine.1.0.0/tools/pickles.exe", args)
    ()
)

Target "Build" (fun _ ->
  let buildMode = getBuildParamOrDefault "buildMode" "Debug"

  let setParams defaults =
    { 
      defaults with
        Verbosity = Some(Quiet)
        Targets = ["Build"]
        Properties =
            [
              "Optimize", "True"
              "DebugSymbols", "True"
              "Configuration", buildMode
            ]
    }

  build setParams "HM.JiraMetrics.sln"
    |> DoNothing
)

Target "Test" (fun _ ->
    !! (srcRoot + @"\**\bin\Debug\*.Test.*.dll")
    --  (srcRoot + @"\**\bin\Debug\*.Fakes.dll")
    |> NUnit (fun p -> 
    {
        p with 
            StopOnError = false 
            OutputFile = testResultFile
    })
)

Target "Publish" (fun _ ->
    let args = "-f src/HM.JiraMetrics.Test.Acceptance/Features -lr " + testResultFile + @" -o " + featuresWithTestResultsDir
    let errorCode = Shell.Exec("packages/Pickles.CommandLine.1.0.0/tools/pickles.exe", args)
    ()
)

// Dependencies
"Clean"
==> "Publish specs"
==> "Build"
==> "Test"
==> "Publish"

// start build
RunTargetOrDefault "Publish"