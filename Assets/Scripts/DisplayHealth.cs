using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class DisplayHealth : MonoBehaviour
{

	[SerializeField]
	Renderer meshRenderer;

	private Damageable health;
	private int lastHP;

	void Start()
	{
		this.health = GetComponent<Damageable>();
		this.lastHP = this.health.HP;
	}

	void Update()
	{
		if (this.health.HP != this.lastHP)
		{
			this.lastHP = this.health.HP;
			float percent = (float)this.lastHP / (float)this.health.initialHP;
			Color c = Color.Lerp(Color.black, Color.white, percent);
			this.meshRenderer.material.color = c;
		}
	}
}
