using System;

public static class EventBus
{
    //Pass loaded scene index
    public static Action<int> onSceneLoaded;        //Called automatically when the scene web template is configured and ready to recognize targets

    public static Action<int> onTargetFound;        //Called automatically when the target is found

    public static Action onTargetTracking;          //Called automatically when the target is tracking

    public static Action<int> onTargetLost;         //Called automatically when the target is lost
}
