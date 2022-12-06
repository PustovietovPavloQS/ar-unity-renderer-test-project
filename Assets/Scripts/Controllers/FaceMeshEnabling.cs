using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Controllers
{
    public class FaceMeshEnabling : MonoBehaviour
    {
        private void Awake()
        {
            ProjectEvents.onFaceMeshHided?.Invoke(false);
        }

        private void OnEnable()
        {
            ProjectEvents.onFaceMeshHided?.Invoke(false);
        }

        private void OnDisable()
        {
            ProjectEvents.onFaceMeshHided?.Invoke(true);
        }

        private void OnDestroy()
        {
            ProjectEvents.onFaceMeshHided?.Invoke(true);
        }
    }
}