using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = new Vector3(0, 0, 1); // Z-axis for 2D
    public float rotationSpeed = 100f; // degrees per second

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}