using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour 
{
	private GameObject Follow_This;
	private Mobile_Game_Controller Power_Up_Control;
	public bool Shield;
	public bool Repulsor_Shield;

	void Start()
	{
		Follow_This = GameObject.FindWithTag ("Player");

		GameObject Game_Controller_Object = GameObject.FindWithTag ("Game_Controller");
		if (Game_Controller_Object != null) 
		{
			Power_Up_Control = Game_Controller_Object.GetComponent <Mobile_Game_Controller>();
		}
		
		if (Game_Controller_Object == null) 
		{
			Debug.Log ("Cant Find 'GameController' script");
		}

	}

	void Update()
	{
		transform.position = Follow_This.transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if(Shield == true )
		{
			if(other.tag == "Enemy" || other.tag == "Enemy_Bolt" || other.tag == "Asteroid")
			{
				Power_Up_Control.Shield_State = 1;
			}
		}

		if(Repulsor_Shield == true )
		{
		}
	}
	
}
