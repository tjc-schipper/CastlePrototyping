using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceAmount
{

    public int materials;
    public int gems;
    public int gold;
    public int energy;


    public bool HasBelowZero()
    {
        return
            this.materials < 0 ||
            this.gems < 0 ||
            this.gold < 0 ||
            this.energy < 0;
    }

    public static ResourceAmount operator -(ResourceAmount a, ResourceAmount b)
    {
        ResourceAmount result = new ResourceAmount
        {
            materials = a.materials - b.materials,
            gems = a.gems - b.gems,
            gold = a.gold - b.gold,
            energy = a.energy - b.energy
        };
        return result;
    }

    public static ResourceAmount operator +(ResourceAmount a, ResourceAmount b)
    {
        ResourceAmount result = new ResourceAmount
        {
            materials = a.materials + b.materials,
            gems = a.gems + b.gems,
            gold = a.gold + b.gold,
            energy = a.energy + b.energy
        };
        return result;
    }

}
