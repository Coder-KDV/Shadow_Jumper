using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float smoothSpeed = 0.125f; // How smooth the camera follows
    public Vector3 offset; // Offset from the player

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // Target position with offset
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smooth interpolation
            transform.position = smoothedPosition;
        }
    }

    // Optional: Lock the camera rotation if you want a fixed angle
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
