using UnityEngine;
using System.Collections;

public class Variable_Modifier : MonoBehaviour {

	private Mobile_Game_Controller mobile_game_Controller ;
	private Mobile_Player_Controller mobile_player_Controller;
	private BG_Scroller bg_Scroller_Controller;

	public bool Is_Fire_Rate_Boost;
	public bool Is_Score_Multiplayer;
	public bool Is_Time_Warp;
	public bool Is_Repair_Node;
	public bool Is_Charge;

	public int Score_Multiplayer;
	public float Score_Multiplayer_Extension;
	private GUIText Score_Multiplayer_Text;

	public float Fire_Rate_Boost;
	public float Fire_Rate_Boost_Extension;

	public float Time_Change;
	public float Time_Change_Duration;
	
	public float Charge_Duration;
	public float Charge_Speed;


	

	void Start () 
	{
		PlayerPrefs.SetInt ("Charge", 0);

		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			mobile_game_Controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}
		
		if (Game_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'GameController' script");
		}

		GameObject Player_Controller_Object = GameObject.FindWithTag ("Player");
		if (Player_Controller_Object != null) 
		{
			mobile_player_Controller = 	Player_Controller_Object.GetComponent <Mobile_Player_Controller>();
		}
		
		if (Player_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'Player' script");
		}
	

		if (Is_Score_Multiplayer == true) 
		{
			Score_Multiplayer_Text = mobile_game_Controller.Score_Multiplayer_Text;
			
			mobile_game_Controller.score_Multiplayer = mobile_game_Controller.score_Multiplayer + Score_Multiplayer;
			Score_Multiplayer_Text.text = "X" + mobile_game_Controller.score_Multiplayer;
			mobile_game_Controller.Score_Multiplayer_Timer = Time.time + Score_Multiplayer_Extension;
		}

		if (Is_Repair_Node== true) 
		{
			if(mobile_game_Controller.Player_Health < 3)
			{
				mobile_game_Controller.Player_Health += 1;
			}
			else
			{
				mobile_game_Controller.Add_Score(150);
			}
		}


		if (Is_Fire_Rate_Boost == true)
		{
			mobile_player_Controller.Fire_Rate = mobile_player_Controller.Fire_Rate - Fire_Rate_Boost;
			mobile_player_Controller.Fire_Boost_Extension = Time.time + Fire_Rate_Boost_Extension;
		}

		if (Is_Time_Warp == true)
		{
			StartCoroutine(Time_Warp());
		}

		if (Is_Charge == true) 
		{
			StartCoroutine(Charge());
		}
	}


	IEnumerator Time_Warp()
	{
		Time.timeScale = Time_Change;
		yield return new WaitForSeconds(Time_Change_Duration);
		Time.timeScale = 1.0f;

	}

	IEnumerator Charge()
	{
		PlayerPrefs.SetFloat ("Charge", Charge_Speed);
		mobile_player_Controller.Next_Fire += Charge_Duration  + 0.01f;
		yield return new WaitForSeconds(Charge_Duration );
		PlayerPrefs.SetFloat ("Charge_Duration", Charge_Duration);
		PlayerPrefs.SetFloat ("Charge", 0);
		Destroy (gameObject);
	}

}
