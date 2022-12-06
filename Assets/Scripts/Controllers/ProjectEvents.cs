using System;
using UnityEngine;

namespace Controllers
{
    public static class ProjectEvents
    {
        public static Action<bool> onFaceMeshHided;
        public static Action<Transform[]> onAnimMaskSpawned;
        public static Action onAnimBonesCleared;
    }
}