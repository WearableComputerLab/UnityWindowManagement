using UnityEngine;

namespace CubeWindow
{
    public class DetectClick : MonoBehaviour
    {
        private void Update()
        {
            // Check for left mouse button click
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform raycast
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object has the correct tag
                    if (hit.collider.gameObject.tag == "ScreenPlane")
                    {
                        // Obtain UV coordinates from the hit point
                        MeshCollider meshCollider = hit.collider as MeshCollider;
                        if (meshCollider == null || meshCollider.sharedMesh == null)
                        {
                            Debug.Log("MeshCollider not found or sharedMesh is null");
                            return;
                        }

                        Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                        Debug.Log("Get the texture coordinate of the mouse:" + pixelUV);

              
                    }
                    else
                    {
                        Debug.Log("Object not the targeted mesh / Vertex threshold exceeded");
                    }
                }
            }
        }
    }
}
