using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	public Rigidbody2D rb;
	public Animator animator;
	public Stats stats;
	protected Weapon weapon;

	protected virtual void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		stats = GetComponent<Stats>();
		weapon = GetComponentInChildren<Weapon>();
	}

	void OnDashEnded()
	{
		stats.dashing = false;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "PlayerWeapon")
		{
			Weapon weapon = collision.collider.GetComponent<Weapon>();
			weapon.DealDamage(gameObject);
		}
	}

	public virtual void GainExp(int amount)
	{
		stats.exp += amount;
	}
}
