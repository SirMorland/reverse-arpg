using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public List<string> skills;

	private CharacterController ch;

	void Start()
	{
		ch = GetComponentInParent<CharacterController>();

		if(skills == null)
			skills = new List<string>();
	}

	public void Attack()
	{
		ch.animator.SetFloat("attackSpeed", ch.stats.AttackSpeed);
		ch.animator.SetTrigger("swordAttack");
		ch.stats.dashing = true;

		if(skills.Contains("dash"))
		{
			ch.rb.velocity = new Vector3(0f, ch.rb.velocity.y);
			ch.rb.AddForce(
				new Vector2(ch.transform.localScale.x * -20f, 0f),
				ForceMode2D.Impulse
			);
		}
	}

	public void DealDamage(GameObject target)
	{
		Stats stats = target.GetComponent<Stats>();

		if(!stats.dashing)
		{
			float damage = ch.stats.Damage;
			if (stats.armor != null)
				damage -= stats.armor.armorValue;

			stats.currentHp -= Mathf.Max(0f, damage);

			float direction = Mathf.Sign(target.transform.position.x - transform.position.x);

			target.GetComponent<Rigidbody2D>().AddForce(
				new Vector2(-direction * 10f, 0f),
				ForceMode2D.Impulse
			);

			if (stats.currentHp <= 0)
			{
				stats.currentHp = 0;

				ch.GainExp(stats.exp);

				target.SetLayer(9);
				stats.alive = false;
				Animator animator = target.GetComponent<Animator>();
				animator.SetFloat("direction", target.transform.localScale.x);
				animator.SetBool("dead", true);
			}
			else if(damage > 0)
			{
				stats.dashing = true;
				target.GetComponent<Animator>().SetTrigger("hurt");
			}
		}
	}
}
