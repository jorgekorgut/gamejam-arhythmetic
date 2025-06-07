using UnityEngine;

public class BouncingBall
{
    public Vector3 center;
    public float radius;

    public Color color;

    public Vector3 velocity;

    GameObject circleObject;

    public BouncingBall(Vector3 center, float radius, Color color, Vector3 initialVelocity)
    {
        this.center = center;
        this.radius = radius;
        this.color = color;
        this.velocity = initialVelocity;
    }

    public void Instantiate()
    {
        // Create a new GameObject for the circle
        circleObject = new GameObject("BouncingBall");
        circleObject.transform.position = center; // Set the position of the circle
        circleObject.transform.localScale = new Vector3(radius, radius, 1); // Set the scale of the circle

        // Add a SpriteRenderer component to the GameObject
        SpriteRenderer spriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = color; // Set the color of the circle
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/bouncing-ball"); // Load a sprite for the circle (ensure you have a CircleSprite in Resources)
        spriteRenderer.sortingOrder = 10; // Ensure the ball renders on top of other objects
    }

    public void UpdateFrame()
    {
        // Update the position of the ball based on its velocity
        center += velocity * GlobalHandler.Instance.deltaTime;

        // Check for collision with the collision circle
        CollisionCircle collisionCircle = GlobalHandler.Instance.sceneHandler.collisionCircle;
        float effectiveRadius = collisionCircle.radius - radius;
        Vector3 toCenter = collisionCircle.center - center;
        if (collisionCircle != null && toCenter.magnitude > effectiveRadius)
        {
            // Reverse the velocity according to the collision normal
            Vector3 directionToCenter = toCenter.normalized;
            velocity = Vector3.Reflect(velocity, directionToCenter);
            // Move the ball back inside the collision circle
            center = collisionCircle.center - directionToCenter * effectiveRadius;
            BallCollided();
        }

        // Update the GameObject's position
        circleObject.transform.position = center;
    }

    void BallCollided()
    {
        GlobalHandler.Instance.sceneHandler.onBallCollided();
    }
}

