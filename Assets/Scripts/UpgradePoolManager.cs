using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePoolManager : MonoBehaviour
{
    public static UpgradePoolManager Instance { get; private set; }
    // has a list of all avalable upgrades
    [SerializeField] List<Upgrade> upgradesPool = new List<Upgrade>();
    [SerializeField] int UpgradeSelections = 4;
    [HideInInspector] public List<Upgrade> UpgradesToSend; // temp

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
    public List<Upgrade> GiveUpgrade()
    {
        if (UpgradeSelections > upgradesPool.Count)
        {
            Debug.LogError("Not enough upgrades in the pool");
            return null;
        }

        List<Upgrade> UpgradePoolTemp = upgradesPool;   // create a temp version of upgrades pool
        UpgradesToSend = new List<Upgrade>();        // create a list of upgrades to send

        for (int i = 0; i < UpgradeSelections; i++)
        {
            int randomIndex = Random.Range(0, UpgradePoolTemp.Count - 1); // get a random index from the temp upgrade pool
            UpgradesToSend.Add(UpgradePoolTemp[randomIndex]); // add the upgrade to the list of upgrades to send
            UpgradePoolTemp.RemoveAt(randomIndex); // remove the upgrade from the temp upgrade pool

        }
        if (UpgradesToSend.Count < UpgradeSelections)
        {
            Debug.LogError("less then the amount of upgrades needed sent");
            Debug.LogError(UpgradesToSend.Count + " upgrades sent");
           
        }
        return UpgradesToSend;
        // Give the selected upgrade to the player
    }
    public void RemoveUpgradeFromPool(Upgrade upgrade)
    {
        upgradesPool.Remove(upgrade);  // Remove upgrade from pool

    }
}

