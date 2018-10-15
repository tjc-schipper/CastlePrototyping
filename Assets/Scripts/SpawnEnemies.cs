using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnEnemies : MonoBehaviour
{

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 0.125f;
    [SerializeField] private SpawnUI spawnUI;

    private Queue<UnitConfig> spawnQueue;
    private Coroutine cr_DoSpawns;


    private void Awake()
    {
        this.spawnQueue = new Queue<UnitConfig>();
    }

    void Start()
    {
        if (this.spawnUI == null)
        {
            Debug.LogError("SpawnUI has not been assigned to SpawnEnemies", this.gameObject);
            return;
        }

        this.spawnUI.Create();
    }

    /// <summary>
    /// Tell this Spawner to spawn a list of units in order of appearance in the unitGroup
    /// </summary>
    /// <param name="unitGroup"></param>
    /// <param name="clearRemaining"></param>
    public void Spawn(SpawnUnitGroup unitGroup, bool clearRemaining = false)
    {
        if (clearRemaining)
            this.spawnQueue.Clear();

        // Add each instance of a spawnable unit to the spawnQueue
        foreach (SpawnUnit spawnUnit in unitGroup.unitsToSpawn)
        {
            for (int i = 0; i < spawnUnit.count; i++)
            {
                this.spawnQueue.Enqueue(spawnUnit.unitType);
            }
        }

        if (this.cr_DoSpawns == null)
            this.cr_DoSpawns = StartCoroutine(CR_SpawnUnits());
    }

    private IEnumerator CR_SpawnUnits()
    {
        int spawnPointIndex = 0;
        while (this.spawnQueue.Count > 0)
        {
            UnitConfig unitToSpawn = this.spawnQueue.Dequeue();
            GameObject.Instantiate(unitToSpawn.prefab, this.spawnPoints[spawnPointIndex].position, Quaternion.identity);
            spawnPointIndex = (spawnPointIndex + 1) % this.spawnPoints.Length;

            yield return new WaitForSeconds(this.spawnInterval);
        }
    }

}
