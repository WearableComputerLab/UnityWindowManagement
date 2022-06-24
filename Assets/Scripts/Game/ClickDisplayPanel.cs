//This script is not used in the final version (It is for a standard quad cube window and is limited to one side of the cube)
//This part of the script is associated with the previous script (RayComponent) and is used to display the cube window.
//The main function of the code is to synchronize the content,that is to say, synchronize the screen of the window selected in the thumbnail to the cube window.
using System.Collections;
using System.Collections.Generic;
using Events;
using ImageCropperNamespace;
using QFramework;
using UnityEngine;


public class ClickDisplayPanel : MonoSingleton<ClickDisplayPanel>

//The specific function is panel generation, only in this panel for four points click to generate a ball.
{
    private MeshRenderer meshRenderer;
    public ImageCropperDemo imageCropperDemo;

    public string windName;
    public void SetDisPlayTexture(string windowName,Texture2D texture2D)//Distinguish the color of the balls
    {
        windName = windowName;
        meshRenderer.material.color = Color.white;
        meshRenderer.material.mainTexture = texture2D;
    }
    
    public void SelectDisPlay()
    {
        meshRenderer.material.color = Color.red;
        meshRenderer.material.mainTexture = null;

    }
    //public bool allowClick; 
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
      //  meshRenderer.material.color = Color.white;
    }

    public void Crop(string windoName,Texture2D texture2D)
    {
        imageCropperDemo.croppedImageHolder.texture = texture2D;
        windName = windoName;
        StartCoroutine(Capture());
    }

    IEnumerator Capture()
    {//This is a coordination event, and this method is fully used in Cropper, as will be explained later when the crop script is explained
        bool ovalSelection = imageCropperDemo.ovalSelectionInput.isOn;
        bool autoZoom = imageCropperDemo.autoZoomInput.isOn;
        float minAspectRatio, maxAspectRatio;
        if( !float.TryParse( imageCropperDemo.minAspectRatioInput.text, out minAspectRatio ) )
            minAspectRatio = 0f;
        if( !float.TryParse( imageCropperDemo.maxAspectRatioInput.text, out maxAspectRatio ) )
            maxAspectRatio = 0f;
        yield return new WaitForEndOfFrame();

        ImageCropper.Instance.Show( imageCropperDemo.croppedImageHolder.texture, ( bool result, Texture originalImage, Texture2D croppedImage ) =>
            {

                // If screenshot was cropped successfully.
                if ( result )
                {
                    // Assign cropped texture to the RawImage.
                    imageCropperDemo.croppedImageHolder.enabled = true;
                    imageCropperDemo.croppedImageHolder.texture = croppedImage;

                    Vector2 size = imageCropperDemo.croppedImageHolder.rectTransform.sizeDelta;
                    if( croppedImage.height <= croppedImage.width )
                        size = new Vector2( 400f, 400f * ( croppedImage.height / (float) croppedImage.width ) );
                    else
                        size = new Vector2( 400f * ( croppedImage.width / (float) croppedImage.height ), 400f );
                    imageCropperDemo.croppedImageHolder.rectTransform.sizeDelta = size;

                    imageCropperDemo.croppedImageSize.enabled = true;
                    imageCropperDemo.croppedImageSize.text = "Image size: " + croppedImage.width + ", " + croppedImage.height;

                    TypeEventSystem.Global.Send<EventCaptureDisPlay>(new EventCaptureDisPlay(){windowName = windName,texture2D =croppedImage });
                }
                else
                {
                    imageCropperDemo.croppedImageHolder.enabled = false;
                    imageCropperDemo.croppedImageSize.enabled = false;
                }

                // Destroy the screenshot as we no longer need it in this case. 
                

            },
            settings: new ImageCropper.Settings()
            {
                ovalSelection = ovalSelection,
                autoZoomEnabled = autoZoom,
                imageBackground = Color.clear, // transparent background.
                selectionMinAspectRatio = minAspectRatio,
                selectionMaxAspectRatio = maxAspectRatio,
                //initialOrientation = ImageCropper.Orientation.Rotate180

            },
            croppedImageResizePolicy: ( ref int width, ref int height ) =>
            {
                // uncomment lines below to save cropped image at half resolution. 
                //width /= 2;
                //height /= 2;
            });
    }
}
