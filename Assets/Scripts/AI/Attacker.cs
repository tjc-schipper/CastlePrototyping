using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
	public Attack attack;
	public float attackRange;
	public float attackRate;
	public Transform attackSource;

	private Damageable currentTarget;

	private float cooldownTimer = 0f;


	void Update()
	{
		this.cooldownTimer -= Time.deltaTime;
	}

	/// <summary>
	/// Find the closest target
	/// </summary>
	/// <returns>True if valid target in range, false otherwise</returns>
	public bool AcquireTarget()
	{
		Collider[] targets = Physics.OverlapSphere(this.transform.position, this.attackRange, LayerMask.GetMask("Enemies"));
		Damageable d = null;
		Damageable closest = null;
		float minDist = Mathf.Infinity;
		float curDist;

		for (int i = 0; i < targets.Length; i++)
		{
			d = targets[i].GetComponent<Damageable>();
			if (d == null) continue;
			if (d.gameObject == this.gameObject) continue;

			curDist = GetRangeToTarget(d);
			if (curDist < minDist)
			{
				minDist = curDist;
				closest = d;
			}
		}

		if (closest != null)
		{
			this.currentTarget = closest;
			return true;
		}
		else
		{
			this.currentTarget = null;
			return false;
		}

	}

	public bool OnCooldown()
	{
		return this.cooldownTimer > 0f;
	}

	public bool Attack()
	{
		if (this.currentTarget == null) return false;

		// Check for stuns, freezes, etc
		Attack newAttack = Instantiate(this.attack);
		newAttack.transform.position = this.attackSource.position;
		newAttack.DoAttack(this.currentTarget);
		this.cooldownTimer = 1f / this.attackRate;
		return true;
	}

	/// <summary>
	/// Get linear range to target from own position
	/// </summary>
	/// <param name="d">Target to measure distance to</param>
	/// <returns></returns>
	private float GetRangeToTarget(Damageable d)
	{
		return Vector3.Distance(this.transform.position, d.transform.position);
	}


	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, this.attackRange);
	}
}