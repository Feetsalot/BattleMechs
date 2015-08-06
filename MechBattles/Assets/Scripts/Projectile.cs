using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	void Start()
	{

	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Player Hit!");
		Destroy (this.gameObject);
	}
}
