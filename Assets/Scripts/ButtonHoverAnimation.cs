using UnityEngine;

public class ButtonHoverAnimation : MonoBehaviour
{
    // Color to change to when hovered
    public Color hoverColor = new Color(0.729f, 0.729f, 0.729f); // RGB values for BABABA color

    private Color originalColor; // Original color of the button
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Store the original color of the button
        originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        // Change the button's color to the hover color when the mouse enters
        spriteRenderer.color = hoverColor;
    }

    void OnMouseExit()
    {
        // Change the button's color back to the original color when the mouse exits
        spriteRenderer.color = originalColor;
    }
}
