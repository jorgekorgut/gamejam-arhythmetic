using UnityEngine;
using System.Collections.Generic;

public class UIGeometricForms : MonoBehaviour
{
    public int minSize = 1;
    public int maxSize = 200;

    public float speed = 20f;
    public float rotationSpeed = 10f;

    public int numberOfForms = 10;

    public List<GameObject> geometricForms = new List<GameObject>();

    public GameObject spawnArea;

    void Start()
    {
        Sprite[] geometryForms = Resources.LoadAll<Sprite>("Images/trigonometric-forms");

        // Get the number of sprites in the room furniture sprite sheet
        int geometryFormsCount = geometryForms.Length - 1;

        //List<Vector3> geometryPositions = new List<Vector3>(); // List to store furniture positions

        // Get spawn area bounds
        RectTransform spawnAreaRect = spawnArea.GetComponent<RectTransform>();
        Vector2 spawnSize = spawnAreaRect.rect.size;
        Vector2 spawnCenter = spawnAreaRect.position;

        // Calculate grid dimensions
        int rows = Mathf.CeilToInt(Mathf.Sqrt(numberOfForms));
        int cols = Mathf.CeilToInt((float)numberOfForms / rows);

        int formCount = 0;
        for (int row = 0; row < rows && formCount < numberOfForms; row++)
        {
            for (int col = 0; col < cols && formCount < numberOfForms; col++)
            {
                // More random position across the entire spawn area
                float xPos = spawnCenter.x - (spawnSize.x / 2) + Random.Range(0, spawnSize.x);
                float yPos = spawnCenter.y - (spawnSize.y / 2) + Random.Range(0, spawnSize.y);

                GameObject form = new GameObject("GeometricForm" + formCount);
                form.transform.position = new Vector3(xPos , yPos , Random.Range(-0.1f, 0.1f));

                // More variation in rotation and scale
                form.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
                float randomSize = Random.Range(minSize * 0.8f, maxSize * 1.2f);
                form.transform.localScale = new Vector3(
                    randomSize,
                    randomSize,
                    1
                );

                SpriteRenderer renderer = form.AddComponent<SpriteRenderer>();
                renderer.sprite = geometryForms[Random.Range(0, geometryFormsCount)];
                renderer.sortingOrder = Random.Range(-50, -54);

                form.transform.parent = spawnArea.transform;
                geometricForms.Add(form);

                formCount++;
            }
        }
    }

    void Update()
    {
        // Get spawn area bounds
        RectTransform spawnAreaRect = spawnArea.GetComponent<RectTransform>();
        Vector3 localScale = spawnAreaRect.localScale;

        foreach (GameObject form in geometricForms)
        {
            // Apply gravity
            form.transform.Translate(Vector3.down * speed * Time.deltaTime);

            // Rotate the form
            form.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            Vector2 spawnSize = spawnAreaRect.rect.size;
            Vector2 spawnCenter = spawnAreaRect.position;
            float halfWidth = spawnSize.x / 2;
            float halfHeight = spawnSize.y / 2;

            Vector3 pos = form.transform.position;
            bool repositioned = false;

            // Check horizontal bounds
            if (pos.x < spawnCenter.x - halfWidth)
            {
                pos.x = spawnCenter.x + halfWidth;
                repositioned = true;
            }
            else if (pos.x > spawnCenter.x + halfWidth)
            {
                pos.x = spawnCenter.x - halfWidth;
                repositioned = true;
            }

            // Check vertical bounds
            if (pos.y < spawnCenter.y - halfHeight)
            {
                pos.y = spawnCenter.y + halfHeight;
                repositioned = true;
            }
            else if (pos.y > spawnCenter.y + halfHeight)
            {
                pos.y = spawnCenter.y - halfHeight;
                repositioned = true;
            }

            // If form was repositioned, update its position and randomize properties
            if (repositioned)
            {
                pos.z = Random.Range(-0.1f, 0.1f);
                form.transform.position = pos;
                form.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180f, 180f));
                float randomSize = Random.Range(minSize * 0.8f, maxSize * 1.2f);
                form.transform.localScale = new Vector3(
                    randomSize,
                    randomSize,
                    1
                );
            }
        }
    }

}