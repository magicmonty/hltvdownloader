// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Paket
open Fake.Testing

// Properties
let testDir = "test/bin/Debug"
let deployDir = "deploy"

// Targets
Target "Clean" (fun _ ->
  !!"**/bin"
  |> DeleteDirs

  !!"**/obj"
  |> DeleteDirs

  DeleteDir deployDir
)

Target "Default" (fun _ ->
  trace "Build successful"
)

Target "BuildRelease" (fun _ ->
    !!"*.sln"
    |> MSBuildRelease "" "Build"
    |> Log "ReleaseBuild-Output"
)

Target "BuildDebug" (fun _ ->
    !!"*.sln"
    |> MSBuildDebug "" "Build"
    |> Log "DebugBuild-Output: "
)

Target "Test" (fun _ ->
    !!(testDir @@ "Pagansoft.*.Test.dll")
      |> NUnit(fun p ->
          { p with DisableShadowCopy = false
                   OutputFile = testDir @@ "TestResults.xml"
                   ToolPath = "packages/NUnit.Runners/tools"})
)

// Dependencies
"Clean"
  ==> "BuildDebug"
  ==> "Test"
  ==> "BuildRelease"

// start build
RunTargetOrDefault "Test"
