using UnityEngine;
using System.Collections;

public class PickUp_Controller : MonoBehaviour {

	public GameObject Object;
	public bool Score_Multiplayer;
	public bool Shield; // Use this to choose weather or not the shield will be spawned when the player hits this object
	public bool Repulsor_Shield;
	public bool Screen_Nuke;
    private GUIText Score_Multiplayer_Text;
	private Mobile_Game_Controller Object_Control;
	



	void Start()
	{
		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			Object_Control = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}
		
		if (Game_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'GameController' script");
		}

	}

	void OnTriggerEnter(Collider Other)
	{

		if (Other.tag == "Player") 
		{
			if(Score_Multiplayer == true)
			{
				Object_Control.Add_Score(50);
				Instantiate (Object, new Vector3(20.0f,0.0f,0.0f) ,  Quaternion.identity);
				Destroy(gameObject);
			}

			if(Screen_Nuke == true)
			{
				Object_Control.Add_Score(100);
				Instantiate (Object,  Other.transform.position,  Quaternion.identity);
				Destroy(gameObject);
			}

			if(Shield == true)
			{
				if(Object_Control.Shield_State == 2)
				{
					Object_Control.Add_Score(50);
					Destroy(gameObject);
				}
				
				if(Object_Control.Shield_State == 1)
				{
					Object_Control.Add_Score(25);
					Instantiate (Object, new Vector3(20.0f,0.0f,0.0f) ,  Quaternion.identity);
					Object_Control.Shield_State = 2;
					Destroy(gameObject);
				}
			}

			if (Repulsor_Shield == true)
			{
				if(Object_Control.Repulsor_Shield_State == 2) //not used curently
				{
					Object_Control.Add_Score(75);
					Destroy(gameObject);
				}//not used currently
				
				if(Object_Control.Repulsor_Shield_State == 1)// allways 1, so far
				{
					Object_Control.Add_Score(75);
					Instantiate (Object, new Vector3(20.0f,0.0f,0.0f) ,  Quaternion.identity);
					//Object_Control.Repulsor_Shield_State = 2;
					Destroy(gameObject);
				}
			}

		}

	}



}
