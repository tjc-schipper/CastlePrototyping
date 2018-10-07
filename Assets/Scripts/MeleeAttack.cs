using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeleeAttack : Attack
{

	[SerializeField]
	float debug_Duration = 0.25f;

	float timer = 0f;

	public override void DoAttack(Damageable target)
	{
		base.DoAttack(target);
		this.transform.DOPunchScale(Vector3.one * 1.25f, this.debug_Duration)
			.OnComplete(() =>
			{
				Destroy(this.gameObject);
			});
	}

}
