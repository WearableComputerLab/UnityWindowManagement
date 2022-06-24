//This script is not used in the final version (It is for a standard quad cube window and is limited to one side of the cube)
//Captured content was projected onto the overlapping panel on a fixed surface of the cube.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;


public class PanelCapture : MonoBehaviour
{
    public UwcWindowList windowList;

    public InputField inputFindWin;
    public Button btnFind;
    // Start is called before the first frame update
    void Start()
    {
        btnFind.onClick.AddListener(() =>
        {
            windowList.FindWindow(inputFindWin.text);
        });
    }

   
}
