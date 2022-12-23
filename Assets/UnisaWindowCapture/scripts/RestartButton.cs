using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public PopRename popRename;
    public CountKeeper countKeeper;
    private void Awake()
    {
        popRename = FindObjectOfType<PopRename>();
        countKeeper = FindObjectOfType<CountKeeper>();
    }
    public void OnRestartClick()
    {
        foreach(GameObject plane in GameObject.FindGameObjectsWithTag("ScreenPlane"))
        {
            popRename.n = 1;
            countKeeper.count= 1;
            Destroy(plane);
        }
    }
    private void Update()
    {
        if(popRename == null)
        {
            popRename = FindObjectOfType<PopRename>();
        }
    }
}
