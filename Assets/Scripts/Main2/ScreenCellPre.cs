using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using QFramework.Example;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
using uWindowCapture;
using Color = UnityEngine.Color;

//This script is used to control each sub window on the thumbnail panel, and each sub object also has a button property.

public class ScreenCellPre : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Image imgBg;
    private Button btn;
    public RawImage imgWindow;
    public TMP_Text screenName;

    private Color defaultColor;
    private Color outlineColor=Color.cyan;
    public UwcWindow window;
     bool showTextureSucceed;

    private float timer = 0.2f;

    private UIPopWindowsListPanel uiPopWindowsListPanel;
    // Start is called before the first frame update. 
    void Awake()
    //has button attribute
    {
        btn = GetComponent<Button>();
        imgBg = GetComponent<Image>();
        defaultColor = imgBg.color;
    }

    public void Init(UIPopWindowsListPanel uiPopWindowsListPanel,UwcWindow window)
    {
        this.uiPopWindowsListPanel = uiPopWindowsListPanel;
        this.window = window;
        
        screenName.text = window.title;
        btn.onClick.AddListener(OnBtnClickRename);
        
    }

    private void OnBtnClickRename()
    {
        //copy a sticker
        Texture2D source = imgWindow.texture as Texture2D;     
        uiPopWindowsListPanel.PopRenamePanel(window.title,TextureRotHelper.RoateTextureUpDown180(source),window);
    }
    

    void Update()
    {
        if (window!=null&& !showTextureSucceed)
        {
            this.window.RequestCapture();
           // Debug.Log("windwo  No null");
        }
        if (window != null&& window.texture&& !showTextureSucceed)
        {
            imgWindow.texture = TextureRotHelper.CopyT2DToWrite(window.texture);
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                //Debug.Log(window.texture.width+" "+window.texture.height);
                showTextureSucceed = true;

            }
        }
        
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(OnBtnClickRename);
        window = null;
    }

    /// <summary>

/// </summary>
/// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        imgBg.color=outlineColor;
    }
/// <summary>

/// </summary>
/// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        imgBg.color=defaultColor;

    }
}
