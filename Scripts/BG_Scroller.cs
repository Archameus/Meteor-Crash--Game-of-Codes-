using UnityEngine;
using System.Collections;

public class BG_Scroller : MonoBehaviour {

	public float Scroll_Speed;
	public float Former_Scroll_Speed;
	public float Size_Of_Background;
	
	private Vector3 Start_Position;
	
	void Start ()
	{
		Start_Position = transform.position;
		Former_Scroll_Speed = Scroll_Speed;
	}
	
	void Update ()
	{
		Charge ();
		float New_Position = Mathf.Repeat(Time.time * Scroll_Speed , Size_Of_Background);
		transform.position = Start_Position + Vector3.forward * New_Position ;
	}

	void Charge()
	{
		if (PlayerPrefs.GetFloat ("Charge") != 0) {
			Scroll_Speed = Former_Scroll_Speed - PlayerPrefs.GetFloat ("Charge") * 5;
		} 
		
		if (PlayerPrefs.GetFloat ("Charge") == 0) {
			Scroll_Speed = Former_Scroll_Speed;
		}

	}
}
