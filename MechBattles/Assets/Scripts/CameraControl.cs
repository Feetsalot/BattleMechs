using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject playerMech;
	float horizontalBuffer = 15.0f;
	float upperBuffer = 3.75f;
	float zoomAmount = -12.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		follow (playerMech);
	}

	void follow(GameObject target)
	{
		if (target.transform.position.x > horizontalBuffer)
			this.transform.position = new Vector3 (target.transform.position.x, this.transform.position.y, zoomAmount);//target.transform.position.y + upperBuffer, zoomAmount);
//		if(target.transform.position.y > upperBuffer || target.transform.position.y < upperBuffer)
//			this.transform.position = new Vector3 (this.transform.position.x, target.transform.position.y + upperBuffer, zoomAmount);
	}
}
