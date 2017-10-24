using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBar : MonoBehaviour 
{
	private Image soundBar;
	private float maxSound = 5f;
	private PlayerManager playerM;

	// Use this for initialization
	void Start () 
	{
		soundBar = GetComponent<Image> ();
		playerM = GameObject.Find ("FPSController").GetComponent<PlayerManager> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		soundBar.fillAmount = playerM.soundLevel / maxSound;
		if (soundBar.fillAmount >= 1)
			soundBar.color = Color.red;
		else
			soundBar.color = Color.white;
	}
}
