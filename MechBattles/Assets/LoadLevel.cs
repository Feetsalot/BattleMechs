using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {


	public static void Load()
	{
		Application.LoadLevel (1);
	}

	public void LoadLevelOne()
	{
		Debug.Log ("Loaded");
		Application.LoadLevel (1);
	}
}
