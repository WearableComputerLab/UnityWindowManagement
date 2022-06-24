using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour //Click quit to exit the program
{
    public void QuitProcess()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

       
#endif
        Application.Quit();
    }
}