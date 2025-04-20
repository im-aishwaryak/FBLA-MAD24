using UnityEngine;


public class SpriteJumper : MonoBehaviour
{
    public float JumpForce; // Adjustable jump force in the Unity Inspector
    public gameLogicData globalLogic;

    // Declare Rigidbody2D reference
    [SerializeField]
    private bool isGrounded = false; // Whether the player is grounded

    Rigidbody2D RB;

    // Reference to the Pause Canvas
    public GameObject pauseCanvas;
    public LogicScript logic;

    public int jumpCount = 0;
    void Start(){
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        // globalLogic = GameObject.FindGameObjectWithTag()
        Debug.Log("time" + Time.timeScale);
    }

    // Called when the script is initialized
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            Debug.Log("input!");
        }
        // Check if the space key is pressed and the character is grounded
        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount < 2))
        {
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); // Apply upward force to simulate jump
            isGrounded = false; // Set isGrounded to false as the character is in the air now
            jumpCount +=1;
        } else if (Input.touchCount > 0 && isGrounded){
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended){
                Debug.Log("Touch ended");
                RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse); // Apply upward force to simulate jump'
                jumpCount +=1;
                isGrounded = false; // Set isGrounded to false as the character is in the air now
            }
        }
    }

    // Called when a collision with another object happens
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the character collides with the ground
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true; // Set isGrounded to true when the character hits the ground
            Debug.Log("On ground");
            jumpCount = 0;
        }

        // Check if the character collides with the snowball
        // if (collision.gameObject.CompareTag("Snowball"))
        // {
        //     // PauseGame(); // Pause the game and show the canvas
        //     // logic.gameOver();//ends game
        //     // gameLogicData.loseStuff();
        // }

        // if(collision.gameObject.CompareTag("ingredient")){
        //     Debug.Log("Get BErry");

        // }
    }

    // Called when the character exits a collision with another object
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("ground"))
    //     {
    //         isGrounded = false; // Set isGrounded to false when the character leaves the ground
    //     }
    // }

    // Method to pause the game and show the canvas
    // private void PauseGame()
    // {
    //     // Time.timeScale = 0f; // Pauses the game
    //     pauseCanvas.SetActive(true); // Makes the pause canvas visible
    // }
}
