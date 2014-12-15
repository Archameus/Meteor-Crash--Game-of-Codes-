using UnityEngine;
using System.Collections;

public class Enemy_Weapon_Controller : MonoBehaviour {

	public GameObject Shot_Spawn;
	public GameObject Shot;
	public float Fire_Rate;
	public float Fire_Delay;

	private float Next_Fire;
	


	void Update()
	{
		if (Time.time > Fire_Delay) 
		{
			if (Time.time > Next_Fire) 
			{
				Next_Fire = Time.time + Fire_Rate;
				Instantiate (Shot, Shot_Spawn.transform.position, Shot_Spawn.transform.rotation);
				if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
			}

		}
	}
}
