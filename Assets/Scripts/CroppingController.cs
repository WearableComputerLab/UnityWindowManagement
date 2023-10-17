using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class CroppingController : MonoBehaviour
{

    public RectTransform cropRect; // Reference to the CropRectangle's RectTransform
    private Vector2 initialClickPosition; // To store the position where the user starts dragging
    public UwcWindowTexture windowTexture; // Reference to the UwcWindowTexture component

    //  This method will capture the initial click position when the user starts dragging.
    public void BeginDrag()
    {
        initialClickPosition = Input.mousePosition;
    }

    // This method will resize the rectangle based on the mouse movement.
    public void OnDrag()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 difference = currentMousePosition - initialClickPosition;
        cropRect.sizeDelta += new Vector2(difference.x, -difference.y);
        initialClickPosition = currentMousePosition;
    }
    // This method will finalize the rectangle's position and size.
    public void EndDrag()
    {
        // Ensure the cropping rectangle stays within the bounds of the window texture
        Vector2 parentSize = cropRect.parent.GetComponent<RectTransform>().sizeDelta;

        // Adjust for left and right boundaries
        if (cropRect.anchoredPosition.x < 0)
        {
            cropRect.sizeDelta += new Vector2(cropRect.anchoredPosition.x, 0);
            cropRect.anchoredPosition = new Vector2(0, cropRect.anchoredPosition.y);
        }
        else if (cropRect.anchoredPosition.x + cropRect.sizeDelta.x > parentSize.x)
        {
            cropRect.sizeDelta = new Vector2(parentSize.x - cropRect.anchoredPosition.x, cropRect.sizeDelta.y);
        }

        // Adjust for bottom and top boundaries
        if (cropRect.anchoredPosition.y < 0)
        {
            cropRect.sizeDelta += new Vector2(0, cropRect.anchoredPosition.y);
            cropRect.anchoredPosition = new Vector2(cropRect.anchoredPosition.x, 0);
        }
        else if (cropRect.anchoredPosition.y + cropRect.sizeDelta.y > parentSize.y)
        {
            cropRect.sizeDelta = new Vector2(cropRect.sizeDelta.x, parentSize.y - cropRect.anchoredPosition.y);
        }
    }

    // this method will drag and drop the RawImage component to display the cropped texture
    public RawImage displayImage; 
    public void DisplayCroppedTexture(Texture2D croppedTexture)
    {
        displayImage.texture = croppedTexture;
    }

    // This method will be triggered when the CropButton is clicked.
    public void OnCropButtonClick()
    {
        // Capture the cropped area based on the rectangle's position and size.
        Texture2D cropped = windowTexture.CaptureCroppedArea(cropRect);

        // Display the cropped texture using the method we defined earlier.
        DisplayCroppedTexture(cropped);
    }

}
