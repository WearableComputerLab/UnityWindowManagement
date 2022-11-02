/****************************************************************************
 * 2022.5 DESKTOP-CEJCKJU
 ****************************************************************************/

using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using uWindowCapture;

//This script is used to control each sub window on the Left panel, and each sub object also has a button property.

namespace QFramework.Example
{
	public partial class leftTabCell : UIElement
	{
		public string windowName ;
		public Texture2D texture2D;
		private Button buttonChangeTexture;
		public Button buttonCrop;//Give the butten attribute
		private UwcWindow _window;
		private bool isCrop=false;//Determine if it is a thumbnail
		private void Awake()
		{
			buttonChangeTexture = GetComponent<Button>();
			BtnClose.onClick.AddListener(()=>{
				//Response to events: After clicking a cell, clear the texture and hide the mash. Destroy the objects in the left window.

				//delete the texture directly
				TypeEventSystem.Global.Send<EventClearTexture>(new EventClearTexture(){windowName = windowName});
				//keep the mesh but not show it
				TypeEventSystem.Global.Send<EventHideCubeWindow>();

				//Delete left panel window
				Destroy(gameObject);
			});

			buttonChangeTexture.onClick.AddListener(() =>
			{
				//SelectComponent.Instance.DisplayPanel.SetDisPlayTexture(windowName,texture2D);
				//TypeEventSystem.Global.Send<EventChangeDisPlayMatTexture>(new EventChangeDisPlayMatTexture(){texture2D = this.texture2D});

				//Show mesh when switching windows
				TypeEventSystem.Global.Send<EventShowCubeWindow>();//Here is the event feedback of the thumbnail of the left panel, and the feedback to the screen on the right.

				if (isCrop)//This will also determine whether this is a screenshot or not.
				{
					TypeEventSystem.Global.Send<EventCaptureDisPlay>(new EventCaptureDisPlay(){windowName = windowName,texture2D =this.texture2D });

				}//If it is a screenshot, the EventCaptureDisPlay event will be sent, and the windowName and texture will be passed to the past,
				else
				{
					TypeEventSystem.Global.Send<EventChangeDisPlayMatTexture>(new EventChangeDisPlayMatTexture(){windowName = this.windowName,texture2D = _window.texture,Window = this._window});
				}//If there is no screenshot, this event will be sent, and _window.texture (dynamic) will be passed.

			});

			buttonCrop.onClick.AddListener(()=>{
				// ClickDisplayPanel.Instance.Crop(windowName,texture2D);
				 CropperController.Instance.Crop(windowName,texture2D);//The thumbnail is opened again by calling the crop method in the cropcontroller.
				TypeEventSystem.Global.Send(new EventVCameraScrollWhereClampCtrol(){isCtrol = false});//Fixed perspective, undoes the ability to use camera movement.
			});
			TypeEventSystem.Global.Register<EventCaptureDisPlay>(OnCaptureDisPlay);
			TypeEventSystem.Global.Register<EventChangeDisPlayMatTexture>(OnChangeDisPlayMatTextureHandler);
			
		}

		private void OnChangeDisPlayMatTextureHandler(EventChangeDisPlayMatTexture obj)
		{
			if (obj.windowName==windowName)
			{
				_window = obj.Window;

			}
		}

		private void OnCaptureDisPlay(EventCaptureDisPlay obj)//Transfer and synchronize the screen to the mesh grid in the list column on the left. (synchronize with methods in Cropper)
		{
			if (obj.windowName==windowName)
			{
				this.texture2D = obj.texture2D;
				RawImg.texture = obj.texture2D;
				isCrop = true;
			}	
		}

		public void Init(string windowName, Texture2D texture2D)
		{
			gameObject.SetActive(true);
			this.windowName = windowName;
			this.texture2D = texture2D;
			RawImg.texture = texture2D;
			Txtname.text = windowName;
		}
	

		protected override void OnBeforeDestroy()
		{
			TypeEventSystem.Global.UnRegister<EventCaptureDisPlay>(OnCaptureDisPlay);
			TypeEventSystem.Global.UnRegister<EventChangeDisPlayMatTexture>(OnChangeDisPlayMatTextureHandler);

		}
	}
}