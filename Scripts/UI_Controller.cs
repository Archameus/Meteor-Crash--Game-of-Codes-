using UnityEngine;
using System.Collections;

public class UI_Controller : MonoBehaviour {

	public GUIText Weapon1_Text;
	public GUIText Weapon1_State;
	public GUIText Weapon2_Text;
	public GUIText Weapon2_State;
	
	private Mobile_Player_Controller mobile_player_Controller;
	private Mobile_Game_Controller mobile_game_Controller;
	private string[] Weapon_Name = new string[9];

	public GameObject []HP_Bar ;
	private int Current_HP;
	
	void Start()
	{

		if (PlayerPrefs.GetInt ("Polski") != 1) 
		{
			Weapon_Name [0] = "";
			Weapon_Name [1] = "Burst\nShot";
			Weapon_Name [2] = "Spread\nShot";
			Weapon_Name [3] = "Energy\nShield";
			Weapon_Name [4] = "Split\nShot";
			Weapon_Name [5] = "Explosive\nShot";
			Weapon_Name [6] = "Plasma\nBlast";
			Weapon_Name [7] = "Lightning\nStrike";
			Weapon_Name [8] = "Time\nWarp";
		}

		else
		{
			Weapon_Name [0] = "";
			Weapon_Name [1] = "Seria";
			Weapon_Name [2] = "Rozlamany\nPocisk";
			Weapon_Name [3] = "Tarcza\nEnergetyczna";
			Weapon_Name [4] = "Podzielony\nPocis";
			Weapon_Name [5] = "Wybuchajacy\nPocisk";
			Weapon_Name [6] = "Fala\nPlasmy";
			Weapon_Name [7] = "Uderzenie\nBlyskawicy";
			Weapon_Name [8] = "Time\nWarp";
		}


		Weapon1_Text.text = Weapon_Name[PlayerPrefs.GetInt("Weapon1_State")];

		Weapon2_Text.text = Weapon_Name[PlayerPrefs.GetInt("Weapon2_State")];


		GameObject Player_Controller_Object = GameObject.FindWithTag ("Player");
		if (Player_Controller_Object != null) 
		{
			mobile_player_Controller = 	Player_Controller_Object.GetComponent <Mobile_Player_Controller>();
		}
		
		if (Player_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'Player' script");
		}

		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			mobile_game_Controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}

	}

	void Update ()
	{

		if (PlayerPrefs.GetInt ("Weapon1_State") != 0) 
		{
			if (mobile_player_Controller.Alt1_Next_Fire < Time.time) {
				Weapon1_State.color = Color.green;
				if (PlayerPrefs.GetInt ("Polski") != 1) {
					Weapon1_State.text = "Ready";
				} else {
					Weapon1_State.text = "Gotowy";
				}

			} else {
				Weapon1_State.color = Color.red;
				if (PlayerPrefs.GetInt ("Polski") != 1) {
					Weapon1_State.text = "Charging";
				} else {
					Weapon1_State.text = "Ladowanie";
				}
			}
		}

		if (PlayerPrefs.GetInt ("Weapon2_State") != 0) 
		{
			if (mobile_player_Controller.Alt2_Next_Fire < Time.time) {
				Weapon2_State.color = Color.green;
				if (PlayerPrefs.GetInt ("Polski") != 1) {
					Weapon2_State.text = "Ready";
				} else {
					Weapon2_State.text = "Gotowy";
				}
				
			} else {
				
				Weapon2_State.color = Color.red;
				Weapon2_State.color = Color.red;
				if (PlayerPrefs.GetInt ("Polski") != 1) {
					Weapon2_State.text = "Charging";
				} else {
					Weapon2_State.text = "Ladowanie";
				}
			}
		}

		Current_HP = mobile_game_Controller.Player_Health;

		if (Current_HP <  1) 
		{
			HP_Bar[0].SetActive(false);
		}
		else
		{
			HP_Bar[0].SetActive(true);
		}

		if(Current_HP < 2)
		{
			HP_Bar[1].SetActive(false);
		}
		else
		{
			HP_Bar[1].SetActive(true);
		}

		if(Current_HP < 3)
		{
			HP_Bar[2].SetActive(false);
		}
		else
		{
			HP_Bar[2].SetActive(true);
		}

		if(Current_HP < 4)
		{
			HP_Bar[3].SetActive(false);
		}
		else
		{
			HP_Bar[3].SetActive(true);
		}

		if(Current_HP < 5)
		{
			HP_Bar[4].SetActive(false);
		}
		else
		{
			HP_Bar[4].SetActive(true);
		}

		if(Current_HP < 6)
		{
			HP_Bar[5].SetActive(false);
		}
		else
		{
			HP_Bar[5].SetActive(true);
		}

	}


}
