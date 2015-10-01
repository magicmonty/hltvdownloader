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
      |> xUnit2(fun p ->
          { p with ShadowCopy = true
                   NoAppDomain = true
                   HtmlOutputPath = Some (testDir @@ "TestResults.html")
                   XmlOutputPath = Some (testDir @@ "TestResults.xml")
                   ToolPath = "packages/xunit.runner.console/tools/xunit.console.exe"})
)

// Dependencies
"Clean"
  ==> "BuildDebug"
  ==> "Test"
  ==> "BuildRelease"
 
// start build
RunTargetOrDefault "Test"
