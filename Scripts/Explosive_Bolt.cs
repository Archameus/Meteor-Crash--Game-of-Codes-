using UnityEngine;
using System.Collections;

public class Explosive_Bolt : MonoBehaviour {

	public GameObject Explosion ;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy" || other.tag == "Enemy_Bolt" || other.tag == "Asteroid") 
		{
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}

	}
}
