using UnityEngine;
using System.Collections;

public class Screen_Nuke : MonoBehaviour {

	public GameObject Nuke;
	
	void Start () 
	{
		StartCoroutine (Screen_nuke ());
	
	}
	
	IEnumerator Screen_nuke()
	{
		Instantiate (Nuke,  transform.position,  Quaternion.identity);
		yield return new WaitForSeconds(0.1f);
		Instantiate (Nuke,  transform.position,  Quaternion.identity);
	}
}
