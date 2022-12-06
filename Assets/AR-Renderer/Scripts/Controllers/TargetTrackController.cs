using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetTrackController : TrackController
{
    [SerializeField] private bool hideTargetImage = false;
    [SerializeField] private TargetPreview targetPreview;

    protected override void OnEnable()
    {
        Receiver.Activate();
        Receiver.onTargetTransformReceived += Tracking;
        base.OnEnable();
    }

    private void OnDisable()
    {
        Receiver.onTargetTransformReceived -= Tracking;
    }

    protected virtual void Awake()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        string targetImg = targetPreview.Image.name;

        LoadScene("image", sceneIndex.ToString(), gameObject.name, targetImg);
    }

    protected override void Start()
    {
        base.Start();
        if (hideTargetImage)
        {
            targetPreview.gameObject.SetActive(false);
        }
    }

    //Called automatically when target image or surface is tracking
    protected void Tracking(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFOV)
    {
        Tracking();
        Quaternion rotationConverted = new Quaternion(rotation.x, -rotation.y, -rotation.z, rotation.w);
        Vector3 scaleConverted = scale * ScaleFactor;

        AddToBuffer(position, rotationConverted, scaleConverted, cameraFOV);
    }
}