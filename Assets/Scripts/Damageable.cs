using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Damageable : MonoBehaviour
{

	public delegate void DamageEvent(Damageable sender, int damage, int remainingHP);
	public delegate void DestroyedEvent(Damageable sender);
	public event DestroyedEvent Destroyed;
	public event DamageEvent TookDamage;

	public int initialHP = 1;

	public int HP
	{
		get; private set;
	}
	public int hitsTaken
	{
		get; private set;
	}
	private List<DamageModifier> modifiers;


	void Awake()
	{
		this.modifiers = new List<DamageModifier>();
	}

	public void Init()
	{
		this.HP = this.initialHP;
		this.hitsTaken = 0;
		HealthBarsUI.RegisterDamageable(this);
	}

	public void ApplyDamage(int dmg)
	{
		ApplyDamage(dmg, Attack.DamageTypes.BASIC);
	}

	public void ApplyDamage(int dmg, Attack.DamageTypes damageType)
	{
		this.hitsTaken++;

		int damageAfterModifiers = dmg;
		for (int i = 0; i < this.modifiers.Count; i++)
		{
			damageAfterModifiers = this.modifiers[i].Apply(damageAfterModifiers, damageType);
		}

		this.HP = Mathf.Max(this.HP - damageAfterModifiers, 0);

		// Fire events
		if (this.HP <= 0)
		{
			if (Destroyed != null)
				Destroyed.Invoke(this);
		}
		else
		{
			if (TookDamage != null)
				TookDamage.Invoke(this, damageAfterModifiers, this.HP);
		}

	}

	public bool IsAlive()
	{
		return (this.HP > 0);
	}


	public void AddModifier(DamageModifier mod)
	{
		int modPriority = mod.GetPriority();
		if (this.modifiers.Count > 0)
		{
			int insertPoint = this.modifiers.FindLastIndex((DamageModifier m) =>
			{
				return (m.GetPriority() == modPriority);
			});
			this.modifiers.Insert(insertPoint, mod);
		}
		else
		{
			this.modifiers.Add(mod);
		}
	}

	public void RemoveModifier(DamageModifier mod)
	{
		this.modifiers.Remove(mod);
	}




	#region MOCKUP OF EVENT-LIKE DAMAGE SYSTEM
	/*
	public void ApplyAttack(AttackInstance a)
	{
		AttackInstance modified = a;
		foreach (DamageModifier mod in this.modifiers)
		{
			modified = mod.Apply(a);
		}
		this.ApplyDamage(modified.damagePool);
	}

	public class AttackInstance
	{
		public enum DamageTypes
		{
			NORMAL = 0,
			PIERCING,
			MAGIC
		}

		public DamageTypes dmgType;
		public int baseDamage;
		public Attacker attacker;
		public Damageable target;

		public int statusEffectsToApply;
	}
	*/
	#endregion
}
