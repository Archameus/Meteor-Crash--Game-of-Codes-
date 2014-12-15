using UnityEngine;
using System.Collections;

public class Expand_Over_Time : MonoBehaviour {

	public float Size_Increase ;
	public float Time_Limit;

	void Start()
	{
		StartCoroutine (Destroy());

	}

	void Update () 
	{
		transform.localScale += new Vector3(Size_Increase, Size_Increase, Size_Increase) * Time.deltaTime * 50 ;
	
	}

	IEnumerator Destroy()
	{

		yield return new WaitForSeconds (Time_Limit);
		Destroy (gameObject);
	}
}
