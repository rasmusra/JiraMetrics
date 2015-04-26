    // include Fake lib
#r @"tools\FAKE\tools\FakeLib.dll"
open Fake
open Fake.EnvironmentHelper

let mutable target = "test"

// Process command args as Fake utilities don't work from fsi invoke
for arg in fsi.CommandLineArgs do
     if arg.StartsWith("target:") then target <- arg.Split(':').GetValue(1) :?> string
     else if arg.StartsWith("Target:") then target <- arg.Split(':').GetValue(1) :?> string

#load "core_build.fsx"

// start build
AdditionalSyntax.RunParameterTargetOrDefault "Test" target

