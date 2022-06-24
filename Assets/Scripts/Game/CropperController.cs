using System.Collections;
using System.Collections.Generic;
using Events;
using ImageCropperNamespace;
using QFramework;
using UnityEngine;
//This script is used to implement the cropper function. The code comes from the reference project and is partially rewritten on this basis.
//Please refer to the readme file in the github of this project to find the original project.
public class CropperController : MonoSingleton<CropperController>//Screenshot of the script
{
    private ImageCropperDemo imageCropperDemo;
  
    public string windName;

    // Start is called before the first frame update
    void Start()//Get the script ImageCropperDemo
    {
        imageCropperDemo = GetComponent<ImageCropperDemo>();
    }

    /// <summary>
    /// Enable screenshot
    /// </summary>
    /// <param name="windoName"></param>
    /// <param name="texture2D"></param>
    public void Crop(string windoName,Texture2D texture2D)//Pass two parameters, one is name and the other is texture,
    {
        imageCropperDemo.croppedImageHolder.texture = texture2D;//Assign a value to the layer (image) of the screenshot,
        windName = windoName;

        StartCoroutine(Capture());
    }

    IEnumerator Capture()//Start a collaborative program to take a screenshot, and the screenshot pop-up window will appear.
    {
        bool ovalSelection = imageCropperDemo.ovalSelectionInput.isOn;//The option that comes with the original script can determine whether the cropped panel is a circle or a rectangle
        bool autoZoom = imageCropperDemo.autoZoomInput.isOn;//Selection of scaling during cropping (shrink or not)
        float minAspectRatio, maxAspectRatio;//The setting of the maximum and minimum values of the window is not used.
        if ( !float.TryParse( imageCropperDemo.minAspectRatioInput.text, out minAspectRatio ) )
            minAspectRatio = 0f;
        if( !float.TryParse( imageCropperDemo.maxAspectRatioInput.text, out maxAspectRatio ) )
            maxAspectRatio = 0f;//The value of the judgment zoom, the default is 1
        yield return new WaitForEndOfFrame();

        ImageCropper.Instance.Show( imageCropperDemo.croppedImageHolder.texture, ( bool result, Texture originalImage, Texture2D croppedImage ) =>
            {
                // Destroy previously cropped texture (if any) to free memory
                //Destroy( imageCropperDemo.croppedImageHolder.texture, 5f );

                // If screenshot was cropped successfully
                if ( result )
                {
                    // Assign cropped texture to the RawImage
                    imageCropperDemo.croppedImageHolder.enabled = true;
                    imageCropperDemo.croppedImageHolder.texture = croppedImage;//Assign a value to the layer (image) of the screenshot

                    Vector2 size = imageCropperDemo.croppedImageHolder.rectTransform.sizeDelta;//Determine the size of the screenshot area, but it is not used.
                    if ( croppedImage.height <= croppedImage.width )
                        size = new Vector2( 400f, 400f * ( croppedImage.height / (float) croppedImage.width ) );
                    else
                        size = new Vector2( 400f * ( croppedImage.width / (float) croppedImage.height ), 400f );
                    imageCropperDemo.croppedImageHolder.rectTransform.sizeDelta = size;

                    imageCropperDemo.croppedImageSize.enabled = true;
                    imageCropperDemo.croppedImageSize.text = "Image size: " + croppedImage.width + ", " + croppedImage.height;
                
                    TypeEventSystem.Global.Send<EventCaptureDisPlay>(new EventCaptureDisPlay(){windowName = windName,texture2D =croppedImage });//Send the screenshots,
                    TypeEventSystem.Global.Send(new EventVCameraScrollWhereClampCtrol() { isCtrol = true });//Then synchronize the screen to the Mesh grid in CreateNewMesh and the Mesh in LeftTabCell.

                }
                else//If there is no result, set the clipping panel to false and close it.
                {
                    imageCropperDemo.croppedImageHolder.enabled = false;
                    imageCropperDemo.croppedImageSize.enabled = false;
                }

            },
            settings: new ImageCropper.Settings()
            {
                //Two variables(bool) corresponding to one of the shapes and clipping above.
                ovalSelection = ovalSelection,
                autoZoomEnabled = autoZoom,

                imageBackground = Color.clear, // transparent background 
                selectionMinAspectRatio = minAspectRatio,
                selectionMaxAspectRatio = maxAspectRatio,
                //initialOrientation = ImageCropper.Orientation.Rotate180

            },
            croppedImageResizePolicy: ( ref int width, ref int height ) =>
            {
                // uncomment lines below to save cropped image at half resolution
                //width /= 2;
                //height /= 2;
            });
    }
}
