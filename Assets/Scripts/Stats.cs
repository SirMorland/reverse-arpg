using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
	public float maxHp = 100;
	public float currentHp = 100;

	public float speed = 5;

	public bool dashing = false;
	public bool alive = true;

	public int gold = 0;

	public Armor armor;
}
