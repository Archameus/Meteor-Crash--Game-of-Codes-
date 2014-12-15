using UnityEngine;
using System.Collections;

public class Mobile_Game_Controller : MonoBehaviour 
{
	public GameObject[] Hazards;
	public GameObject[] Pick_Ups;

	public Vector3 Spawn_Values;
	public float[] Path_Choice; // defining an array
	public int Hazard_Count;
	public int Wave_Increase;
	public int Spawn_Wait_Decrase;
	public float Spawn_Wait;
	public float Start_Wait;
	public float Wave_Wait;

	public int Asteroid_Size; // Must be greater than 1

	public float Hazard_Speed;
	private int Wave_Counter;
	public int[] Difficulty_Points;

	public GUIText Score_Text;
	public GUIText Score_Multiplayer_Text;
	private int Score;

	public GUIText Restart_Text;
	public GUIText Game_Over_Text;
	public GUIText Game_Over_Score_Text;
	public GUIText Game_Over_Currency_Text;
	public GUIText Wave_Counter_Text;
	public int Game_Over;
	private int Text_Clear;
	public int Shield_State;
	public int Repulsor_Shield_State; // not used currently

	private int Pause_Control;

	public int score_Multiplayer;
	public float Score_Multiplayer_Timer;

	private Menu_Control menu_Control ;
	private UI_Controller UI_Control ;

	public int Player_Health;
	private int Former_Currency;
	private int New_Currency;

	private bool Game_Over_Once ;

	public GameObject Upgrade_Shield;

	private string[] Gameplay_Text = new string[85];
	private string[] Gameplay_Text_Eng = new string[85];
	private string[] Gameplay_Text_Pl = new string[85];



	void Start()
	{
		Language_Set ();

		Game_Over = 0;
		Restart_Text.text = "";
		Game_Over_Text.text = "";
		Score = 0;
		Shield_State = 1;
		Repulsor_Shield_State = 1; // not used currently
		Upgrade_Check ();
		Update_Score ();
		StartCoroutine (Spawn_Waves ());
		Text_Clear = 0;

		Pause_Control = 0;
		score_Multiplayer = 1;
		Former_Currency = PlayerPrefs.GetInt ("Currency");

		Game_Over_Once = false;

		Wave_Counter = 0;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Gain acces to Menu Control
		GameObject Menu_Controller_Object = GameObject.FindWithTag ("Menu_Control");
		if (Menu_Controller_Object != null) 
		{
			menu_Control = Menu_Controller_Object.GetComponent <Menu_Control>();

		}
		
		if (Menu_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'Menu_Control' script");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Gain acces to UI Controller
		GameObject UI_Controller_Object = GameObject.FindWithTag ("UI");
		if (Menu_Controller_Object != null) 
		{
			UI_Control = UI_Controller_Object.GetComponent <UI_Controller>();
			
		}
		
		if (Menu_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'UI_Controller' script");
		}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	}

	void Update()
	{
		if (Text_Clear == 1) 
		{
			Score_Text.text = "";
			UI_Control.Weapon1_Text.text = "";
			UI_Control.Weapon1_State.text = "";
			UI_Control.Weapon2_Text.text = "";
			UI_Control.Weapon2_State.text = "";
		}

		if (Input.GetKeyDown (KeyCode.Escape) && Pause_Control == 0) 
		{
			Pause_Control = 1;
			Time.timeScale = 0.0f;
			audio.mute = true;
		}

		else if (Input.GetKeyDown (KeyCode.Escape) && Pause_Control == 1) 
		{
			Pause_Control = 0;
			Time.timeScale = 1.0f;
			audio.mute = false;
		}

		if (score_Multiplayer > 1) 
		{
			if(Score_Multiplayer_Timer < Time.time)
			{
				score_Multiplayer = 1;
				Score_Multiplayer_Text.text=("");
			}

		}

		if (Game_Over == 1) 
		{


			if( Game_Over_Once == false)
			{
				StartCoroutine(GameOver());
				Game_Over_Once = true;
			}

		}

	}

	IEnumerator Spawn_Waves()
	{
		yield return new WaitForSeconds(Start_Wait);

		while(true)
		{

			for (int i =0;i < Hazard_Count; i++)
			{

				GameObject Hazard = Hazards[Random.Range(0, Hazards.Length)];
				GameObject Pick_Up = Pick_Ups[Random.Range(0, Pick_Ups.Length)];

				Vector3 Spawn_Position = new Vector3 (Path_Choice[Random.Range(0, Path_Choice.Length)],Spawn_Values.y,Spawn_Values.z);
				Quaternion Spawn_Rotation = Quaternion.identity;
				
				Instantiate (Hazard, Spawn_Position, Spawn_Rotation);

				yield return new WaitForSeconds(Spawn_Wait);


				if( i == 3  ||  i == Random.Range(Hazard_Count - 5,Hazard_Count) ) // || i == Hazard_Count / 2
				{
					Instantiate (Pick_Up, Spawn_Position, Spawn_Rotation);
				}
			}

			yield return new WaitForSeconds(Wave_Wait);

			Wave_Counter ++;
			Hazard_Count += Wave_Increase;
			Spawn_Wait -= Spawn_Wait_Decrase;

			if(Wave_Counter < Difficulty_Points[0])
			{
				Hazard_Speed += 0.1F;
				Debug.Log("Difficulty 1");
			}

			else if(Wave_Counter < Difficulty_Points[1])
			{
				Hazard_Speed += 0.3F;
				Debug.Log("Difficulty 2");
			}

			else if(Wave_Counter < Difficulty_Points[2])
			{
				Hazard_Speed += 0.5F;
				Debug.Log("Difficulty 3");
			}	

			else if(Wave_Counter > Difficulty_Points[2])
			{
				Hazard_Speed += 0.5F;
				Asteroid_Size ++;
				Debug.Log("Difficulty 3");
			}	

		}



	}
	
	void Update_Score()
	{
		Score_Text.text = Gameplay_Text[1] + Score;
	}

	public void Add_Score(int New_Score_Value)
	{
		Score += New_Score_Value * score_Multiplayer;
		Update_Score();
	}

	public IEnumerator GameOver()
	{
		Text_Clear = 1;

		yield return new WaitForSeconds(0.6f);

		Game_Over_Text.text = Gameplay_Text[0];
		yield return new WaitForSeconds(0.6f);

		Wave_Counter_Text.text = Gameplay_Text [3] + Wave_Counter;
		yield return new WaitForSeconds(0.6f);

		Game_Over_Score_Text.text = Gameplay_Text[1] + Score;
		yield return new WaitForSeconds(0.6f);

		Game_Over_Currency_Text.text = Gameplay_Text[2] + Former_Currency ;
		yield return new WaitForSeconds(0.6f);

		New_Currency = Former_Currency + Score / 10;
		Game_Over_Currency_Text.text = Gameplay_Text[2] + Former_Currency + " + "  + (New_Currency - Former_Currency) ;
		yield return new WaitForSeconds(0.4f);

		Game_Over_Currency_Text.text = Gameplay_Text[2] + New_Currency ;
		PlayerPrefs.SetInt ("Currency", New_Currency );
		yield return new WaitForSeconds(0.4f);

		menu_Control.Menu_State = 100;
	}

	void Upgrade_Check ()
	{
		switch (PlayerPrefs.GetInt ("Health_Upgrade")) 
		{
		case 1:
			Player_Health+= 1;
			break;

		case 2:
			Player_Health+= 2;
			break;

		case 3:
			Player_Health+= 3;
			break;

		default :

			break;
		}

		if(PlayerPrefs.GetInt("Shield_Pack_Upgrade") == 1)
		{
			Upgrade_Shield.SetActive(true);

		}

	}

	void Language_Set()
	{
		Gameplay_Text_Eng [0] = "Game Over"; Gameplay_Text_Pl [0] = "Koniec Gry";
		Gameplay_Text_Eng [1] = "Score: "; Gameplay_Text_Pl [1] = "Punkty: ";
		Gameplay_Text_Eng [2] = "Currency : "; Gameplay_Text_Pl [2] = "Waluta : ";
		Gameplay_Text_Eng [3] = "Waves Survived : "; Gameplay_Text_Pl [3] = " Fale przetrwane : ";

		if (PlayerPrefs.GetInt ("Polski") != 1) 
		{
			for(int x = 0; x <= 3 ; x++)
			{
				Gameplay_Text[x] = Gameplay_Text_Eng[x];
			}
	
		} 
		else 
		{
			for(int x = 0; x <= 3 ; x++)
			{
				Gameplay_Text[x] = Gameplay_Text_Pl[x];
			}

		}

	}


	


}