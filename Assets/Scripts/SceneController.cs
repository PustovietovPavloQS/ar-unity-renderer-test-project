using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject infoObj;
    [SerializeField] private bool infoVisibleAtStart = false;

    [Header("Buttons")]
    [SerializeField] private Button returnBtn;
    [SerializeField] private Button infoBtn;

    [Header("Loading")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    
    // Start is called before the first frame update
    private void Start()
    {
        loadingScreen.SetActive(false);
        mainMenu.SetActive(true);
        if(infoObj) infoObj.SetActive(infoVisibleAtStart);
        returnBtn.onClick.AddListener(BackToMainMenu);
        if(infoBtn) infoBtn.onClick.AddListener(ShowInfoButton);
    }

    private void ShowInfoButton()
    {
        infoObj.SetActive(!infoObj.activeSelf);
    }

    private void BackToMainMenu()
    {
        loadingScreen.SetActive(true);
        mainMenu.SetActive(false);
        SceneLoader.Instance.LoadScene(0);
    }
}
