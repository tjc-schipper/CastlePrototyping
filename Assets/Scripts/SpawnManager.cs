using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] AttackConfig currentAttackConfig;
    public SpawnEnemies[] spawners;

    void Start()
    {
        SpawnCurrentConfig();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCurrentConfig();
        }
    }

    void SpawnCurrentConfig()
    {
        foreach (AttackWave wave in currentAttackConfig.waves)
        {
            foreach (SpawnUnitGroup unitGroup in wave.unitGroups)
            {
                spawners[(int)unitGroup.lane].Spawn(unitGroup);
            }
        }
    }

}
