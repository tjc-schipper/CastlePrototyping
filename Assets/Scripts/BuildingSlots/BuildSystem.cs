using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour {

    [SerializeField] BuildableSlot[] slots;
    [SerializeField] BuildableSet buildableSet;

    public void Build(Buildable buildable, BuildableSlot slot)
    {
        if (slot.SlotState == BuildableSlot.SlotStates.EMPTY)
        {
            GameObject newTower = Instantiate(buildable.Prefab, slot.transform.position, slot.transform.rotation);
            slot.AssignContent(newTower);
        }
    }
}
