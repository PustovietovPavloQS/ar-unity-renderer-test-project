using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button backBtn;

    [Header("Scenes")] 
    [SerializeField] private List<TrackingSceneButton> sceneButtons;

    [Header("Loading screen")] 
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private List<MenuPage> menuPages;

    private MenuPage activePage;
  
    private void Start()
    {
        loadingScreen.SetActive(false);
        mainMenu.SetActive(true);
        backBtn.gameObject.SetActive(false);
        OpenPage(MainMenuPageType.Main);
        foreach (var page in menuPages)
        {
            if(page.pageOpenButton == null) continue;
            page.pageOpenButton.onClick.AddListener(() => OpenPage(page.pageType));
        }
        backBtn.onClick.AddListener(OpenPreviousPage);

        foreach (var button in sceneButtons)
        {
            button.button.onClick.AddListener(() => LoadScene(button.sceneIndex));
        }
    }

    private void OpenPage(MainMenuPageType pageType)
    {
        foreach (var page in menuPages)
        {
            var isActive = page.pageType == pageType;
            page.page.SetActive(isActive);
            if (isActive) activePage = page;
        }
        backBtn.gameObject.SetActive(pageType != MainMenuPageType.Main);
    }

    private void OpenPreviousPage()
    {
        if (activePage == null)
        {
            OpenPage(MainMenuPageType.Main);
            return;
        }
        OpenPage(activePage.previousPage);
    }
    
    private void LoadScene(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        mainMenu.SetActive(false);
        SceneLoader.Instance.LoadScene(sceneIndex);
    }
}

[Serializable]
public class TrackingSceneButton
{
    public Button button;
    public int sceneIndex;
}

[Serializable]
public class MenuPage
{
    public GameObject page;
    public MainMenuPageType pageType;
    public Button pageOpenButton;
    public MainMenuPageType previousPage;
}

public enum MainMenuPageType
{
    Main,
    Images
}
