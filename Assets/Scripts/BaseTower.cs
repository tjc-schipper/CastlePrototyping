using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{

	[SerializeField]
	ProjectileAttack attack;

	[SerializeField]
	Transform attackOrigin;

	[SerializeField]
	[Range(0.01f, 10f)]
	float fireRate = 1f;

	[SerializeField]
	float range = 1f;

	private Coroutine cr;
	private Damageable currentTarget;

	private const float DETECTION_INTERVAL = 1f;

	private float detectionTimer = 0f;
	private float fireTimer = 0f;

	void Start()
	{
		this.cr = StartCoroutine(CR_TowerActive());
	}

	IEnumerator CR_TowerActive()
	{
		while (true)
		{
			this.detectionTimer -= Time.deltaTime;
			this.fireTimer -= Time.deltaTime;

			if (this.detectionTimer <= 0)
			{
				this.currentTarget = AcquireTarget();
				this.detectionTimer = DETECTION_INTERVAL;
			}

			if (this.fireTimer <= 0)
			{
				if (this.currentTarget != null)
				{
					ProjectileAttack newAttack = Instantiate<ProjectileAttack>(this.attack, this.transform.position, Quaternion.identity);
					newAttack.DoAttack(this.currentTarget);
					this.fireTimer = 1f / this.fireRate;
				}
			}

			yield return new WaitForEndOfFrame();
		}
	}

	/// <summary>
	/// Do an overlapSphere check to detect valid targets in range, and select the closest one to attack.
	/// </summary>
	/// <returns></returns>
	Damageable AcquireTarget()
	{
		Collider[] collidersInRange = Physics.OverlapSphere(this.transform.position, this.range, LayerMask.GetMask("Enemies"));
		if (collidersInRange != null && collidersInRange.Length > 0)
		{
			Damageable closest = null;
			Damageable current = null;
			float minDistance = Mathf.Infinity;
			float curDistance = 0f;
			for (int i = 0; i < collidersInRange.Length; i++)
			{
				current = collidersInRange[i].GetComponent<Damageable>();
				if (current == null) continue;

				curDistance = Vector3.SqrMagnitude(current.transform.position - this.transform.position);
				if (curDistance < minDistance)
				{
					minDistance = curDistance;
					closest = current;
				}
			}
			return closest;
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// Draw a circle around the tower indicating its range. Turns red if targets are in range.
	/// </summary>
	void OnDrawGizmosSelected()
	{
		Color prevColor = Gizmos.color;
		Gizmos.color = (this.currentTarget != null) ?
			Color.red :
			Color.green;

		int SEGMENTS = 16;
		float angleIncrement = (2f * Mathf.PI) / (float)SEGMENTS;
		Vector3 start, end;
		for (int i = 0; i < SEGMENTS; i++)
		{
			start = this.transform.position + this.range * new Vector3(
				Mathf.Sin(i * angleIncrement),
				0f,
				Mathf.Cos(i * angleIncrement));
			int j = (i + 1) % SEGMENTS;
			end = this.transform.position + this.range * new Vector3(
				Mathf.Sin(j * angleIncrement),
				0f,
				Mathf.Cos(j * angleIncrement));
			Gizmos.DrawLine(start, end);
		}

		Gizmos.color = prevColor;
	}
}
