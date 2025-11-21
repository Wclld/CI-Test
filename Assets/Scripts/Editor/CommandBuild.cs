using UnityEditor;

using UnityEngine;

public class CommandBuild
{

    public static void TEST_BUILD()
    {
        Debug.Log("Building Test");
        Debug.Log("Building Test");
    }
    public static void BuildWindows()
    {
        Debug.Log("Building Windows");
        new BuildSettingsBuilder(BuildTargetGroup.Standalone)
            .Build();
    }

    public static void BuildAndroid()
    {
        Debug.Log("Building Android");
        new BuildSettingsBuilder(BuildTargetGroup.Android)
            .Build();
    }
    
    [MenuItem("Build/Standalone")]
    public static void BuildWindowsEditor()
    {
        new BuildSettingsBuilder(BuildTargetGroup.Standalone)
            .DoNotQuitOnFinish()
            .Build();
    }

    [MenuItem("Build/Android")]
    public static void BuildAndroidEditor()
    {
        new BuildSettingsBuilder(BuildTargetGroup.Android)
            .DoNotQuitOnFinish()
            .Build();
    }

}