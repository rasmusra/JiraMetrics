    // include Fake lib
#r @"packages\FAKE.3.17.3\tools\FakeLib.dll"
open Fake

// Properties
let srcRoot = "src"
let outputDllFiles = !! "**/bin/**/*.dll"
let projectFiles = !! "**/*.csproj"
let buildArtifacts = "buildArtifacts"
let testResultFile = buildArtifacts + @"/testResults.xml"
let featuresDir = buildArtifacts + @"/features"
let featuresWithTestResultsDir = buildArtifacts + @"/featuresWithTestResults"
let chrome = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
let includeCategory = getBuildParamOrDefault "includeCategory" ""
let mongod = @"C:\Program Files\MongoDB\Server\3.0\bin\mongod.exe"
let mongoPort = "27113"
let mongoDbPath = String.concat @"\" [__SOURCE_DIRECTORY__; "data" ; "db"]
let mongoLogDir = String.concat @"\" [__SOURCE_DIRECTORY__; "data" ; "log" ]

// Targets  
Target "Clean" (fun _ ->
    trace __SOURCE_DIRECTORY__
    DeleteFiles outputDllFiles
    CleanDir buildArtifacts
)

Target "Publish specs" (fun _ ->
    let args = "-f src/Olifant.JiraMetrics.Test.Acceptance/Features" + @" -o " + featuresDir
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

  build setParams "Olifant.JiraMetrics.sln"
    |> DoNothing
)

Target "Test" (fun _ ->
    
    match includeCategory with
    | "" -> ActivateFinalTarget "Publish"
    | _ -> ()

    !! (srcRoot + @"\**\bin\Debug\*.Test.*.dll")
    --  (srcRoot + @"\**\bin\Debug\*.Fakes.dll")
    |> NUnit (fun p -> 
    {
        p with 
            StopOnError = false 
            OutputFile = testResultFile
            IncludeCategory = includeCategory
    })
)

FinalTarget "Publish" (fun _ ->
    let args = "-f src/Olifant.JiraMetrics.Test.Acceptance/Features -lr " + testResultFile + @" -o " + featuresWithTestResultsDir
    Shell.Exec("packages/Pickles.CommandLine.1.0.0/tools/pickles.exe", args)
    |> ignore

    let log = String.concat @"\" [__SOURCE_DIRECTORY__; featuresWithTestResultsDir; "index.html"]
    Shell.Exec(chrome, log)     
    |> ignore

    ()
)

Target "StartMongoDb" (fun _ ->
    CreateDir mongoDbPath
    CreateDir mongoLogDir
    let logfile = String.concat @"\" [ mongoLogDir ; "JiraMetricsDB.log" ]
    let args = "--dbpath " + mongoDbPath + " --logpath " + logfile + " --port " + mongoPort
    let errorCode = Shell.Exec(mongod, args)
    ()
)


// Dependencies
"Clean"
==> "Publish specs"
==> "Build"
==> "Test"

// start build
RunTargetOrDefault "Test"
