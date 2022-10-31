using System;
using UnityEngine;

public class ButtonStateSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] buttonsBackgrounds;

    private int activeButton = 0;

    private void Awake()
    {
        foreach (var background in buttonsBackgrounds)
        {
            background.SetActive(false);
        }
        buttonsBackgrounds[activeButton].SetActive(true);
    }

    public void EnableNewButton(int index)
    {
        if (activeButton == index) return;
        buttonsBackgrounds[activeButton].SetActive(false);
        buttonsBackgrounds[index].SetActive(true);
        activeButton = index;
    }

}