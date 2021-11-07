using System.Collections;
using UnityEngine;

public class MoveUwcWindows : MonoBehaviour
{

    Vector3 objPos;
    Vector3 offset;

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        StartCoroutine(OnMouseDown());

    }


    IEnumerator OnMouseDown()
    {
   
        objPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 900);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        offset = transform.position - mousePos;

        
        while (Input.GetMouseButton(0))
        {
            //Debug.Log("test");
            Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 900);
            curMousePos = Camera.main.ScreenToWorldPoint(curMousePos);
            

            transform.position = curMousePos + offset;
            yield return new WaitForFixedUpdate();

            if (Input.GetMouseButton(1))
            {
                Destroy(gameObject);
            }
        }
    }
}
