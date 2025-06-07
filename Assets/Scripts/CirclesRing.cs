using System.Collections.Generic;
using UnityEngine;

public class CirclesRing
{

    private float rotatingAngleSpeed = 2f;

    int innerRadius; // Radius of the inner circles
    int outerRadius; // Radius of the outer circle
    int count; // Number of circles in the ring

    List<Circle> circles = new List<Circle>();

    GameObject parentObject;

    public CirclesRing(int innerRadius, int outerRadius, int count)
    {
        this.innerRadius = innerRadius; // Set the inner radius
        this.outerRadius = outerRadius; // Set the outer radius
        this.count = count; // Set the number of circles
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
            float angle = i * Mathf.PI * 2 / count; // Calculate the angle for each circle
            Vector3 position = new Vector3(Mathf.Cos(angle) * outerRadius, Mathf.Sin(angle) * outerRadius, 0); // Calculate the position
            Circle circle = new Circle(position, innerRadius, Color.white); // Create a new circle

            circle.Instantiate();

            circle.circleObject.transform.parent = parentObject.transform; // Set the parent of the circle object

            circles.Add(circle); // Add the circle to the list
        }
    }

    public void RotateLeft()
    {
        parentObject.transform.Rotate(Vector3.forward, -rotatingAngleSpeed);
    }

    public void RotateRight()
    {
        parentObject.transform.Rotate(Vector3.forward, +rotatingAngleSpeed);
    }

    public Circle CheckCollision(Vector3 position)
    {
        // Check if the position is within the bounds of any circle in the ring
        foreach (Circle circle in circles)
        {
            float distance = Vector3.Distance(circle.circleObject.transform.position, position);
            if (distance <= circle.radius)
            {
                return circle; // Return the first circle that collides with the position
            }
        }
        return null; // No collision found
    }
}

