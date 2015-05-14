    // include Fake lib
#r @"..\tools\FAKE\tools\FakeLib.dll"
open Fake

// Properties
let projRoot = String.concat "/" [__SOURCE_DIRECTORY__ ; ".."]
let srcRoot = projRoot + "/src"
let outputDllFiles = !! (srcRoot + "/**/bin/**/*.dll")
let projectFiles = !! (srcRoot + "/**/*.csproj")
let buildArtifacts = projRoot + "/buildArtifacts"
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
let mongoDbPath = projRoot + "/data/db"
let mongoLogDir = projRoot + "/data/log"
let nunitPath = projRoot + "/packages/NUnit.Runners.2.6.4/tools"

// Targets  
Target "Clean" (fun _ ->
    DeleteFiles outputDllFiles
    CleanDir buildArtifacts
)

Target "Publish specs" (fun _ ->
    let picklesArgs = "-f " + srcRoot + "/Olifant.JiraMetrics.Test.Acceptance/Features -o " + featuresDir
    let pickles = projRoot + "/packages/Pickles.CommandLine.1.0.0/tools/pickles.exe"
    trace pickles
    trace picklesArgs
    let errorCode = Shell.Exec(pickles, picklesArgs)
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

  build setParams (projRoot + "/Olifant.JiraMetrics.sln")
    |> DoNothing
)

Target "Test" (fun _ ->
    ActivateFinalTarget "Publish test report"

    !! (srcRoot + "/**/bin/Debug/*.Test.Unit.dll")
    ++ (srcRoot + "/**/bin/Debug/*.Test.Acceptance.dll")
    -- (srcRoot + "/**/bin/Debug/*.Fakes.dll")
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
    let nunitOrange = projRoot + "/packages/NUnitOrange.2.1/tools/nunitorange.exe"
    Shell.Exec(nunitOrange, nunitOrangeArgs)
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
    let logfile = String.concat "/" [ mongoLogDir ; mongoDb + ".log" ]
    let mongodArgs = "--dbpath " + mongoDbPath + " --logpath " + logfile + " --port " + mongoPort
    let errorCode = Shell.Exec(mongoPath + "/mongod", mongodArgs)
    ()
)

// populates mongo db with the test data dump file
Target "SetupMongoDb" (fun _ ->
    let args = "-d " + mongoDb + " -c issue --file " + srcRoot + "/Olifant.JiraMetrics.Test.Utilities/Stubs/jirametricsdb_dump.json --upsert -h localhost:" + mongoPort
    let errorCode = Shell.Exec(mongoPath + "/mongoimport", args)
    ()
)

// Dependencies
"Clean"
==> "Build"
==> "Test"
