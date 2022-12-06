using System.Collections.Generic;
using UnityEngine;

public class FacemeshModelAnimation : MonoBehaviour
{
    public List<Transform> bones;

    public void Animate(Vector3[] vertices) {
        for (int i = 0; i < bones.Count; i++)
        {
            int targetVertex = int.Parse(bones[i].name);
            CalculateBonePosition(bones[i], vertices[targetVertex]);
        }
    }

    private void CalculateBonePosition(Transform bone, Vector3 vertexPosition) {
        bone.position = vertexPosition;
    }
}