using UnityEngine;
using System.Collections;

public class Mobile_Player_Controller : MonoBehaviour {
	/// <summary>
	/// Pick Up ideas: no cooldows on alternative weapons, a charge, a Pulsar.
	/// A Weapon Idea, Laser Burst.
	/// </summary>
	public float tilt;
	public float Speed;

	public GameObject Shot;
	public Transform Shot_Spawn;
	public Transform Lightning_Shot_Spawn;

	public float Fire_Rate;
	private float former_Fire_Rate;
	public float Fire_Boost_Extension;

	public float Next_Fire; // has to be public for other scritps to be able to access it DO NOT EDIT
	private int Path;

	public int Weapon_1_Choice ;
	public float Alt1_Fire_Rate;
	public float Alt1_Next_Fire;// has to be public for other scritps to be able to access it DO NOT EDIT

	public int Weapon_2_Choice ;
	public float Alt2_Fire_Rate;
	public float Alt2_Next_Fire;// has to be public for other scritps to be able to access it DO NOT EDIT
	

	public float Burst_Shot_Fire_Rate;
	private float Burst_Shot_Next_Fire; 
	public float Burst_Shot_Spread;
	public int Burst_Shot_Number_Of_Shots;

	public float Spread_Shot_Fire_Rate;
	private float Spread_Shot_Next_Fire; 
	public float Spread_Shot_Spread;
	public int Spread_Shot_Number_Of_Shots;

	public GameObject Split_Shot;
	public float Split_Shot_Fire_Rate;

	public GameObject Explosive_Shot;
	public float Explosive_Shot_Fire_Rate;

	public GameObject Lightning_strike;
	public GameObject Lightning_strike_VFX;
	public float Lightning_Strike_Rate;

	public GameObject Player_shield;
	public float Player_Shield_CD; // Has to be greater than the Effect Duration, CD - Cooldown
	public float Player_Shield_Effect_Duration;
	private float Player_Shield_Time; 

	public GameObject Blast;
	public float Blast_CD; // Has to be greater than the Effect Duration, CD - Cooldown

	private int Direction;
	private SwipeScript Swipe_Script;
	private float Swipe_Input_Check;

	public float[] Fire_Rate_Upgrade;
	private float Fire_Rate_Reduction;

	public float[] Speed_Upgrade;
	private float Speed_Boost;

	public float[] Alt_Weapons_Fire_Rate_Upgrade;
	private float Front_Weapon_Fire_Rate_Reduction;
	private float Back_Weapon_Fire_Rate_Reduction;

	private int Plasma_Cannon_Upgrade;
	public GameObject Plasma_Bolt;

	public GameObject Tutortial_Movement_Icon;
	public GameObject Tutortial_Weapons_Icon;

	void Start()
	{
		Path = 3;
		former_Fire_Rate = Fire_Rate;
		Weapon_1_Choice = PlayerPrefs.GetInt ("Weapon1_State");
		Weapon_2_Choice = PlayerPrefs.GetInt ("Weapon2_State");



////////////////////////////////////////////////////////////////////////////////////////Swipe Control
		GameObject Swipe_Control_Object = GameObject.FindWithTag ("Player");
		if (Swipe_Control_Object != null) 
		{
			Swipe_Script = Swipe_Control_Object.GetComponent <SwipeScript>();
		}
		
		if (Swipe_Control_Object == null) 
		{
			Debug.Log ("Cant Find 'Swipe Control' script");
		}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	
	}
	
	void Update()
	{
		Upgrade_Check ();
		Tutortial ();

		if(Fire_Boost_Extension < Time.time)
		{
			Fire_Rate = former_Fire_Rate;
		}

		if(Input.GetKey(KeyCode.Q))
		{
			Time.timeScale = 2.0f;
		}

		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				 Application.Quit();	
				return;
			}
		}


		if ( Time.time > Next_Fire ) 
		{
			Next_Fire = Time.time + Fire_Rate - Fire_Rate_Reduction;

			if(Plasma_Cannon_Upgrade ==1)
			{
				Instantiate(Plasma_Bolt, Shot_Spawn.position, Shot_Spawn.rotation);
			}
			else
			{
				Instantiate(Shot, Shot_Spawn.position, Shot_Spawn.rotation);
			}

			if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}

		}

		if (Input.GetButton("Fire2") && Time.time > Alt1_Next_Fire  || Input.GetKey(KeyCode.Z) && Time.time > Alt1_Next_Fire  || Direction == 3 && Time.time > Alt1_Next_Fire ) 
		{


			if(Weapon_1_Choice == 1)
			{
				Alt1_Fire_Rate = Burst_Shot_Fire_Rate * Front_Weapon_Fire_Rate_Reduction;
				StartCoroutine(Burst_Shot());

				Next_Fire += 1.0f;
			}

			if(Weapon_1_Choice == 2)
			{
				Alt1_Fire_Rate = Spread_Shot_Fire_Rate * Front_Weapon_Fire_Rate_Reduction;
				StartCoroutine(Spread_Shot());

				Next_Fire += 1.0f;
			}

			if(Weapon_1_Choice == 3)
			{
				Alt1_Fire_Rate = Player_Shield_CD * Front_Weapon_Fire_Rate_Reduction;
				StartCoroutine(Player_Shield());
				
				Next_Fire += 1.0f;	
			}

			if(Weapon_1_Choice == 4)
			{
				Alt1_Fire_Rate = Split_Shot_Fire_Rate * Front_Weapon_Fire_Rate_Reduction;
				Split_shot();
				
				Next_Fire += 1.0f;	
			}

			if(Weapon_1_Choice == 5)
			{
				Alt1_Fire_Rate = Explosive_Shot_Fire_Rate * Front_Weapon_Fire_Rate_Reduction;
				Explosive_shot();
				
				Next_Fire += 1.0f;	
			}

			if(Weapon_1_Choice == 6)
			{
				Alt1_Fire_Rate = Blast_CD * Front_Weapon_Fire_Rate_Reduction;
				StartCoroutine(blast());	
				
				Next_Fire += 1.0f;
			}

			if(Weapon_1_Choice == 7)
			{
				Alt1_Fire_Rate = Lightning_Strike_Rate * Front_Weapon_Fire_Rate_Reduction;
				Lightning_Strike();	
				
				Next_Fire += 1.0f;
			}

			Alt1_Next_Fire = Time.time + Alt1_Fire_Rate;
			Swipe_Input_Check = Time.time + 0.000001f;
		}

		if (Input.GetKey(KeyCode.X) && Time.time > Alt2_Next_Fire   || Direction == 4 && Time.time > Alt2_Next_Fire ) 
		{
			
			
			if(Weapon_2_Choice == 1)
			{
				Alt2_Fire_Rate = Burst_Shot_Fire_Rate * Back_Weapon_Fire_Rate_Reduction;
				StartCoroutine(Burst_Shot());
				
				Next_Fire += 1.0f;
			}
			
			if(Weapon_2_Choice == 2)
			{
				Alt2_Fire_Rate = Spread_Shot_Fire_Rate * Back_Weapon_Fire_Rate_Reduction;
				StartCoroutine(Spread_Shot());	
				
				Next_Fire += 1.0f;
			}
			
			if(Weapon_2_Choice == 3)
			{
				Alt2_Fire_Rate = Player_Shield_CD * Back_Weapon_Fire_Rate_Reduction;
				StartCoroutine(Player_Shield());	
				
				Next_Fire += 1.0f;
			}
			
			if(Weapon_2_Choice == 4)
			{
				Alt2_Fire_Rate = Split_Shot_Fire_Rate * Back_Weapon_Fire_Rate_Reduction;
				Split_shot();	
				
				Next_Fire += 1.0f;
			}
			
			if(Weapon_2_Choice == 5)
			{
				Alt2_Fire_Rate = Explosive_Shot_Fire_Rate * Back_Weapon_Fire_Rate_Reduction;
				Explosive_shot();	
				
				Next_Fire += 1.0f;
			}
			
			if(Weapon_2_Choice == 6)
			{
				Alt2_Fire_Rate = Blast_CD * Back_Weapon_Fire_Rate_Reduction;
				StartCoroutine(blast());	
				
				Next_Fire += 1.0f;
			}
			
			if(Weapon_2_Choice == 7)
			{
				Alt2_Fire_Rate = Lightning_Strike_Rate * Back_Weapon_Fire_Rate_Reduction;
				Lightning_Strike();	
				
				Next_Fire += 1.0f;
			}
			
			Alt2_Next_Fire = Time.time + Alt2_Fire_Rate * Back_Weapon_Fire_Rate_Reduction;
			Swipe_Input_Check = Time.time + 0.000001f;
		}

		////////////////////////////////////////////////////////////////////////////////////////Swipe Control
		Direction = Swipe_Script.Direction;

		if (Time.time > Swipe_Input_Check) 
		{
			Swipe_Script.Direction = 0;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	}

	void FixedUpdate()
	{


		rigidbody.rotation = Quaternion.Euler ( 0.0f, 0.0f, rigidbody.velocity.x * -tilt);

		switch (Path) 
		{
		case 1:

			if(Input.GetKey (KeyCode.D) || Direction == 1  ) { rigidbody.velocity = new Vector3 (Speed + Speed_Boost, 0.0f, 0.0f); Path = 2; Swipe_Input_Check = Time.time + 0.000001f;  }
			break;

		case 2:

			if(transform.position.x < -4.5f   ) { rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f); transform.position = new Vector3(-4.5f,0.0f,-2.5f); Path = 1; Swipe_Input_Check = Time.time + 0.001f;  }
			if(transform.position.x > -0.1f   ) { rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f); transform.position = new Vector3(0.0f,0.0f,-2.5f); Path = 3; Swipe_Input_Check = Time.time + 0.001f; }


			break;

		case 3:

			if(Input.GetKey (KeyCode.D) || Direction == 1) { rigidbody.velocity = new Vector3 (Speed + Speed_Boost, 0.0f, 0.0f); Path = 4; Swipe_Input_Check = Time.time + 0.000001f; }
			if(Input.GetKey (KeyCode.A) || Direction == 2) { rigidbody.velocity = new Vector3 (-Speed - Speed_Boost, 0.0f, 0.0f); Path = 2; Swipe_Input_Check = Time.time + 0.000001f; }

			break;

		case 4:

			if(transform.position.x > 4.5f   ) { rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f); transform.position = new Vector3(4.5f,0.0f,-2.5f); Path = 5; Swipe_Input_Check = Time.time + 0.001f; }
			if(transform.position.x < 0.1f   ) { rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f); transform.position = new Vector3(0.0f,0.0f,-2.5f); Path = 3; Swipe_Input_Check = Time.time + 0.001f;}

			break;

		case 5:

			if(Input.GetKey (KeyCode.A) || Direction == 2) { rigidbody.velocity = new Vector3 (-Speed - Speed_Boost, 0.0f, 0.0f); Path = 4; Swipe_Input_Check = Time.time + 0.000001f; }

			break;
	    }



	}
	

	IEnumerator Burst_Shot()
	{
		for(int f = 0; f < Burst_Shot_Number_Of_Shots; f++)
		{
			Instantiate(Shot, Shot_Spawn.position, Shot_Spawn.rotation);
			yield return new WaitForSeconds(Burst_Shot_Spread);
			if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
				
		}
	}

	IEnumerator Spread_Shot()
	{
		for(int f = 0; f < Spread_Shot_Number_Of_Shots; f++)
		{
			Instantiate(Shot, Shot_Spawn.position, Shot_Spawn.rotation);
			Instantiate(Shot, Shot_Spawn.position,  Quaternion.Euler(new Vector3(0, 20, 0)));
			Instantiate(Shot, Shot_Spawn.position,  Quaternion.Euler(new Vector3(0, -20, 0)));
			yield return new WaitForSeconds(Spread_Shot_Spread);
			if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
			
		}
	}

	void Split_shot()
	{
		Instantiate(Split_Shot, Shot_Spawn.position, Shot_Spawn.rotation);
		if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
	}

	void Explosive_shot()
	{
		Instantiate(Explosive_Shot, Shot_Spawn.position, Shot_Spawn.rotation);
		if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
	}

	void Lightning_Strike()
	{
		Instantiate(Lightning_strike, Lightning_Shot_Spawn.position, Lightning_Shot_Spawn.rotation);
		Instantiate(Lightning_strike_VFX, Lightning_Shot_Spawn.position, Lightning_Shot_Spawn.rotation);
		if(PlayerPrefs.GetInt("Audio") !=1){ audio.Play ();	}
	}

	IEnumerator blast()
	{
		Instantiate(Blast, transform.position, Shot_Spawn.rotation);
		yield return new WaitForSeconds(0.1f);
		Instantiate(Blast, transform.position, Shot_Spawn.rotation);
	}

	IEnumerator Player_Shield()
	{
		Player_shield.SetActive(true);
		yield return new WaitForSeconds(Player_Shield_Effect_Duration);

		Player_shield.transform.position = new Vector3 (10.0f, 0.0f, 0.0f);
		Player_shield.SetActive(false);

	}

	void Upgrade_Check()
	{
		switch (PlayerPrefs.GetInt("Fire_Rate_Upgrade")) 
		{
		case 1:
			Fire_Rate_Reduction = Fire_Rate_Upgrade[0];
			break;
			
		case 2:
			Fire_Rate_Reduction = Fire_Rate_Upgrade[1];
			break;
			
		case 3:
			Fire_Rate_Reduction = Fire_Rate_Upgrade[2];
			break;
			
		default :
			Fire_Rate_Reduction = 0;
			break;
		}

		switch (PlayerPrefs.GetInt("Speed_Upgrade")) 
		{
		case 1:
			Speed_Boost = Speed_Upgrade[0];
			break;
			
		case 2:
			Speed_Boost = Speed_Upgrade[1];
			break;
			
		case 3:
			Speed_Boost = Speed_Upgrade[2];
			break;
			
		default :
			Speed_Boost = 0;
			break;
		}

		switch (PlayerPrefs.GetInt("Front_Weapon_Upgrade")) 
		{
		case 1:
			Front_Weapon_Fire_Rate_Reduction = Alt_Weapons_Fire_Rate_Upgrade[0];
			break;
			
		case 2:
			Front_Weapon_Fire_Rate_Reduction = Alt_Weapons_Fire_Rate_Upgrade[1];
			break;
			
		case 3:
			Front_Weapon_Fire_Rate_Reduction = Alt_Weapons_Fire_Rate_Upgrade[2];
			break;
			
		default :
			Front_Weapon_Fire_Rate_Reduction = 1;
			break;
		}

		switch (PlayerPrefs.GetInt("Back_Weapon_Upgrade")) 
		{
		case 1:
			Back_Weapon_Fire_Rate_Reduction = Alt_Weapons_Fire_Rate_Upgrade[0];
			break;
			
		case 2:
			Back_Weapon_Fire_Rate_Reduction = Alt_Weapons_Fire_Rate_Upgrade[1];
			break;
			
		case 3:
			Back_Weapon_Fire_Rate_Reduction = Alt_Weapons_Fire_Rate_Upgrade[2];
			break;
			
		default :
			Back_Weapon_Fire_Rate_Reduction = 1;
			break;
		}

		Plasma_Cannon_Upgrade = PlayerPrefs.GetInt ("Plasma_Cannons_Upgrade");

	}

	void Tutortial()
	{
		if (PlayerPrefs.GetInt ("Movement_Tutortial") != 1) 
		{
			if( Path == 3)
			{
				Tutortial_Movement_Icon.SetActive(true);
			}

			if( Path != 3)
			{
				Tutortial_Movement_Icon.SetActive(false);
				PlayerPrefs.SetInt("Movement_Tutortial", 1);
			}


		}

		if (PlayerPrefs.GetInt ("Weapons_Tutortial") != 1) 
		{
			if(PlayerPrefs.GetInt("Weapon_Active") == 1)
			{
				if(Direction !=3 && Direction != 4)
				{
					Tutortial_Weapons_Icon.SetActive(true);
				}
				
				if(Direction ==3 || Direction == 4)
				{
					Tutortial_Weapons_Icon.SetActive(false);
					PlayerPrefs.SetInt("Weapon_Active", 2);
				}

			}
		}

	}


}
