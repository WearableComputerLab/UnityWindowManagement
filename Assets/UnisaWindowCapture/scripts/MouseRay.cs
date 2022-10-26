using System;
using System.Collections.Generic;
using Events;
using QFramework;
using UnityEngine;

//This script is bound in "MainCamera" ,use to create mouse ray and identify the collision point of the ray on the cube surface.
//The script will generate 2 parameters: 1. The coordinates of the collision point 2. Number of collision points,
//these two parameters will be widely used in all scripts that create meshes.

namespace CubeWindow
{
    public class MouseRay : MonoBehaviour //This script is bound to the camera and recognizes mouse behavior. 
    {
        private Ray ray;
        public GameObject canvas;
        private RaycastHit hit;
        public GameObject clickTag;
        Vector3 target;
        GameObject _curGameObject;
        public List<Vector3> vertices = new List<Vector3>();
        public List<GameObject> screenObjects = new List<GameObject> ();
        public List<GameObject> spheres = new List<GameObject>();
        public int count = 0;
        
        private void Start()
        {
            TypeEventSystem.Global.Register<EventRestartMeshCreate>(OnRestartMeshCreateHandler);
            foreach(GameObject ScrObj in GameObject.FindGameObjectsWithTag("ScreenObjects"))
            {
                screenObjects.Add(ScrObj);
            }
        }

        private void OnRestartMeshCreateHandler(EventRestartMeshCreate obj)//Define the function of the addmore button
        {
            vertices.Clear();
            count = 0;//Clear coordinates and counts
            canvas.gameObject.SetActive(true);
            foreach(GameObject plane in GameObject.FindGameObjectsWithTag("ScreenPlane"))
            {
                var texChange = plane.GetComponent<TextureChange>();
                texChange.clicked = false;
            }
            foreach (GameObject ScrObj in screenObjects)
            { 
                ScrObj.transform.localScale = new Vector3(1.92f, 1.08f, 0.999f);//Show the three buttons in the lower right corner... 
            }
           // Destroy(FindObjectOfType<CreateNewMeshCtrl>().gameObject);//Destroy the mesh, four points. ****Commented that line to be able to "Add" more screens not restart
            /*foreach (GameObject o in GameObject.FindGameObjectsWithTag("ClickTag"))
            {
                Destroy(o.gameObject);
            }*/
        }

        void Update()
        {
            //When the left mouse button is pressed .       
            if (Input.GetMouseButtonDown(0))
            {

                //The position of the mouse on the screen. 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && count <= 3)
                {
                    //Draw a red ray from the camera
                    //(The results of this section will not be displayed during the run and are only used for checking during early development work)
                    
                    Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
                    target = hit.point;//Get the coordinates of the mouse（碰撞到正方体上）

                    //Get the information of the object clicked by the mouse
                    //(This part only used for checking during early development work)
                    
                    _curGameObject = hit.transform.gameObject;

                    Quaternion rot = new Quaternion(0, 0, 0, 0);//Get the coordinates of the mouse ray
                    GameObject tag = Instantiate(clickTag, target, rot);//A small red dot appears at the collision location,
                    spheres.Add(tag);
                    vertices.Add(tag.transform.position);
                    count++;//Count the number of collisions with the little red dot.

                    Debug.Log("Get the world coordinate position of the mouse:" + target);
                    Debug.Log("vertex list:" + vertices.Count);
                }
                else
                {
                    Debug.Log("Object not touching / Vertex threshold exceeded");
                }

            }

        }

        private void OnDestroy()//addmore is defined here.
        {
            TypeEventSystem.Global.UnRegister<EventRestartMeshCreate>(OnRestartMeshCreateHandler);
        }
    }
}