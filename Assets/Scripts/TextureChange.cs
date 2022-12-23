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
        if(popRename == null)
        {
            popRename= FindObjectOfType<PopRename>();
        }
        if (adjusting)
        {
            clicked = true;
            for (int i = 0; i < sphereChildren.Count; i++)
            {
                newSphereVerts[i] = sphereChildren[i].transform.position;
            }            
        }
        if (adjustButton != null)
        {
            adjustButton.onClick.AddListener(() =>
            {
                if (clicked)
                {
                    adjusting = true;
                    refreshed = false;
                    freeCam.enabled = false;
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
            doneButton.onClick.AddListener(() =>
            {
                if (sphereChildren[0].activeInHierarchy)
                {
                    if (clicked)
                    {
                        adjusting = false;
                        if (!refreshed)
                        {
                            Vector2[] uvs = new Vector2[]
                             {
                              new Vector2(0, 0),
                              new Vector2(1, 0),
                              new Vector2(1, 1),
                              new Vector2(0, 1),
                             };
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
    public void OnMouseEnter()
    {
        hovering = true;
    }
    private void OnMouseExit()
    {
        hovering = false;
    }  
}
