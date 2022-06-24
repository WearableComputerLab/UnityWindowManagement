//This script is not used in the final version (It is for a standard quad cube window and is limited to one side of the cube)
//This part of the code is responsible for the control of the ball, including the display or hiding of
//the ball according to the click of the mouse, and the control of the color.
using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
public class ClickSelectItem : MonoBehaviour
{
    public int id;
    public int Nextid;

    public Color scelectColor=Color.red;
    public Color unScelectColor=Color.cyan;

    private MeshRenderer meshRenderer;
    //public bool allowClick; 
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        ChangeSelectStatus(false);
    }

    public void ChangeSelectStatus(bool select)//Determine if the ball is hit
    {
        if (select)//If it is clicked, it will be red
        {
            meshRenderer.material.color = scelectColor;
            meshRenderer.enabled = true;
        }
        else//If it is not tapped, the ball is not displayed. will be cyan
        {
            meshRenderer.material.color = unScelectColor;
            meshRenderer.enabled = false;
        }
    }
    private void OnMouseDown()//When the mouse is clicked, determine whether the position of the ball is generated.
    {
        TypeEventSystem.Global.Send<EventClickSelectItem>( new EventClickSelectItem(){clickItem = this,id = id,nextid = this.Nextid});
    }
}
