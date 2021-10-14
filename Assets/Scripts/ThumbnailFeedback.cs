using UnityEngine;
using UnityEngine.EventSystems;
public class ThumbnailFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    
    //�����ͣ�Ŵ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3((float)1.12, (float)1.12, (float)1.11);
        //Debug.Log("on");
    }
    //����뿪��С
    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        //Debug.Log("off");
    }
}
