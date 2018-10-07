using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{

	private GridPos gridPos;
	private GridPos targetPos;

	[SerializeField]
	float moveSpeed = 1f;

	private const float ARRIVED_THRESHOLD = 0.05f;

	public void Init(GridPos pos)
	{
		this.gridPos = pos;
	}

	void Update()
	{
		List<GridCell> cells = null;
		if (Input.GetKeyDown(KeyCode.UpArrow))
			cells = GridManager.instance.GetCells(this.gridPos, GridManager.GridCardinals.FORWARD);
		if (Input.GetKeyDown(KeyCode.DownArrow))
			cells = GridManager.instance.GetCells(this.gridPos, GridManager.GridCardinals.BACKWARD);
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			cells = GridManager.instance.GetCells(this.gridPos, GridManager.GridCardinals.LEFT);
		if (Input.GetKeyDown(KeyCode.RightArrow))
			cells = GridManager.instance.GetCells(this.gridPos, GridManager.GridCardinals.RIGHT);

		if (cells != null)
		{
			SetTargetPos(cells[cells.Count - 1].GridPos);
		}
	}

	void LateUpdate()
	{
		if (this.targetPos != null)
		{
			Vector3 worldPos;
			if (GridManager.instance.GridToWorldPos(targetPos, out worldPos))
			{
				Vector3 deltaPos = worldPos - this.transform.position;
				this.transform.position += deltaPos.normalized * this.moveSpeed * Time.deltaTime;

				// Update position on grid
				GridPos newGridPos;
				if (GridManager.instance.WorldToGridPos(this.transform.position, out newGridPos))
				{
					if (!newGridPos.Equals(this.gridPos))
					{
						GridCell cell;
						if (GridManager.instance.GetCell(this.gridPos, out cell))
							cell.RemoveObjectFromCell(this.gameObject);
						if (GridManager.instance.GetCell(newGridPos, out cell))
							cell.AddObjectToCell(this.gameObject);

						this.gridPos = newGridPos;
					}
				}

				// If we are close enough to arriving, stop moving :)
				if (Vector3.Distance(this.transform.position, worldPos) <= ARRIVED_THRESHOLD)
				{
					this.transform.position = worldPos;
					this.targetPos = null;
				}
			}
		}
	}


	public void SetTargetPos(GridPos targetPos)
	{
		this.targetPos = targetPos;
	}

}
