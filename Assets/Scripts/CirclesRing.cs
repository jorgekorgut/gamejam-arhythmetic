using System.Collections.Generic;
using UnityEngine;

public class CirclesRing
{
    public float rotatingAngle = 0f; // Angle of rotation for the ring

    int innerRadius; // Radius of the inner circles
    int outerRadius; // Radius of the outer circle
    public int count; // Number of circles in the ring

    public bool IsActive = false;

    public List<Circle> circles = new List<Circle>();

    public GameObject parentObject;

    private float lastTimePlayed = 0f; // Last time an audio track was played

    public CirclesRing(int innerRadius, int outerRadius, int count)
    {
        this.innerRadius = innerRadius; // Set the inner radius
        this.outerRadius = outerRadius; // Set the outer radius
        this.count = count; // Set the number of circles
    }

    public void PointerAt(float angle)
    {
        // Calculate the index of the circle based on the angle
        angle = angle - 360f / count;

        float clampedAngle = (angle + rotatingAngle) % 360f; // Adjust the angle based on the current rotation

        float index = (360f - clampedAngle) / (360f / count);

        int floorIndex = Mathf.FloorToInt(index) % count;

        float timeBeforePlaying = GlobalHandler.Instance.sceneHandler.loopDurationSeconds / (count/2); // Calculate the time before playing the audio track

        DeselectAllCircles(); // Deselect all circles before selecting the new one
        circles[floorIndex].SelectCircle(); // Select the circle at the calculated index

        Circle circle = circles[floorIndex]; // Get the circle at the calculated index

        if (circle.isSpecial) // Check if the circle is special and active
        {
            if (Time.time - lastTimePlayed > timeBeforePlaying)
            {
                if (IsActive)
                {
                    if (circle.isHit)
                    {
                        

                        GlobalHandler.Instance.musicHandler.PlayTrack(circle.audioTrack1, circle.audioTrack1Volume, circle.audioTrack1Duration);
                        GlobalHandler.Instance.musicHandler.PlayTrack(circle.audioTrack2, circle.audioTrack2Volume, circle.audioTrack2Duration);
                        GlobalHandler.Instance.musicHandler.PlayTrack(circle.audioTrack3, circle.audioTrack3Volume, circle.audioTrack3Duration);
                    }

                    if (!circle.isHit && circle.isSpecial)
                    {
                        GlobalHandler.Instance.animationHandler.InstanciateSparklingEffect(parentObject.transform, circle.circleObject.transform.position, circle.color, 2f);
                    }
                    lastTimePlayed = Time.time; // Update the last time an audio track was played
                }
            }
        }
    }

    private void DeselectAllCircles()
    {
        // Deselect all circles in the ring
        foreach (Circle circle in circles)
        {
            circle.DeselectCircle();
        }
    }

    public void Activate()
    {
        IsActive = true;
        // Activate all circles in the ring
        foreach (Circle circle in circles)
        {
            circle.SetActive(true); // Call the SetActive method to activate the circle
        }
    }

    public void Destroy()
    {
        // Destroy the parent object and all circles in the ring
        foreach (Circle circle in circles)
        {
            Object.Destroy(circle.circleObject); // Call the Destroy method of each circle
        }
        Object.Destroy(parentObject); // Destroy the parent object
    }

    public void Instantiate()
    {
        // Create a parent object for all circles
        parentObject = new GameObject("CirclesRing");
        parentObject.transform.position = Vector3.zero; // Set the position of the parent object
        parentObject.transform.localScale = Vector3.one; // Set the scale of the parent object
        // Populate the outer circle with evenly spaced circles
        for (int i = 0; i < count; i++)
        {
            float angle = Mathf.PI / 2 + i * Mathf.PI * 2 / count; // Calculate the angle for each circle
            Vector3 position = new Vector3(Mathf.Cos(angle) * outerRadius, Mathf.Sin(angle) * outerRadius, 0); // Calculate the position
            Circle circle = new Circle(position, innerRadius, Globals.ColorList[6]); // Create a new circle

            circle.Instantiate();

            circle.circleObject.transform.parent = parentObject.transform; // Set the parent of the circle object

            circles.Add(circle); // Add the circle to the list

            circle.SetActive(false); // Initially set the circle to inactive
        }
    }

    public void OuterTheCirclesAndFade(int radius, float fadeStrength)
    {
        // Move the circles outward and fade them
        foreach (Circle circle in circles)
        {
            if (!circle.isActive) continue;

            Vector3 toCenter = parentObject.transform.position - circle.circleObject.transform.position;
            toCenter.Normalize();

            Vector3 position = circle.circleObject.transform.position - toCenter * radius;
            circle.circleObject.transform.position = position;

            Color color = circle.color;
            color.a -= fadeStrength;
            circle.SetColor(color);
        }
    }

    public void RotateLeft()
    {
        // rotatingAngleSpeed += 0.05f; // Gradually increase rotation speed
        // rotatingAngleSpeed = Mathf.Min(rotatingAngleSpeed, 2f); // Cap maximum speed
        rotatingAngle -= GlobalHandler.Instance.sceneHandler.rotatingAngleSpeed; // Decrease the angle for left rotation
        if (rotatingAngle < 0f) rotatingAngle += 360f; // Wrap around if the angle goes below 0
    }

    public void RotateRight()
    {
        // rotatingAngleSpeed += 0.05f; // Gradually increase rotation speed
        // rotatingAngleSpeed = Mathf.Min(rotatingAngleSpeed, 2f); // Cap maximum speed
        rotatingAngle += GlobalHandler.Instance.sceneHandler.rotatingAngleSpeed; // Increase the angle for right rotation
        if (rotatingAngle >= 360f) rotatingAngle -= 360f; // Wrap around if the angle goes above 360
    }
    
    public void UpdateFrame()
    {
        // Update the rotation of the parent object based on the rotating angle
        parentObject.transform.rotation = Quaternion.Euler(0, 0, rotatingAngle);
    }

    public Circle CheckCollision(Vector3 position)
    {
        // Check if the position is within the bounds of any circle in the ring
        var sortedCollisions = new List<(float distance, Circle circle)>();
        
        foreach (Circle circle in circles)
        {
            if (!circle.isActive) continue;

            float distance = Vector3.Distance(circle.circleObject.transform.position, position);
            if (distance <= circle.radius * 1.6f) 
            {
                sortedCollisions.Add((distance - circle.radius, circle));
            }
        }

        if (sortedCollisions.Count > 0)
        {
            sortedCollisions.Sort((a, b) => a.distance.CompareTo(b.distance));
            return sortedCollisions[0].circle;
        }
        
        return null;
    }
}

