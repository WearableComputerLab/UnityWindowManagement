using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using uWindowCapture;

//Generation of Thumbnail Panel

namespace QFramework.Example
{
	public class UIPopWindowsListPanelData : UIPanelData
	{
	}
	public partial class UIPopWindowsListPanel : UIPanel
	{
		public GameObject cellpre;

		public RectTransform content;

		//Store all ListItems, each Item corresponds to a Window¡£
		Dictionary<int, ScreenCellPre> items_ = new Dictionary<int, ScreenCellPre>();
		protected override void ProcessMsg(int eventId, QMsg msg)
		{
			
		}
	
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIPopWindowsListPanelData ?? new UIPopWindowsListPanelData();
			//  Create Thumbnails

			PopRename.gameObject.SetActive(false);//The rename panel is hidden until one of the thumbnail buttons is clicked.

			// Register event Called once when a new window is opened 
			UwcManager.onWindowAdded.AddListener(OnWindowAdded);
			// Register event Called once when a new window is removed 
			UwcManager.onWindowRemoved.AddListener(OnWindowRemoved);

			//initialization list 
			foreach (var pair in UwcManager.windows) {
				OnWindowAdded(pair.Value);
			}
		}

		public void PopRenamePanel(string windowName,Texture2D texture2D,UwcWindow window)//Initialize the rename panel
		{
			
			PopRename.Init(windowName,texture2D,window);
			
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		//Generate newly opened window
		void OnWindowAdded(UwcWindow window)//The specific monitoring steps determine that a new window is opened.
											//As long as a new window is opened, its name, texture and other attributes will be passed into the cell.
		{
			if (!window.isAltTabWindow || window.isBackground || window.isDesktop ||window.isApplicationFrameWindow) return;

			var cell = Instantiate(cellpre, content);
			cell.name = window.id.ToString();
			cell.SetActive(true);
			var screencell = cell.GetComponent<ScreenCellPre>();
			screencell.Init(this,window);
			//Debug.Log(screencell.name);

			items_.Add(window.id, screencell);//New windows that open are added to the Thumbnails panel

			window.RequestCaptureIcon();//window screen
			window.RequestCapture(CapturePriority.Low);
		}
		/// <summary>
		/// Remove closed windows
		/// </summary>
		/// <param name="window"></param>
		void OnWindowRemoved(UwcWindow window)//If the window is closed, you need to clear all the internal properties of the window on the thumbnail.
		{
			ScreenCellPre listItem;
			items_.TryGetValue(window.id, out listItem);
			if (listItem) {
				Destroy(listItem.gameObject);
			}
		}
		protected override void OnShow()
		{
			
		}
		
		protected override void OnHide()
		{
			
		}

		public void Scale(Vector3 scale)
		{
			transform.localScale = scale;
		}
		protected override void OnClose()
		{
		}
	}
}
