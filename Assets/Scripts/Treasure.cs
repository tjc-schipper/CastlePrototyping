using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Treasure : MonoBehaviour {

    private Damageable treasureHP;

    private void Start()
    {
        this.treasureHP = this.gameObject.GetComponent<Damageable>();
        HealthBarsUI.RegisterDamageable(this.treasureHP);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.gameObject.layer.Equals("Enemies"))
        {
            Destroy(collision.rigidbody.gameObject);
            treasureHP.ApplyDamage(1);
        }
    }
}
