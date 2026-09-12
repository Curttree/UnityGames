using System.Collections;
using UnityEngine;

public class MenuBallController : MonoBehaviour
{
    [SerializeField]
    private float maxY, minY, speed;

    [SerializeField]
    private bool moveDown;

    [SerializeField]
    private GameObject ball;

    private float squishDuration = 0.1f;
    private float stretchDuration = 0.25f;
    private float restoreDuration = 0.05f;
    private float targetSquishScale = 2.5f;
    private float targetStretchScale = 4.2f;

    void FixedUpdate()
    {
        if (moveDown && transform.position.y < minY)
        {
            moveDown = false;
            StartCoroutine(Squish());
            ball.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
        }
        else if (!moveDown && transform.position.y > maxY)
        {
            ball.transform.localScale = new Vector3(3.5f, 3.5f, 1f);
            moveDown = true;
        }
    }

    IEnumerator Squish()
    {
        Vector3 startScale = ball.transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < squishDuration)
        {
            // Advance time
            timeElapsed += Time.deltaTime;

            // Calculate interpolation factor (0 to 1)
            float lerpFactor = timeElapsed / squishDuration;

            // Lerp scale
            ball.transform.localScale = Vector3.Lerp(startScale, new Vector3(startScale.x, targetSquishScale, startScale.z), lerpFactor);

            // Wait for the next frame
            yield return null;
        }

        ball.transform.localScale = new Vector3(startScale.x, targetSquishScale, startScale.z);
        StartCoroutine(Stretch(startScale));
    }
    IEnumerator Stretch(Vector3 startScale)
    {
        Vector3 squishedScale = ball.transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < stretchDuration)
        {
            // Advance time
            timeElapsed += Time.deltaTime;

            // Calculate interpolation factor (0 to 1)
            float lerpFactor = timeElapsed / stretchDuration;

            // Lerp scale
            ball.transform.localScale = Vector3.Lerp(squishedScale, new Vector3(squishedScale.x, targetStretchScale, squishedScale.z), lerpFactor);

            // Wait for the next frame
            yield return null;
        }

        ball.transform.localScale = new Vector3(squishedScale.x, targetStretchScale, squishedScale.z);
        StartCoroutine(Restore(startScale));
    }
    IEnumerator Restore(Vector3 startScale)
    {
        Vector3 stretchedScale = ball.transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < restoreDuration)
        {
            // Advance time
            timeElapsed += Time.deltaTime;

            // Calculate interpolation factor (0 to 1)
            float lerpFactor = timeElapsed / restoreDuration;

            // Lerp scale
            ball.transform.localScale = Vector3.Lerp(stretchedScale, startScale, lerpFactor);

            // Wait for the next frame
            yield return null;
        }

        ball.transform.localScale = startScale;
    }
}
