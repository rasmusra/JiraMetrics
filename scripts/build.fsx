    // include Fake lib
#r @"..\tools\FAKE\tools\FakeLib.dll"
open Fake
#load "build_targets.fsx"
RunTargetOrDefault "Test"
