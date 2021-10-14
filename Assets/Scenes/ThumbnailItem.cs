using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class ThumbnailItem : MonoBehaviour
{
    public RawImage _imageTexture;
    public UwcWindow window { get; set; }
    public ThumbWindowList list { get; set; }
    public UwcWindowTexture windowTexture { get; set; }

    private Button BtnThumbail;

    private void Start()
    {
        BtnThumbail = GetComponent<Button>();
        BtnThumbail.onClick.AddListener(OnClick);

        var ThumbailTitle = window.title;
        //Debug.Log(ThumbailTitle);

        const int MaxThumbailText = 20;

        //如果窗口title长途过长，则以20单位chart为分界线显示部分title
        if (ThumbailTitle.Length > MaxThumbailText)
        {
            ThumbailTitle = ThumbailTitle.Substring(0, MaxThumbailText);
        }
        //将窗口title传入缩略图text子对象
        Text text = BtnThumbail.transform.Find("Text").GetComponent<Text>();
        text.text = string.IsNullOrEmpty(ThumbailTitle) ? "-No Name-" : ThumbailTitle;
   
    }

    void Update()
    {
        if (window == null) return;
        _imageTexture.texture = window.texture;
    }

    //点击显示/关闭image
    public void OnClick()
    {
        var manager = list.windowTextureManager;
        UwcWindowTexture uwcWindowTexture=manager.Get(window.id);
        if (uwcWindowTexture.gameObject.layer==LayerMask.NameToLayer("window"))
        {
            //如果正在显示的状态
            manager.ChangeLayer(window,false);
        }
        else
        {
            manager.ChangeLayer(window,true);
        }
        list.uiWindowTexture.gameObject.SetActive(true);
    }
}