using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour
{
    // The UI region to capture
    [SerializeField] private RectTransform region;
    
    // The UI Image to display the captured screenshot
    [SerializeField] private Image image;

    // The camera to use for capturing the screenshot
    private Camera cameraToUse;

    // Initialize the camera to use
    private void Start()
    {
        cameraToUse = Camera.main;
    }

    // Check for user input to trigger the screenshot capture
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("click a");
            StartCoroutine(TakeScreenshot());
        }
    }

    // Coroutine to capture the screenshot
    private IEnumerator TakeScreenshot()
    {
        // Wait until the end of the frame to ensure the screen is fully rendered
        yield return new WaitForEndOfFrame();

        // Get the corners of RectTransform rect and store it in an array vector
        Vector3[] corners = new Vector3[4];
        region.GetWorldCorners(corners);

        // Calculate the width and height of the region
        int width = ((int)corners[3].x - (int)corners[0].x);
        int height = (int)corners[1].y - (int)corners[0].y;
        var startX = corners[0].x;
        var startY = corners[0].y;

        // Make a temporary texture and read pixels from it
        Texture2D ss = new Texture2D(width, height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        ss.Apply();

        // Encode the texture to PNG format
        var bytes = ss.EncodeToPNG();

        // Create a sprite from the Texture2D and assign it to the Image component
        Sprite sprite = Sprite.Create(ss, new Rect(0, 0, ss.width, ss.height), new Vector2(0.5f, 0.5f));
        image.sprite = sprite;
    }
}
