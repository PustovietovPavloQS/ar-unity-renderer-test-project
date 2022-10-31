#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

class BuildPrecrocessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        FileUtil.DeleteFileOrDirectory("Assets/WebGLTemplates/AR-Renderer/Targets");
        FileUtil.CopyFileOrDirectory("Assets/Targets", "Assets/WebGLTemplates/AR-Renderer/Targets");
    }
}
#endif