/****************************************************************************
 * 2022.6 DESKTOP-CEJCKJU
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class leftTabCell
	{
		[SerializeField] public UnityEngine.UI.RawImage RawImg;
		[SerializeField] public TMPro.TextMeshProUGUI Txtname;
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Button BtnCrop;

		public void Clear()
		{
			RawImg = null;
			Txtname = null;
			BtnClose = null;
			BtnCrop = null;
		}

		public override string ComponentName
		{
			get { return "leftTabCell";}
		}
	}
}
