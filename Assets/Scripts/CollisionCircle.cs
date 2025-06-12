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

        // // // Get max number of splitted sprites in a resource
        // Sprite[] roomFurnitures = Resources.LoadAll<Sprite>("Images/room-furniture");

        // // Get the number of sprites in the room furniture sprite sheet
        // int furnitureSpriteCount = roomFurnitures.Length -1;

        // List<Vector3> furniturePositions = new List<Vector3>(); // List to store furniture positions

        // // Ranndomly populate the circle with the furniture sprites
        // for (int i = 0; i < 10; i++)
        // {
        //     // Get A random number between 1 and 5 for furniture sprite selection
        //     int furnitureSpriteIndex = Random.Range(0, furnitureSpriteCount); // Assuming you have 5 different furniture sprites named "room-furniture_1", "room-furniture_2", etc.

        //     Vector3 furniturePosition = center + Random.insideUnitSphere * radius * 0.8f; // Place furniture inside the circle

        //     // Ensure furniture position is not too close to the center and not overlapping with existing furniture
        //     while (furniturePositions.Exists(pos => Vector3.Distance(pos, furniturePosition) < 0.5f))
        //     {
        //         furniturePosition = center + Random.insideUnitSphere * radius * 0.8f; // Recalculate position
        //     }

        //     GameObject furniture = new GameObject("Furniture" + i);
        //     furniture.transform.position = furniturePosition; // Place furniture inside the circle
        //     furniture.transform.localScale = Vector3.one; // Scale down the furniture
        //     SpriteRenderer furnitureRenderer = furniture.AddComponent<SpriteRenderer>();

        //     furnitureRenderer.sprite = roomFurnitures[furnitureSpriteIndex]; // Load a sprite for the furniture (ensure you have a FurnitureSprite in Resources)
        //     furnitureRenderer.sortingOrder = 1; // Set a higher sorting order for the furniture
        //     furniture.transform.parent = gameObject.transform; // Set the parent of the furniture to the circle

        //     furniturePositions.Add(furniture.transform.position); // Store the position of the furniture
        // }
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

