using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TargetPreview : MonoBehaviour
{
    [SerializeField] private Texture image;
    public Texture Image { get { return image; } }

    private void OnValidate()
    {
        Material material = new Material(Shader.Find("Unlit/Texture"));
        float ratio = (float)image.width / (float)image.height;
        transform.localScale = new Vector3(1, 1, 1/ratio);
        material.mainTexture = image;
        material.mainTextureScale = new Vector2(-1, -1);
        GetComponent<MeshRenderer>().material = material;
    }

    public void Display(bool state) {
        GetComponent<MeshRenderer>().enabled = state;
    }
}
