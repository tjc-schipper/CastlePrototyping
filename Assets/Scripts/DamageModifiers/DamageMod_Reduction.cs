using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModReduction : DamageModifier
{

	private int reduction;
	private int priority;


	public DamageModReduction(int _reduction, int priority = 1)
	{
		this.reduction = _reduction;
		this.priority = priority;
	}


	public int Apply(int damage, Attack.DamageTypes dmgType)
	{
		return Mathf.Max(damage - this.reduction, 0);
	}

	public int GetPriority()
	{
		return this.priority;
	}
}
