using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public Stats stats;

	public LayerMask canJumpOn;
	
	private Rigidbody2D rb;
	private Animator animator;
	private Weapon weapon;

	private SpriteRenderer torso;
	private Sprite naked;

	private bool grounded = false;

	void Start()
	{
		DontDestroyOnLoad(gameObject);

		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		stats = GetComponent<Stats>();
		weapon = GetComponentInChildren<Weapon>();

		torso = transform.Find("Torso").GetComponent<SpriteRenderer>();
		naked = torso.sprite;
		PutOnArmor(stats.armor);

		if(weapon != null)
			weapon.skills.Add("dash");
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
				if(weapon != null)
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
		Camera mainCamera = Camera.main;
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		AsyncOperation ao = SceneManager.LoadSceneAsync("SampleScene");
		ao.completed += OnSceneLoaded;
	}

	void OnSceneLoaded(AsyncOperation ao)
	{
		transform.position = new Vector3(0, -1.5f, 0);
	}


	public void PutOnArmor(Armor armor)
	{
		stats.armor = armor;
		if(armor)
		{
			torso.sprite = stats.armor.sprite;
		}
		else
		{
			torso.sprite = naked;
		}
	}

	void OnDashEnded()
	{
		stats.dashing = false;
	}
}
