using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
	public Text tooltip;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData != null) 
		{
			string gunName = eventData.pointerCurrentRaycast.gameObject.GetComponentInChildren<Image> ().sprite.ToString ().Substring (0, 3);

			if (!gunName.Contains ("nul"))
				tooltip.text = gunName;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.text = "";
	}
}
