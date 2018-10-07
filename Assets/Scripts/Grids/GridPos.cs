using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridPos
{

	public int x;
	public int z;

	public GridPos(int x, int z)
	{
		this.x = x;
		this.z = z;
	}

	private GridPos(Vector2 pos)
	{
		this.x = Mathf.RoundToInt(pos.x);
		this.z = Mathf.RoundToInt(pos.y);
	}

	private GridPos(Vector3 pos)
	{
		this.x = Mathf.RoundToInt(pos.x);
		this.z = Mathf.RoundToInt(pos.z);
	}

	#region Conversions

	public static implicit operator GridPos(Vector2 pos)
	{
		return new GridPos(pos);
	}

	public static implicit operator GridPos(Vector3 pos)
	{
		return new GridPos(pos);
	}

	#endregion

	#region Operators

	public static GridPos operator +(GridPos a, GridPos b)
	{
		return new GridPos(
			a.x + b.x,
			a.z + b.z
			);
	}

	public static GridPos operator -(GridPos a, GridPos b)
	{
		return new GridPos(
			a.x - b.x,
			a.z - b.z
			);
	}

	public static GridPos operator *(GridPos a, int b)
	{
		return new GridPos(
			a.x * b,
			a.z * b
			);
	}

	#endregion

	public static GridPos Zero
	{
		get
		{
			return new GridPos(0, 0);
		}
	}

	public static GridPos One
	{
		get
		{
			return new GridPos(1, 1);
		}
	}

}
