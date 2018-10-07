using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack {

	[SerializeField]
	float debug_Duration = 0.25f;
	
	float age = 0f;

	public override void DoAttack(Damageable target)
	{
		LineRenderer lr = this.gameObject.AddComponent<LineRenderer>();
		lr.startWidth = 0.05f;
		lr.endWidth = 0.05f;
		lr.useWorldSpace = true;
		lr.SetPositions(new Vector3[]
		{
			this.transform.position,
			target.transform.position
		});

		base.DoAttack(target);
	}
	
	void Update()
	{
		this.age += Time.deltaTime;
		if (this.age > this.debug_Duration)
		{
			Destroy(this.gameObject);
		}
	}
}