using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneHandler
{
    public int currentLevel = 0;
    public float currentClockAngle = 0f; // Current angle for the clock pointer

    public CollisionCircle collisionCircle;

    public BouncingBall bouncingBall;

    public List<CirclesRing> circlesRingList;

    public float loopDurationSeconds = 2.8f;

    public int currentRingIndex = 0; // Index of the currently active circles ring
    private float rotatingAngleSpeed = 2f;

    private int maxMissedCircles = 1; // Maximum number of missed circles before game over
    private int missedCirclesCount = 0; // Count of missed circles

    private GameObject clockPointer;
    private GameObject roomParentObject;
    
    private bool ignorePlayerControls = false; // Flag to ignore player controls
    public SceneHandler()
    {

    }

    public void LoadNextLevel()
    {
        currentLevel += 1; // Increment the level

        if (currentLevel == 1)
        {
            GlobalHandler.Instance.OnGameOver();
        }
    }

    public void DestroyLevel()
    {
        if (bouncingBall != null)
        {
            Object.Destroy(bouncingBall.circleObject); // Destroy the bouncing ball object
            bouncingBall = null; // Clear the reference
        }

        if (collisionCircle != null)
        {
            Object.Destroy(collisionCircle.gameObject); // Destroy the collision circle
            collisionCircle = null; // Clear the reference
        }

        if (circlesRingList != null)
        {
            foreach (CirclesRing ring in circlesRingList)
            {
                ring.Destroy(); // Destroy each circles ring
            }
            circlesRingList.Clear(); // Clear the list of circles rings
        }

        if (clockPointer != null)
        {
            Object.Destroy(clockPointer); // Destroy the clock pointer object
            clockPointer = null; // Clear the reference
        }

        currentClockAngle = 0f; // Reset the clock angle
        currentRingIndex = 0; // Reset the current ring index
        missedCirclesCount = 0; // Reset the missed circles count
    }

    public void Reset()
    {
        DestroyLevel(); // Destroy the current level objects

        currentLevel = 0;
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

        CirclesRing secondRing = CreateCirclesRing(2, 30, 36);
        //secondRing.Activate(); // Activate the second circles ring

        CirclesRing thirdRing = CreateCirclesRing(2, 30, 36);
        //thirdRing.Activate(); // Activate the third circles ring

        CirclesRing fourthRing = CreateCirclesRing(2, 30, 36);
        //fourthRing.Activate(); // Activate the fourth circles ring

        CirclesRing fifthRing = CreateCirclesRing(2, 30, 36);
        //fifthRing.Activate(); // Activate the fifth circles ring

        CirclesRing sixthRing = CreateCirclesRing(2, 30, 36);
        //sixthRing.Activate(); // Activate the sixth circles ring


        circlesRingList.Add(firstRing); // Add the circles ring to the list
        circlesRingList.Add(secondRing);
        circlesRingList.Add(thirdRing);
        circlesRingList.Add(fourthRing);
        circlesRingList.Add(fifthRing);
        circlesRingList.Add(sixthRing);

        GlobalHandler.Instance.musicHandler.LoadFirstRingMusic1(circlesRingList); // Load the first music track for the circles ring
    }

    public void RemovePlayerControlls()
    {
        ignorePlayerControls = true; // Set the flag to ignore player controls
        bouncingBall.Pause();
    }

    public void RestorePlayerControlls()
    {
        ignorePlayerControls = false; // Reset the flag to allow player controls

        if(bouncingBall != null)
        {
            bouncingBall.Resume(); // Resume the bouncing ball
        }
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

        Circle collidedCircle = circlesRingList[currentRingIndex].CheckCollision(collisionPosition);

        if (collidedCircle != null)
        {
            onRingCircleColided(collidedCircle); // Call the method to handle the collision with the circle

            bool circleRingFinished = true;
            foreach (Circle circle in circlesRingList[currentRingIndex].circles)
            {
                if (circle.isSpecial && !circle.isHit)
                {
                    circleRingFinished = false; // If any special circle is not hit, the ring is not finished
                    break;
                }
            }

            if (circleRingFinished)
            {
                currentRingIndex += 1;

                if (currentRingIndex >= circlesRingList.Count)
                {
                    GlobalHandler.Instance.OnGameWin(); // Trigger game win if all rings are finished
                }
                else
                {
                    onRingCircleFinished();
                }
            }
        }
        else
        {
            onRingCircleMissed();
        }
    }

    public void onRingCircleFinished()
    {
        for (int i = 0; i < currentRingIndex; i++)
        {
            circlesRingList[i].OuterTheCirclesAndFade(5, 0.3f);
            //circlesRingList[i].Deactivate(); // Deactivate all previous circles rings
        }
        circlesRingList[currentRingIndex].Activate(); // Activate the next circles ring
    }

    public void onRingCircleColided(Circle circle)
    {
        if (circle.isSpecial)
        {
            circle.Hit(); // Call the Hit method to change the color and mark as hit
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
        if (ignorePlayerControls)
        {
            return; // Ignore player controls if the flag is set
        }

        foreach (CirclesRing circlesRing in circlesRingList)
        {
            circlesRing.RotateLeft(); // Rotate the circles ring to the left
        }
        currentClockAngle += rotatingAngleSpeed; // Rotate the clock pointer to the left
    }

    public void RotateRight()
    {
        if (ignorePlayerControls)
        {
            return; // Ignore player controls if the flag is set
        }

        foreach (CirclesRing circlesRing in circlesRingList)
        {
            circlesRing.RotateRight(); // Rotate the circles ring to the right
        }
        currentClockAngle += -rotatingAngleSpeed; // Rotate the clock pointer to the right
    }
}
