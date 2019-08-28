using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed = 3f;

	private Camera camera;
	private Rigidbody2D rb;
	private Animator animator;

	private GameObject player;

	private bool dashing = false;
	private bool alive = true;

	void Start()
	{
		camera = Camera.main;
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		player = GameObject.FindWithTag("Player");

		animator.SetBool("walking", true);

		StartCoroutine(Attack());
	}

	void Update()
	{
	}

	void FixedUpdate()
	{
		if(alive && !dashing)
		{
			float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
			rb.velocity = new Vector2(direction * speed, rb.velocity.y);
			
			transform.localScale = new Vector3(-direction, 1, 1);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.tag == "PlayerWeapon")
		{
			float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
			rb.AddForce(new Vector2(-direction * 10f, 0f), ForceMode2D.Impulse);

			gameObject.SetLayer(9);
			alive = false;
			animator.SetTrigger("die");
		}
	}

	void OnDashEnded()
	{
		dashing = false;
	}

	IEnumerator Attack()
	{
		yield return new WaitForSeconds(Random.Range(0.5f, 1f));

		while(true)
		{
			if(alive)
			{
				animator.SetTrigger("swordAttack");
				dashing = true;
				yield return new WaitForSeconds(Random.Range(0.5f, 1f));
			}
			else
			{
				break;
			}
		}
	}
}
