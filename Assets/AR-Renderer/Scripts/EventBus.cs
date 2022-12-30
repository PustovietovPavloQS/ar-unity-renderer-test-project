using System;
using UnityEngine;

public static class EventBus
{
    /// <summary>
    /// int - scene index
    /// </summary>
    public static Action<int> onSceneLoaded;        //Called automatically when the scene web template is configured and ready to recognize targets

    /// <summary>
    /// int - target index
    /// </summary>
    public static Action<int> onTargetFound;        //Called automatically when the target is found

    /// <summary>
    /// Vector3 - position,
    /// Quaternion - rotation,
    /// Vector3 - scale,
    /// float - camera field of view,
    /// string - face mesh vertices array as string
    /// </summary>
    public static Action<
         Vector3,
         Quaternion,
         Vector3,
         float,
         string> onFaceTracking;                    //Called automatically when the face is tracking


    /// <summary>
    /// Vector3 - position,
    /// Quaternion - rotation,
    /// Vector3 - scale,
    /// float - camera field of view,
    /// int - target index
    /// </summary>
    public static Action<
        Vector3,
        Quaternion,
        Vector3,
        float,
        int> onTargetTracking;                      //Called automatically when the target is tracking

    /// <summary>
    /// int - scene index
    /// </summary>
    public static Action<int> onTargetLost;         //Called automatically when the target is lost
}
