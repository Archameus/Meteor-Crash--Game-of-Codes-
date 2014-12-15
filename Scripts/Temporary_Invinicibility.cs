using UnityEngine;
using System.Collections;

public class Temporary_Invinicibility : MonoBehaviour {


	public float Invincibillity_Time_Value;
	private float Invincibility;

	void Start()
	{
		Invincibility = Time.time + Invincibillity_Time_Value;

	}
	void Update () 
	{
		if (Time.time < Invincibility) 
		{
			collider.enabled = false;
		}
		else
		{
			collider.enabled = true;
		}


	
	}
}
