using UnityEngine;

public class Circle
{
    private Vector3 initialCenter;
    public float radius;

    public Color color;

    public GameObject circleObject;

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
            SpriteRenderer spriteRenderer = circleObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color; // Update the color of the circle
            }
        }
    }

    public void Instantiate()
    {
        // Create a new GameObject for the circle
        circleObject = new GameObject("Circle");
        circleObject.transform.position = initialCenter; // Set the position of the circle
        circleObject.transform.localScale = new Vector3(radius, radius, 1); // Set the scale of the circle

        // Add a SpriteRenderer component to the GameObject
        SpriteRenderer spriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = color; // Set the color of the circle
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/circle"); // Load a sprite for the circle (ensure you have a CircleSprite in Resources)
        spriteRenderer.sortingOrder = 0; // Set the sorting order for rendering
    }
}

