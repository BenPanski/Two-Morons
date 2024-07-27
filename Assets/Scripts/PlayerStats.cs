using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    List<Upgrade> MyUpgrades = new List<Upgrade>();
    public void AddUpgrade(Upgrade upgrade)
    {
        MyUpgrades.Add(upgrade);
        // Add the upgrade to the player's stats
    }
   
}
