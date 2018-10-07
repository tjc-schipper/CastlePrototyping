using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

	[SerializeField]
	GameObject prefab_Unit;

	[SerializeField] GridPos pos;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			Spawn(this.pos);
	}

	void Spawn(GridPos pos)
	{
		Vector3 worldPos;
		if (GridManager.instance.GridToWorldPos(pos, out worldPos))
		{
			GameObject newUnit = Instantiate(prefab_Unit, worldPos, Quaternion.identity);
			GridManager.instance.RegisterObject(newUnit);
			newUnit.GetComponent<GridMover>().Init(pos);
		}
	}
}
