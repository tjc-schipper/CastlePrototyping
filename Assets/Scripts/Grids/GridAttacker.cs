using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAttacker : MonoBehaviour {

	[SerializeField]
	int attackRange = 1;

	[SerializeField]
	int attackDamage = 1;

	[SerializeField]
	float attackRate = 1.0f;


	private float attackTimer = 0f;
	private Damageable currentTarget;


	void LateUpdate()
	{
		if (this.currentTarget != null)
		{
			attackTimer -= Time.deltaTime;
			if (attackTimer <= 0f)
			{
			}
		}
	}

	public void DoAttack(Damageable d)
	{

	}

	private bool StillInRange()
	{
		return
			this.currentTarget != null &&
			(Vector3.Distance(this.transform.position, this.currentTarget.transform.position) <= this.attackRange);
	}
}
