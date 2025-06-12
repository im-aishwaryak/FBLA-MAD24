using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;
public class WallChanger : MonoBehaviour
{
    public float moveForce = 10f;          // Force applied to the wall for each movement
    public float moveDuration = 0.6f;       // Duration of movement in seconds
    public float maxYPosition = 0;    // Upper boundary for the wall’s position
    public float minYPosition = 0;     // Lower boundary for the wall’s position

    private Rigidbody2D rb;
    private bool isMoving = false;

    // ELEVATION TRACKER
    public Text elevationText;
    private float distanceLeft = 0;

    public GameObject quizCanvas;
    public SpriteChanger spriteChanger;

    private bool reachedtop = false;

    [SerializeField] private AudioClip climbingSound;
    [SerializeField] private AudioClip scratchingSound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Keeps the wall stationary initially

        distanceLeft = (transform.position.y - minYPosition);
    }

    void Update()
    {
        // Clamp the wall's position within the allowed Y bounds
        float clampedY = Mathf.Clamp(transform.position.y, minYPosition, maxYPosition);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        // Update the score UI
        distanceLeft = (transform.position.y - minYPosition);
        elevationText.text = "Distance Left - " + distanceLeft + " ft";

        if ((transform.position.y == minYPosition) && (reachedtop == false))
        {
            reachedtop = true;
            OnMinHeightReached();
            Debug.Log("am i the problem uwu >>>");

        }
    }
    public void Move(bool isCorrect)
    {
        if (!isMoving)
        {
            isMoving = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;

            // Move up or down based on whether the answer is correct
            isCorrect = !(isCorrect);
            Vector2 forceDirection;
            if (isCorrect) 
            {
                forceDirection = Vector2.up;
                SoundManager.instance.PlaySound(scratchingSound);
            } else
            {
                forceDirection = Vector2.down;
                SoundManager.instance.PlaySound(climbingSound);
            }
            rb.AddForce(forceDirection * moveForce, ForceMode2D.Impulse);

            // Stop movement after the specified duration
            Invoke(nameof(StopMovement), moveDuration);
        }
    }

    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        isMoving = false;
    }

    private void OnMinHeightReached()
    {
        Debug.Log("MAX height reached!");

        // Directly hide the canvas by setting it inactive
        if (quizCanvas != null)
        {
            quizCanvas.SetActive(false);
        }

       spriteChanger.climbCorner();
       StartCoroutine(TransitionAfterDelay());

        // Application.Quit(); // Stops the program
    }

    private IEnumerator TransitionAfterDelay()
    {
        yield return new WaitForSeconds(4f); // Give sprite time to animate
        SceneManager.LoadScene("DinoRun");
    }
}
