using UnityEngine;
using UnityEditor;

public static class Editor
{
    [MenuItem("GameObject/AR-Renderer/AR-Face-Tracking-Scene", false, -1)]
    static void CreatFaceScene()
    {
        GameObject arController = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/AR-Renderer/Prefabs/AR-Renderer-Face-Track-Controller.prefab", typeof(GameObject));
        GameObject instance = PrefabUtility.InstantiatePrefab(arController) as GameObject;
        PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }

    [MenuItem("GameObject/AR-Renderer/AR-Target-Tracking-Scene", false, -1)]
    static void CreateTargetScene()
    {
        GameObject arController = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/AR-Renderer/Prefabs/AR-Renderer-Target-Track-Controller.prefab", typeof(GameObject));
        GameObject instance = PrefabUtility.InstantiatePrefab(arController) as GameObject;
        PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }

    [MenuItem("GameObject/AR-Renderer/AR-Surface-Tracking-Scene", false, -1)]
    static void CreateSurfaceScene()
    {
        GameObject arController = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/AR-Renderer/Prefabs/AR-Renderer-Surface-Track-Controller.prefab", typeof(GameObject));
        GameObject instance = PrefabUtility.InstantiatePrefab(arController) as GameObject;
        PrefabUtility.UnpackPrefabInstance(instance, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
    }
}