using UnityEngine;
using System.Collections.Generic;

namespace uWindowCapture
{

    public class UwcWindowList : MonoBehaviour
    {
        [SerializeField] GameObject windowListItem;
        [SerializeField] Transform listRoot;

        public UwcWindowTextureManager windowTextureManager;

        //存储所有的ListItem 每个Item对应一个Window
        Dictionary<int, UwcWindowListItem> items_ = new Dictionary<int, UwcWindowListItem>();

        void Start()
        {
            // 注册事件 开启新窗口时调用一次 
            UwcManager.onWindowAdded.AddListener(OnWindowAdded);
            // 注册事件 移除新窗口时调用一次 
            UwcManager.onWindowRemoved.AddListener(OnWindowRemoved);

            //初始化列表
            foreach (var pair in UwcManager.windows)
            {
                OnWindowAdded(pair.Value);
            }
        }
        /// <summary>
        /// 查找并更新windowItem列表
        /// </summary>
        /// <param name="windName"></param>
        public void FindWindow(string windName)
        {
            //隐藏所有实例化的listItem 
            foreach (UwcWindowListItem item in items_.Values)
            {
                item.gameObject.SetActive(false);
            }
            //匹配对应的Item进行显示
            if (windName == "")
            {
                foreach (UwcWindowListItem item in items_.Values)
                {
                    item.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (UwcWindowListItem item in items_.Values)
                {
                    //如果包含搜索的窗口名就显示
                    if (item.window.title.Contains(windName))
                    {
                        item.gameObject.SetActive(true);
                    }

                }
            }
        }

        //生成新打开的窗口
        void OnWindowAdded(UwcWindow window)
        {
            if (!window.isAltTabWindow || window.isBackground) return;

            var gameObject = Instantiate(windowListItem, listRoot, false);
            var listItem = gameObject.GetComponent<UwcWindowListItem>();
            listItem.window = window;
            listItem.list = this;
            items_.Add(window.id, listItem);

            window.RequestCaptureIcon();
            window.RequestCapture(CapturePriority.Low);
        }
        /// <summary>
        /// 移除关闭的窗口
        /// </summary>
        /// <param name="window"></param>
        void OnWindowRemoved(UwcWindow window)
        {
            UwcWindowListItem listItem;
            items_.TryGetValue(window.id, out listItem);
            if (listItem)
            {
                Destroy(listItem.gameObject);
            }
        }
    }

}