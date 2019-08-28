using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public float damage = 50f;

	public void Attack(GameObject attacker)
	{
		attacker.GetComponent<Animator>().SetTrigger("swordAttack");
		attacker.GetComponent<Stats>().dashing = true;
		attacker.GetComponent<Rigidbody2D>().AddForce(
			new Vector2(attacker.transform.localScale.x * -20f, 0f),
			ForceMode2D.Impulse
		);
	}

	public void DealDamage(GameObject target)
	{
		Stats stats = target.GetComponent<Stats>();

		if(!stats.dashing)
		{
			stats.currentHp -= (damage - stats.armor);

			float direction = Mathf.Sign(target.transform.position.x - transform.position.x);
			target.GetComponent<Rigidbody2D>().AddForce(
				new Vector2(-direction * 10f, 0f),
				ForceMode2D.Impulse
			);

			if (stats.currentHp <= 0)
			{
				stats.currentHp = 0;

				target.SetLayer(9);
				stats.alive = false;
				Animator animator = target.GetComponent<Animator>();
				animator.SetFloat("direction", target.transform.localScale.x);
				animator.SetBool("dead", true);
			}
			else
			{
				stats.dashing = true;
				target.GetComponent<Animator>().SetTrigger("hurt");
			}
		}
	}
}
