using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public enum attackType {Hammer, Lazer, Ball_Lazer, Ball_Iron};

	public attackType type;

	public Weapon(attackType type)
	{
		type = attackType;
	}
}
