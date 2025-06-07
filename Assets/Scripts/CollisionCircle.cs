using UnityEngine;

public class CollisionCircle
{
    public Vector3 center;
    public float radius;
    public Color color;

    public CollisionCircle(Vector3 center, float radius, Color color)
    {
        this.center = center;
        this.radius = radius;
        this.color = color;
    }

    public void Instantiate()
    {
        // Create a new GameObject for the circle
        GameObject circleObject = new GameObject("CollisionCircle");
        circleObject.transform.position = center; // Set the position of the circle
        circleObject.transform.localScale = new Vector3(radius, radius, 1); // Set the scale of the circle

        // Add a SpriteRenderer component to the GameObject
        SpriteRenderer spriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = color; // Set the color of the circle
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/collision-circle"); // Load a sprite for the circle (ensure you have a CircleSprite in Resources)
        spriteRenderer.sortingOrder = 0; // Set the sorting order for rendering
    }

    public bool IsPointOutside(Vector3 point)
    {
        // Check if the point is inside the circle
        return Vector3.Distance(center, point) > radius;
    }
}

