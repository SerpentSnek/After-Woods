using UnityEngine;
using System.Collections;

public class DarkMaskOpacityController : MonoBehaviour
{
    public Transform target; 
    public SpriteRenderer darkMaskRenderer; 
    public float thresholdYValue = 5.0f; 
    public float maxOpacity = 0.8f; 
    public float fadeDuration = 2.0f; 

    private bool isAboveThreshold = false; 

    void Start()
    {
        SetOpacity(target.position.y >= thresholdYValue ? maxOpacity : 0f);
    }

    void Update()
    {
        // Check if the target crosses above the threshold
        if (target.position.y >= thresholdYValue && !isAboveThreshold)
        {
            isAboveThreshold = true; // Mark as above the threshold
            StopAllCoroutines(); // Ensure no other fade coroutines are running
            StartCoroutine(FadeToMaxOpacity()); // Start fading to max opacity
        }
        else if (target.position.y < thresholdYValue && isAboveThreshold)
        {
            isAboveThreshold = false; // Mark as below the threshold
            StopAllCoroutines(); // Ensure no other fade coroutines are running
            StartCoroutine(FadeToMinOpacity()); // Start fading to min opacity
        }
    }

    IEnumerator FadeToMaxOpacity()
    {
        float startOpacity = darkMaskRenderer.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, maxOpacity, elapsedTime / fadeDuration);
            SetOpacity(newOpacity);
            yield return null;
        }
        SetOpacity(maxOpacity);
    }

    IEnumerator FadeToMinOpacity()
    {
        float startOpacity = darkMaskRenderer.color.a;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newOpacity = Mathf.Lerp(startOpacity, 0f, elapsedTime / fadeDuration);
            SetOpacity(newOpacity);
            yield return null;
        }
        SetOpacity(0f);
    }

    void SetOpacity(float opacity)
    {
        Color newColor = darkMaskRenderer.color;
        newColor.a = opacity;
        darkMaskRenderer.color = newColor;
    }
}
