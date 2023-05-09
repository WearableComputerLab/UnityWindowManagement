using QFramework;
using QFramework.Example;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uWindowCapture;

public class TextureChange : MonoBehaviour
{
    public Material mat;
    public float cameraDistance = -2f;
    public bool clicked = false;
    public bool hovering = false;
    public bool refreshed = false;
    bool adjusting = false;
    public Button adjustButton;
    public Button doneButton;
    Ray ray;
    RaycastHit hit;    
    public List<GameObject> sphereChildren = new List<GameObject>();
    public List<Vector3> newSphereVerts = new List<Vector3>();
    public FreeCamera freeCam;
    public PopRename popRename;
    public CountKeeper countKeeper;
    

    private void Start()
    {
        freeCam = Camera.main.GetComponent<FreeCamera>();
        mat = Resources.Load<Material>("planemat");
        countKeeper = FindObjectOfType<CountKeeper>();
    }
    void Update()
    {
        // Checking if an instance of the PopRename class exists
        if (popRename == null)
        {
            popRename= FindObjectOfType<PopRename>();
        }

        // Updating the new sphere vertices if currently adjusting
        if (adjusting)
        {
            clicked = true;
            for (int i = 0; i < sphereChildren.Count; i++)
            {
                newSphereVerts[i] = sphereChildren[i].transform.position;
            }            
        }

        // Adding a onclick listener to the adjust button if it exists
        if (adjustButton != null)
        {
            adjustButton.onClick.AddListener(() =>
            {
                if (clicked)
                {
                    adjusting = true;
                    refreshed = false;
                    freeCam.enabled = false;

                    // Moving the camera to a certain distance from the texture plane
                    Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, cameraDistance);                                        
                    foreach (GameObject sphere in sphereChildren)
                    {
                        sphere.SetActive(true);
                    }
                }
            });
        }
        if (doneButton != null)
        {
            // Adding a onclick listener to the adjust button if it exists
            doneButton.onClick.AddListener(() =>
            {
                if (sphereChildren[0].activeInHierarchy)
                {
                    if (clicked)
                    {
                        adjusting = false;
                        if (!refreshed)
                        {
                            // Creating a new mesh with 4 vertices
                            Vector2[] uvs = new Vector2[]
                             {
                              new Vector2(0, 0),
                              new Vector2(1, 0),
                              new Vector2(1, 1),
                              new Vector2(0, 1),
                             };
                            // Adding components to the new mesh object
                            GameObject newMesh = CreatenewMeshHelper.RefreshMeshBy4Point(newSphereVerts[0], newSphereVerts[1], newSphereVerts[2], newSphereVerts[3], mat, uvs);
                            newMesh.AddComponent<CreateNewMeshCtrl>();
                            newMesh.AddComponent<BoxCollider>();
                            newMesh.AddComponent<TextureChange>();
                            newMesh.tag = "ScreenPlane";
                            refreshed = true;
                            foreach (GameObject sphere in sphereChildren)
                            {
                                freeCam.enabled = true;
                                sphere.transform.SetParent(newMesh.transform);
                                newMesh.GetComponent<TextureChange>().sphereChildren.Add(sphere);
                                newMesh.GetComponent<TextureChange>().newSphereVerts.Add(sphere.transform.position);
                                sphere.SetActive(false);
                            }
                        }

                    }
                    Destroy(gameObject);
                }
            });
        }
        // Checking if the mouse is hovering over the texture plane
        if (hovering)
        {
            if(Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
        }

        // Checking if the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            clicked = false;
        }

        // Updating the clicked flag if left mouse button is pressed
        if (clicked)
        {
            // Finding the adjust button game object if it does not exist
            if (adjustButton == null)
            {
                adjustButton = GameObject.Find("Button_Adjust").GetComponent<Button>();
            }
            // Finding the done button game object if it does not exist
            if (doneButton == null)
            {
                doneButton = GameObject.Find("Button_Done").GetComponent<Button>();
                
            }
            // Casting a ray from the camera to the mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Checking if the mouse is hovering or not and the colliding object is a screen plane
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
                    }
                }
            }

            if(Input.GetKey(KeyCode.Delete))
            {
                popRename.n--;
                countKeeper.count--;
                Destroy(gameObject);
            }       
            
        }
    }

    // Called when the mouse enters the texture plane
    public void OnMouseEnter()
    {
        hovering = true;
    }
    // Called when the mouse exits the texture plane
    private void OnMouseExit()
    {
        hovering = false;
    }  
}
