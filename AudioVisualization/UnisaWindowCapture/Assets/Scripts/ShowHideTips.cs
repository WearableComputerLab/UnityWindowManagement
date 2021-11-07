using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.Sprites;
public class ShowHideTips : MonoBehaviour
{
    GameObject ti;
    void Start()
    {
        ti = GameObject.FindGameObjectWithTag("EditorOnly");
    }

    public void ShowHideTip()
    {
      
        if (ti.active == false)
        {
            
            ti.SetActive(true);

        }
        else if (ti.active == true)
        {
            
            ti.SetActive(false);

        }
    }
}