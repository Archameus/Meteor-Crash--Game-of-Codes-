using UnityEngine;
using System.Collections;

public class Random_Rotator : MonoBehaviour 
{
	public float Tumble;

	void Start()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * Tumble;
	}



}
