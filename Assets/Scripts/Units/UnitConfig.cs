using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ING/Units/UnitConfig")]
public class UnitConfig : ScriptableObject {

    [SerializeField] GameObject prefab;
    [SerializeField] int cost;
    [SerializeField] string displayName;
    [SerializeField] Sprite icon;

}
