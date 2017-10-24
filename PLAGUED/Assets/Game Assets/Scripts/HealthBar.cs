using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
	private Image healthBar;
	private GameObject player;
	private PlayerManager playerM;
	private float maxHealth;

	// Use this for initialization
	void Start () 
	{
		healthBar = GetComponent<Image>();
		player = GameObject.Find ("FPSController");
		playerM = player.GetComponent<PlayerManager>();
		maxHealth = playerM.maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		healthBar.fillAmount = playerM.health / maxHealth;
	}
}
