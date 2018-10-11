using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Armor : MonoBehaviour
{

    [SerializeField]
    int armorStrength = 2;

    DamageModifier mod;

    void Start()
    {
        Damageable t = GetComponent<Damageable>();
        if (t != null)
        {
            this.mod = new DamageModReduction(this.armorStrength);
            t.AddModifier(this.mod);
        }
    }

    void OnDisable()
    {
        Damageable t = GetComponent<Damageable>();
        if (t != null && this.mod != null)
        {
            t.RemoveModifier(this.mod);
            this.mod = null;
        }
    }
}
