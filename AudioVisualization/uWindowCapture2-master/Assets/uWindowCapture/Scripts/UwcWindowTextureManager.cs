using System;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace uWindowCapture
{
    public class UwcWindowTextureManager : MonoBehaviour
    {
        [SerializeField] GameObject windowPrefab;

        Dictionary<int, UwcWindowTexture> windows_ = new Dictionary<int, UwcWindowTexture>();

        public Dictionary<int, UwcWindowTexture> windows
        {
            get { return windows_; }
        }

        UwcWindowTextureEvent onWindowTextureAdded_ = new UwcWindowTextureEvent();

        public UwcWindowTextureEvent onWindowTextureAdded
        {
            get { return onWindowTextureAdded_; }
        }

        UwcWindowTextureEvent onWindowTextureRemoved_ = new UwcWindowTextureEvent();

        public UwcWindowTextureEvent onWindowTextureRemoved
        {
            get { return onWindowTextureRemoved_; }
        }

        private void Start()
        {
            //显示所有的窗口
            foreach (var pair in UwcManager.windows) {
                AddWindowTexture(pair.Value);
            }
            
            // 注册事件 开启新窗口时调用一次 
            UwcManager.onWindowAdded.AddListener(va =>
            {
                
                AddWindowTexture(va);
            });
            // 注册事件 移除新窗口时调用一次 
            UwcManager.onWindowRemoved.AddListener(RemoveWindowTexture);
           
        }

        public UwcWindowTexture AddWindowTexture(UwcWindow window)
        {
            if (!window.isAltTabWindow || window.isBackground) return null;
            if (!windowPrefab)
            {
                Debug.LogError("windowPrefab is null.");
                return null;
            }

            var obj = Instantiate(windowPrefab, transform);
            var windowTexture = obj.GetComponent<UwcWindowTexture>();
           
            Assert.IsNotNull(windowTexture, "Prefab must have UwcWindowTexture component.");
            windowTexture.window = window;
            windowTexture.manager = this;
            //Debug.Log("3dwindow    "+window.className+"    "+window.id);
            windows_.Add(window.id, windowTexture);
            onWindowTextureAdded.Invoke(windowTexture);
            return windowTexture;
        }

        /// <summary>
        /// 刷新显示层级 //改变显示层级 只有在window层再能被看到
        ///windowTexture.gameObject.layer = LayerMask.GetMask("Default");
        /// </summary>
        /// <param name="WindowName"></param>
        public void RefreshLayout(UwcWindow window)
        {
           
            foreach (UwcWindowTexture uwcWindowTexture in windows_.Values)
            {
                if (uwcWindowTexture.window.id==window.id)
                {
                    uwcWindowTexture.gameObject.layer = LayerMask.NameToLayer("window");
                }
                uwcWindowTexture.gameObject.layer =  LayerMask.NameToLayer("igonwindow");
            }
        }

        /// <summary>
        /// 改变layer  
        /// </summary>
        /// <param name="window"></param>
        /// <param name="isWindow">如果为true  就更该层级为window</param>
        public void ChangeLayer(UwcWindow window,bool isWindow)
        {
            Get(window.id).gameObject.layer =isWindow?LayerMask.NameToLayer("window"): LayerMask.NameToLayer("igonwindow");
        }
        public void RemoveWindowTexture(UwcWindow window)
        {
            UwcWindowTexture windowTexture;
            windows_.TryGetValue(window.id, out windowTexture);
            if (windowTexture)
            {
                onWindowTextureRemoved.Invoke(windowTexture);
                windows_.Remove(window.id);
                Destroy(windowTexture.gameObject);
            }
        }

        public UwcWindowTexture Get(int id)
        {
            UwcWindowTexture window = null;
            windows.TryGetValue(id, out window);
            return window;
        }
    }
}