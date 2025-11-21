using System;
using System.Linq;

using UnityEditor;
using UnityEditor.Build.Reporting;

using UnityEngine;

public class BuildSettingsBuilder
{
    private readonly BuildTargetGroup _platform = BuildTargetGroup.Unknown;
    private bool _playStoreBuild = false;

    public BuildSettingsBuilder(BuildTargetGroup platform)
    {
        _platform = platform;
    }

    public void Build()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(x => x.enabled)
            .Select(x => x.path)
            .ToArray();
        BuildOptions buildOptions = BuildOptions.None;

        DisableSplashScreensForProLicense();
        var outPath = GetBuildPath();
        var target = GetBuildTarget();
        BuildReport buildReport = BuildPipeline.BuildPlayer(scenes, outPath, target, buildOptions);
        if (buildReport.summary.result == BuildResult.Succeeded)
            Debug.Log($"Build succeeded - [Output: {outPath}]");
        else
            Debug.LogError($"Build {buildReport.summary.result} - [Errors: {buildReport.SummarizeErrors()}]");

        EditorApplication.Exit(buildReport.summary.result == BuildResult.Succeeded ? 0 : 1);
    }
    
    private string GetBuildPath() =>
        _platform switch
        {
            BuildTargetGroup.Standalone => "build/win/game.exe",
            BuildTargetGroup.iOS => "build/ios/game.ipa",
            BuildTargetGroup.Android => _playStoreBuild
                ? "build/android/game.aab"
                : "build/android/game.apk",
            _ => throw new ArgumentOutOfRangeException()
        };

    private BuildTarget GetBuildTarget() =>
        _platform switch
        {
            BuildTargetGroup.Standalone => BuildTarget.StandaloneWindows,
            BuildTargetGroup.iOS => BuildTarget.iOS,
            BuildTargetGroup.Android => BuildTarget.Android,
            _ => throw new ArgumentOutOfRangeException()
        };


    private static void DisableSplashScreensForProLicense()
    {
        if (Application.HasProLicense())
        {
            PlayerSettings.SplashScreen.show = false;
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
    }
    
    public BuildSettingsBuilder SetPlayStoreBuild()
    {
        _playStoreBuild = true;
        return this;
    }
}