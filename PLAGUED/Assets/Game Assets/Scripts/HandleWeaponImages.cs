using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleWeaponImages : MonoBehaviour 
{
	private Image image;
	private Sprite M4Img;
	private Sprite axeImg;
	private Sprite UMPImg;
	private PlayerManager playerM;
	public Sprite[] images;

	// Use this for initialization
	void Start () 
	{
		image = GetComponent<Image>();
		M4Img = images [0];
		axeImg = images [1];
		UMPImg = images [2];
		playerM = GameObject.Find ("FPSController").GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerM.M4A1_GUN.activeSelf) 
		{
			image.enabled = true;
			image.sprite = M4Img;
		} 
		else if (playerM.AXE.activeSelf) 
		{
			image.enabled = true;
			image.sprite = axeImg;
		} 
		else if (playerM.UMP45.activeSelf) 
		{
			image.enabled = true;
			image.sprite = UMPImg;
		}
		else
			image.enabled = false;
	}
}
