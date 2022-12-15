using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetTrackController : TrackController
{
    [SerializeField] private Target[] targets;

    protected virtual void OnValidate() {
        CreateScheme();
    }

    public void CreateScheme() {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex < 0)
        {
            Debug.LogError("Invalid scene build index (" + buildIndex + "). The scheme for scene has not been created.");
            return;
        }
        DotMindSchemasController.CreateSchema(SceneManager.GetActiveScene().buildIndex, targets);
        Debug.Log("The scheme for scene has been created.");
    }

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

        LoadScene("image", sceneIndex.ToString(), gameObject.name, targets.Length.ToString());
    }

    protected override void Start()
    {
        base.Start();
        HideAllTargets();
    }

    public override void StartTracking(int targetIndex)
    {
        base.StartTracking(targetIndex);
        if (targetIndex != -1)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (i == targetIndex)
                {
                    targets[i].DisplayObj(true);
                }
                else {
                    targets[i].DisplayObj(false);
                }
            }
        }
    }

    public override void StopTracking(int targetIndex)
    {
        base.StopTracking(targetIndex);

        if (targetIndex != -1)
        {
            targets[targetIndex].DisplayObj(false);
        }
    }

    //Called automatically when target image or surface is tracking
    protected void Tracking(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFOV, int targetIndex)
    {
        Tracking();
        Quaternion rotationConverted = new Quaternion(rotation.x, -rotation.y, -rotation.z, rotation.w);
        Vector3 scaleConverted = scale * ScaleFactor;

        AddToBuffer(position, rotationConverted, scaleConverted, cameraFOV);
    }

    void HideAllTargets() {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].DisplayObj(false);
            targets[i].DisplayPreview(false);
        }
    }
}