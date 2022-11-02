using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CubeWindow;

//Binding to the cancel button, use to clear the clicked point and saved coordinates.

public class Cancel : MonoBehaviour
{
    private GameObject[] tags;
    public MouseRay mouseRay;
    private void Awake()
    {
        mouseRay = GetComponent<MouseRay>();
    }
    public void Delete ()
    {
        GameObject.Find("Main Camera").GetComponent<MouseRay>().vertices =  new List<Vector3>();//Find the four defined points and clear the four points that were previously clicked
        GameObject.Find("Main Camera").GetComponent<MouseRay>().count = 0;//and clears the count of the previous four points.
        tags = GameObject.FindGameObjectsWithTag("ClickTag");//Remove the four points from the scene. (removed in sequence)
        mouseRay.spheres.Clear();
        foreach (GameObject tag in tags)
        {
            Destroy(tag);
        }
    }
}
