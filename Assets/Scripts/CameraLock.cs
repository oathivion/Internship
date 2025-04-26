    using UnityEngine;

    public class CameraLock : MonoBehaviour {
        public float fixedRotationZ = 0; // Rotation value to keep

        void LateUpdate() {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, fixedRotationZ);
        }
    }