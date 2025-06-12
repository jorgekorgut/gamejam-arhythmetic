using UnityEngine;

public class Circle
{
    private Vector3 initialCenter;
    public float radius;
    public Color color;

    public GameObject circleObject;

    public bool isSpecial = false;

    public bool isActive = false;

    public string audioTrack1 = null;
    public string audioTrack2 = null;

    public string audioTrack3 = null;

    public float audioTrack1Duration = -1f;
    public float audioTrack2Duration = -1f;
    public float audioTrack3Duration = -1f;
    
    public float audioTrack1Volume = 1f;
    public float audioTrack2Volume = 1f;
    public float audioTrack3Volume = 1f;

    SpriteRenderer spriteRenderer;

    public Circle(Vector3 center, float radius, Color color)
    {
        this.initialCenter = center;
        this.radius = radius;
        this.color = color;
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        if (circleObject != null)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color; // Update the color of the circle
            }
        }
    }

    public void SelectCircle()
    {
        // Darkens the circle color to indicate selection
        if (circleObject != null)
        {
            Color selectedColor = color * 0.5f; // Darken the color by multiplying with a factor
            spriteRenderer.color = selectedColor; // Update the color of the circle
        }
    }

    public void DeselectCircle()
    {
        // Restores the original color of the circle
        if (circleObject != null)
        {
            spriteRenderer.color = color; // Restore the original color of the circle
        }
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        if (circleObject != null)
        {
            circleObject.SetActive(isActive); // Activate or deactivate the circle GameObject
        }
    }

    public void Instantiate()
    {
        this.isActive = true; // Set the circle as active
        // Create a new GameObject for the circle
        circleObject = new GameObject("Circle");
        circleObject.transform.position = initialCenter; // Set the position of the circle
        circleObject.transform.localScale = new Vector3(radius, radius, 1); // Set the scale of the circle

        // Add a SpriteRenderer component to the GameObject
        spriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = color; // Set the color of the circle
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/circle"); // Load a sprite for the circle (ensure you have a CircleSprite in Resources)
        spriteRenderer.sortingOrder = 9; // Set the sorting order for rendering
    }
}

