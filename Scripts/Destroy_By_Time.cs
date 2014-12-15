using UnityEngine;
using System.Collections;

public class Destroy_By_Time : MonoBehaviour {

	public float Lifetime;

	void Start () 
	{
		Destroy (gameObject, Lifetime);
	
	}

}
