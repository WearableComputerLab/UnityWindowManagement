using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////一个隐藏的物体等待t秒后显示，Invoke实现
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