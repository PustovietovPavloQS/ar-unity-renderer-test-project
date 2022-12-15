using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;


public class TrackController : MonoBehaviour
{
    [SerializeField] private bool smoothScene = true;
    [SerializeField, Range(0, 1f)]
    private float smoothStrength = 0;
    [SerializeField] protected GameObject renderLayerGO;
    private float SmoothMin = 0;
    private float SmoothMax = 0.25f;

    //Private
    [Header ("Lerp speed")]
    private static readonly float FOVLerpSpeed = 15f;
    private static readonly float PosLerpSpeed = 15f;
    private static readonly float RotLerpSpeed = 15f;
    private static readonly float ScaleLerpSpeed = 15f;
    private static readonly int StackSize = 3;

    private Vector3[] positions;
    private Quaternion[] rotations;
    private Vector3[] scales;
    private float[] cameraFOVs;

    protected bool isTracking;
    protected static readonly float ScaleFactor = 0.115f;

    private int stackElementIndex = 0;
    
    //External function. Should be called when scene is loaded
    [DllImport("__Internal")]
    protected static extern int LoadScene(string message, string index, string controller, string targetImg);

    protected virtual void OnEnable() {
        isTracking = false;
        renderLayerGO.SetActive(false);
    }
    
    protected virtual void Start()
    {
        SetupBuffer();

        smoothStrength = Mathf.Clamp01(smoothStrength);
    }

    private void Update()
    {
        if (isTracking && renderLayerGO.activeSelf)
        {
            LerpSceneTransform(positions, rotations, scales, cameraFOVs);
        }
    }



    //Public functions
    //Called automatically when the target is found
    public virtual void StartTracking(int targetIndex) 
    {
        renderLayerGO.SetActive(true);
        isTracking = true;
        EventBus.onTargetFound?.Invoke(targetIndex);
    }
    
    //Called automatically when the target is tracking
    protected void Tracking() 
    {
        if (isTracking)
        {
            EventBus.onTargetTracking?.Invoke();
        }
    }

    //Called automatically when the target is lost
    public virtual void StopTracking(int targetIndex)
    {
        isTracking = false;
        renderLayerGO.SetActive(false);
        EventBus.onTargetLost?.Invoke(targetIndex);
    }

    public void SceneLoadedEvent(int sceneIndex) {
        EventBus.onSceneLoaded?.Invoke(sceneIndex);
    }



    //Private functions
    private void SetupBuffer() {
        positions = new Vector3[StackSize];
        rotations = new Quaternion[StackSize];
        scales = new Vector3[StackSize];
        cameraFOVs = new float[StackSize];
        cameraFOVs[0] = 60;
    }

    //Add received transform to buffer
    protected void AddToBuffer(Vector3 position, Quaternion rotation, Vector3 scale, float cameraFOV)
    {
        positions[stackElementIndex] = position;
        rotations[stackElementIndex] = rotation;
        scales[stackElementIndex] = scale;
        cameraFOVs[stackElementIndex] = cameraFOV;

        stackElementIndex++;

        if (stackElementIndex >= StackSize)
        {
            stackElementIndex = 0;
        }
    }

    //Smooth change scene transform (position, rotation, scale, camera field of view).
    private void LerpSceneTransform(Vector3[] pos, Quaternion[] rot, Vector3[] sc, float[] camFOVs)
    {
        if (smoothScene)
        {
            float smooth = Mathf.Lerp(SmoothMin, SmoothMax, smoothStrength);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, GetAverage(cameraFOVs), Time.deltaTime * FOVLerpSpeed * (1- smooth));
            renderLayerGO.transform.position = Vector3.Lerp(renderLayerGO.transform.position, GetAverage(pos), Time.deltaTime * PosLerpSpeed * (1 - smooth));
            renderLayerGO.transform.rotation = Quaternion.Lerp(renderLayerGO.transform.rotation, GetAverage(rot), Time.deltaTime * RotLerpSpeed * (1 - smooth));
            renderLayerGO.transform.localScale = Vector3.Lerp(renderLayerGO.transform.localScale, GetAverage(sc), Time.deltaTime * ScaleLerpSpeed * (1 - smooth));
        } else {
            Camera.main.fieldOfView = GetAverage(camFOVs);
            renderLayerGO.transform.position = GetAverage(pos);
            renderLayerGO.transform.rotation = GetAverage(rot);
            renderLayerGO.transform.localScale = GetAverage(sc);
        }
    }

    private Vector3 GetAverage(Vector3[] vectors)
    {
        if (vectors.Length == 0)
        {
            return Vector3.zero;
        }

        return vectors.Aggregate(Vector3.zero, (acc, v) => acc + v) / vectors.Length;
    }

    private float GetAverage(float[] floats)
    {
        if (floats.Length == 0)
        {
            return 0;
        }
        return floats.Average();
    }

    private Quaternion GetAverage(Quaternion[] quaternions)
    {
        if (quaternions.Length == 0)
        {
            return Quaternion.identity;
        }
        float x = 0;
        float y = 0;
        float z = 0;
        float w = 0;

        foreach (Quaternion quaternion in quaternions)
        {
            x += quaternion.x;
            y += quaternion.y;
            z += quaternion.z;
            w += quaternion.w;

        }

        x /= quaternions.Length;
        y /= quaternions.Length;
        z /= quaternions.Length;
        w /= quaternions.Length;

        return new Quaternion (x, y, z, w);
    }
}