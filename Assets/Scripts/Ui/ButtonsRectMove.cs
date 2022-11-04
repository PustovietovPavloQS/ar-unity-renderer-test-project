using System;
using UnityEngine;
using UnityEngine.UI;


public class ButtonsRectMove : MonoBehaviour
{
    [SerializeField] private Scrollbar target;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private GameObject content;
    [SerializeField] private float offset;
    
    public void Start()
    {
        target.value = 0;
        leftButton.interactable = false;
        leftButton.onClick.AddListener(Decrement);
        rightButton.onClick.AddListener(Increment);
        target.onValueChanged.AddListener(UpdateButtonsInteractable);
    }
 
    private void Increment()
    {
        content.transform.localPosition += new Vector3(-offset, 0, 0);
    }
 
    private void Decrement()
    {
        content.transform.localPosition += new Vector3(offset, 0, 0);
    }

    private void UpdateButtonsInteractable(float newValue)
    {
        Debug.Log(target.value);
        rightButton.interactable = target.value < 1;
        leftButton.interactable = target.value > 0;
    }
    
}