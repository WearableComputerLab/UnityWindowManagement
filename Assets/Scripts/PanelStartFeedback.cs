using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////һ�����ص�����ȴ�t�����ʾ��Invokeʵ��
public class PanelStartFeedback : MonoBehaviour
{
	public int t = 1;

	// Use this for initialization
	public void HidePanel()
	{
		Invoke("ActiveFalse", t);
		
		Invoke("UpAnimation", t);		
		Invoke("DownAnimation", t);
		
	}

	// Update is called once per frame
	public void UpAnimation()
	{
		transform.Translate(0, 250f, 0);		
	}

	public void DownAnimation()
	{
		transform.Translate(0, -250f, 0);
	}

	public void ActiveFalse()
	{
		gameObject.SetActive(false);
	}

	
}