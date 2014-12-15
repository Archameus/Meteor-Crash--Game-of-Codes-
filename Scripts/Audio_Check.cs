using UnityEngine;
using System.Collections;

public class Audio_Check : MonoBehaviour {
	
	void Start () 
	{
		if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
	
	}

}
