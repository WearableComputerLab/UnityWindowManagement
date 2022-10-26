using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
   
    public void OnRestartClick()
    {
        foreach(GameObject plane in GameObject.FindGameObjectsWithTag("ScreenPlane"))
        {
            Destroy(plane);
        }
    }
}
