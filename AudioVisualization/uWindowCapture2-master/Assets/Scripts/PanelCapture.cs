using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using uWindowCapture;
using Button = UnityEngine.UI.Button;

public class PanelCapture : MonoBehaviour
{
    public UwcWindowList windowList;

    public InputField inputFindWin;
    public Button btnFind;

    public ScrollView ScrollVThumb;
    public GameObject thumbItem;
    // Start is called before the first frame update
    void Start()
    {
        btnFind.onClick.AddListener(() =>
        {
            windowList.FindWindow(inputFindWin.text);
        });
    }

    public void InstanceThumbItem()
    {
        
    }

   
}
