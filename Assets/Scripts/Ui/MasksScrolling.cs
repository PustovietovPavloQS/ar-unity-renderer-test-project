using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasksScrolling : MonoBehaviour
{
    public Action<int> onSelectedMaskChanged;
    
    [SerializeField, Range(0f, 20f)]
    private float snapSpeed;
    [SerializeField, Range(1f, 20f)]
    private float scaleSpeed;
    [SerializeField] private float buttonsMaxSpacing;
    [SerializeField] private float buttonsMinSpacing;
    [SerializeField] private float buttonsSize;
    [Header("Other Objects")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject[] maskButtons;
    
    private Vector2[] masksPos;
    private Vector2[] masksScale;
 
    private RectTransform contentRect;
    private Vector2 contentVector;
 
    private int selectedMaskID;
    private bool isScrolling;
    // Start is called before the first frame update
    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        ArrangeMasks();
        masksPos = new Vector2[maskButtons.Length];
        masksScale = new Vector2[maskButtons.Length];
        
        for (int i = 0; i < maskButtons.Length; i++)
        {
            masksPos[i] = -maskButtons[i].transform.localPosition;
            masksScale[i] = maskButtons[i].transform.localScale;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (scrollRect.inertia && (contentRect.anchoredPosition.x >= masksPos[0].x && !isScrolling ||
            contentRect.anchoredPosition.x <= masksPos[^1].x && !isScrolling))
        {
            scrollRect.inertia = false;
            RecalculateCurrentMask();
        }
        
        float nearestPos = Mathf.Abs(contentRect.anchoredPosition.x - masksPos[selectedMaskID].x);
        for (int i = 0; i < maskButtons.Length; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - masksPos[i].x);
            if (distance < nearestPos && (scrollRect.inertia || isScrolling))
            {
                nearestPos = distance;
                if (selectedMaskID != i)
                {
                    ChangeSelectedMaskButton(i);
                }
            }
            
            float scale = Mathf.Clamp(1 - distance / (contentRect.rect.width / 2f), 0.25f, 1f);
            masksScale[i].x = Mathf.SmoothStep(maskButtons[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            masksScale[i].y = Mathf.SmoothStep(maskButtons[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            maskButtons[i].transform.localScale = masksScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollRect.inertia && scrollVelocity < 400 && !isScrolling)
        {
            scrollRect.inertia = false;
            RecalculateCurrentMask();
        }
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, masksPos[selectedMaskID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }
    
    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    private void ArrangeMasks()
    {
        float btnSpace = contentRect.rect.width / maskButtons.Length;
        if (buttonsMinSpacing + buttonsSize > btnSpace) btnSpace = buttonsMinSpacing + buttonsSize;
        if (btnSpace > buttonsMaxSpacing + buttonsSize) btnSpace = buttonsMaxSpacing + buttonsSize;

        int counter = 0;
        foreach (var button in maskButtons)
        {
            float newXValue = (-contentRect.rect.width/2) + counter * btnSpace + btnSpace / 2;
            var position = button.transform.localPosition;
            position = new Vector3(newXValue, position.y, position.z);
            button.transform.localPosition = position;
            counter++;
        }
    }

    private void RecalculateCurrentMask()
    {
        onSelectedMaskChanged?.Invoke(selectedMaskID);
    }

    public void ChangeSelectedMaskButton(int index)
    {
        selectedMaskID = index;
    }

    public void ChangeSelectedMaskButtonWithNotify(int index)
    {
        ChangeSelectedMaskButton(index);
        RecalculateCurrentMask();
    }
}
