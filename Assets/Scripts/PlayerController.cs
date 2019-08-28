using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 5f;

	private Camera camera;
	private Rigidbody2D rb;
	private Animator animator;

	private bool dashing = false;

	void Start()
	{
		camera = Camera.main;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}


	void Update()
	{
		if(Input.GetAxis("Horizontal") > 0.1)
		{
			animator.SetBool("walking", true);
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if(Input.GetAxis("Horizontal") < -0.1)
		{
			animator.SetBool("walking", true);
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			animator.SetBool("walking", false);
		}

		if(!dashing && Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("swordAttack");
			dashing = true;
			rb.AddForce(new Vector2(transform.localScale.x * -20f, 0f), ForceMode2D.Impulse);
		}
	}

	void FixedUpdate()
	{
		if(!dashing && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
		{
			rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
		}
	}

	void LateUpdate()
	{
		float x = transform.position.x - camera.aspect * camera.orthographicSize + 2f;
		float y = camera.transform.position.y;
		float z = camera.transform.position.z;

		camera.transform.position = new Vector3(x, y, z);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "EnemyWeapon")
		{
			float direction = Mathf.Sign(collision.transform.position.x - transform.position.x);
			rb.AddForce(new Vector2(-direction * 10f, 0f), ForceMode2D.Impulse);

			dashing = true;
			animator.SetTrigger("hurt");
		}
	}

	void OnDashEnded()
	{
		dashing = false;
	}
}
