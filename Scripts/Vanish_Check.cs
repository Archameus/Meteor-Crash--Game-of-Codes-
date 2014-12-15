using UnityEngine;
using System.Collections;

public class Vanish_Check : MonoBehaviour {

	public GameObject Player;
	public Mobile_Game_Controller mobile_game_Controller;

	void Start () 
	{
		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			mobile_game_Controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}
		
		if (Game_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'GameController' script");
		}
	
	}
	

	void Update () 
	{
		GameObject Player_Controller_Object = GameObject.FindWithTag ("Player");
		if (Player_Controller_Object != null) 
		{
			transform.position = Player_Controller_Object.transform.position;
		}
		
		if (Player_Controller_Object == null) 
		{
			if(mobile_game_Controller.Player_Health > 1)
			{
				Debug.Log("Player lost");
				Instantiate(Player, transform.position, transform.rotation);
			}

		}
	
	}
}
