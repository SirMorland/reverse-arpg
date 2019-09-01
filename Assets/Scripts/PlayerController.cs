using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharacterController
{
	public GameObject pointWindow;

	public LayerMask canJumpOn;

	private SpriteRenderer torso;
	private Sprite naked;

	private bool grounded = false;

	private string nextLevel = "Town-3";
	private bool loadingScene = false;

	private bool gameEnding = false;

	protected override void Start()
	{
		base.Start();

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
					weapon.Attack();
			}
		}
		else if(!gameEnding)
		{
			gameEnding = true;
			StartCoroutine(EndGame());
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
				rb.AddForce(new Vector2(0f, 25f), ForceMode2D.Impulse);
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

		transform.position = new Vector3(transform.position.x, Mathf.Max(-1.75f, transform.position.y), transform.position.z);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "EnemyWeapon")
		{
			Weapon weapon = collision.collider.GetComponent<Weapon>();
			weapon.DealDamage(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(!loadingScene)
		{
			loadingScene = true;
			stats.currentHp = stats.MaxHp;

			AsyncOperation ao = SceneManager.LoadSceneAsync(nextLevel.Split('-')[0]);
			ao.completed += OnSceneLoaded;

			switch (nextLevel)
			{
				case "DemonLevel":
					nextLevel = "Town-3";
					break;
				case "Town-3":
					nextLevel = "OrcLevel";
					break;
				case "OrcLevel":
					nextLevel = "Town-2";
					break;
				case "Town-2":
					nextLevel = "FarmerLevel";
					break;
				case "FarmerLevel":
					nextLevel = "Town-1";
					break;
				case "Town-1":
					nextLevel = "FinalBoss";
					break;
				default:
					nextLevel = "BonusLevel";
					break;
			}
		}
	}

	void OnSceneLoaded(AsyncOperation ao)
	{
		transform.position = new Vector3(0, -1.5f, 0);
		loadingScene = false;
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

	public override void GainExp(int amount)
	{
		base.GainExp(amount);
		if(stats.exp >= 10)
		{
			stats.exp -= 10;
			stats.lvl--;
			stats.sp += 10;
			GameObject newWindow = Instantiate(pointWindow);
			newWindow.transform.SetParent(Camera.main.transform);
			newWindow.transform.localPosition = new Vector3(0f, 0f, 10f);
		}
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(2f);

		SceneManager.LoadScene("DemonLevel");
		Destroy(gameObject);
	}
}
