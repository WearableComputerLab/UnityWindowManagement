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

//This script is used to control the rename panel.

namespace QFramework.Example
{
	public partial class PopRename : UIElement
	{
		public UwcWindow window;
		public Texture2D texture2D;
		private void Awake()//Implementation of window renaming function
		{
			Button.onClick.AddListener(() =>//is the function of the ok button
			{
				//window.title = InputHereName.text;
				Debug.Log($"Rename Success {InputEnterName.text} to {InputHereName.text}");

				UILeftTabPanel leftTabPanel = UIKit.GetPanel<UILeftTabPanel>();//Determine if the left panel exists
				if (leftTabPanel)
				{
					leftTabPanel.AddLeftTabCell(InputHereName.text,texture2D);//Pass parameters to the left panel
				}
				else
				{
					UIKit.OpenPanel<UILeftTabPanel>().AddLeftTabCell(InputHereName.text,texture2D);//If there is no left panel, create the left panel first and then pass the parameters.
				}
				//Change the texture of the playback panel
				// SelectComponent.Instance.DisplayPanel.SetDisPlayTexture(InputHereName.text,texture2D);
				TypeEventSystem.Global.Send<EventChangeDisPlayMatTexture>(new EventChangeDisPlayMatTexture(){windowName = InputHereName.text,texture2D = window.texture,Window = this.window});
				//Sending this event (in the createnewmeshcontrol script) will pass the selected window to the uwc windows of the cube on the right and the mesh on the left.
				// UIKit.ClosePanel<UIPopWindowsListPanel>();
				UIKit.GetPanel<UIPopWindowsListPanel>().Scale(Vector3.zero);//After clicking ok, the thumbnail is reduced to 0 and hidden.
				gameObject.SetActive(false);
			});
			BtnClose.onClick.AddListener(()=> {//Hide renamed panels.
				gameObject.SetActive(false);
			});
		}

		public void Init( string windowName,Texture2D texture2D,UwcWindow window)//Initialize the cube and synchronize the contents of the selected window
		{
			this.window = window;
			gameObject.SetActive(true);
			//this.window = window;
			this.texture2D = texture2D;
			InputEnterName.text = windowName;
		}
		
		protected override void OnBeforeDestroy()
		{
		}
	}
}