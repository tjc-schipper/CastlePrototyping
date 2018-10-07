using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModMultiplier : DamageModifier
{

	private float multiplier;
	private int priority;

	public DamageModMultiplier(float _multiplier, int priority = 0)
	{
		this.multiplier = _multiplier;
		this.priority = priority;
	}

	public int Apply(int damage, Attack.DamageTypes dmgType)
	{
		return Mathf.RoundToInt(damage * this.multiplier);
	}

	public int GetPriority()
	{
		return this.priority;
	}
}
