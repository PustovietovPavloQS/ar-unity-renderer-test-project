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
            faceMeshModelAnimation.bones.Clear();
        }

        private void SetNewBones(Transform[] bones)
        {
            ClearBones();
            foreach (var bone in bones)
            {
                faceMeshModelAnimation.bones.Add(bone);
            }
        }
    }
}