using UnityEngine;

public class SpriteJumper : MonoBehaviour
{
    public float JumpForce; // Adjustable jump force in the Unity Inspector

    // Declare Rigidbody2D reference
    [SerializeField]
    bool isGrounded = false; // Whether the player is grounded

    Rigidbody2D RB;

    // Reference to the Pause Canvas
    public GameObject pauseCanvas;

    // Called when the script is initialized
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the space key is pressed and the character is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); // Apply upward force to simulate jump
            isGrounded = false; // Set isGrounded to false as the character is in the air now
        }
    }

    // Called when a collision with another object happens
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the character collides with the ground
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true; // Set isGrounded to true when the character hits the ground
        }

        // Check if the character collides with the snowball
        if (collision.gameObject.CompareTag("Snowball"))
        {
            PauseGame(); // Pause the game and show the canvas
        }
    }

    // Called when the character exits a collision with another object
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false; // Set isGrounded to false when the character leaves the ground
        }
    }

    // Method to pause the game and show the canvas
    private void PauseGame()
    {
        Time.timeScale = 0f; // Pauses the game
        pauseCanvas.SetActive(true); // Makes the pause canvas visible
    }
}
