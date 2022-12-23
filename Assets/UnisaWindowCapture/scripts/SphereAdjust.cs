using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereAdjust : MonoBehaviour
{

    public bool hovering, clicked;
    public Button rightButton;
                   
    public void RightButton()
    {
        transform.position += new Vector3(0.03f, 0, 0);
    }
    public void LefttButton()
    {
        transform.position += new Vector3(-0.03f, 0, 0);
    }
    public void UpButton()
    {
        transform.position += new Vector3(0, 0.03f, 0);
    }
    public void DownButton()
    {
        transform.position += new Vector3(0, -0.03f, 0);
    }
}
