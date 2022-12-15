using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MasksController : MonoBehaviour
{
    [SerializeField] private GameObject[] masksObjects;
    [SerializeField] private MasksScrolling scrolling;
    [SerializeField] private FaceMesh faceMesh;

    private int currentMaskIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ProjectEvents.onFaceMeshHided += HideFaceMesh;
        EnableMask(currentMaskIndex);
        scrolling.onSelectedMaskChanged += ChangeMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextMask();
        }
    }

    public void EnableMask(int maskIndex)
    {
        if (currentMaskIndex == maskIndex) return;
        
        masksObjects[currentMaskIndex].SetActive(false);
        masksObjects[maskIndex].SetActive(true);
        currentMaskIndex = maskIndex;
    }

    private void NextMask()
    {
        int newMaskIndex = currentMaskIndex + 1;
        if (newMaskIndex >= masksObjects.Length) newMaskIndex = 0;
        EnableMask(newMaskIndex);
        scrolling.ChangeSelectedMaskButton(newMaskIndex);
    }

    private void ChangeMask(int newIndex)
    {
        if (newIndex == currentMaskIndex) return;

        if (newIndex >= masksObjects.Length)
        {
            Debug.LogError("You have button for mask but dont have the mask prefab for it");
            newIndex = 0;
        }
        EnableMask(newIndex);
    }

    private void HideFaceMesh(bool isHided)
    {
        faceMesh.HideFacemesh(isHided);
    }

    private void OnDestroy()
    {
        scrolling.onSelectedMaskChanged -= ChangeMask;
    }
}
