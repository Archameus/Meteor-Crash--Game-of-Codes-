using UnityEngine;
using System.Collections;

public class Destroy_By_Boundry : MonoBehaviour 
{
	void OnTriggerExit(Collider other) {
		Destroy(other.gameObject);
	}
}
