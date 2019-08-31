using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
	public float currentHp;

	public float speed;

	public bool dashing = false;
	public bool alive = true;

	public int gold;

	//Stat window stats
	public int lvl;
	public int exp;
	public int sp;
    public int str;
    public int dex;
    public int vit;

    public Armor armor;
	public Armor oldArmor;

	public int Damage
	{
		get
		{
			return str;
		}
	}
	public float AttackSpeed
	{
		get
		{
			return dex / 100f + 0.5f;
		}
	}
	public int MaxHp
	{
		get
		{
			return vit;
		}
	}

	void Start()
	{
		currentHp = MaxHp;
	}
}
