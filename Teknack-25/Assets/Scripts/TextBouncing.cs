using UnityEngine;
using System.Collections;

public class TextBouncing : MonoBehaviour
{
    [SerializeField] private RectTransform buttonTransform;
    [SerializeField] private float bounceDuration = 0.5f;
    [SerializeField] private float bounceScale = 1.2f;

    private Vector3 originalScale;
    private bool isBouncing = false;

    void Start()
    {
        if (buttonTransform == null)
            buttonTransform = GetComponent<RectTransform>();

        originalScale = buttonTransform.localScale;
        StartCoroutine(Bounce());
    }

    private IEnumerator Bounce()
    {
        while (true)
        {
            yield return StartCoroutine(ScaleTo(bounceScale));
            yield return StartCoroutine(ScaleTo(originalScale.x));
        }
    }

    private IEnumerator ScaleTo(float targetScale)
    {
        float timeElapsed = 0f;
        Vector3 startScale = buttonTransform.localScale;
        Vector3 target = originalScale * targetScale;

        while (timeElapsed < bounceDuration)
        {
            buttonTransform.localScale = Vector3.Lerp(startScale, target, timeElapsed / bounceDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        buttonTransform.localScale = target;
    }
}
