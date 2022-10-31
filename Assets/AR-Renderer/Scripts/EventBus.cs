using System;

public static class EventBus
{
    public static Action<int> onSceneLoaded;        //Pass loaded scene index
    public static Action onTargetFound;
    public static Action onTargetTracking;
    public static Action onTargetLost;
}
