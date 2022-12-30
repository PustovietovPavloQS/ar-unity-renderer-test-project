using System;
using UnityEngine;

namespace Controllers
{
    public class FaceMeshBonesController : MonoBehaviour
    {
        [SerializeField] private FacemeshModelAnimation faceMeshModelAnimation;

        private void Start()
        {
            ProjectEvents.onAnimMaskSpawned += SetNewBones;
            ProjectEvents.onAnimBonesCleared += ClearBones;
        }

        private void ClearBones()
        {
            //faceMeshModelAnimation.bones.Clear();
            Array.Clear(faceMeshModelAnimation.bones, 0, faceMeshModelAnimation.bones.Length);
        }

        private void SetNewBones(Transform[] bones)
        {
            ClearBones();
            // foreach (var bone in bones)
            // {
            //     faceMeshModelAnimation.bones.Add(bone);
            // }

            faceMeshModelAnimation.bones = new Transform[bones.Length];

            for (int i = 0; i < bones.Length; i++)
            {
                faceMeshModelAnimation.bones[i] = bones[i];
            }
        }
    }
}