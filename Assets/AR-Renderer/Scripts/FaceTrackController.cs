using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceTrackController : TrackController
{
    [SerializeField] private FaceMesh faceMesh;
    [SerializeField] private bool hideFacemesh = false;
    
    protected override void OnEnable()
    {
        Receiver.Activate();
        Receiver.onFacemeshTransformReceived += Tracking;
        base.OnEnable();
    }

    private void OnDisable()
    {
        Receiver.onFacemeshTransformReceived -= Tracking;
    }

    private void Awake()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene("face", sceneIndex.ToString(), gameObject.name, "");
    }

    protected override void Start()
    {
        base.Start();
        faceMesh.HideFacemesh(hideFacemesh);        //Hide default facemesh if necessary
    }

    //Called automatically when face is tracking
    protected void Tracking(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFOV, string facemeshData)
    {
        Tracking();
        if (faceMesh != null && faceMesh.gameObject.activeSelf)
        {
            faceMesh.RecalculateFaceMesh(facemeshData);
        }

        Vector3 positionConverted = new Vector3(-position.x, position.y, position.z);
        Vector3 scaleConverted = scale * ScaleFactor;

        AddToBuffer(positionConverted, rotation, scaleConverted, cameraFOV);
    }
}