using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class ThumbWindowList : MonoBehaviour
{
    [SerializeField] GameObject windowListItem;
    [SerializeField] Transform listRoot;
    public UwcWindowTextureManager windowTextureManager;
    //存储所有的ListItem 每个Item对应一个Window
    Dictionary<int, ThumbnailItem> items_ = new Dictionary<int, ThumbnailItem>();
public RawImage uiWindowTexture;
    void Start()
    {
        // 注册事件 开启新窗口时调用一次 
        UwcManager.onWindowAdded.AddListener(OnWindowAdded);
        // 注册事件 移除新窗口时调用一次 
        UwcManager.onWindowRemoved.AddListener(OnWindowRemoved);

        //初始化列表
        foreach (var pair in UwcManager.windows) {
            OnWindowAdded(pair.Value);
        }
    }

    //生成新打开的窗口
    void OnWindowAdded(UwcWindow window)
    {
        if (!window.isAltTabWindow || window.isBackground) return;

        var gameObject = Instantiate(windowListItem, listRoot, false);
        var listItem = gameObject.GetComponent<ThumbnailItem>();
        listItem.window = window;
        listItem.list = this;
        
        items_.Add(window.id, listItem);
       // Debug.Log("UIwindow    "+window.className+"    "+window.id);
        window.RequestCaptureIcon();
        window.RequestCapture(CapturePriority.Low);
    }
/// <summary>
/// 移除关闭的窗口
/// </summary>
/// <param name="window"></param>
    void OnWindowRemoved(UwcWindow window)
    {
        ThumbnailItem listItem;
        items_.TryGetValue(window.id, out listItem);
        if (listItem) {
            Destroy(listItem.gameObject);
        }
    }

}
