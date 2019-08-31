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

    //Stat window stats
    public int sp;
    public int str;
    public int dex;
    public int vit;
    public int lvl;
    public int dmg;
    public float atkSpd;

    public Armor armor;
	public Armor oldArmor;
}
