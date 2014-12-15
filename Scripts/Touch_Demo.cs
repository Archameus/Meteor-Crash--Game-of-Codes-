using UnityEngine;
using System.Collections;

public class Touch_Demo : MonoBehaviour {

	public bool TouchDemo;
	public bool MoveDemo;


	void Update()
	{

		if (MoveDemo == true) 
		{
			if(Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				float x =  -1.5f + 3  * touch.position.x / Screen.width ;
				float y =  -3 + 8 * touch.position.y / Screen.height  ;

				transform.position = new Vector3(x, y, 0.0f);
				
			}
			
		}

	}

	void OnGUI()
	{

		if (TouchDemo == true) 
		{
			foreach(Touch touch in Input.touches)
			{
				string message = "";
				message += "ID: " + touch.fingerId +  "\n" ;
				message += "Phase: " + touch.phase.ToString() +  "\n" ;
				message += "Tap Count : " + touch.tapCount +  "\n" ;
				message += "Pos X: " + touch.position.x +  "\n" ;
				message += "Pos Y: " + touch.position.y +  "\n" ;
				
				int num = touch.fingerId;
				
				GUI.Label(new Rect(0 + 120 * num, 0, 120, 100), message);
			}
		}

	}
}
