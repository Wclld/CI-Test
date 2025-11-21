using UnityEditor;

public class CommandBuild
{

    public static void BuildWindows()
    {
        new BuildSettingsBuilder(BuildTargetGroup.Standalone)
            .Build();
    }

    public static void BuildAndroid()
    {
        new BuildSettingsBuilder(BuildTargetGroup.Android)
            .Build();
    }

}