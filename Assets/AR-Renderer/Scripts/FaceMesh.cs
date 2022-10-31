using System.Globalization;
using UnityEngine;

public class FaceMesh : MonoBehaviour
{
    private Mesh mesh;
    
    public void HideFacemesh(bool state) 
    {
        gameObject.GetComponent<MeshRenderer>().enabled = !state;
    }

    public void RecalculateFaceMesh(string str)
    {
        if (mesh == null)
        {
            mesh = GetComponent<MeshFilter>().mesh;
            return;
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
    }

    public float[] StringToFloatArray(string str) 
    {
        string[] strArr = str.Split('\u002C');
        float[] floats = new float[strArr.Length];

        for (int i = 0; i < strArr.Length; i++)
        {
            floats[i] = float.Parse(strArr[i], CultureInfo.InvariantCulture);
        }

        return floats;
    }
}
