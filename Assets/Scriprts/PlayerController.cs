using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
     public float boundaryX = 5f; // Adjust the X-axis boundary
    public float boundaryY = 5f; // Adjust the Y-axis boundary
    private ScoreManager scoreManager;
    private int maxHealth = 100;
    private int currentHealth;
    private int chances = 5;
    public Slider healthSlider;

    private HashSet<Collider2D> encounteredTrees = new HashSet<Collider2D>();


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        scoreManager = FindObjectOfType<ScoreManager>();
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
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
            TakeDamage(10);
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
    


     void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health slider value
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // Check if player has run out of chances
        if (currentHealth <= 0 && chances > 0)
        {
            // Player lost a chance, reset health and decrement chances
            currentHealth = maxHealth;
            chances--;

            // Update the health slider value
            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
            }
        }
        else if (currentHealth <= 0 && chances == 0)
        {
            // Player is out of chances, handle game over or other logic
            Debug.Log("Game Over");
        }
    }
}

