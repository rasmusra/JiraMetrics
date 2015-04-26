    // include Fake lib
#r @"tools\FAKE\tools\FakeLib.dll"
open Fake

// Properties
let srcRoot = "src"
let outputDllFiles = !! "**/bin/**/*.dll"
let projectFiles = !! "**/*.csproj"
let buildArtifacts = "buildArtifacts"
let testResultFile = "testResults.xml"
let testReport = buildArtifacts + "/testReport.html"
let featuresDir = buildArtifacts + "/features"
let featuresWithTestResultsDir = buildArtifacts + "/featuresWithTestResults"
let chrome = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
let includeCategory = getBuildParamOrDefault "includeCategory" ""
let showResultInBrowser = getBuildParamOrDefault "showResultInBrowser" "yes"
let mongoPath = @"C:\Program Files\MongoDB\Server\3.0\bin"
let mongoPort = getBuildParamOrDefault "port" "27113"
let mongoDb = "JiraMetricsDb"
let mongoDbPath = String.concat @"\" [__SOURCE_DIRECTORY__; "data" ; "db"]
let mongoLogDir = String.concat @"\" [__SOURCE_DIRECTORY__; "data" ; "log" ]
let nunitPath = @"packages\NUnit.Runners.2.6.4\tools"

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
    ActivateFinalTarget "Publish test report"

    !! (srcRoot + @"\**\bin\Debug\*.Test.Unit.dll")
    ++ (srcRoot + @"\**\bin\Debug\*.Test.Acceptance.dll")
    -- (srcRoot + @"\**\bin\Debug\*.Fakes.dll")
    |> NUnit (fun p -> 
    {
        p with 
            StopOnError = false 
            OutputFile = testResultFile
            IncludeCategory = includeCategory
            ToolPath = nunitPath
    })

)

FinalTarget "Publish test report" (fun _ ->
    let nunitOrangeArgs = testResultFile + " " + testReport
    Shell.Exec("packages/NUnitOrange.2.1/tools/nunitorange.exe", nunitOrangeArgs)
    |> ignore

    match showResultInBrowser with
    | "yes" -> Shell.Exec(chrome, testReport) |> ignore
    | _ -> ()

    ()
)

// helps for starting up a mongo db, used in dev env 
Target "StartMongoDb" (fun _ ->
    CreateDir mongoDbPath
    CreateDir mongoLogDir
    let logfile = String.concat @"\" [ mongoLogDir ; mongoDb + ".log" ]
    let cmd = mongoPath + @"\mongod"
    let args = "--dbpath " + mongoDbPath + " --logpath " + logfile + " --port " + mongoPort
    trace (cmd + " " + args)
    let errorCode = Shell.Exec(cmd, args)
    ()
)

// populates mongo db with the test data dump file
Target "SetupMongoDb" (fun _ ->
    let args = "-d " + mongoDb + " -c issue --file src\Olifant.JiraMetrics.Test.Utilities\Stubs\jirametricsdb_dump.json --upsert -h localhost:" + mongoPort
    let errorCode = Shell.Exec(mongoPath + @"\mongoimport", args)
    ()
)

// Dependencies
"Clean"
==> "Build"
==> "Test"
