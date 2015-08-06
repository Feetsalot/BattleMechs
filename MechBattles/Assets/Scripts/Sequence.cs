using UnityEngine;
using System.Collections;

public class Sequence : MonoBehaviour {

	[System.Serializable]
	public struct actionTimeTable
	{
		public float time;
		public string action;
	}

	public Sequence.actionTimeTable[] TimeActTable;
	public AudioClip sound;
//	public Hashtable actionTimeTables;

	public Sequence(actionTimeTable[] att, AudioClip sound)
	{
		this.TimeActTable = att;
		this.sound = sound;
//		foreach(actionTimeTable current in att)
//		{
//			this.actionTimeTables.Add (current.time, current.action);
//		}
	}


}
