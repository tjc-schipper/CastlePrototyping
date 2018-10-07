using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	
	public enum DamageTypes
	{
		BASIC = 0,
		MAGIC,
		EXPLOSIVE,
		FIRE
	}

	public int damage;
	public DamageTypes damageType;

	public virtual void DoAttack(Damageable d)
	{
		d.ApplyDamage(this.damage, this.damageType);
	}
	
}
