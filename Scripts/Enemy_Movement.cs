using UnityEngine;
using System.Collections;

public class Enemy_Movement : MonoBehaviour {

	public float Dodge_Wait_Min;
	public float Dodge_Wait_Max;
	public float Dodge_Speed;
	public float Bank;

	private float Dodge_Direction;
	

	void Start()
	{
		Dodge_Direction = Random.Range (0.0f, 3.0f);
		Debug.Log (Dodge_Direction);

		StartCoroutine (Evade());
	}

	IEnumerator Evade()
	{
		yield return new WaitForSeconds(Random.Range(Dodge_Wait_Min, Dodge_Wait_Max));


		if (Dodge_Direction > 0 && Dodge_Direction < 1) 
		{
			rigidbody.velocity = new Vector3 (Dodge_Speed, 0.0f , -5);
			rigidbody.rotation = Quaternion.Euler ( 0.0f, 0.0f, rigidbody.velocity.x * -Bank);
		}

		if (Dodge_Direction > 1 && Dodge_Direction < 2) 
		{
			rigidbody.velocity = new Vector3 (-Dodge_Speed, 0.0f , -5);
			rigidbody.rotation = Quaternion.Euler ( 0.0f, 0.0f, rigidbody.velocity.x * -Bank);
		}

		if (Dodge_Direction > 2 && Dodge_Direction < 3) 
		{
		}



	
	}




	

}
