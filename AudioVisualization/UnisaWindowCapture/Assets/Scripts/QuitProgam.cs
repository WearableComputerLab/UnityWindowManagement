using UnityEngine;
using System.Collections;

public class QuitProgam : MonoBehaviour
{
    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
#endif
    }
}