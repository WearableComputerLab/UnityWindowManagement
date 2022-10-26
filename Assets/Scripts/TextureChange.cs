using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uWindowCapture;

public class TextureChange : MonoBehaviour
{

    public bool clicked = false;
    public bool hovering = false;
    
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        if(hovering)
        {
            if(Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            clicked = false;
        }
        if (clicked)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if ((!hovering && hit.collider.CompareTag("ScreenPlane")) || (!hovering && hit.collider.CompareTag("ScreenObjects")))
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        clicked = false;
                    }                    
                }
            }
            if(Input.GetKey(KeyCode.Delete))
            {
                Destroy(gameObject);
            }
        }
    }
    public void OnMouseEnter()
    {
        hovering = true;
    }
    private void OnMouseExit()
    {
        hovering = false;
    }  
}
