using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance {get;private set;}
    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
    
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadYourAsyncScene(sceneIndex));
    }
    
    IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}