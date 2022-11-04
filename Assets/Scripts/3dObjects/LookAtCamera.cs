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
        HandleLooking();
        #endif
    }

    private void HandleLooking()
    {
        if(mainCamera == null) mainCamera = Camera.main;
        var lookVector = mainCamera.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(lookVector, Vector3.forward);
    }

    private void OnEnable()
    {
        HandleLooking();
        EventBus.onTargetTracking += HandleLooking;
    }

    private void OnDisable()
    {
        EventBus.onTargetTracking -= HandleLooking;
    }
}
