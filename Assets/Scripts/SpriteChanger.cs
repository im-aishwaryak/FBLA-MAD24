using UnityEngine;
using System.Collections;

public class SpriteChanger : MonoBehaviour
{
    public Sprite[] sprites;  // Array of sprites to cycle through
    public Sprite[] cornerSprites;
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
        if (!isChangingSprites && spriteRenderer != null && sprites.Length > 0)
        {
            StartCoroutine(ChangeToNextSprites(4, cornerSprites));
            //transform.position += new Vector3(5, 5, 0) * Time.deltaTime;
            //transform.Translate(new Vector3(20, 20, 0) * 0.2f * Time.deltaTime);
        }
        Debug.Log("I climbed THE CORNER!");
    }

    private IEnumerator ChangeToNextSprites(int numberOfSprites, Sprite[] sprites)
    {
        isChangingSprites = true;  // Set to true to indicate coroutine is running

        for (int i = 0; i < numberOfSprites; i++)
        {
            // Update to the next sprite in the array
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
            Debug.Log("Changed to sprite index: " + currentSpriteIndex);

            // Wait for 0.5 seconds before showing the next sprite
            yield return new WaitForSeconds(0.2f);
        }

        isChangingSprites = false;  // Reset when done
    }
}
