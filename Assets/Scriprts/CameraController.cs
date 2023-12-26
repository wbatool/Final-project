using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scaleFactor = 1.5f; // Adjust the scale factor as needed

    void Start()
    {
        // Set initial camera scale
        Camera.main.orthographicSize = scaleFactor;
    }
}
