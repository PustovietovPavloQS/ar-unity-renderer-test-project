using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MasksController : MonoBehaviour
{
    [SerializeField] private Transform masksContainer;
    [SerializeField] private GameObject[] masksPrefabs;
    [SerializeField] private Button nextMaskButton;
    [SerializeField] private ButtonStateSwitcher switcher;

    private Queue<GameObject> masksQueue;
    private GameObject currentMask;
    private int currentMaskIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        EnableMask(currentMaskIndex);
        nextMaskButton.onClick.AddListener(NextMask);
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
        
        switcher.EnableNewButton(maskIndex);
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
}
