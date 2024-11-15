using UnityEngine;
using System.Collections;
using TMPro;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;  // Array of sprites to cycle through for regular climbing
    public Sprite[] cornerSprites;  // Sprites for reaching the top
    public Sprite[] walkingSprites;  // Sprites for walking off-screen
    public SpriteRenderer spriteRenderer;
    public int currentSpriteIndex = 0;
    private bool isChangingSprites = false;  // Tracks if coroutine is running

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void climb(int numberOfSprites)
    {
        if (!isChangingSprites && spriteRenderer != null && sprites.Length > 0)
        {
            StartCoroutine(ChangeToNextSprites(numberOfSprites, sprites));
        }
        Debug.Log("I climbed!");
    }

    public void climbCorner()
    {
        StartCoroutine(ClimbAndStandAnimation());
        /*if (!isChangingSprites && spriteRenderer != null && cornerSprites.Length > 0)
        {
            // Start the sprite change for the corner (climbing-to-standing) animation twice
            StartCoroutine(ClimbAndStandAnimation());
        }*/
        Debug.Log("I climbed THE CORNER!");
    }

    private IEnumerator ClimbAndStandAnimation()
    {
        isChangingSprites = true;

        // Perform the climbing-to-standing animation with cornerSprites (4 sprites, twice)
        for (int i = 0; i < 4; i++)
        {
            yield return ChangeToNextSprites(1, cornerSprites);
            transform.position += new Vector3(2, 2, 0);
        }

        // Start the walking animation after reaching the top
        StartCoroutine(WalkOffScreen());

        isChangingSprites = false;
    }

    // Coroutine to smoothly move the character to the side
    private IEnumerator WalkOffScreen()
    {
        Vector3 targetPosition = new Vector3(-60, 30, 0);
        float walkSpeed = 15f; // Speed of walking, adjust as needed
        float spriteChangeInterval = 0.1f; // Time between sprite changes
        float lastSpriteChangeTime = 0f; // Time of the last sprite change

        int walkSpriteIndex = 0; // Track the walking sprite index

        // Keep walking until we reach the target position
        while (transform.position.x < targetPosition.x)
        {
            // Move the character at a constant speed
            transform.position += new Vector3(walkSpeed * Time.deltaTime, 0, 0);

            // Check if it's time to change the sprite
            if (Time.time - lastSpriteChangeTime >= spriteChangeInterval)
            {
                // Update the sprite
                spriteRenderer.sprite = walkingSprites[walkSpriteIndex];
                walkSpriteIndex = (walkSpriteIndex + 1) % walkingSprites.Length;

                // Update the time of the last sprite change
                lastSpriteChangeTime = Time.time;
            }

            yield return null; // Wait for the next frame
        }

        // Ensure character reaches the target position exactly and stop immediately
        transform.position = targetPosition;
    }



    private IEnumerator ChangeToNextSprites(int numberOfSprites, Sprite[] sprites)
    {
        isChangingSprites = true;

        for (int i = 0; i < numberOfSprites; i++)
        {
            // Update to the next sprite in the array
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            Debug.Log("Changed to sprite index: " + currentSpriteIndex);

            // Wait for 0.2 seconds before showing the next sprite
            yield return new WaitForSeconds(0.2f);
        }

        isChangingSprites = false;
    }
}
