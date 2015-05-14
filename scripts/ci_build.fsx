    // include Fake lib
#r @"..\tools\FAKE\tools\FakeLib.dll"
open Fake

trace "restoring packages..."
RestorePackages() |> DoNothing

#load "core_build.fsx"

RunTargetOrDefault "Test"
