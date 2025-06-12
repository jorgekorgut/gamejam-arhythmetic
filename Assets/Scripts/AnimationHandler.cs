using UnityEngine;

public class AnimationHandler
{
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

    private System.Collections.IEnumerator FadeOutEffect(SpriteRenderer spriteRenderer, float duration)
    {
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(initialColor.a, 0f, elapsedTime / duration);
            if(spriteRenderer == null)
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
