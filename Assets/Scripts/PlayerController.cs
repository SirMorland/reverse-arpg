using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public LayerMask canJumpOn;

	private Camera mainCamera;
	private Rigidbody2D rb;
	private Animator animator;
	private Stats stats;
	private Weapon weapon;

	private bool grounded = false;

	void Start()
	{
		mainCamera = Camera.main;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		stats = GetComponent<Stats>();
		weapon = GetComponentInChildren<Weapon>();
	}


	void Update()
	{
		grounded = Physics2D.OverlapArea(
			new Vector2(transform.position.x - 0.125f, transform.position.y - 0.1f),
			new Vector2(transform.position.x + 0.125f, transform.position.y - 0.2f),
			canJumpOn
		);
		animator.SetBool("onAir", !grounded);

		if (stats.alive)
		{
			if (Input.GetAxis("Horizontal") > 0.1)
			{
				animator.SetBool("walking", true);
				transform.localScale = new Vector3(-1, 1, 1);
			}
			else if (Input.GetAxis("Horizontal") < -0.1)
			{
				animator.SetBool("walking", true);
				transform.localScale = new Vector3(1, 1, 1);
			}
			else
			{
				animator.SetBool("walking", false);
			}

			if (!stats.dashing && Input.GetButtonDown("Fire1"))
			{
				weapon.Attack(gameObject);
			}
		}
	}

	void FixedUpdate()
	{
		if (stats.alive && !stats.dashing)
		{
			if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
			{
				rb.velocity = new Vector2(Input.GetAxis("Horizontal") * stats.speed, rb.velocity.y);
			}
			if(grounded && Input.GetButton("Jump"))
			{
				rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
			}
		}
	}

	void LateUpdate()
	{
		float x = transform.position.x - mainCamera.aspect * mainCamera.orthographicSize + 2f;
		float y = mainCamera.transform.position.y;
		float z = mainCamera.transform.position.z;

		mainCamera.transform.position = new Vector3(x, y, z);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "EnemyWeapon")
		{
			Weapon weapon = collision.collider.GetComponent<Weapon>();
			weapon.DealDamage(gameObject);
		}
	}

	void OnDashEnded()
	{
		stats.dashing = false;
	}
}
