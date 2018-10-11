using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{

    public ResourceAmount playerResources;

    /// <summary>
    /// Returns whether a player has enough resources to pay for 'cost'.
    /// </summary>
    /// <param name="cost"></param>
    /// <returns></returns>
    public bool CanAfford(ResourceAmount cost)
    {
        return !(this.playerResources - cost).HasBelowZero();
    }

    /// <summary>
    /// Try to subtract a resource amount from the player's resources. Will return false if insufficient funds.
    /// </summary>
    /// <param name="cost">The amount to subtract.</param>
    /// <returns></returns>
    public bool Pay(ResourceAmount cost)
    {
        if (CanAfford(cost))
        {
            this.playerResources -= cost;
            return true;
        }
        else
            return false;
    }


    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(5f, 5f, 120f, 100f));

        GUI.contentColor = (this.playerResources.materials >= 0) ? Color.white : Color.red;
        GUILayout.Label("Materials: " + this.playerResources.materials.ToString());

        GUI.contentColor = (this.playerResources.gems >= 0) ? Color.white : Color.red;
        GUILayout.Label("Gems: " + this.playerResources.gems.ToString());

        GUI.contentColor = (this.playerResources.gold >= 0) ? Color.white : Color.red;
        GUILayout.Label("Gold: " + this.playerResources.gold.ToString());

        GUI.contentColor = (this.playerResources.energy >= 0) ? Color.white : Color.red;
        GUILayout.Label("Energy: " + this.playerResources.energy.ToString());

        GUILayout.EndArea();
    }
}
