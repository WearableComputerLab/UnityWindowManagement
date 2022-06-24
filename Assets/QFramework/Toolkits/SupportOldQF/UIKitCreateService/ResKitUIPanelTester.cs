using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework
{
    public class ResKitUIPanelTester : MonoBehaviour
    {
 
            public string PanelName;

            public UILevel Level;

            [SerializeField] private List<UIPanelTesterInfo> mOtherPanels;

            private void Awake()
            {
                ResKit.Init();
            }

            private IEnumerator Start()
            {
                yield return new WaitForSeconds(0.2f);
			
                UIKit.OpenPanel(PanelName, Level);

                mOtherPanels.ForEach(panelTesterInfo => { UIKit.OpenPanel(panelTesterInfo.PanelName, panelTesterInfo.Level); });
            }
    }
}