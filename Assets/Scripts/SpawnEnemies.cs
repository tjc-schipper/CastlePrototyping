using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnEnemies : MonoBehaviour {

    public Transform[] spawnPoints;
    public GameObject[] enemies;
    public int[] nrEnemies;
    public Dictionary<GameObject, int> occurringEnemies = new Dictionary<GameObject, int>();
    
    public GameObject archer;
    public GameObject ogre;
    public int nrArchers = 5;
    public int nrOgres = 3;
    public SpawnUI spawnUI;


    // Use this for initialization
    void Start () {
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    occurringEnemies.Add(enemies[i], nrEnemies[i]);
        //}
        // InvokeRepeating("Spawn", 0.0f, 5.0f);
        if (spawnUI != null)
        {
            spawnUI = GetComponentInChildren<SpawnUI>();
            spawnUI.Create();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
    
	}

    public void Spawn(SpawnUnitGroup unitGroup) {
        SpawnUnit[] availableUnits = unitGroup.unitsToSpawn.Where(unit => unit.count > 0).ToArray();
//some loop?
            int unitToSpawn = Random.Range(0, availableUnits.Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(unitGroup.unitsToSpawn[unitToSpawn].unitType.prefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            unitGroup.unitsToSpawn[unitToSpawn].count--;
        
        //List<KeyValuePair<GameObject, int>> availableTypes = new List<KeyValuePair<GameObject, int>>();

        //foreach (KeyValuePair<GameObject, int> occurringEnemy in occurringEnemies)
        //{
        //    if (occurringEnemy.Value > 0)
        //    {
        //        availableTypes.Add(occurringEnemy);
        //    }
        //}

        //GameObject enemy = enemies[Random.Range(0, availableTypes.Count)];
        //Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        //if (enemy == archer)
        //{
        //    nrArchers--;
        //}
        //else
        //{
        //    nrOgres--;
        //}
       // yield return new WaitForSeconds(3);
    }

}
