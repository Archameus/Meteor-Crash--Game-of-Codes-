using UnityEngine;
using System.Collections;

public class Spawn_After_Death : MonoBehaviour {

	
	private Mobile_Game_Controller game_controller;
	
	void Start()
	{
		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			game_controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}
		
		if (Game_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'Game_Controller' script");
		}

	}
	
	void OnTriggerEnter(Collider Other)
	{
		if (Other.tag == "Bolt") 
		{
			GameObject Spawn_Object = game_controller.Pick_Ups[Random.Range( 0 , game_controller.Pick_Ups.Length)];

			Instantiate (Spawn_Object, transform.position, Quaternion.Euler (0.0f ,0.0f, 0.0f));
			gameObject.collider.enabled = false;
		}
	}
}
