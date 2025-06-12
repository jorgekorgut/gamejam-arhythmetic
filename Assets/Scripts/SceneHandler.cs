using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneHandler
{
    public float currentClockAngle = 0f; // Current angle for the clock pointer

    public CollisionCircle collisionCircle;

    public BouncingBall bouncingBall;

    public List<CirclesRing> circlesRingList;

    public float loopDurationSeconds = 2.8f;
    private float rotatingAngleSpeed = 2f;


    private int maxMissedCircles = 3; // Maximum number of missed circles before game over
    private int missedCirclesCount = 0; // Count of missed circles

    private GameObject clockPointer;
    private GameObject roomParentObject;
    public SceneHandler()
    {
        
    }

    public void LoadMusic1()
    {
        // Initialize the scene handler
        CreateCollisionCircleRoom(30);
        CreateBouncingBall();

        CreateClockPointer();

        circlesRingList = new List<CirclesRing>();
        // Clear all existing circles rings
        foreach (CirclesRing ring in circlesRingList)
        {
            ring.Destroy(); // Destroy the existing circles rings
        }
        circlesRingList.Clear(); // Clear the list of circles rings

        CirclesRing firstRing = CreateCirclesRing(2, 30, 36);
        //firstRing.Activate(); // Activate the circles ring to make it ready for interaction

        CirclesRing secondRing = CreateCirclesRing(2, 35, 36);
        //secondRing.Activate(); // Activate the second circles ring

        CirclesRing thirdRing = CreateCirclesRing(2, 40, 36);
        //thirdRing.Activate(); // Activate the third circles ring

        CirclesRing fourthRing = CreateCirclesRing(2, 45, 36);
        //fourthRing.Activate(); // Activate the fourth circles ring

        CirclesRing fifthRing = CreateCirclesRing(2, 50, 36);
        //fifthRing.Activate(); // Activate the fifth circles ring

        CirclesRing sixthRing = CreateCirclesRing(2, 55, 36);
        //sixthRing.Activate(); // Activate the sixth circles ring


        circlesRingList.Add(firstRing); // Add the circles ring to the list
        circlesRingList.Add(secondRing);
        circlesRingList.Add(thirdRing);
        circlesRingList.Add(fourthRing);
        circlesRingList.Add(fifthRing);
        circlesRingList.Add(sixthRing);

        GlobalHandler.Instance.musicHandler.LoadFirstRingMusic1(circlesRingList); // Load the first music track for the circles ring
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

    private void CreateCollisionCircleRoom(int radius)
    {
        collisionCircle = new CollisionCircle(Vector3.zero, radius, Globals.ColorFromHex("#D5CFC1"));
        collisionCircle.Instantiate(); // Instantiate the collision circle outline
    }

    private CirclesRing CreateCirclesRing(int innerRadius, int outerRadius, int count)
    {
        CirclesRing circlesRing = new CirclesRing(innerRadius, outerRadius, count);
        circlesRing.Instantiate(); // Instantiate the circles ring
        return circlesRing;
    }

    private void CreateClockPointer()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/clock-pointer");
        if (prefab != null)
        {
            clockPointer = Object.Instantiate(prefab);
            clockPointer.transform.position = Vector3.zero; // Set the position of the clock pointer
            clockPointer.GetComponent<SpriteRenderer>().sortingOrder = 15; // Set the sorting order for rendering
        }
        else
        {
            Debug.LogError("ClockPointer prefab not found in Resources/Prefabs.");
        }
    }

    public void Update()
    {
        UpdateClockPointer(); // Update the clock pointer's rotation
    }

    public void UpdateFrame()
    {
        if (bouncingBall == null || collisionCircle == null || circlesRingList == null || clockPointer == null)
        {
            return;
        }

        currentClockAngle += GlobalHandler.Instance.deltaTime * (360f / loopDurationSeconds ); // Increment the angle based on time
        if (currentClockAngle >= 360f) currentClockAngle -= 360f; // Reset angle to avoid overflow

        bouncingBall.UpdateFrame(); // Update the bouncing ball's position

        if (Controller.Instance.IsRotateLeft())
        {
            RotateLeft(); // Rotate the circles ring to the left
        }
        else if (Controller.Instance.IsRotateRight())
        {
            RotateRight(); // Rotate the circles ring to the right
        }
        
        foreach (CirclesRing circlesRing in circlesRingList)
        {
            circlesRing.UpdateFrame(); // Update each circles ring's rotation
        }
    }

    public void UpdateClockPointer()
    {
        if(clockPointer == null)
        {
            return;
        }
        
        clockPointer.transform.rotation = Quaternion.Euler(0, 0, -currentClockAngle); // Rotate the clock pointer

        foreach (CirclesRing circlesRing in circlesRingList)
        {
            circlesRing.PointerAt(currentClockAngle); // Play sound based on the current clock angle
        }
    }

    public void onBallCollided()
    {
        // Get position of the collision for the bouncing ball
        Vector3 toCenter = collisionCircle.center - bouncingBall.center;

        Vector3 collisionPosition = collisionCircle.center - toCenter.normalized * collisionCircle.radius;

        Circle collidedCircle = circlesRingList[0].CheckCollision(collisionPosition);

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
        if (circle.isSpecial)
        {
            // Change the color of the collided circle
            circle.SetColor(new Color(Random.value, Random.value, Random.value, 1f));
            circle.circleObject.GetComponent<SpriteRenderer>().color = circle.color; // Update the color in the SpriteRenderer
        }
        else
        {
            circle.SetActive(false);
        }
    }

    public void onRingCircleMissed()
    {
        missedCirclesCount++;
        if (missedCirclesCount >= maxMissedCircles)
        {
            GlobalHandler.Instance.OnGameOver(); // Trigger game over if maximum missed circles reached
        }
    }

    public void RotateLeft()
    {
        foreach (CirclesRing circlesRing in circlesRingList)
        {
           circlesRing.RotateLeft(); // Rotate the circles ring to the left
        }
        currentClockAngle += rotatingAngleSpeed; // Rotate the clock pointer to the left
    }

    public void RotateRight()
    {
        foreach (CirclesRing circlesRing in circlesRingList)
        {
            circlesRing.RotateRight(); // Rotate the circles ring to the right
        }
        currentClockAngle += -rotatingAngleSpeed; // Rotate the clock pointer to the right
    }
}
