using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler
{
    private LineRenderer polygonRenderer;
    public AnimationHandler()
    {
        GameObject polygonObject = new GameObject("PolygonAnimation");
        polygonRenderer = polygonObject.AddComponent<LineRenderer>();
    }
    // Update is called once per frame
    public void UpdateFrame()
    {
        if (GlobalHandler.Instance.sceneHandler.bouncingBall != null)
        {
            GlobalHandler.Instance.sceneHandler.bouncingBall.animator.SetFloat("SpeedY", GlobalHandler.Instance.sceneHandler.bouncingBall.velocity.y);

            if (GlobalHandler.Instance.sceneHandler.bouncingBall.velocity.x >= 0)
            {
                GlobalHandler.Instance.sceneHandler.bouncingBall.circleObject.transform.rotation = (Quaternion.Euler(0, 0, 0));
            }
            else
            {
                GlobalHandler.Instance.sceneHandler.bouncingBall.circleObject.transform.rotation = (Quaternion.Euler(0, 180f, 0));
            }
        }
    }

    public void InstanciateSparklingEffect(Transform parent, Vector3 position, Color color, float duration)
    {
        GameObject sparklingEffect = Resources.Load<GameObject>("Prefabs/sparkling");
        if (sparklingEffect != null)
        {
            // Instantiate the sparkling effect
            sparklingEffect = Object.Instantiate(sparklingEffect);

            sparklingEffect.transform.position = position;
            sparklingEffect.transform.localScale = Vector3.one*0.8f; // Set the scale to 1
            sparklingEffect.transform.SetParent(parent); // Set the parent without changing the world position
            // Fade out after duration
            Object.Destroy(sparklingEffect, duration*1.2f);

            // change color of the effect to fade out progressivelly alpha
            SpriteRenderer spriteRenderer = sparklingEffect.GetComponent<SpriteRenderer>();

            spriteRenderer.color = color;
            // Start a coroutine to fade out the effect
            GlobalHandler.Instance.StartCoroutine(FadeOutEffect(spriteRenderer, duration));
        }
    }

    public void InstanciatePolygonAnimation(List<Vector3> position, Color color, float duration)
    {

        // Get material
        Material material = Resources.Load<Material>("Materials/glow-material");

        polygonRenderer.positionCount = position.Count;
        polygonRenderer.SetPositions(position.ToArray());
        polygonRenderer.startColor = color;
        polygonRenderer.endColor = color;
        polygonRenderer.startWidth = 3f;
        polygonRenderer.endWidth = 3f;
        polygonRenderer.enabled = true;
        polygonRenderer.sortingOrder = 100;
        polygonRenderer.loop = true;

        polygonRenderer.material = material; // Set the material to the LineRenderer

        // Make it fill the polygon

        GlobalHandler.Instance.StartCoroutine(FadeOutPolygon(polygonRenderer, duration));
    }

    private System.Collections.IEnumerator FadeOutPolygon(LineRenderer lineRenderer, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = lineRenderer.startColor;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0f, elapsedTime / duration);
            if (lineRenderer == null)
            {
                yield break; // Exit if the lineRenderer is destroyed
            }
            lineRenderer.startColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            lineRenderer.endColor = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        // Ensure the final color is fully transparent
        lineRenderer.startColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        lineRenderer.endColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        lineRenderer.enabled = false; // Disable the LineRenderer after fading out
    }

    private System.Collections.IEnumerator FadeOutEffect(SpriteRenderer spriteRenderer, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0f, elapsedTime / duration);
            if (spriteRenderer == null)
            {
                yield break; // Exit if the spriteRenderer is destroyed
            }
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        // Ensure the final color is fully transparent
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
    }
}
