using Events;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using uWindowCapture;

//Generation of Left Panel

namespace QFramework.Example
{
	public class UILeftTabPanelData : UIPanelData
	{
	}
	public partial class UILeftTabPanel : UIPanel
	{
		protected override void ProcessMsg(int eventId, QMsg msg)
		{
			throw new System.NotImplementedException();
		}
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UILeftTabPanelData ?? new UILeftTabPanelData();//Create the left window

			Button_ReStart.onClick.AddListener(() =>//It is the application of addmore (re-add a new window)
			{
				//reset selection
				//RayComponent.Instance.ReStartClick();
				TypeEventSystem.Global.Send<EventRestartMeshCreate>();
			});
		}

		
		public void AddLeftTabCell(string windowName, Texture2D texture2D)//Arrange windows, from top to bottom
		{
			var cell = Instantiate(leftTabCell, Content);
			
			cell.Init(windowName,texture2D);
		}
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
