using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropTest : MonoBehaviour
{
    public TextureChange texChange;
    public CreateNewMeshCtrl crtNewMesh;
    private void Start()
    {
        crtNewMesh = GetComponent<CreateNewMeshCtrl>();
        texChange = GetComponent<TextureChange>();
    }
    private void Update()
    {
        if (texChange.clicked && Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}
