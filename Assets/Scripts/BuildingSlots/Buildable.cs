using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ING/SO/Buildable")]
public class Buildable : ScriptableObject
{

    [SerializeField] string displayName;
    [SerializeField] string id;

    [SerializeField] GameObject prefab;

    [SerializeField] Sprite buildMenuIcon;

    [SerializeField] int cost;


    public int Cost
    {
        get
        {
            return this.cost;
        }
    }

    public string DisplayName
    {
        get
        {
            return this.displayName;
        }
    }

    public string ID
    {
        get
        {
            return this.id;
        }
    }

    public GameObject Prefab
    {
        get
        {
            return this.prefab;
        }
    }

    public Sprite BuildMenuIcon
    {
        get
        {
            return this.buildMenuIcon;
        }
    }

}
