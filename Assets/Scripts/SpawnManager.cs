using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public AttackConfig attackConfig;
    public GameObject[] lanes;
    // Use this for initialization
    void Start()
    {
        foreach (AttackWave wave in attackConfig.waves)
        {
            foreach (SpawnUnitGroup unitGroup in wave.unitGroups)
            {
                switch (unitGroup.lane)
                {
                    case Lane.Lanes.LEFT:
                        print("Spawn enemies in left lane");
                        var lane = lanes[0];
                        lanes[0].GetComponent<SpawnEnemies>().Spawn(unitGroup);
                        break;
                    case Lane.Lanes.MID:
                       // print("MID");
                         break;
                    case Lane.Lanes.RIGHT:
                       // print("RIGHT");
                        break;
                }


            }
            //spawn units at lane
        }
        }
    

    // Update is called once per frame
    void Update()
    {

    }

}
