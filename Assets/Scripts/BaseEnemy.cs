using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Damageable), typeof(Rigidbody), typeof(NavMeshAgent))]
public class BaseEnemy : MonoBehaviour
{

	[SerializeField]
	Transform moveTarget;
	
	private Damageable health;
	private Rigidbody rb;
	
	void Start()
	{
		this.rb = GetComponent<Rigidbody>();
		this.health = GetComponent<Damageable>();
		this.health.Init();
		this.health.Destroyed += HandleDestroyed;

		GetComponent<NavMeshAgent>().SetDestination(this.moveTarget.position);

	}

	void OnDestroy()
	{
		this.health.Destroyed -= HandleDestroyed;
	}
	

	private void HandleDestroyed(Damageable d)
	{
		Destroy(this.gameObject);
	}

}
