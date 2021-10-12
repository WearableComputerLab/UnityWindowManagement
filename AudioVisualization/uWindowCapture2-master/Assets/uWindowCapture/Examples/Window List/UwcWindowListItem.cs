﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace uWindowCapture
{

[RequireComponent(typeof(Image))]
public class UwcWindowListItem : MonoBehaviour 
{
    Image image_;
    [SerializeField] Color selected;
    [SerializeField] Color notSelected;

    public UwcWindow window { get; set; }
    public UwcWindowList list { get; set; }
    public UwcWindowTexture windowTexture { get; set; }
    
    [SerializeField] RawImage icon;
    [SerializeField] Text title;
    [SerializeField] Text x;
    [SerializeField] Text y;
    [SerializeField] Text z;
    [SerializeField] Text width;
    [SerializeField] Text height;
    [SerializeField] Text status;

    void Awake()
    {
        image_ = GetComponent<Image>();
        image_.color = notSelected;
    }
    
    void Update()
    {
        if (window == null) return;

        if (!window.hasIconTexture && !window.isIconic) {
            icon.texture = window.texture;
        } else {
            icon.texture = window.iconTexture;
        }

        var windowTitle = window.title;
        title.text = string.IsNullOrEmpty(windowTitle) ? "-No Name-" : windowTitle;

        x.text = window.isMinimized ? "-" : window.x.ToString();
        y.text = window.isMinimized ? "-" : window.y.ToString();
        z.text = window.zOrder.ToString();

        width.text = window.width.ToString();
        height.text = window.height.ToString();

        status.text = 
            window.isIconic ? "Iconic" :
            window.isZoomed ? "Zoomed" :
            "-";
    }

    public void OnClick()
    {
        var manager = list.windowTextureManager;
        UwcWindowTexture uwcWindowTexture=manager.Get(window.id);
        if (uwcWindowTexture.gameObject.layer==LayerMask.NameToLayer("window"))
        {
            //如果正在显示的状态
            manager.ChangeLayer(window,false);
            image_.color = notSelected;
        }
        else
        {
            manager.ChangeLayer(window,true);
            image_.color = selected;
        }
       list.uiWindowTexture.gameObject.SetActive(true);
    }
    
}

}