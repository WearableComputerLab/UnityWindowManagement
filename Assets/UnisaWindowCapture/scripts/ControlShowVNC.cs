using Events;
using QFramework;
using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlShowVNC : MonoBehaviour
{
    // Start is called before the first frame update
    public void HideCubeWindow()//Hide the local window on the cube,
    {
        //GameObject.Find("VNCScreen/Screen").GetComponent<Renderer>().enabled = false;
        TypeEventSystem.Global.Send<EventHideCubeWindow>();


    }
 
    public void ShowCubeWindow()//This method is bound to the quit VNC button.
    {
        // GameObject.Find("VNCScreen/Screen").GetComponent<Renderer>().enabled = true;      
        TypeEventSystem.Global.Send<EventShowCubeWindow>();
    }
}
