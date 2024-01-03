using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public Animator animator;
    public float speed = 5f;
    public float boundaryX = 5f;
    public float boundaryY = 5f;
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

        // Check if this player belongs to the local player
        if (photonView.IsMine)
        {
            SetupLocalPlayer();
        }
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            animator.SetFloat("Speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            rb.velocity = movement * speed;

            float clampedX = Mathf.Clamp(rb.position.x, -boundaryX, boundaryX);
            float clampedY = Mathf.Clamp(rb.position.y, -boundaryY, boundaryY);
            rb.position = new Vector2(clampedX, clampedY);
        }
    }

    [PunRPC]
  void AddScoreAndResetTree(int actorNumber)
    {
        if (photonView.Owner.ActorNumber == actorNumber)
        {
            scoreManager.AddScore(10);
            TakeDamage(10);

            // Convert HashSet to an array using ToArray() on the List obtained from HashSet
            Collider2D[] treeColliders = new List<Collider2D>(encounteredTrees).ToArray();
            
            foreach (Collider2D treeCollider in treeColliders)
            {
                encounteredTrees.Remove(treeCollider);
                treeCollider.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree") && !encounteredTrees.Contains(other))
        {
            photonView.RPC("AddScoreAndResetTree", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
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

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0 && chances > 0)
        {
            currentHealth = maxHealth;
            chances--;

            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
            }
        }
        else if (currentHealth <= 0 && chances == 0)
        {
            Debug.Log("Game Over");
        }
    }

    void SetupLocalPlayer()
    {
        Camera.main.orthographicSize = 1.5f; // Set the initial camera size for the local player
    }
}
