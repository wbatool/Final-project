using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPun
{
    public float scaleFactor = 1.5f; // Adjust the scale factor as needed

    void Start()
    {
        if (photonView.IsMine)
        {
            // Set initial camera scale only for the local player
            Camera.main.orthographicSize = scaleFactor;
        }
        else
        {
            // Disable the Camera component for remote players
            Camera.main.enabled = false;
        }
    }
}
