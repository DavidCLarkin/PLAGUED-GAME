using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieSound : MonoBehaviour 
{
	public AudioClip[] sounds;

	//public AudioSource randSound;

	//private int randomSound = 0;
	//private float randomTime = 20;
	//private float timer;

	// Use this for initialization
	void Start () 
	{
		callAudio ();
	}

	void getRandomSound()
	{
		if(GetComponent<AudioSource>().isPlaying)	return;
		GetComponent<AudioSource>().clip = sounds [Random.Range (0, sounds.Length)];
		GetComponent<AudioSource>().Play();
		callAudio ();
	}

	void callAudio()
	{
		Invoke ("getRandomSound", Random.Range (1, 10));
	}
}
