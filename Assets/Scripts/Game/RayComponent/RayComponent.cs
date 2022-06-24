//This script is not used in the final version (It is for a standard quad cube window and is limited to one side of the cube)
//This script is used to generate a window on the cube. The purpose of this script is that when the user clicks on the four corners of one side of the cube,
//red balls can be generated at the positions of the clicked four vertices, and the number of generated balls can be recorded and judged. , when the number reaches 4,
//it means that the four vertices of the window are created, and then the mesh and the material on the mesh are generated in it.
using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using QFramework;
using QFramework.Example;
using UnityEngine;

public class RayComponent : MonoSingleton<RayComponent>
{
    public GameObject sphere;//Generated ball object
    public Transform sphereParent;//A parent object for the generated ball

    public int ClickCount = 0;//number of clicks
    public Material mat;//Generate mesh material
    public GameObject newMesh;//Generated mesh object

    public List<GameObject> clickSpherelist = new List<GameObject>();//The set of four points generated.

    private void Awake()
    {
        ResKit.Init();//Initialize resources
    }

    // Start is called before the first frame update 
    void Start()
    {
       
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "DisplayPanel"&& newMesh==null&& ClickCount<4)
                {
                    GameObject obj = GameObject.Instantiate(sphere, sphereParent);
                    Vector3 office =sphereParent.TransformPoint(sphereParent.localPosition - new Vector3(0, 0, 0.03f)) ;
                    obj.transform.position = hit.point+office;
                    clickSpherelist.Add(obj);//Add the generated ball to the collection
                    ClickCount++;//Increased accumulation of pellets
                    if (ClickCount==4)
                    {
                        Debug.Log("four tags");
                        //After the conditions are met, the ball is confirmed to be in position.
                        newMesh = CreatenewMeshHelper.CreateMeshBy4Point(clickSpherelist[0].transform.position,
                            clickSpherelist[1].transform.position,
                            clickSpherelist[2].transform.position,
                            clickSpherelist[3].transform.position,mat);
                        newMesh.AddComponent<CreateNewMeshCtrl>();
                        //This script handles some mesh-related stuff after the ball's position has been determined.
                        WindowListPop();
                       
                    }
                    
                }
               
            }
        }
    }
    private void WindowListPop()
    //Open the thumbnail window
    {
        if (UIKit.GetPanel<UIPopWindowsListPanel>())
        {
            UIKit.GetPanel<UIPopWindowsListPanel>().Scale(Vector3.one);
        }
        else
        {
            UIKit.OpenPanel<UIPopWindowsListPanel>();
        }
            
    }

    public void ReStartClick()
    {
        ClickCount = 0;//clear the number of balls
        if (newMesh)
        {
            Destroy(newMesh.gameObject);
            foreach (GameObject o in clickSpherelist)
            {
                Destroy(o);//If there is a new mesh, destroy the old mesh,
            }
            clickSpherelist.Clear();//Delete the balls in order.
            newMesh = null;
        }
    }
   

    private void OnDestroy()
    {

    }
}