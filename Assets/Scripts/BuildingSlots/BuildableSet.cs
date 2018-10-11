using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ING/SO/BuildableSet")]
public class BuildableSet : ScriptableObject
{

    [SerializeField] private List<Buildable> buildables;

    public Buildable GetBuildable(string id)
    {
        Buildable match = this.buildables.Find((Buildable b) =>
        {
            return b.ID.Equals(id);
        });

        if (match == null)
            Debug.LogWarning("Requested buildable with id '" + id + "' that does not exist!", this);

        return match;
    }

}
