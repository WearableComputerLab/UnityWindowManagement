using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.Sprites;
public class ShowHideUI : MonoBehaviour
{
   
    //bool ifShow = true;
    public Sprite show;
    public Sprite unshow;
    GameObject gu;

    void Start()
    {
        gu = GameObject.FindGameObjectWithTag("Finish");
    }

    public void ShowHide()
    {
        GameObject b1 = GameObject.Find("button_UPview");
        if (gu.active == false)
        {
            gameObject.GetComponent<Image>().sprite = show;
            gu.SetActive(true);

        }
        else if (gu.active == true)
        {
            gameObject.GetComponent<Image>().sprite = unshow;
            gu.SetActive(false);
            
        }
    }
}