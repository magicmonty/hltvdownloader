// include Fake lib
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Paket
open Fake.Testing
open Fake.ILMergeHelper

// Properties
let testDir = "test/bin/Debug"
let deployDir = "deploy"
let releaseDir = "src/bin/Release"

// Targets
Target "Clean" (fun _ ->
  !!"**/bin"
  |> DeleteDirs

  !!"**/obj"
  |> DeleteDirs

  CleanDir deployDir
)

Target "Default" (fun _ ->
  trace "Build successful"
)

Target "BuildDebug" (fun _ ->
    !!"*.sln"
    |> MSBuildDebug "" "Build"
    |> Log "DebugBuild-Output: "
)

Target "Test" (fun _ ->
    !!(testDir @@ "Pagansoft.*.Test.dll")
      |> NUnit3(fun p ->
          { p with OutputDir = testDir @@ "TestResults.xml" })
)

Target "BuildRelease" (fun _ ->
    !!"*.sln"
    |> MSBuildRelease "" "Build"
    |> Log "ReleaseBuild-Output"
)

Target "Deploy" (fun _ ->
  releaseDir @@ "HLTVDownloader.exe"
    |> ILMerge (fun p -> { p with ToolPath = "packages/ILRepack/tools/ILRepack.exe"
                                  TargetKind = Exe
                                  SearchDirectories = [ releaseDir ]
                                  AllowWildcards = true
                                  Libraries = [ releaseDir @@ "*.dll" ] })
        (deployDir @@ "HLTVDownloader.exe")

  DeleteFile (deployDir @@ "HLTVDownloader.exe.mdb")
  [ "hltvcomplete"; "hltverror"; "hltvdownloader" ]
    |> Seq.map (fun f -> releaseDir @@ f)
    |> Copy deployDir

  !! (deployDir @@ "*.*")
    -- (deployDir @@ "*.zip")
    |> Zip deployDir (deployDir + "/HLTVDownloader.zip")
)

// Dependencies
"Clean"
  ==> "BuildDebug"
  ==> "Test"
  ==> "BuildRelease"
  ==> "Deploy"

// start build
RunTargetOrDefault "Deploy"
