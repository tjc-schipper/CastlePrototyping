using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModIncrease : DamageModifier
{

	private int increase;
	private int priority;

	public DamageModIncrease(int _increase, int _priority = 1)
	{
		this.increase = _increase;
		this.priority = _priority;
	}

	public int Apply(int damage, Attack.DamageTypes dmgType)
	{
		return damage + this.increase;
	}

	public int GetPriority()
	{
		return this.priority;
	}
}
