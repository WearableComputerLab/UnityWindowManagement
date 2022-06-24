using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:b35c615a-b0a1-4d2f-bd08-672de22168f9
	public partial class UIPopWindowsListPanel
	{
		public const string Name = "UIPopWindowsListPanel";
		
		[SerializeField]
		public UnityEngine.RectTransform Content;
		[SerializeField]
		public PopRename PopRename;
		
		private UIPopWindowsListPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Content = null;
			PopRename = null;
			
			mData = null;
		}
		
		public UIPopWindowsListPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIPopWindowsListPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIPopWindowsListPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
