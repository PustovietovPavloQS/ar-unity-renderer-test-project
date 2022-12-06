using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class MasksController : MonoBehaviour
{
    [SerializeField] private Transform masksContainer;
    [SerializeField] private GameObject[] masksPrefabs;
    [SerializeField] private Button nextMaskButton;
    [SerializeField] private MasksScrolling scrolling;
    [SerializeField] private FaceMesh faceMesh;

    private Queue<GameObject> masksQueue;
    private GameObject currentMask;
    private int currentMaskIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ProjectEvents.onFaceMeshHided += HideFaceMesh;
        EnableMask(currentMaskIndex);
        nextMaskButton.onClick.AddListener(NextMask);
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
        if(currentMask) Destroy(currentMask.gameObject);
        
        var newMaskPrefab = masksPrefabs[maskIndex];
        currentMask = Instantiate(newMaskPrefab, masksContainer);
    }

    private void CreateQueue()
    {
        masksQueue = new Queue<GameObject>();
        foreach (var prefab in masksPrefabs)
        {
            masksQueue.Enqueue(prefab);
        }
    }

    private void NextMask()
    {
        currentMaskIndex++;
        if (currentMaskIndex >= masksPrefabs.Length) currentMaskIndex = 0;
        EnableMask(currentMaskIndex);
        scrolling.ChangeSelectedMaskButton(currentMaskIndex);
    }

    private void ChangeMask(int newIndex)
    {
        if (newIndex == currentMaskIndex) return;

        currentMaskIndex = newIndex;
        if (currentMaskIndex >= masksPrefabs.Length)
        {
            Debug.LogError("You have button for mask but dont have the mask prefab for it");
            currentMaskIndex = 0;
        }
        EnableMask(currentMaskIndex);
    }
    
    private void UpdateMask()
    {
        if(currentMask) Destroy(currentMask.gameObject);

        var newMask = masksQueue.Dequeue();
        if (newMask)
        {
            currentMask = Instantiate(newMask, masksContainer);
            masksQueue.Enqueue(newMask);
        }
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
