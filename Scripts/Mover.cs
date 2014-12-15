using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	private Mobile_Game_Controller mobile_game_Controller;
	public float Shot_Speed;
	public bool Is_Shot;
	void Start()
	{
		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			mobile_game_Controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}

		if (Is_Shot == true) {

			rigidbody.velocity = transform.forward * Shot_Speed;

				} else {
			rigidbody.velocity = transform.forward * (mobile_game_Controller.Hazard_Speed * -1);
				}
	}


	


}
