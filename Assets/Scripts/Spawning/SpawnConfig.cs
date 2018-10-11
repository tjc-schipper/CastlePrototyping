using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConfig : MonoBehaviour {

    [SerializeField] SpawnWave[] waves;
    [SerializeField] Lane.Lanes lane;

    [System.Serializable]
    public class SpawnWave
    {
        public UnitEntry[] unitsToSpawn;
    }

    [System.Serializable]
    public class UnitEntry
    {
        public int count;
        public ScriptableObject UnitType;
    }

}
