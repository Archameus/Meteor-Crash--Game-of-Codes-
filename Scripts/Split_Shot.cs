using UnityEngine;
using System.Collections;

public class Split_Shot : MonoBehaviour {
	

	public GameObject Bolt ;
	public float Shot_Spread;


	void Start () 
	{
		Instantiate(Bolt, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
		StartCoroutine (Shoot ());
	}

	IEnumerator Shoot()
	{
		while (true) 
		{
			yield return new WaitForSeconds(Shot_Spread);
			Instantiate(Bolt, transform.position, Quaternion.Euler(new Vector3(0, 90, 0)));
			Instantiate(Bolt, transform.position, Quaternion.Euler(new Vector3(0, -90, 0)));
			audio.Play ();

		}

	}
}
