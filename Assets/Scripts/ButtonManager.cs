using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for dealing with UI elements


public class ButtonManager : MonoBehaviour
{

    public Button button; // Assign this in the inspector
    public float duration = 1.0f; // Duration of one cycle (fade in and fade out)
    public float minOpacity = 0.5f; // Minimum opacity to fade to

    void Start()
    {
        if (button != null)
        {
            StartCoroutine(ChangeOpacityLoop());
        }
    }

    IEnumerator ChangeOpacityLoop()
    {
        Image buttonImage = button.GetComponent<Image>();
        while (true) // Loop indefinitely
        {
            // Fade to minimum opacity
            for (float t = 0.0f; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                // Lerp the alpha value of the button's color between full opacity and minimum opacity
                buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, Mathf.Lerp(1, minOpacity, normalizedTime));
                yield return null;
            }

            // Fade back to full opacity
            for (float t = 0.0f; t < duration; t += Time.deltaTime)
            {
                float normalizedTime = t / duration;
                // Lerp the alpha value of the button's color between minimum opacity and full opacity
                buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, Mathf.Lerp(minOpacity, 1, normalizedTime));
                yield return null;
            }
        }
    }
}
