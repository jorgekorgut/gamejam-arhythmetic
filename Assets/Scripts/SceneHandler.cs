using UnityEngine;

public class SceneHandler
{
    public float currentCLockAngle = 0f; // Current angle for the clock pointer

    public CollisionCircle collisionCircle;

    public BouncingBall bouncingBall;

    public CirclesRing circlesRing;
    private int circlesRingCount = 32; // Number of circles in the ring

    float loopDurationSeconds = 5f;

    private int maxMissedCircles = 3; // Maximum number of missed circles before game over
    private int missedCirclesCount = 0; // Count of missed circles

    private GameObject clockPointer;
    public SceneHandler()
    {
        // Initialize the scene handler
        CreateCollisionCircle(30);
        CreateBouncingBall();

        CreateCirclesRing(2, 30, circlesRingCount);
        CreateClockPointer();
    }

    private void CreateBouncingBall()
    {
        Vector3 initialPosition = new Vector3(0, 10, 0); // Initial position of the bouncing ball
        float radius = 1f; // Radius of the bouncing ball
        Color color = new Color(0, 1, 0, 1); // Color of the bouncing ball (green)
        Vector3 initialVelocity = new Vector3(30f, 15f, 0); // Initial velocity of the bouncing ball

        bouncingBall = new BouncingBall(initialPosition, radius, color, initialVelocity);
        bouncingBall.Instantiate(); // Instantiate the bouncing ball
    }

    private void CreateCollisionCircle(int radius)
    {
        collisionCircle = new CollisionCircle(Vector3.zero, radius, new Color(1, 0, 0, 0.4f));
        collisionCircle.Instantiate(); // Instantiate the collision circle outline
    }

    private void CreateCirclesRing(int innerRadius, int outerRadius, int count)
    {
        circlesRing = new CirclesRing(innerRadius, outerRadius, count);
        circlesRing.Instantiate(); // Instantiate the circles ring
    }

    private void CreateClockPointer()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/clock-pointer");
        if (prefab != null)
        {
            clockPointer = Object.Instantiate(prefab);
            clockPointer.transform.position = Vector3.zero; // Set the position of the clock pointer
        }
        else
        {
            Debug.LogError("ClockPointer prefab not found in Resources/Prefabs.");
        }
    }

    public void UpdateFrame()
    {
        currentCLockAngle += GlobalHandler.Instance.deltaTime * (360f / loopDurationSeconds); // Increment the angle based on time
        if (currentCLockAngle >= 360f) currentCLockAngle -= 360f; // Reset angle to avoid overflow

        UpdateClockPointer(); // Update the clock pointer's rotation
        bouncingBall.UpdateFrame(); // Update the bouncing ball's position
    }

    public void UpdateClockPointer()
    {
        clockPointer.transform.rotation = Quaternion.Euler(0, 0, -currentCLockAngle); // Rotate the clock pointer
    }

    public void onBallCollided()
    {
        // Get position of the collision for the bouncing ball
        Vector3 toCenter = collisionCircle.center - bouncingBall.center;

        Vector3 collisionPosition = collisionCircle.center - toCenter.normalized * collisionCircle.radius;

        Circle collidedCircle = circlesRing.CheckCollision(collisionPosition);

        if (collidedCircle != null)
        {
            onRingCircleColided(collidedCircle); // Call the method to handle the collision with the circle
        }
        else
        {
            onRingCircleMissed();
        }

    }

    public void onRingCircleColided(Circle circle)
    {
        // Change the color of the collided circle
        circle.SetColor(new Color(Random.value, Random.value, Random.value, 1f));
        circle.circleObject.GetComponent<SpriteRenderer>().color = circle.color; // Update the color in the SpriteRenderer
    }

    public void onRingCircleMissed()
    {
        missedCirclesCount++;
        if (missedCirclesCount >= maxMissedCircles)
        {
            GlobalHandler.Instance.OnGameOver(); // Trigger game over if maximum missed circles reached
        }
    }
}
