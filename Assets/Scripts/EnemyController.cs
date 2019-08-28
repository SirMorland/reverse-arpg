using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private Rigidbody2D rb;
	private Animator animator;
	private Stats stats;
	private Weapon weapon;

	private GameObject player;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		stats = GetComponent<Stats>();
		weapon = GetComponentInChildren<Weapon>();

		player = GameObject.FindWithTag("Player");

		animator.SetBool("walking", true);

		StartCoroutine(Attack());
	}

	void Update()
	{
	}

	void FixedUpdate()
	{
		if(stats.alive && !stats.dashing)
		{
			float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
			rb.velocity = new Vector2(direction * stats.speed, rb.velocity.y);
			
			transform.localScale = new Vector3(-direction, 1, 1);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "PlayerWeapon")
		{
			Weapon weapon = collision.collider.GetComponent<Weapon>();
			weapon.DealDamage(gameObject);
		}
	}

	void OnDashEnded()
	{
		stats.dashing = false;
	}

	IEnumerator Attack()
	{
		yield return new WaitForSeconds(Random.Range(0.5f, 1f));

		while(true)
		{
			if(stats.alive)
			{
				weapon.Attack(gameObject);
				yield return new WaitForSeconds(Random.Range(0.5f, 1f));
			}
			else
			{
				break;
			}
		}
	}
}
