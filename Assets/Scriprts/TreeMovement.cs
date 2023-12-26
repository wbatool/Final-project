using UnityEngine;

public class TreeMovement : MonoBehaviour
{
    public float moveDistance = 1.0f; // Adjust the movement distance as needed
    public float moveSpeed = 2.0f; // Adjust the movement speed as needed

    private Vector3 originalPosition;
    private bool isMoving = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the tree horizontally
            float newPositionX = Mathf.PingPong(Time.time * moveSpeed, moveDistance) + originalPosition.x - moveDistance / 2;
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player is near the tree, start moving
            isMoving = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player has left, stop moving
            isMoving = false;
            transform.position = originalPosition; // Reset the tree to its original position
        }
    }
}
