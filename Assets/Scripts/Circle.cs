using UnityEngine;

public class Circle
{
    private Vector3 initialCenter;
    public float radius;
    public Color color;

    public GameObject circleObject;

    public bool isSpecial = false;

    public bool isActive = false;

    public bool isHit = false; 

    public string audioTrack1 = null;
    public string audioTrack2 = null;

    public string audioTrack3 = null;

    public float audioTrack1Duration = -1f;
    public float audioTrack2Duration = -1f;
    public float audioTrack3Duration = -1f;
    
    public float audioTrack1Volume = 1f;
    public float audioTrack2Volume = 1f;
    public float audioTrack3Volume = 1f;

    SpriteRenderer spriteRenderer;

    SpriteRenderer borderRenderer;

    public Circle(Vector3 center, float radius, Color color)
    {
        this.initialCenter = center;
        this.radius = radius;
        this.color = color;
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        if (circleObject != null)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color; 
                borderRenderer.color = new Color(color.r * 0.7f, color.g * 0.7f, color.b * 0.7f, color.a); 
            }
        }
    }

    public void SetSpecial()
    {
        isSpecial = true;
        if (circleObject != null)
        {
            Vector3 currentScale = circleObject.transform.localScale;
            borderRenderer.color = new Color(color.r , color.g , color.b , color.a);
            circleObject.transform.localScale = new Vector3(radius*0.8f, radius*0.8f, currentScale.z);

            // Compensate scale for children
            foreach (Transform child in circleObject.transform)
            {
                child.localScale = new Vector3(1 / 0.8f, 1 / 0.8f, 1);
            }
        }
    }

    public void Hit()
    {
        // Add a new sprite object in the center of the circle
        if (!isHit)
        {
            GameObject hitEffect = new GameObject("HitEffect");
            SpriteRenderer hitEffectRenderer = hitEffect.AddComponent<SpriteRenderer>();
            hitEffectRenderer.sprite = Resources.Load<Sprite>("Images/correct");
            hitEffectRenderer.sortingOrder = 10;
            hitEffect.transform.localScale = new Vector3(radius, radius, 1);
            hitEffect.transform.position = circleObject.transform.position; 

            // Start a coroutine to fade away the hit effect and move it up
            Object.Destroy(hitEffect, 1f); // Destroy after 1 second
            hitEffect.AddComponent<Rigidbody2D>().gravityScale = -1f; // Add upward movement

            // PLay audio sound
            GlobalHandler.Instance.musicHandler.PlayTrack("Correct_Collision", 0.5f, 0.2f);
        }

        isHit = true; // Mark the circle as hit
        if (circleObject != null)
        {
            spriteRenderer.color = color;
            borderRenderer.color = new Color(color.r * 0.7f, color.g * 0.7f, color.b * 0.7f, color.a);
        }
    }

    public void SelectCircle()
    {
        if (circleObject != null)
        {
            if (isSpecial)
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, color.a);
                borderRenderer.color = new Color(spriteRenderer.color.r * 0.7f, spriteRenderer.color.g * 0.7f, spriteRenderer.color.b * 0.7f, color.a);
            }
            else
            {
                spriteRenderer.color =  new Color(spriteRenderer.color.r* 0.7f, spriteRenderer.color.g* 0.7f, spriteRenderer.color.b* 0.7f, color.a); 
                borderRenderer.color =  new Color(spriteRenderer.color.r * 0.5f, spriteRenderer.color.g * 0.5f, spriteRenderer.color.b * 0.5f, color.a); 
            }
            
        }
    }

    public void DeselectCircle()
    {
        // Restores the original color of the circle
        if (circleObject != null)
        {
            if (isSpecial)
            {
                if (isHit)
                {
                    spriteRenderer.color = color; // Darken the circle color
                    borderRenderer.color = new Color(color.r * 0.7f, color.g * 0.7f, color.b * 0.7f, color.a); // Darken the border color
                }
                else
                {
                    spriteRenderer.color = Globals.ColorList[6]; // Reset to the special color
                    borderRenderer.color = new Color(color.r * 0.7f, color.g * 0.7f, color.b * 0.7f, color.a); // Darken the border color
                }
            }
            else
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, color.a); // Keep the darkened color if hit
                borderRenderer.color = new Color(color.r * 0.7f, color.g * 0.7f, color.b * 0.7f, color.a); // Darken the border color
            }
        }
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        if (circleObject != null)
        {
            circleObject.SetActive(isActive); // Activate or deactivate the circle GameObject
        }
    }

    public void Instantiate()
    {
        this.isActive = true; // Set the circle as active
        // Create a new GameObject for the circle
        circleObject = new GameObject("Circle");
        circleObject.transform.position = initialCenter; // Set the position of the circle
        circleObject.transform.localScale = new Vector3(radius * 0.9f, radius * 0.9f, 1); // Set the scale of the circle

        // Add a SpriteRenderer component to the GameObject
        spriteRenderer = circleObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(color.r, color.g, color.b, color.a); // Set the color of the circle
        spriteRenderer.sprite = Resources.Load<Sprite>("Images/circle"); // Load a sprite for the circle (ensure you have a CircleSprite in Resources)
        spriteRenderer.sortingOrder = 9; // Set the sorting order for rendering

        // Create a border GameObject as a child of the circle
        GameObject borderObject = new GameObject("Border");
        borderObject.transform.SetParent(circleObject.transform);
        borderObject.transform.localPosition = Vector3.zero;
        borderObject.transform.localScale = new Vector3(1.2f, 1.2f, 1);

        // Add a SpriteRenderer for the border
        borderRenderer = borderObject.AddComponent<SpriteRenderer>();
        borderRenderer.sprite = Resources.Load<Sprite>("Images/circle");
        borderRenderer.color = new Color(color.r, color.g, color.b, color.a); // Make border darker while preserving alpha
        borderRenderer.sortingOrder = 8; // Render behind the main circle
    }
}

