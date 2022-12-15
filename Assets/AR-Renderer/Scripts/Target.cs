using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private TargetPreview targetPreview;
    [SerializeField] private GameObject obj;

    public TargetPreview TargetPreview { get { return targetPreview; } }

    public void DisplayPreview(bool state) {
        targetPreview.Display(state);
    }

    public void DisplayObj(bool state)
    {
        obj.SetActive(state);
    }
}
