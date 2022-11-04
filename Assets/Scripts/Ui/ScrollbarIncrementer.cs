using System;
using UnityEngine;
using UnityEngine.UI;
 
public class ScrollbarIncrementer : MonoBehaviour
{
    [SerializeField] private Scrollbar target;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject content;
    
    private float step = 0.1f;

    public void Start()
    {
        target.value = 0;
        leftButton.onClick.AddListener(Decrement);
        rightButton.onClick.AddListener(Increment);
    }
 
    private void Increment()
    {
        UpdateStep();
        target.value = Mathf.Clamp(target.value + step, 0, 1);
        Debug.Log(target.value);
        rightButton.interactable = target.value < 1;
        leftButton.interactable = true;
    }
 
    private void Decrement()
    {
        UpdateStep();
        target.value = Mathf.Clamp(target.value - step, 0, 1);
        Debug.Log(target.value);
        leftButton.interactable = target.value > 0;
        rightButton.interactable = true;
    }

    private void UpdateStep()
    {
        var childCount = content.transform.childCount;
        step = 1f / (float)childCount;
        Debug.Log(step);
    }

}