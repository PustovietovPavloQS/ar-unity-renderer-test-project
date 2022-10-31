using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class SurfaceTrackController : TargetTrackController
{
    [DllImport("__Internal")]
    private static extern int ScanSurfaceCmd(string sceneIndex, string controller);

    protected override void Awake()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene("surface", sceneIndex.ToString(), gameObject.name,null);
    }

    public void ScanSurface() 
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        ScanSurfaceCmd(sceneIndex.ToString(), gameObject.name);
    }
}
