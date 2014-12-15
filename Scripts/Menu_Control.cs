using UnityEngine;
using System.Collections;

public class Menu_Control : MonoBehaviour {


	public GameObject Gameplay_elements;

	public GUIText Customize_label ;
	public GameObject Dummy_Ship;
	public GameObject Dummy_Ship2;
	public GameObject Player_Ship; 
	public GameObject Meteor_Text;
	public GameObject Crash_Text;
	public GUIText Weapon_Info ;
	public GUIText Weapon_Description ;
	public GUIText Weapon_Cost;
	public GUIText Weapon_Affordability;
	public GUIText Current_Currency_Text;

	public int Menu_State; // 0 = gameplay, 1-7 = main menu , 100- Game Over Screen //Public to allow other scripts to acces, do not edit
	private int Weapon_1_Hover;
	private int Hover;

	private int Currect_Currency;
	public int[] weapon_cost;

	Mobile_Game_Controller mobile_game_Controller;
	
	public Material[] Player_Skins;
	public GameObject[] Upgrade_Icons;
	public int[] Skin_Cost;
	public int[] Health_Upgrade_Cost;
	public int[] Fire_Rate_Upgrade_Cost;
	public int[] Speed_Upgrade_Cost;
	public int[] Front_Weapon_Upgrade_Cost;
	public int[] Back_Weapon_Upgrade_Cost;
	public int Shield_Pack_Upgrade_Cost;
	public int Plasma_Cannons_Upgrade_Cost;

	public Material[] Menu_Icons;
	public GameObject Menu_Icon;
	public GameObject Weapon1_Icon;
	public GameObject Weapon2_Icon;
	public GameObject Weapon1_Choice_Icon;
	public GameObject Weapon2_Choice_Icon;

	public GUISkin Menu_Style;

	private string[] Menu_Text = new string[90];
	private string[] Menu_Text_Eng = new string[90];
	private string[] Menu_Text_Pl = new string[90];

	public Texture Quit_Icon;
	public Texture[] Audio_Icons;

	public GameObject Menu_Music;




	void Start () 
	{
		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			mobile_game_Controller = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}

		Menu_Music.SetActive (true);

		Gameplay_elements.SetActive (false);
		Menu_State = 1;

		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs.SetInt ("Currency",2200);
		Currect_Currency = PlayerPrefs.GetInt ("Currency");


		if(PlayerPrefs.GetInt("Replay") == 1 )
	    {
			Customize_label.text = "";
			Weapon_Info.text = "";
			Weapon_Description.text = "" ;

			Gameplay_elements.SetActive(true);
			Time.timeScale = 1.0f;
			Menu_State = 0;
			Menu_Music.SetActive (false);

			if (PlayerPrefs.GetInt ("Audio") != 1) 
			{
				mobile_game_Controller.audio.Play();
			}

			PlayerPrefs.SetInt("Replay", 0);
		}


		Dummy_Ship.SetActive(false); 
		Dummy_Ship2.SetActive(false); 

		Upgrade_Icons[0].SetActive(false);
		Upgrade_Icons[1].SetActive(false);
		Upgrade_Icons[2].SetActive(false);

		if (PlayerPrefs.GetInt("Weapon_Active") == 1) 
		{
			Weapon1_Choice_Icon.renderer.material = Menu_Icons [PlayerPrefs.GetInt ("Weapon1_State")-1];
			Weapon2_Choice_Icon.renderer.material = Menu_Icons [PlayerPrefs.GetInt ("Weapon2_State")-1];
		}

		

		Skin_Assign (); /// Assings currently equipped skin to all ships, 2 dummy ships in the menu and the gameplay ship
		Set_Language ();

		if (PlayerPrefs.GetInt ("Audio") != 1) 
		{
			Menu_Music.audio.Play();
		}

	}

	void OnGUI()
	{
		GUI.skin = Menu_Style;

		if (Menu_State == 100) ///////////////////////////////////////////////////////////////////////////////////////////////////// Game Over
		{

			if(GUI.Button(new Rect(Screen.width/2 - 200 , Screen.height/2 + 80, 160,120), Menu_Text[0]))
			{
				Application.LoadLevel("Tutortial_Scene");
			}
			
			if(GUI.Button(new Rect(Screen.width/2 +40, Screen.height/2 + 80, 160,120), Menu_Text[1]))
			{
				PlayerPrefs.SetInt("Replay", 1);
				Application.LoadLevel("Tutortial_Scene");
			}

		}

		if (Menu_State == 0) /////////////////////////////////////////////////////////////////////////////////////////////////////////// Gameplay
		{
			
			if(GUI.Button(new Rect(Screen.width - 105 , 5, 80, 70), Menu_Text[2]))
			{
				Time.timeScale = 0.0f;
				Menu_State = 101;
				mobile_game_Controller.audio.mute = true;
			}
		}	

		if (Menu_State == 101) //////////////////////////////////////////////////////////////////////////////////////////////////// Pause
		{
			
			if(GUI.Button(new Rect(Screen.width/2 - 220 , Screen.height/2 , 160,120), Menu_Text[3]))
			{
				Time.timeScale = 1.0f;
				Menu_State = 0;

				if (PlayerPrefs.GetInt ("Audio") != 1) 
				{
					mobile_game_Controller.audio.mute = false;
				}
			}
			
			if(GUI.Button(new Rect(Screen.width/2 +50, Screen.height/2 , 160,120), Menu_Text[4]))
			{
				Application.LoadLevel("Tutortial_Scene");
				Time.timeScale = 1.0f;
			}

			if(PlayerPrefs.GetInt("Audio") !=1)
			{
				if(GUI.Button(new Rect(Screen.width/2-40 , Screen.height/2-80 , 80,80), Audio_Icons[0]))
				{
					PlayerPrefs.SetInt("Audio", 1);
				}
			}

			else if(PlayerPrefs.GetInt("Audio") ==1)
			{
				if(GUI.Button(new Rect(Screen.width/2 -40 , Screen.height/2-80 , 80,80), Audio_Icons[1]))
				{
					PlayerPrefs.DeleteKey("Audio");
					mobile_game_Controller.audio.Play ();
				}
			}


		}	

		if (Menu_State == 1) /////////////////////////////////////////////////////////////////////////////////////////////// Main Menu
		{
			Dummy_Ship.SetActive(false); 
			Dummy_Ship2.SetActive(false); 

			Meteor_Text.SetActive(true);
			Crash_Text.SetActive(true);

			Menu_Icon.SetActive(false);
			Weapon1_Icon.SetActive(false);
			Weapon2_Icon.SetActive(false);

			Customize_label.text = "";
			Weapon_Info.text = "";
			Weapon_Description.text = "" ;

			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 -200, 280, 160), Menu_Text[5]))
			{
				Gameplay_elements.SetActive(true);
				Menu_Music.SetActive (false);
				Meteor_Text.SetActive(false);
				Crash_Text.SetActive(false);

				if (PlayerPrefs.GetInt ("Audio") != 1) 
				{
					mobile_game_Controller.audio.Play ();
				}
				Time.timeScale = 1.0f;
				Menu_State = 0;
			}

			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 -20, 280, 160), Menu_Text[6]))
			{
				Meteor_Text.SetActive(false);
				Crash_Text.SetActive(false);

				Menu_State = 2;
			}

			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 +160 , 280, 160), Menu_Text[7]))
			{
				Meteor_Text.SetActive(false);
				Crash_Text.SetActive(false);
				
				Menu_State = 7;
			}

			if(GUI.Button(new Rect(Screen.width/2 +185, Screen.height/2 -435, 60,60), Quit_Icon))
			{
				Menu_State = 8;
				Meteor_Text.SetActive(false);
				Crash_Text.SetActive(false);
			}

		}

		if (Menu_State == 8) ////////////////////////////////////////////////////////////////////////////////////// Quit Question
		{
			Customize_label.text = Menu_Text[69];

			if(GUI.Button(new Rect(Screen.width/2 - 220 , Screen.height/2 , 160,120), Menu_Text[71]))
			{
				Application.Quit();
			}
			
			if(GUI.Button(new Rect(Screen.width/2 +50, Screen.height/2 , 160,120), Menu_Text[73]))
			{
				Menu_State = 1;
				Customize_label.text = "";
			}

		}

		if (Menu_State == 2) //////////////////////////////////////////////////////////////////////////////////////Customization Screen
		{
			Customize_label.text = Menu_Text[8];
			Current_Currency_Text.text = Menu_Text[9] + Currect_Currency.ToString();
			Dummy_Ship.SetActive(true);  
			Weapon_Info.text = "";
			Weapon_Description.text = "" ;
			Weapon_Affordability.text = "";
			Weapon_Cost.text = "";

			if(PlayerPrefs.GetInt("Weapon1_State") != 0)
			{
				Weapon1_Icon.SetActive(true);
				Weapon1_Icon.renderer.material = Menu_Icons[PlayerPrefs.GetInt("Weapon1_State")-1];
			}
			if(PlayerPrefs.GetInt("Weapon2_State") != 0)
			{
				Weapon2_Icon.SetActive(true);
				Weapon2_Icon.renderer.material = Menu_Icons[PlayerPrefs.GetInt("Weapon2_State")-1];
			}

			if(GUI.Button(new Rect(Screen.width/2-75  , Screen.height/2 - 140, 150, 150), Menu_Text[10]))
			{
				Menu_State = 3;
				Dummy_Ship.SetActive(false); 
				Customize_label.text = "";
			}


			if(GUI.Button(new Rect(Screen.width/2 -230  , Screen.height/2 + 120 , 150, 150), Menu_Text[12]))
			{
				Menu_State = 5;
				Dummy_Ship.SetActive(false); 
				Customize_label.text = "";
			}

			if(GUI.Button(new Rect(Screen.width/2 +80 , Screen.height/2 + 120 , 150, 150), Menu_Text[13]))
			{
				Menu_State = 6;
				Customize_label.text = "";
			}

			if(GUI.Button(new Rect(Screen.width/2 -235, Screen.height/2 +300, 180,120 ), Menu_Text[14]))
			{
				Menu_State = 1;
				Dummy_Ship.SetActive(false); 
				Customize_label.text = "";
				Current_Currency_Text.text = "";

				Weapon1_Icon.SetActive(false);
				Weapon2_Icon.SetActive(false);
			}

		}

		if (Menu_State == 3) ////////////////////////////////////////////////////////////////////////////////////Choosing alt weapons
		{
			Customize_label.text = Menu_Text[15];
			Weapon1_Icon.SetActive(false);
			Weapon2_Icon.SetActive(false);

			if(GUI.Button(new Rect(Screen.width/2 - 240 , Screen.height/2 -270, 110,110), Menu_Text[16]))
			{
				Hover =1;
				Weapon_1_Hover = 1;
				Weapon_Info.text = Menu_Text[17];
				Weapon_Description.text = Menu_Text[18] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[0];

				if(PlayerPrefs.GetInt("Burst_Shot_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
			    else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
			}

			if(GUI.Button(new Rect(Screen.width/2 - 120 , Screen.height/2 - 270 , 110,110), Menu_Text[19]))
			{
				Hover =2;
				Weapon_1_Hover = 2;
				Weapon_Info.text = Menu_Text[20];
				Weapon_Description.text = Menu_Text[21] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[1];

				if(PlayerPrefs.GetInt("Spread_Shot_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
				else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
				
			}

			if(GUI.Button(new Rect(Screen.width/2   , Screen.height/2 - 270 , 110,110), Menu_Text[22]))
			{
				Hover =3;
				Weapon_1_Hover = 3;
				Weapon_Info.text = Menu_Text[23];
				Weapon_Description.text = Menu_Text[24] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[2];

				if(PlayerPrefs.GetInt("Energy_Shield_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
				else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
				
			}

			if(GUI.Button(new Rect(Screen.width/2 + 120 , Screen.height/2 - 270 , 110,110), Menu_Text[25]))
			{
				Hover =4;
				Weapon_1_Hover = 4;
				Weapon_Info.text = Menu_Text[26];
				Weapon_Description.text = Menu_Text[27] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[3];

				if(PlayerPrefs.GetInt("Split_Shot_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
				else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
				
			}

			if(GUI.Button(new Rect(Screen.width/2 - 240 , Screen.height/2 - 150 , 110,110), Menu_Text[28]))
			{
				Hover =5;
				Weapon_1_Hover = 5;
				Weapon_Info.text = Menu_Text[29];
				Weapon_Description.text = Menu_Text[30] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[4];

				if(PlayerPrefs.GetInt("Explosive_Shot_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
				else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
				
			}

			if(GUI.Button(new Rect(Screen.width/2 - 120 , Screen.height/2 - 150 , 110,110), Menu_Text[31]))
			{
				Hover =6;
				Weapon_1_Hover = 6;
				Weapon_Info.text = Menu_Text[32];
				Weapon_Description.text = Menu_Text[33] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[5];

				if(PlayerPrefs.GetInt("Plasma_Burst_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
				else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
				
			}

			if(GUI.Button(new Rect(Screen.width/2  , Screen.height/2 - 150 , 110,110), Menu_Text[34]))
			{
				Hover =7;
				Weapon_1_Hover = 7;
				Weapon_Info.text = Menu_Text[35];
				Weapon_Description.text = Menu_Text[36] ;

				Menu_Icon.SetActive(true);
				Menu_Icon.renderer.material = Menu_Icons[6];

				if(PlayerPrefs.GetInt("Lightning_Strike_Unlock") == 1) { Weapon1_Choice_Icon.SetActive(true); Weapon2_Choice_Icon.SetActive(true); }
				else{ Weapon1_Choice_Icon.SetActive(false); Weapon2_Choice_Icon.SetActive(false);}
				
			}

			if(GUI.Button(new Rect(Screen.width/2 -235, Screen.height/2 +300, 180,120 ), Menu_Text[37]))
			{
				Menu_State = 2;	
				Hover = 0;
				Weapon_Cost.text = "" ;
				Weapon_Affordability.text = "";

				Menu_Icon.SetActive(false);
				Weapon1_Choice_Icon.SetActive(false);
				Weapon2_Choice_Icon.SetActive(false);
			}

			Weapon_Unlock_Check(1);

			
		}


		if (Menu_State == 5) /////////////////////////////////////////////////////////////////////////////////////// Upgrades
		{
			Weapon1_Icon.SetActive(false);
			Weapon2_Icon.SetActive(false);

			if(GUI.Button(new Rect(Screen.width/2 - 240 , Screen.height/2 -270, 110,110), Menu_Text[39]))
			{
				Hover =31;
				Weapon_Info.text = Menu_Text[40];
				Weapon_Description.text = Menu_Text[41] ;
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 120 , Screen.height/2 - 270 , 110,110), Menu_Text[42]))
			{
				Hover =32;
				Weapon_Info.text = Menu_Text[43];
				Weapon_Description.text = Menu_Text[44];
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2   , Screen.height/2 - 270 , 110,110), Menu_Text[45]))
			{
				Hover =33;
				Weapon_Info.text = Menu_Text[46];
				Weapon_Description.text = Menu_Text[47];
				
			}

			if(GUI.Button(new Rect(Screen.width/2 + 120 , Screen.height/2 - 270 , 110,110), Menu_Text[48]))
			{
				Hover =34;
				Weapon_Info.text = Menu_Text[49];
				Weapon_Description.text = Menu_Text[50];
					
			}

			if(GUI.Button(new Rect(Screen.width/2 - 240 , Screen.height/2 - 150 , 110,110), Menu_Text[51]))
			{
				Hover =35;
				Weapon_Info.text = Menu_Text[52];
				Weapon_Description.text = Menu_Text[53];
					
			}

			if(GUI.Button(new Rect(Screen.width/2 - 120 , Screen.height/2 - 150 , 110,110), Menu_Text[54]))
			{
				Hover =36;
				Weapon_Info.text = Menu_Text[55];
				Weapon_Description.text = Menu_Text[56];
					
			}

			if(GUI.Button(new Rect(Screen.width/2  , Screen.height/2 - 150 , 110,110), Menu_Text[57]))
			{
				Hover =37;
				Weapon_Info.text = Menu_Text[58];
				Weapon_Description.text = Menu_Text[59];
					
			}



			if(GUI.Button(new Rect(Screen.width/2 -235, Screen.height/2 +300, 180,120 ), Menu_Text[60]))
			{
				Menu_State = 2;	
				Hover = 0;
				Weapon_Affordability.color = Color.red;
				Menu_Icon.SetActive(false);
				Upgrade_Icons[0].SetActive(false);
				Upgrade_Icons[1].SetActive(false);
				Upgrade_Icons[2].SetActive(false);

			}

			Weapon_Unlock_Check(1);

		}

		if (Menu_State == 6) /////////////////////////////////////////////////////////////////////////////////////////////////////// Ship Skin Choice
		{
			Weapon1_Icon.SetActive(false);
			Weapon2_Icon.SetActive(false);

			Dummy_Ship.SetActive(false); 
			Dummy_Ship2.SetActive(true); 

			if(GUI.Button(new Rect(Screen.width/2 - 240 , Screen.height/2 -270, 110,110), Menu_Text[61]))
			{
				Hover =21;
				Dummy_Ship2.renderer.material = Player_Skins[0];  
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 120 , Screen.height/2 - 270 , 110,110), Menu_Text[62]))
			{
				Hover =22;
				Dummy_Ship2.renderer.material = Player_Skins[1]; 
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2   , Screen.height/2 - 270 , 110,110), Menu_Text[63]))
			{
				Hover =23;
				Dummy_Ship2.renderer.material = Player_Skins[2]; 
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2 + 120 , Screen.height/2 - 270 , 110,110), Menu_Text[64]))
			{
				Hover =24;
				Dummy_Ship2.renderer.material = Player_Skins[3]; 
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 240 , Screen.height/2 - 150 , 110,110), Menu_Text[65]))
			{
				Hover =25;
				Dummy_Ship2.renderer.material = Player_Skins[4]; 
				
			}

			if(GUI.Button(new Rect(Screen.width/2 -235, Screen.height/2 +300, 180,120 ), Menu_Text[66]))
			{
				Menu_State = 2;	
				Hover = 0;
				Dummy_Ship.SetActive(true); 
				Dummy_Ship2.SetActive(false); 
			}

			Weapon_Unlock_Check(1);

		}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Options
		if (Menu_State == 7) 
		{
			if(PlayerPrefs.GetInt("Audio") !=1)
			{
				if(GUI.Button(new Rect(Screen.width/2-40 , Screen.height/2-400 , 80,80), Audio_Icons[0]))
				{
					PlayerPrefs.SetInt("Audio", 1);
					Menu_Music.audio.mute = true;
				}
			}
			
			else if(PlayerPrefs.GetInt("Audio") ==1)
			{
				if(GUI.Button(new Rect(Screen.width/2-40 , Screen.height/2-400, 80,80), Audio_Icons[1]))
				{
					PlayerPrefs.DeleteKey("Audio");
					Menu_Music.audio.mute = false;
					Menu_Music.audio.Play ();
				}
			}


			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 -300, 280, 160), Menu_Text[67]))
			{
				Menu_State = 71;
			}

			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 -120, 280, 160), Menu_Text[79]))
			{
				Menu_State = 72;
			}

			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 +60, 280, 160), Menu_Text[85]))
			{
				Menu_State = 73;
			}


			if(GUI.Button(new Rect(Screen.width/2 -235, Screen.height/2 +300, 180,120), Menu_Text[68]))
			{
				Menu_State = 1;
				Customize_label.text = "";
				Weapon_Info.text = "";
				Weapon_Description.text = "" ;
			}

		}

		if (Menu_State == 71) 
		{
			Customize_label.text =(Menu_Text[69]);
			Current_Currency_Text.text = (Menu_Text[70]);

			if(GUI.Button(new Rect(Screen.width/2 - 230 , Screen.height/2 , 180,160), Menu_Text[71]))
			{
				Menu_State = 7;
				Currect_Currency = 0;
				Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
				PlayerPrefs.DeleteAll();

				Skin_Assign();
				Customize_label.text =("");
				Current_Currency_Text.text = ("");
				Set_Language ();



			}
			
			if(GUI.Button(new Rect(Screen.width/2 +50, Screen.height/2 , 180,160), Menu_Text[73]))
			{
				Menu_State = 7;
				Customize_label.text =("");
				Current_Currency_Text.text = ("");

			}
			
		}

		if (Menu_State == 72) 
		{
			Customize_label.text =(Menu_Text[80]);
			Current_Currency_Text.text = "";

			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 -200, 280, 160), Menu_Text[81]))
			{
				PlayerPrefs.DeleteKey("Polski");
				Set_Language ();
			}
			
			if(GUI.Button(new Rect(Screen.width/2 -140, Screen.height/2 -20, 280, 160), Menu_Text[82]))
			{
				PlayerPrefs.SetInt("Polski", 1);
				Set_Language ();
			}

			if(GUI.Button(new Rect(Screen.width/2 -200, Screen.height/2 +300, 180,120), Menu_Text[68]))
			{
				Menu_State = 7;
				Customize_label.text =("");
			}

			
		}

		if (Menu_State == 73) 
		{
			Customize_label.text = Menu_Text[86];
			Weapon_Description.text = Menu_Text[87];
			if(GUI.Button(new Rect(Screen.width/2 -200, Screen.height/2 +300, 180,120), Menu_Text[68]))
			{
				Menu_State = 7;
				Customize_label.text =("");
				Weapon_Description.text =("");
			}
		
		}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Options		


		
	}

////////////////////////////////////////////////////////////////////////////////////////////////////////////// Unlocking and assining
	void Weapon_Unlock_Check(int Weapon_Type)
	{

///////////////////////////////////////////////////////////////////////////////////////////////// Weapons

		switch(Hover)
		{
		case 1:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Burst_Shot_Unlock") != 1)
			{
				Weapon_Cost.text =  Menu_Text[77] + weapon_cost[0] ;

				if(Currect_Currency < weapon_cost[0])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Burst_Shot_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[0];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
						
					}
				}


			}
			
			if(PlayerPrefs.GetInt("Burst_Shot_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
						PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
						PlayerPrefs.SetInt("Weapon_Active",1);
						Weapon1_Icon.renderer.material = Menu_Icons[0];

					    Weapon1_Choice_Icon.SetActive(true);
					    Weapon1_Choice_Icon.renderer.material =Menu_Icons[0];
				}

				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;

					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[0];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[0];
				}
			}
			break;
			
		case 2:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Spread_Shot_Unlock") != 1)
			{
				Weapon_Cost.text =  Menu_Text[77] + weapon_cost[1] ;
				
				if(Currect_Currency < weapon_cost[1])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Spread_Shot_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
						
					}
				}


			}
			
			if(PlayerPrefs.GetInt("Spread_Shot_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
					PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon1_Icon.renderer.material = Menu_Icons[1];

					Weapon1_Choice_Icon.SetActive(true);
					Weapon1_Choice_Icon.renderer.material =Menu_Icons[1];
				}
				
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;
					
					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[1];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[1];
					
				}

			}
			break;
			
		case 3:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Energy_Shield_Unlock") != 1)
			{
				Weapon_Cost.text =  Menu_Text[77] + weapon_cost[2] ;
				
				if(Currect_Currency < weapon_cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Energy_Shield_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
						
					}
				}


			}
			
			if(PlayerPrefs.GetInt("Energy_Shield_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
					PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon1_Icon.renderer.material = Menu_Icons[2];

					Weapon1_Choice_Icon.SetActive(true);
					Weapon1_Choice_Icon.renderer.material =Menu_Icons[2];
				}
				
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;
					
					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[2];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[2];
					
				}
			}
			break;
			
		case 4:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Split_Shot_Unlock") != 1)
			{
				Weapon_Cost.text =  Menu_Text[77] + weapon_cost[3] ;
				
				if(Currect_Currency < weapon_cost[3])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Split_Shot_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[3];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
						
					}
				}


			}
			
			if(PlayerPrefs.GetInt("Split_Shot_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
					PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon1_Icon.renderer.material = Menu_Icons[3];

					Weapon1_Choice_Icon.SetActive(true);
					Weapon1_Choice_Icon.renderer.material =Menu_Icons[3];

				}
				
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;
					
					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[3];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[3];
					
				}
			}
			break;
			
		case 5:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Explosive_Shot_Unlock") != 1)
			{
				Weapon_Cost.text =  Menu_Text[77] + weapon_cost[4] ;
				
				if(Currect_Currency < weapon_cost[4])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Explosive_Shot_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[4];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
						
					}
				}


			}
			
			if(PlayerPrefs.GetInt("Explosive_Shot_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
					PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon1_Icon.renderer.material = Menu_Icons[4];

					Weapon1_Choice_Icon.SetActive(true);
					Weapon1_Choice_Icon.renderer.material =Menu_Icons[4];
				}
				
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;
					
					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[4];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[4];
					
				}
			}
			break;
			
		case 6:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Plasma_Burst_Unlock") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + weapon_cost[5] ;
				
				if(Currect_Currency < weapon_cost[5])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Plasma_Burst_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[5];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
						
					}
				}


			}
			
			if(PlayerPrefs.GetInt("Plasma_Burst_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
					PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon1_Icon.renderer.material = Menu_Icons[5];

					Weapon1_Choice_Icon.SetActive(true);
					Weapon1_Choice_Icon.renderer.material =Menu_Icons[5];
				}
				
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;
					
					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[5];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[5];
					
				}
			}
			break;
			
		case 7:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Lightning_Strike_Unlock") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + weapon_cost[6];
				
				if(Currect_Currency < weapon_cost[6])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Lightning_Strike_Unlock",1);
						Currect_Currency = Currect_Currency - weapon_cost[6];
						Current_Currency_Text.text = "Currency : " + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
					    PlayerPrefs.SetInt(Menu_Text[72], Currect_Currency);

					}
				}


			}
			
			if(PlayerPrefs.GetInt("Lightning_Strike_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +200, 110,110), Menu_Text[83]))
				{
					//Menu_State = 2;
					//Hover = 0;
					PlayerPrefs.SetInt("Weapon1_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon1_Icon.renderer.material = Menu_Icons[6];

					Weapon1_Choice_Icon.SetActive(true);
					Weapon1_Choice_Icon.renderer.material =Menu_Icons[6];
				}
				
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[84]))
				{
					//Menu_State = 2;
					//Hover = 0;
					
					PlayerPrefs.SetInt("Weapon2_State",Weapon_1_Hover);
					PlayerPrefs.SetInt("Weapon_Active",1);
					Weapon2_Icon.renderer.material = Menu_Icons[6];

					Weapon2_Choice_Icon.SetActive(true);
					Weapon2_Choice_Icon.renderer.material =Menu_Icons[6];
					
				}
			}
			break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// Skins

		case 21:

			Weapon_Affordability.text = "";
			Weapon_Cost.text= "";
			if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[76]))
			{
				Hover = 0;
				
				PlayerPrefs.SetInt("Player_Skin",1);
				
				Skin_Assign ();
			}


			break;

		case 22:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Skin2_Unlock") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Skin_Cost[1];
				
				if(Currect_Currency < Skin_Cost[1])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Skin2_Unlock",1);
						Currect_Currency = Currect_Currency - Skin_Cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Skin2_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[76]))
				{
					Hover = 0;
					
					PlayerPrefs.SetInt("Player_Skin",2);

					Skin_Assign ();
				}
			}
			break;

		case 23:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Skin3_Unlock") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Skin_Cost[2];
				
				if(Currect_Currency < Skin_Cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Skin3_Unlock",1);
						Currect_Currency = Currect_Currency - Skin_Cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Skin3_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[76]))
				{
					Hover = 0;
					
					PlayerPrefs.SetInt("Player_Skin",3);

					Skin_Assign ();
				}
			}
			break;

		case 24:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Skin4_Unlock") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Skin_Cost[3];
				
				if(Currect_Currency < Skin_Cost[3])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Skin4_Unlock",1);
						Currect_Currency = Currect_Currency - Skin_Cost[3];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Skin4_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[76]))
				{
					Hover = 0;
					
					PlayerPrefs.SetInt("Player_Skin",4);

					Skin_Assign ();
				}
			}
			break;

		case 25:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Skin5_Unlock") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Skin_Cost[4];
				
				if(Currect_Currency < Skin_Cost[4])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Skin5_Unlock",1);
						Currect_Currency = Currect_Currency - Skin_Cost[4];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Skin5_Unlock") == 1)
			{
				Weapon_Cost.text = "" ;
				if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[76]))
				{
					Hover = 0;
					
					PlayerPrefs.SetInt("Player_Skin",5);

					Skin_Assign ();
				}
			}
			break;

///////////////////////////////////////////////////////////////////////////////////////////////// Upgrades

		case 31:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Health_Upgrade") != 1 && PlayerPrefs.GetInt("Health_Upgrade") != 2 && PlayerPrefs.GetInt("Health_Upgrade") != 3)
			{
				Weapon_Cost.text = Menu_Text[77] + Health_Upgrade_Cost[0];

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Health_Upgrade_Cost[0])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Health_Upgrade",1);
						Currect_Currency = Currect_Currency - Health_Upgrade_Cost[0];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Health_Upgrade") == 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Health_Upgrade_Cost[1];
				Upgrade_Icons[0].SetActive(true);
				
				if(Currect_Currency < Health_Upgrade_Cost[1])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Health_Upgrade",2);
						Currect_Currency = Currect_Currency - Health_Upgrade_Cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}

			if(PlayerPrefs.GetInt("Health_Upgrade") == 2)
			{
				Weapon_Cost.text = Menu_Text[77] + Health_Upgrade_Cost[2];
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				
				if(Currect_Currency < Health_Upgrade_Cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Health_Upgrade",3);
						Currect_Currency = Currect_Currency - Health_Upgrade_Cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}

			if(PlayerPrefs.GetInt("Health_Upgrade") == 3)
			{
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				Upgrade_Icons[2].SetActive(true);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];

			}
			break;

		case 32:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Fire_Rate_Upgrade") != 1 && PlayerPrefs.GetInt("Fire_Rate_Upgrade") != 2 && PlayerPrefs.GetInt("Fire_Rate_Upgrade") != 3)
			{
				Weapon_Cost.text = Menu_Text[77] + Fire_Rate_Upgrade_Cost[0];

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Fire_Rate_Upgrade_Cost[0])
				{
					Weapon_Cost.color = Color.red;
							Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Fire_Rate_Upgrade",1);
						Currect_Currency = Currect_Currency - Fire_Rate_Upgrade_Cost[0];
								Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Fire_Rate_Upgrade") == 1)
			{
						Weapon_Cost.text = Menu_Text[77] + Fire_Rate_Upgrade_Cost[1];
				Upgrade_Icons[0].SetActive(true);
				
				if(Currect_Currency < Fire_Rate_Upgrade_Cost[1])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Fire_Rate_Upgrade",2);
						Currect_Currency = Currect_Currency - Fire_Rate_Upgrade_Cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Fire_Rate_Upgrade") == 2)
			{
				Weapon_Cost.text = Menu_Text[77] + Fire_Rate_Upgrade_Cost[2];
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				
				if(Currect_Currency < Fire_Rate_Upgrade_Cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Fire_Rate_Upgrade",3);
						Currect_Currency = Currect_Currency - Fire_Rate_Upgrade_Cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Fire_Rate_Upgrade") == 3)
			{
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				Upgrade_Icons[2].SetActive(true);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];
				
			}
			break;

		case 33:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Speed_Upgrade") != 1 && PlayerPrefs.GetInt("Speed_Upgrade") != 2 && PlayerPrefs.GetInt("Speed_Upgrade") != 3)
			{
				Weapon_Cost.text = Menu_Text[77] + Speed_Upgrade_Cost[0];

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Speed_Upgrade_Cost[0])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Speed_Upgrade",1);
						Currect_Currency = Currect_Currency - Speed_Upgrade_Cost[0];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Speed_Upgrade") == 1)
			{
				Upgrade_Icons[0].SetActive(true);
				Weapon_Cost.text = Menu_Text[77] + Speed_Upgrade_Cost[1];
				
				if(Currect_Currency < Speed_Upgrade_Cost[1])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Speed_Upgrade",2);
						Currect_Currency = Currect_Currency - Speed_Upgrade_Cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Speed_Upgrade") == 2)
			{
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				Weapon_Cost.text = Menu_Text[77] + Speed_Upgrade_Cost[2];
				
				if(Currect_Currency < Speed_Upgrade_Cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Speed_Upgrade",3);
						Currect_Currency = Currect_Currency - Speed_Upgrade_Cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Speed_Upgrade") == 3)
			{
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				Upgrade_Icons[2].SetActive(true);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];
				
			}
			break;

		case 34:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Front_Weapon_Upgrade") != 1 && PlayerPrefs.GetInt("Front_Weapon_Upgrade") != 2 && PlayerPrefs.GetInt("Front_Weapon_Upgrade") != 3)
			{
				Weapon_Cost.text = Menu_Text[77] + Front_Weapon_Upgrade_Cost[0];

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Front_Weapon_Upgrade_Cost[0])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Front_Weapon_Upgrade",1);
						Currect_Currency = Currect_Currency - Front_Weapon_Upgrade_Cost[0];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Front_Weapon_Upgrade") == 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Front_Weapon_Upgrade_Cost[1];
				Upgrade_Icons[0].SetActive(true); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);

				if(Currect_Currency < Front_Weapon_Upgrade_Cost[1])
				{

					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Front_Weapon_Upgrade",2);
						Currect_Currency = Currect_Currency - Front_Weapon_Upgrade_Cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Front_Weapon_Upgrade") == 2)
			{
				Upgrade_Icons[0].SetActive(true); Upgrade_Icons[1].SetActive(true); Upgrade_Icons[2].SetActive(false);
				Weapon_Cost.text = Menu_Text[77] + Front_Weapon_Upgrade_Cost[2];
				
				if(Currect_Currency < Front_Weapon_Upgrade_Cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Front_Weapon_Upgrade",3);
						Currect_Currency = Currect_Currency - Front_Weapon_Upgrade_Cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Front_Weapon_Upgrade") == 3)
			{
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				Upgrade_Icons[2].SetActive(true);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];
				
			}
			break;

		case 35:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Back_Weapon_Upgrade") != 1 && PlayerPrefs.GetInt("Back_Weapon_Upgrade") != 2 && PlayerPrefs.GetInt("Back_Weapon_Upgrade") != 3)
			{
				Weapon_Cost.text = Menu_Text[77] + Back_Weapon_Upgrade_Cost[0];

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Back_Weapon_Upgrade_Cost[0])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Back_Weapon_Upgrade",1);
						Currect_Currency = Currect_Currency - Back_Weapon_Upgrade_Cost[0];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
				
				
			}
			
			if(PlayerPrefs.GetInt("Back_Weapon_Upgrade") == 1)
			{
				Upgrade_Icons[0].SetActive(true); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				Weapon_Cost.text = Menu_Text[77] + Back_Weapon_Upgrade_Cost[1];
				
				if(Currect_Currency < Back_Weapon_Upgrade_Cost[1])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Back_Weapon_Upgrade",2);
						Currect_Currency = Currect_Currency - Back_Weapon_Upgrade_Cost[1];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Back_Weapon_Upgrade") == 2)
			{
				Upgrade_Icons[0].SetActive(true); Upgrade_Icons[1].SetActive(true); Upgrade_Icons[2].SetActive(false);

				Weapon_Cost.text = Menu_Text[77] + Back_Weapon_Upgrade_Cost[2];
				
				if(Currect_Currency < Back_Weapon_Upgrade_Cost[2])
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Back_Weapon_Upgrade",3);
						Currect_Currency = Currect_Currency - Back_Weapon_Upgrade_Cost[2];
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Back_Weapon_Upgrade") == 3)
			{
				Upgrade_Icons[0].SetActive(true);
				Upgrade_Icons[1].SetActive(true);
				Upgrade_Icons[2].SetActive(true);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];
				
			}
			break;

		case 36:

			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Shield_Pack_Upgrade") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Shield_Pack_Upgrade_Cost;

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Shield_Pack_Upgrade_Cost)
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Shield_Pack_Upgrade",1);
						Currect_Currency = Currect_Currency - Shield_Pack_Upgrade_Cost;
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						Weapon_Cost.color = Color.green;
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
		
			if(PlayerPrefs.GetInt("Shield_Pack_Upgrade") == 1)
			{
				Upgrade_Icons[0].SetActive(true); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];
				
			}
			break;

		case 37:
			
			Weapon_Affordability.text = "";
			if(PlayerPrefs.GetInt("Plasma_Cannons_Upgrade") != 1)
			{
				Weapon_Cost.text = Menu_Text[77] + Plasma_Cannons_Upgrade_Cost;

				Upgrade_Icons[0].SetActive(false); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				
				if(Currect_Currency < Plasma_Cannons_Upgrade_Cost)
				{
					Weapon_Cost.color = Color.red;
					Weapon_Affordability.text = Menu_Text[74];
				}
				else
				{
					Weapon_Cost.color = Color.green;
					if(GUI.Button(new Rect(Screen.width/2 +125 , Screen.height/2 +315, 110,110), Menu_Text[75]))
					{
						PlayerPrefs.SetInt("Plasma_Cannons_Upgrade",1);
						Currect_Currency = Currect_Currency - Plasma_Cannons_Upgrade_Cost;
						Current_Currency_Text.text = Menu_Text[72] + Currect_Currency.ToString();
						Weapon_Affordability.text = "";
						PlayerPrefs.SetInt("Currency", Currect_Currency);
					}
				}
			}
			
			if(PlayerPrefs.GetInt("Plasma_Cannons_Upgrade") == 1)
			{
				Upgrade_Icons[0].SetActive(true); Upgrade_Icons[1].SetActive(false); Upgrade_Icons[2].SetActive(false);
				Weapon_Cost.text = "";
				Weapon_Affordability.color = Color.green;
				Weapon_Affordability.text = Menu_Text[78];
				
			}
			break;


		}
	}

	void Skin_Assign()
	{
		switch(PlayerPrefs.GetInt("Player_Skin"))
		{
		case 1:

			Dummy_Ship.renderer.material = Player_Skins[0];
			Dummy_Ship2.renderer.material = Player_Skins[0];
			Player_Ship.renderer.material = Player_Skins[0];

			break;

		case 2:

			Dummy_Ship.renderer.material = Player_Skins[1];
			Dummy_Ship2.renderer.material = Player_Skins[1];
			Player_Ship.renderer.material = Player_Skins[1];

			break;

		case 3:

			Dummy_Ship.renderer.material = Player_Skins[2];
			Dummy_Ship2.renderer.material = Player_Skins[2];
			Player_Ship.renderer.material = Player_Skins[2];

			break;

		case 4:

			Dummy_Ship.renderer.material = Player_Skins[3];
			Dummy_Ship2.renderer.material = Player_Skins[3];
			Player_Ship.renderer.material = Player_Skins[3];

			break;

		case 5:

			Dummy_Ship.renderer.material = Player_Skins[4];
			Dummy_Ship2.renderer.material = Player_Skins[4];
			Player_Ship.renderer.material = Player_Skins[4];

			break;

		}
	}

	void Set_Language()
	{
		Menu_Text_Eng [0] = "Main Menu"; Menu_Text_Pl [0] = "Główne Menu";
		Menu_Text_Eng [1] = "Replay"; Menu_Text_Pl [1] = "Zagraj\nPonownie";
		Menu_Text_Eng [2] = "Pause"; Menu_Text_Pl [2] = "Pauza";
		Menu_Text_Eng [3] = "Resume"; Menu_Text_Pl [3] = "Wznów";
		Menu_Text_Eng [4] = "Main Menu"; Menu_Text_Pl [4] = "Główne Menu";
		Menu_Text_Eng [5] = "Play"; Menu_Text_Pl [5] = "Graj";
		Menu_Text_Eng [6] = "Customize"; Menu_Text_Pl [6] = "Ulepszenia";
		Menu_Text_Eng [7] = "Options"; Menu_Text_Pl [7] = "Opcje";
		Menu_Text_Eng [8] = "Customizie Your Ship"; Menu_Text_Pl [8] = "Ulepsz swoj statek";
		Menu_Text_Eng [9] = "Currency : "; Menu_Text_Pl [9] = "Waluta : ";
		Menu_Text_Eng [10] = "Weapons"; Menu_Text_Pl [10] = "Bronie";
		Menu_Text_Eng [11] = "Back\nWeapon"; Menu_Text_Pl [11] = "Tylna\nBron";
		Menu_Text_Eng [12] = "Upgrade"; Menu_Text_Pl [12] = "Ulepsz";
		Menu_Text_Eng [13] = "Skin"; Menu_Text_Pl [13] = "Wyglad";
		Menu_Text_Eng [14] = "Back"; Menu_Text_Pl [14] = "Wroc";
		Menu_Text_Eng [15] = "Choose The Weapons"; Menu_Text_Pl [15] = "Wybierz Bronie";
		Menu_Text_Eng [16] = "Burst\nShot"; Menu_Text_Pl [16] = "Seria";
		Menu_Text_Eng [17] = "Burst Shot"; Menu_Text_Pl [17] = "Seria";
		Menu_Text_Eng [18] = "Shoots a burst of\nlaser bolts"; Menu_Text_Pl [18] = "Wystrzela serie\nlaserowych pociskow";
		Menu_Text_Eng [19] = "Spread\nShot"; Menu_Text_Pl [19] = "Rozlamany\nPocisk";
		Menu_Text_Eng [20] = "Spread Shot"; Menu_Text_Pl [20] = "Rozlamany Pocisk";
		Menu_Text_Eng [21] = "Shoots a series of\nangled shots to\nyour sides."; Menu_Text_Pl [21] = "Wytrzela serie\npociskow pod\nkatem.";
		Menu_Text_Eng [22] = "Energy\nShield"; Menu_Text_Pl [22] = "Tarcza\nEnergetyczna";
		Menu_Text_Eng [23] = "Energy Shield"; Menu_Text_Pl [23] = "Tarcza\nEnergetyczna";
		Menu_Text_Eng [24] = "Protects your ship from\nany harm for a couple\nof seconds."; Menu_Text_Pl [24] = "Chroni twoj statek\nod jakiejkolwiek\nkrzywdy.";
		Menu_Text_Eng [25] = "Split\nShot"; Menu_Text_Pl [25] = "Podzielony\nPocisk";
		Menu_Text_Eng [26] = "Split Shot"; Menu_Text_Pl [26] = "Podzielony Pocisk";
		Menu_Text_Eng [27] = "Shoots a slow moving\nbolt that shoots bolts\nsideways."; Menu_Text_Pl [27] = "Wystrzeliwuje powolny \npocisk, który strzela\nna boki.";
		Menu_Text_Eng [28] = "Explo\nsive\nShot"; Menu_Text_Pl [28] = "Wybuchajacy\nPocisk";
		Menu_Text_Eng [29] = "Explosive Shot"; Menu_Text_Pl [29] = "Wybuchajacy Pocisk";
		Menu_Text_Eng [30] = "Shoots a bolt that\nexplodes on impact."; Menu_Text_Pl [30] = "Ten pocisk wybucha\nprzy kontakcie.";
		Menu_Text_Eng [31] = "Plasma\nBurst"; Menu_Text_Pl [31] = "Fala\nPlasmy";
		Menu_Text_Eng [32] = "Plasma Burst"; Menu_Text_Pl [32] = "Fala Plasmy";
		Menu_Text_Eng [33] = "Release a wave of\nplasma, destroying\neverything near\nyour ship."; Menu_Text_Pl [33] = "Wypuszcza fale plasmy\nktora niszczy\nwszystko blisko\ntwojego statku.";
		Menu_Text_Eng [34] = "Lightning\nStrike"; Menu_Text_Pl [34] = "Uderzenie\nBlyskawicy";
		Menu_Text_Eng [35] = "Lightning Strike"; Menu_Text_Pl [35] = "Uderzenie\nBlyskawicy";
		Menu_Text_Eng [36] = "Summon a bolt of\nlightning. Everything in a\nlane will be destroyed."; Menu_Text_Pl [36] = "Przyzwij blyskawice\nWszystko w jednej linii\nzostanie zniszczone.";
		Menu_Text_Eng [37] = "Back"; Menu_Text_Pl [37] = "Wroc";
		Menu_Text_Eng [38] = "Choose Back Weapon"; Menu_Text_Pl [38] = "Wybierz Tylna Bron";
		Menu_Text_Eng [39] = "Health"; Menu_Text_Pl [39] = "Zycie";
		Menu_Text_Eng [40] = "Health"; Menu_Text_Pl [40] = "Zycie";
		Menu_Text_Eng [41] = "Increases the amount\nof hits your ship\ncan take"; Menu_Text_Pl [41] = "Zwieksza punkty\nzycia twojego statku.";
		Menu_Text_Eng [42] = "Fire Rate"; Menu_Text_Pl [42] = "Szybkosc\nOgnia";
		Menu_Text_Eng [43] = "Fire Rate"; Menu_Text_Pl [43] = "Szybkosc Ognia";
		Menu_Text_Eng [44] = "Boosts the rate of fire\nof your main gun."; Menu_Text_Pl [44] = "Zwieksza szybkosc ognia\ntwojej glownej broni.";
		Menu_Text_Eng [45] = "Speed"; Menu_Text_Pl [45] = "Szybkosc";
		Menu_Text_Eng [46] = "Speed"; Menu_Text_Pl [46] = "Szybkosc";
		Menu_Text_Eng [47] = "Makes you fly quicker\nbetween lanes."; Menu_Text_Pl [47] = "Zwieksza twoja szybkosc\npomiedzy liniami.";
		Menu_Text_Eng [48] = "Front\nweapon\nCool\ndown"; Menu_Text_Pl [48] = "Przeladowanie\nPrzedniej\nBroni";
		Menu_Text_Eng [49] = "Front weapon\nCooldown"; Menu_Text_Pl [49] = "Przeladowanie\nPrzedniej Broni";
		Menu_Text_Eng [50] = "Your front weapon now\nrecharges faster."; Menu_Text_Pl [50] = "Zmniejsza czas\nprzeladowania twojej\nprzedniej broni.";
		Menu_Text_Eng [51] = "Back\nweapon\nCool\ndown"; Menu_Text_Pl [51] = "Przeladowanie\nTylnej\nBroni";
		Menu_Text_Eng [52] = "Back weapon\nCooldown"; Menu_Text_Pl [52] = "Przeladowanie\nTylnej Broni";
		Menu_Text_Eng [53] = "Your back weapon now\nrecharges faster."; Menu_Text_Pl [53] = "Zmniejsza czas\nprzeladowania twojej\ntylnej broni.";
		Menu_Text_Eng [54] = "Shield\nPack"; Menu_Text_Pl [54] = "Tarcza";
		Menu_Text_Eng [55] = "Shield Pack"; Menu_Text_Pl [55] = "Tarcza";
		Menu_Text_Eng [56] = "You start the game with\na blue shield."; Menu_Text_Pl [56] = "Zaczynasz gre\nz niebieska tarcza.";
		Menu_Text_Eng [57] = "Plasma\ncannons"; Menu_Text_Pl [57] = "Dzialko\nPlasmowe";
		Menu_Text_Eng [58] = "Plasma\ncannons"; Menu_Text_Pl [58] = "Dzialko\nPlasmowe";
		Menu_Text_Eng [59] = "Your main guns now\npenetrate trough 1\ntarget."; Menu_Text_Pl [59] = "Twoja glowna bron\nteraz przebija sie\nprzez 1 cel.";
		Menu_Text_Eng [60] = "Back"; Menu_Text_Pl [60] = "Wroc";
		Menu_Text_Eng [61] = "Grey"; Menu_Text_Pl [61] = "Szary";
		Menu_Text_Eng [62] = "Silver"; Menu_Text_Pl [62] = "Srebrny";
		Menu_Text_Eng [63] = "Gold"; Menu_Text_Pl [63] = "Zloty";
		Menu_Text_Eng [64] = "Green"; Menu_Text_Pl [64] = "Zielony";
		Menu_Text_Eng [65] = "Azure"; Menu_Text_Pl [65] = "Blekitny";
		Menu_Text_Eng [66] = "Back"; Menu_Text_Pl [66] = "Wroc";
		Menu_Text_Eng [67] = "Reset Save"; Menu_Text_Pl [67] = "Zresetuj Zapis";
		Menu_Text_Eng [68] = "Back";  Menu_Text_Pl [68] = "Wroc";
		Menu_Text_Eng [69] = "Are you sure ?"; Menu_Text_Pl [69] = "Czy jestes pewny ?";
		Menu_Text_Eng [70] = "This will wipe away\nall your progres"; Menu_Text_Pl [70] = "Ten krok zresetuje\ncay twoj postep";
		Menu_Text_Eng [71] = "Yes"; Menu_Text_Pl [71] = "Tak";
		Menu_Text_Eng [72] = "Currency : "; Menu_Text_Pl [72] = "Waluta : ";
		Menu_Text_Eng [73] = "No"; Menu_Text_Pl [73] = "Nie";
		Menu_Text_Eng [74] = "You can't\nafford this."; Menu_Text_Pl [74] = "Nie stac\ncie na to.";
		Menu_Text_Eng [75] = "Buy"; Menu_Text_Pl [75] = "Kup";
		Menu_Text_Eng [76] = "Assign"; Menu_Text_Pl [76] = "Wybierz";
		Menu_Text_Eng [77] = "Cost :"; Menu_Text_Pl [77] = "Koszt :";
		Menu_Text_Eng [78] = "This is fully\nupgraded."; Menu_Text_Pl [78] = "To jest w pelni\nulepszone.";
		Menu_Text_Eng [79] = "Language"; Menu_Text_Pl [79] = "Jezyk";
		Menu_Text_Eng [80] = "Choose the\nLanguage"; Menu_Text_Pl [80] = "Wybierz Jezyk";
		Menu_Text_Eng [81] = "English"; Menu_Text_Pl [81] = "Angielski";
		Menu_Text_Eng [82] = "Polish"; Menu_Text_Pl [82] = "Polski";
		Menu_Text_Eng [83] = "Assign\nFront"; Menu_Text_Pl [83] = "Przydziel\nDo\nprzodu";
		Menu_Text_Eng [84] = "Assign\nBack"; Menu_Text_Pl [84] = "Przydziel\nDo\ntylu";
		Menu_Text_Eng [85] = "Credits"; Menu_Text_Pl [85] = "Tworcy";
		Menu_Text_Eng [86] = "A game by :\nMichal Kowalczyk\n\n Music:\n'Cipher'\n'Complexity'\nBy Kevin Macleod\n(incompetech.com)";  Menu_Text_Pl [86] = "Gra stworzona\nprzez:\nMichal Kowalczyk\n\nMuzyka:\n'Cipher'\n'Complexity'\nstworzone przez:\nKevin Macleod\n(incompetech.com)";
		Menu_Text_Eng [87] = "Licensed under\nCreative Commons:\nBy Attribution 3.0\nhttp://creativecommons.org\n/licenses/by/3.0/"; Menu_Text_Pl [87] = "Licensed under\nCreative Commons:\nBy Attribution 3.0\nhttp://creativecommons.org\n/licenses/by/3.0/";

		if (PlayerPrefs.GetInt("Polski") != 1) 
		{
			for (int x = 0; x<= 87; x++) 
			{
				Menu_Text[x] = Menu_Text_Eng[x];
			}
		}

		if (PlayerPrefs.GetInt("Polski") == 1) 
		{
			for (int x = 0; x<= 87; x++) 
			{
				Menu_Text[x] = Menu_Text_Pl[x];
			}
		}


	}
	
}
