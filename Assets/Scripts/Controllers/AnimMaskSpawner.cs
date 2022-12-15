using System;
using UnityEngine;

namespace Controllers
{
    public class AnimMaskSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] bones;

        private void OnEnable()
        {
            if (bones != null)
            {
                SendBonesToAnimModel();
            }
        }

        private void SendBonesToAnimModel()
        {
            ProjectEvents.onAnimMaskSpawned?.Invoke(bones);
        }

        private void SendClearBonesCommand()
        {
            ProjectEvents.onAnimBonesCleared?.Invoke();
        }

        private void OnDisable()
        {
            if (bones != null)
            {
                SendClearBonesCommand();
            }
        }

        private void OnDestroy()
        {
            if (bones != null)
            {
                SendClearBonesCommand();
            }
        }
    }
}