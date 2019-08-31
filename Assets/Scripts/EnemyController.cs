using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
	private GameObject player;

	protected override void Start()
	{
		base.Start();

		player = GameObject.FindWithTag("Player");

		animator.SetBool("walking", true);

		StartCoroutine(Attack());
	}

	void FixedUpdate()
	{
		if(stats.alive && !stats.dashing && Vector3.Distance(transform.position, player.transform.position) < 8f)
		{
			float direction = Mathf.Sign(player.transform.position.x - transform.position.x);
			rb.velocity = new Vector2(direction * stats.speed, rb.velocity.y);
			
			transform.localScale = new Vector3(-direction, 1, 1);
		}
	}

	IEnumerator Attack()
	{
		yield return new WaitForSeconds(Random.Range(0.5f, 1f));

		while(true)
		{
			if(stats.alive)
			{
				if (Vector3.Distance(transform.position, player.transform.position) < 8f)
				{
					weapon.Attack();
				}

				yield return new WaitForSeconds(Random.Range(0.5f, 1f) / stats.AttackSpeed);
			}
			else
			{
				break;
			}
		}
	}
}
