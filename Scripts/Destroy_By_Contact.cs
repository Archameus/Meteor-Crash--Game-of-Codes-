using UnityEngine;
using System.Collections;

public class Destroy_By_Contact : MonoBehaviour 
{
	public GameObject Explosion;
	public GameObject Player_Explosion;
	public int Score_Value;
	private Game_Controller game_Controller;
	private Mobile_Game_Controller mobile_game_Controller;

	public bool Spawn_PickUp_After_Death;
	
	private int Asteroid_Size;
	public bool Is_Asteroid;

	private int Player_Health;

	public GameObject Plasma_Bolt_Child;


	void Start()
	{
		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			game_Controller = Game_Controller_Object.GetComponent <Game_Controller>();
			mobile_game_Controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}

		if (Game_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'GameController' script");
		}

		Asteroid_Size = mobile_game_Controller.Asteroid_Size;

	}

	void Update ()
	{
		Player_Health = mobile_game_Controller.Player_Health;

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") 
		{
			return;
		}
		if (other.tag != "PickUp" || other.tag != "Repulsor"  ) 
		{
			if(Is_Asteroid == false)
			{
				if (other.tag == "PickUp" || other.tag == "Repulsor" ) 
				{
					return ;
				}
				else
				{
					Destroy (gameObject);
					Instantiate(Explosion, transform.position, transform.rotation);

				}
			}

			if(Is_Asteroid == true)
			{
				if (other.tag == "PickUp" || other.tag == "Repulsor" ) 
				{
					return ;
				}

				if (other.tag == "BigBolt" ) 
				{
					Instantiate(Explosion, transform.position, transform.rotation);
					Destroy (gameObject);
					Asteroid_Size-= 1000;
				
				}

				if(Asteroid_Size != 0)
				{
					gameObject.transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
					Instantiate(Explosion, transform.position, transform.rotation);
					Asteroid_Size-= 1;
				}

				if(Asteroid_Size == 0)
				{
					if(Spawn_PickUp_After_Death == true)
					{
						GameObject Spawn_Object = mobile_game_Controller.Pick_Ups[Random.Range( 0 , mobile_game_Controller.Pick_Ups.Length)];
						Instantiate (Spawn_Object, transform.position, Quaternion.Euler (0.0f ,0.0f, 0.0f));
					}
					
					Instantiate(Explosion, transform.position, transform.rotation);
					Destroy (gameObject);
				}

			}
		}
		
		if (other.tag == "Player") 
		{
			if (Player_Health > 1)
			{
				Instantiate (Player_Explosion, other.transform.position, other.transform.rotation);
				mobile_game_Controller.Player_Health -= 1;
				Destroy(gameObject);

			}
			if (Player_Health <= 1)
			{
				Instantiate (Player_Explosion, other.transform.position, other.transform.rotation);
				Destroy(other.gameObject);
				mobile_game_Controller.Game_Over = 1;
				mobile_game_Controller.Player_Health -= 1;
			}
		}
		
		if (other.tag == "Enemy" || other.tag == "Bolt" || other.tag == "Shield" || other.tag == "BigBolt") 
		{
			if (other.tag != "PickUp" || other.tag != "Repulsor"  ) 
			{
				Destroy(other.gameObject);
				game_Controller.Add_Score (Score_Value);
				mobile_game_Controller.Add_Score (Score_Value);
			}
		}

		if(other.tag == "Plasma_Bolt")
		{

			if (other.tag != "PickUp" || other.tag != "Repulsor" || other.tag != "Shield" ) 
			{
				Instantiate (Plasma_Bolt_Child, transform.position, Quaternion.Euler (0.0f ,0.0f, 0.0f));
				Destroy(other.gameObject);
				game_Controller.Add_Score (Score_Value);
				mobile_game_Controller.Add_Score (Score_Value);

			}

		}


		if (gameObject.tag == "Enemy_Bolt" && other.tag == "Repulsor") 
		{
			rigidbody.velocity = transform.forward * 15;
		}



	}
}
