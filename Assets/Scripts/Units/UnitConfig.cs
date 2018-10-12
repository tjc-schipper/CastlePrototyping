using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ING/Units/UnitConfig")]
public class UnitConfig : ScriptableObject {

    public GameObject prefab;
    [SerializeField] int cost;
    [SerializeField] string displayName;
    [SerializeField] Sprite icon;

}
