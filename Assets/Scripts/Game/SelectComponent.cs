//This script is not used in the final version (It is for a standard quad cube window and is limited to one side of the cube)
//It was originally used to detect and judge how many small balls were generated (that is, the user clicked several times),
//and then judged whether the thumbnail panel could be popped up according to the number of times.
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Serialization;

namespace QFramework.Example
{
    public partial class SelectComponent : MonoSingleton<SelectComponent>
    {
        public ClickDisplayPanel DisplayPanel;

        public List<ClickSelectItem> clickSelectItems;
        
        private int clickCount = 0;
        private int startId = 0;
        private int nextId = 0;


        void Start()
        {
            ResKit.Init();
            
            TypeEventSystem.Global.Register<EventClickSelectItem>(OnClickSelectItem);
        }

        private void OnClickSelectItem(EventClickSelectItem obj)
        {
            if (clickCount == 0)
            {
                startId = obj.id;
                nextId = obj.nextid;
                clickSelectItems.Find(va => va.id == obj.id).ChangeSelectStatus(true);
                clickCount++;
            }

            if (nextId == obj.clickItem.id)
            {
                clickSelectItems.Find(va => va.id == obj.id).ChangeSelectStatus(true);
                nextId = obj.nextid;
                clickCount++;
                if (nextId == startId)
                {
                    

                    Debug.Log("All Selected");
                    foreach (ClickSelectItem item in clickSelectItems)
                    {
                        item.ChangeSelectStatus(false);
                        DisplayPanel.SelectDisPlay();
                        RestartStatus();
                    }
                    clickCount = 0;
                }
            }
        }

        private void RestartStatus()
        {
            if (UIKit.GetPanel<UIPopWindowsListPanel>())
            {
                UIKit.GetPanel<UIPopWindowsListPanel>().Scale(Vector3.one);
            }
            else
            {
                UIKit.OpenPanel<UIPopWindowsListPanel>();
            }
            
        }

       

        protected override void OnDestroy()
        {
            base.OnDestroy();
            TypeEventSystem.Global.UnRegister<EventClickSelectItem>(OnClickSelectItem);
        }
    }
}