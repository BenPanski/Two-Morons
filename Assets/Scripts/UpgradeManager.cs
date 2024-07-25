using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }
    // has a list of all avalable upgrades
    List<Upgrade> upgrades = new List<Upgrade>();


    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void GiveUpgrade(Upgrade upgrade, int playerIndex)
    {
        // Give the selected upgrade to the player
    }

}
