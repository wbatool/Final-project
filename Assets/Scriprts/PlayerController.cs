using UnityEngine;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
     public float boundaryX = 5f; // Adjust the X-axis boundary
    public float boundaryY = 5f; // Adjust the Y-axis boundary
    private ScoreManager scoreManager;
    private HashSet<Collider2D> encounteredTrees = new HashSet<Collider2D>();


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Move the player based on the input
        rb.velocity = movement * speed;

        // Move the camera to follow the player
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);

//max boundary on x and y player can move
        float clampedX = Mathf.Clamp(rb.position.x, -boundaryX, boundaryX);
        float clampedY = Mathf.Clamp(rb.position.y, -boundaryY, boundaryY);
        rb.position = new Vector2(clampedX, clampedY);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree") && !encounteredTrees.Contains(other))
        {
            // Player reached a tree, add score and reset tree position
            scoreManager.AddScore(10);
            encounteredTrees.Add(other);
            other.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
    }
void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            encounteredTrees.Remove(other);
        }
    }
    
}
