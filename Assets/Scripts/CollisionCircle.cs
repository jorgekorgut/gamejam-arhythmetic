using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UnityEngine;

public class CollisionCircle
{
    public Vector3 center;
    public float radius;
    public Color color;

    public GameObject gameObject; // GameObject representing the collision circle

    public CollisionCircle(Vector3 center, float radius, Color color)
    {
        this.center = center;
        this.radius = radius;
        this.color = color;
    }

    public void Instantiate()
    {

        GameObject prefab = Resources.Load<GameObject>("Prefabs/World-1");
        if (prefab != null)
        {
            gameObject = Object.Instantiate(prefab);
            gameObject.transform.position = center; // Set the position of the clock pointer
            //gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15; // Set the sorting order for rendering
        }
        else
        {
            Debug.LogError("ClockPointer prefab not found in Resources/Prefabs.");
        }
    }

    public void Rotate(float angle)
    {
        // Rotate the collision circle around its center
        gameObject.transform.Rotate(Vector3.forward, angle);

        // cancel all rotation of the child objects (furniture)
        foreach (Transform child in gameObject.transform)
        {
            child.rotation = Quaternion.identity; // Reset rotation of each child object
        }
    }

    public bool IsPointOutside(Vector3 point)
    {
        // Check if the point is inside the circle
        return Vector3.Distance(center, point) > radius;
    }
}

