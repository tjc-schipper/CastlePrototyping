using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class SphereTasks : MonoBehaviour
{

	[SerializeField]
	float moveSpeed = 1f;

	[SerializeField]
	float runSpeed = 2f;

	[SerializeField]
	float threatRadius = 2f;

	[SerializeField]
	float fleeDistance = 5f;

	[SerializeField]
	float goalRange = 50f;

	[SerializeField]
	float markDistance = 3f;

	[SerializeField]
	Transform anchorTransform;

	[SerializeField]
	float randomPositionRange = 5f;

	private const float ARRIVED_THRESHOLD = 0.05f;

	private Collider mark;
	private Collider closestThreat;
	private Collider[] nearbyThreats;

	private System.Nullable<Vector3> targetPosition;
	private bool moving = false;
	private bool running = false;



	[Task]
	void GetNearbyThreats()
	{
		this.nearbyThreats = Physics.OverlapSphere(this.transform.position, this.threatRadius, LayerMask.GetMask("Triangles"));
		Task.current.Complete(this.nearbyThreats != null && this.nearbyThreats.Length > 0);
	}

	[Task]
	bool IsSafe()
	{
		return (this.nearbyThreats != null && this.nearbyThreats.Length == 0);
	}

	[Task]
	bool HasTargetPos()
	{
		return (this.targetPosition != null && this.targetPosition.HasValue);
	}

	[Task]
	bool IsAtTargetPos()
	{
		return this.targetPosition == null ||
			(this.targetPosition.HasValue && Vector3.Distance(this.targetPosition.Value, this.transform.position) <= ARRIVED_THRESHOLD);
	}

	[Task]
	void SetMoving()
	{
		this.moving = true;
		Task.current.Succeed();
	}

	[Task]
	void GetTargetPos()
	{
		// Find all nearby squares
		Collider[] squares = Physics.OverlapSphere(this.transform.position, this.goalRange, LayerMask.GetMask("Squares"));
		if (squares == null || squares.Length == 0)
		{
			Task.current.Fail();
			return;
		}

		// Find the closest square...
		float curDist = 0f;
		float minDist = Mathf.Infinity;
		Collider closest = null;

		for (int i = 0; i < squares.Length; i++)
		{
			curDist = Vector3.SqrMagnitude(squares[i].transform.position - this.transform.position);
			if (curDist < minDist)
			{
				minDist = curDist;
				closest = squares[i];
			}
		}

		//...and set it as target position
		this.targetPosition = closest.transform.position;
		Task.current.Succeed();
	}

	[Task]
	void GetClosestThreat()
	{
		float curDist = 0f;
		float minDist = Mathf.Infinity;
		this.closestThreat = null;

		for (int i = 0; i < this.nearbyThreats.Length; i++)
		{
			curDist = Vector3.SqrMagnitude(this.nearbyThreats[i].transform.position - this.transform.position);
			if (curDist < minDist)
			{
				minDist = curDist;
				this.closestThreat = this.nearbyThreats[i];
			}
		}

		Task.current.Complete(this.closestThreat != null);
	}

	[Task]
	void GetSafePosition()
	{
		Vector3 dir = (this.transform.position - this.closestThreat.transform.position).normalized;
		this.targetPosition = this.closestThreat.transform.position + dir * this.fleeDistance;
		Task.current.Succeed();
	}

	[Task]
	void RunToPosition()
	{
		if (this.targetPosition == null || !this.targetPosition.HasValue)
		{
			Task.current.Fail();
			return;
		}

		// NOTE: DON'T USE SetMoving() because it'll succeed the task! PITFALL!
		this.moving = true;
		this.running = true;

		// Update() will handle the movement itself, we just need to check for arrival

		if (IsAtTargetPos())
		{
			this.running = false;
			Task.current.Succeed();
		}
	}

	[Task]
	void StopMoving()
	{
		this.targetPosition = null;
		this.moving = false;
		Task.current.Succeed();
	}



	[Task]
	bool HasMark()
	{
		return this.mark != null;
	}

	[Task]
	bool StillInRangeOfMark()
	{
		return this.mark != null &&
			Vector3.Distance(this.mark.transform.position, this.transform.position) <= this.markDistance;
	}

	[Task]
	void GetNewMark()
	{
		Collider[] circles = Physics.OverlapSphere(this.transform.position, this.markDistance, LayerMask.GetMask("Spheres"));
		if (circles == null || circles.Length == 0)
		{
			Task.current.Fail();
			return;
		}

		float curDist = 0f;
		float minDist = Mathf.Infinity;

		for (int i = 0; i < circles.Length; i++)
		{
			curDist = Vector3.SqrMagnitude(circles[i].transform.position - this.transform.position);
			if (curDist < minDist)
			{
				minDist = curDist;
				this.mark = circles[i];
			}
		}

		Task.current.Complete(this.mark != null);
	}

	[Task]
	void GetRandomTargetPos()
	{
		Vector3 pos = this.anchorTransform.position;
		Vector3 offset = Vector3.ProjectOnPlane(Random.insideUnitSphere, Vector3.up);
		pos += offset * randomPositionRange;

		this.targetPosition = pos;
		Task.current.Succeed();
	}



	void Update()
	{
		if (this.moving)
		{
			if (this.mark != null)
			{
				this.transform.position += (this.mark.transform.position - this.transform.position).normalized * moveSpeed * Time.deltaTime;
			}
			else
			{
				if (this.targetPosition != null && this.targetPosition.HasValue)
				{
					float speed = (this.running) ?
						this.runSpeed :
						this.moveSpeed;
					this.transform.position += (this.targetPosition.Value - this.transform.position).normalized * speed * Time.deltaTime;
				}
			}
		}
	}
}
