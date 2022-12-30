using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        #if UNITY_EDITOR
        HandleLooking(default, default, default, default, default);
        #endif
    }

    private void HandleLooking(Vector3 position, Quaternion rotation, Vector3 scale, float fov, int targetIndex)
    {
        if(mainCamera == null) mainCamera = Camera.main;
        var lookVector = mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(lookVector, Vector3.forward);
    }

    private void OnEnable()
    {
        HandleLooking(default, default, default, default, default);
        EventBus.onTargetTracking += HandleLooking;
    }

    private void OnDisable()
    {
        EventBus.onTargetTracking -= HandleLooking;
    }
}
