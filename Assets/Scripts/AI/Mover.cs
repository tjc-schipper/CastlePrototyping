using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class Mover : MonoBehaviour
{

	private Vector3 targetPosition;
	private NavMeshAgent agent;

	private const float ARRIVED_THRESHOLD = 0.05f;

	void Awake()
	{
		this.agent = GetComponent<NavMeshAgent>();
	}


	public bool SetDestination(Vector3 worldPosition)
	{
		this.targetPosition = worldPosition;
		NavMeshPath path = new NavMeshPath();
		if (this.agent.CalculatePath(this.targetPosition, path))
		{
			this.agent.SetPath(path);
			return true;
		}
		else
		{
			return false;
		}
	}

	public void ClearDestination()
	{
		this.agent.ResetPath();
	}

	public bool IsAtDestination()
	{
		return (Vector3.Distance(this.transform.position, this.targetPosition) <= ARRIVED_THRESHOLD);
	}
	

	public bool HasPath()
	{
		return this.agent.hasPath;
	}


	public void StartMoving()
	{
		if (HasPath())
			this.agent.isStopped = false;
	}

	public void StopMoving()
	{
		this.agent.isStopped = true;
	}

	public bool IsMoving()
	{
		return (this.agent.hasPath && !this.agent.isStopped);
	}
}
