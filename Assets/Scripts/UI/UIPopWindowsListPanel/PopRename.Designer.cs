/****************************************************************************
 * 2022.5 DESKTOP-CEJCKJU
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class PopRename
	{
		[SerializeField] public TMPro.TMP_InputField InputEnterName;
		[SerializeField] public TMPro.TMP_InputField InputHereName;
		[SerializeField] public UnityEngine.UI.Button Button;
		[SerializeField] public UnityEngine.UI.Button BtnClose;

		public void Clear()
		{
			InputEnterName = null;
			InputHereName = null;
			Button = null;
			BtnClose = null;
		}

		public override string ComponentName
		{
			get { return "PopRename";}
		}
	}
}
