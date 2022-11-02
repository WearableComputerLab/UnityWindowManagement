using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class TextureChange : MonoBehaviour
{

    public bool clicked = false;
    public bool hovering = false;
    public Button adjustButton;
    public Button doneButton;
    Ray ray;
    RaycastHit hit;
    public RectTransform leftPanel;
    public List<GameObject> sphereChildren = new List<GameObject>();
    
    void Update()
    {
        if (adjustButton != null)
        {
            adjustButton.onClick.AddListener(() =>
            {
                if (clicked)
                {
                    foreach (GameObject sphere in sphereChildren)
                    {
                        sphere.SetActive(true);
                    }
                }
            });
        }
        if (doneButton != null)
        {
            doneButton.onClick.AddListener(() =>
            {
                if (clicked)
                {
                    foreach (GameObject sphere in sphereChildren)
                    {
                        sphere.SetActive(false);
                    }
                }
            });
        }
        if (hovering)
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
            if(adjustButton == null)
            {
                adjustButton = GameObject.Find("Button_Adjust").GetComponent<Button>();
            }
            if (doneButton == null)
            {
                doneButton = GameObject.Find("Button_Done").GetComponent<Button>();
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (!hovering && hit.collider.CompareTag("ScreenPlane"))
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        clicked = false;
                    }                    
                }
                if(!hovering && hit.collider.CompareTag("ScreenObjects"))
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        clicked = false;
                        adjustButton.gameObject.SetActive(false);
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
