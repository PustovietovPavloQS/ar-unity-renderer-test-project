using System.Globalization;
using UnityEngine;

public class FaceMesh : MonoBehaviour
{
    public Transform LeftEye { get; private set; }
    public Transform RightEye { get; private set; }
    
    private Mesh mesh;
    FacemeshModelAnimation model;
    private Vector3[] points;
    public void HideFacemesh(bool state) 
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !state;
    }

    //Set new facemesh vertices
    public void RecalculateFaceMesh(string str)
    {
        if (mesh == null)
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }

        if (LeftEye == null || RightEye == null)
        {
            LeftEye = new GameObject("LeftEye").transform;
            RightEye = new GameObject("RightEye").transform;
        }
        float[] faceMeshData = StringToFloatArray(str);
        Vector3[] positions = new Vector3[faceMeshData.Length / 3];
        for (int i = 0; i < faceMeshData.Length / 3; i++)
        {
            positions[i] = new Vector3(faceMeshData[i * 3], faceMeshData[i * 3 + 1], faceMeshData[i * 3 + 2]);
        }
        mesh.vertices = positions;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
        if (model == null)
        {
            model = GetComponent<FacemeshModelAnimation>();
        }
        points = mesh.vertices;
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.TransformPoint(points[i]);
        }
        RecalculateLeftEye();
        RecalculateRightEye();
        model.Animate(points);
    }

    /// <summary>
    /// Recalculation of the position of the left eye.
    /// 263 and 362 - indices of the vertices of the edges of the eye according to the canonical face.
    /// </summary>
    private void RecalculateLeftEye()
    {
        var leftEyePosition = (points[263] + points[362])/2;
        LeftEye.position = leftEyePosition;
    }
    
    /// <summary>
    /// Recalculation of the position of the right eye.
    /// 133 and 33 - indices of the vertices of the edges of the eye according to the canonical face.
    /// </summary>
    private void RecalculateRightEye()
    {
        var rightEyePosition = (points[133] + points[33])/2;
        RightEye.position = rightEyePosition;
    }
    
    private float[] StringToFloatArray(string str) 
    {
        string[] strArr = str.Split('\u002C');              //Split array by comma (,)
        float[] floats = new float[strArr.Length];

        for (int i = 0; i < strArr.Length; i++)
        {
            floats[i] = float.Parse(strArr[i], CultureInfo.InvariantCulture);
        }

        return floats;
    }
}
