using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GridManager : MonoBehaviour
{

	public enum GridCardinals
	{
		FORWARD,
		BACKWARD,
		LEFT,
		RIGHT
	}

	[SerializeField]
	int sizeX = 10;
	[SerializeField]
	int sizeZ = 10;

	[SerializeField]
	float cellSize = 1f;

	private new BoxCollider collider;
	private GridCell[,] cells;

	private static GridManager _instance;
	public static GridManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectsOfType<GridManager>()[0];
			}
			return _instance;
		}
	}


	void Awake()
	{
		this.collider = GetComponent<BoxCollider>();
		this.cells = new GridCell[this.sizeX, this.sizeZ];
		for (int x = 0; x < this.sizeX; x++)
		{
			for (int z = 0; z < this.sizeZ; z++)
			{
				this.cells[x, z] = new GridCell(x, z);
			}
		}
	}

	public void RegisterObject(GameObject obj)
	{
		GridPos gridPos;
		if (WorldToGridPos(obj.transform.position, out gridPos))
		{
			this.cells[gridPos.x, gridPos.z].AddObjectToCell(obj);
		}
	}

	public bool GetCell(GridPos gridPos, out GridCell cell)
	{
		if (IsValidGridPos(gridPos))
		{
			cell = this.cells[gridPos.x, gridPos.z];
			return true;
		}
		else
		{
			cell = null;
			return false;
		}
	}

	public List<GridCell> GetCells(GridPos basePos, GridCardinals direction)
	{
		return GetCells(basePos, direction, Mathf.Max(this.sizeX, this.sizeZ));
	}

	public List<GridCell> GetCells(GridPos basePos, GridCardinals direction, int distance)
	{
		GridPos dir = GridPos.Zero;
		switch (direction)
		{
			case GridCardinals.FORWARD:
				dir = new GridPos(0, -1);
				break;
			case GridCardinals.BACKWARD:
				dir = new GridPos(0, 1);
				break;
			case GridCardinals.LEFT:
				dir = new GridPos(1, 0);
				break;
			case GridCardinals.RIGHT:
				dir = new GridPos(-1, 0);
				break;
		}

		List<GridCell> list = new List<GridCell>();

		int count = 0;
		GridPos cellPos = basePos;
		while (count < distance)
		{
			cellPos = basePos + dir * count;
			if (!IsValidGridPos(cellPos)) break;
			list.Add(this.cells[cellPos.x, cellPos.z]);
			count++;
		}

		return list;
	}


	public bool GridToWorldPos(GridPos gridPos, out Vector3 worldPos)
	{
		if (!IsValidGridPos(gridPos))
		{
			worldPos = Vector3.zero;
			return false;
		}
		else
		{
			worldPos = new Vector3(
				this.transform.position.x + gridPos.x * this.cellSize,
				this.transform.position.y,
				this.transform.position.z + gridPos.z * this.cellSize
				);
			return true;
		}
	}

	public bool WorldToGridPos(Vector3 worldPos, out GridPos gridPos)
	{
		Vector3 deltaPos = Vector3.ProjectOnPlane(worldPos - this.transform.position, this.transform.up);
		GridPos gp = new GridPos(
			Mathf.RoundToInt(deltaPos.x / this.cellSize),
			Mathf.RoundToInt(deltaPos.z / this.cellSize)
			);

		if (IsValidGridPos(gp))
		{
			gridPos = gp;
			return true;
		}
		else
		{
			gridPos = GridPos.Zero;
			return false;
		}
	}

	public bool IsValidGridPos(GridPos gridPos)
	{
		return
			gridPos.x >= 0 &&
			gridPos.z >= 0 &&
			gridPos.x < this.sizeX &&
			gridPos.z < this.sizeZ;
	}



	void OnDrawGizmos()
	{
		Vector3 size = new Vector3(this.sizeX * this.cellSize, 0.1f, this.sizeZ * this.cellSize);
		Gizmos.DrawWireCube(
			this.transform.position + size / 2f - (new Vector3(this.cellSize, 0f, this.cellSize) / 2f),
			size);
	}

	void OnDrawGizmosSelected()
	{
		for (int x = 0; x < this.sizeX; x++)
		{
			for (int z = 0; z < this.sizeZ; z++)
			{

				Gizmos.color = (Application.isPlaying && this.cells[x, z].GetObjectsInCell().Length > 0) ? Color.blue : Color.white;
				Gizmos.DrawCube(this.transform.position + new Vector3(x * this.cellSize, 0f, z * this.cellSize), Vector3.one * 0.5f);
			}
		}
	}


}
