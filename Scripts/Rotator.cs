using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public int x;
	public int y;
	public int z;
	
	
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3 (x, y, z) * Time.deltaTime); //Default for Pickup - 15,30,45
		
	}


}
