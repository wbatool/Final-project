using UnityEngine;

public class BushController : MonoBehaviour
{
    public float detectionRadius = 5f; // Radius to detect the player
    private Animator animator;
    private bool isPlayerInRange = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Initially disable the enemy's animations
        animator.enabled = false;
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            // Enable the animator when the player is in range
            animator.enabled = true;

            // Add conditions to trigger different animations based on game logic
            // For example, you can use bool parameters in the animator to control specific animations
            // animator.SetBool("IsAttacking", true);
        }
        else
        {
            // Disable the animator when the player is out of range
            animator.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Optionally reset animations or perform other actions when the player exits the range
        }
    }
}