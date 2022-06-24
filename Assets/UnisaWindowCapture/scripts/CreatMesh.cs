//This script is used for mesh creation, not used in the final version, 
//but its content is used by other scripts (including CreateMesh_VNCwin and LoadThumbnail)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeWindow;
using uWindowCapture;
using QFramework;
using QFramework.Example;

public class CreatMesh : MonoBehaviour
                                      
{
    private MeshFilter filter;
    private Mesh mesh;

    public void creatMesh()
    {
        //Create new mesh
        List<Vector3> v = FindObjectOfType<MouseRay>().vertices;
        if (v.Count == 4)
        {
            filter = GetComponent<MeshFilter>();
            mesh = new Mesh();
            filter.mesh = mesh;
            CreatNewMesh(v);

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

        Vector2[] uvs = new Vector2[4]{
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),
        };
        mesh.uv = uvs;
    }
}
