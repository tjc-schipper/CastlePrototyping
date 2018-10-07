using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{

	private GridPos gridPos;
	public GridPos GridPos
	{
		get
		{
			return new GridPos(this.gridPos.x, this.gridPos.z);
		}
	}

	public enum CellStates
	{
		PASSABLE = 0,
		BLOCKED
	}

	public CellStates CellState
	{
		get; private set;
	}

	private List<GameObject> objectsInCell;

	public GridCell(int x, int z) : this(new GridPos(x, z)) { }

	public GridCell(GridPos pos)
	{
		this.gridPos = pos;
		this.objectsInCell = new List<GameObject>();
	}

	public GameObject[] GetObjectsInCell()
	{
		return this.objectsInCell.ToArray();
	}

	public void AddObjectToCell(GameObject obj)
	{
		if (!this.objectsInCell.Contains(obj))
			this.objectsInCell.Add(obj);
	}

	public void RemoveObjectFromCell(GameObject obj)
	{
		this.objectsInCell.Remove(obj);
	}
}
