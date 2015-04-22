    // include Fake lib
#r @"tools\FAKE\tools\FakeLib.dll"
open Fake

RestorePackages() |> DoNothing

#load "core_build.fsx"

RunTargetOrDefault "Test"
