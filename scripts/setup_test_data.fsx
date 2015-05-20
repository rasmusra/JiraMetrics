// this ones depends on compiled assemblies in src
#r @"..\src\Olifant.JiraMetrics.Lib\bin\Debug\Olifant.JiraMetrics.Lib.dll"
#r @"..\src\Olifant.JiraMetrics.Test.Utilities\bin\Debug\Olifant.JiraMetrics.Test.Utilities.dll"
open Olifant.JiraMetrics.Test.Utilities.Helpers
open System.IO

let stubsDir = @"..\src\Olifant.JiraMetrics.Test.Utilities\bin\Debug\Stubs"
let mutable pattern = "key=*.json"

// Process command args 
for arg in fsi.CommandLineArgs do
     if arg.StartsWith("pattern:") then pattern <- arg.Split(':').GetValue(1) :?> string
     else if arg.StartsWith("Pattern:") then pattern <- arg.Split(':').GetValue(1) :?> string

Directory.GetFiles(stubsDir, pattern)
|> Seq.iter(printfn "%s")

JiraStubFactory.Setup(stubsDir)
MongoWrapper.Init("server=localhost:27113;database=JiraMetricsDb", JiraStubFactory.CreateFromFiles(pattern), JiraStubFactory.FakeJiraRestClient.ReadAllJsonFiles()) 

