using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeWindow;
using uWindowCapture;
using QFramework;
using QFramework.Example;

//This script is used to create a mesh for the VNC window.
public class CreatMesh_VNCwin : MonoBehaviour 
{
    private MeshFilter filter;
    private Mesh mesh;
    private bool ifrun = false;

     public void creatVNCmesh()
    {
        //Creat new mesh for VNC screen 
        List<Vector3> v = GameObject.Find("Main Camera").GetComponent<MouseRay>().vertices;
        if (v.Count == 4 && ifrun == false)//Determine if the count of points is 4
        {
            filter = GetComponent<MeshFilter>();
            mesh = new Mesh();
            filter.mesh = mesh;
            CreatNewMesh(v);
            ifrun = true;//4 points are good, the mesh is successfully created, and VNC can capture.


        }
    }

    void CreatNewMesh(List<Vector3> x)
    {
        mesh.name = "CustomNewMesh";
        Vector3[] ver = x.ToArray();
        mesh.vertices = ver;

        // Create triangles from vertices for meshes.
        int[] triangles = new int[2 * 3]{
             1, 3, 0, 1, 2, 3,//front      
             };//bottom

        mesh.triangles = triangles;

        Vector2[] uvs = new Vector2[4]{  //Defines how the contents of the VNC window are being displayed in the mesh by setting the order of UV coordinates.
        new Vector2(0, 1),
        new Vector2(1, 1),
        new Vector2(1, 0),
        new Vector2(0, 0),
        };
        mesh.uv = uvs;

        Debug.Log("VNC mesh" + ver[0] + ver[1] + ver[2] + ver[3]);
    }
}
