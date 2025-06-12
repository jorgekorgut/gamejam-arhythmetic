using UnityEngine;

public class BouncingBall
{
    public Vector3 center;
    public float radius;

    public Color color;

    public Vector3 velocity;

    public GameObject circleObject;

    public Animator animator;

    public bool isPaused = false;

    public BouncingBall(Vector3 center, float radius, Color color, Vector3 initialVelocity)
    {
        this.center = center;
        this.radius = radius;
        this.color = color;
        this.velocity = initialVelocity;
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void Instantiate()
    {
        // Create a new GameObject with the sprites "player-backward" and "player-forward"
        circleObject = new GameObject("BouncingBall");
        circleObject.transform.position = center; // Set the position of the circle
        circleObject.transform.localScale = new Vector3(radius, radius, 1); // Set the scale of the circle

        SpriteRenderer spriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/player-forward_0");
        spriteRenderer.sortingOrder = 10;

        // Add Animation component to the GameObject
        animator = circleObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/player-animation");

    }

    public void UpdateFrame()
    {
        if (isPaused)
            return;
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
        if(isPaused)
            return;

        GlobalHandler.Instance.sceneHandler.onBallCollided();
    }
}

