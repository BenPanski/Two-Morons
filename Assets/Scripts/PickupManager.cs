using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

// PickupManager Script
// Role: Manages detection and handling of player pickups.
// Functionality: Detects items in range, identifies item types, and communicates with the appropriate script to handle pickups.

public class PickupManager : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    private int MyPlayerIndex;
    public List<Weapon> WeaponsInRange = new List<Weapon>();

    void Start()
    {
        FindReferences();
    }

    private void FindReferences()
    {
            MyPlayerIndex = PlayersManager.Instance.GetClosestPlayerNumber(this.transform);
        // Find the EquipmentManager on the player
        if (equipmentManager == null)
        {
            try
            {
                equipmentManager = GetComponentInParent<EquipmentManager>();
            }
            catch (System.Exception)
            {
                Debug.LogError("EquipmentManager not found on parent.");
                throw;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon.MyPlayer == MyPlayerIndex || weapon.MyPlayer == 0)
            {
                WeaponsInRange.Add(weapon);
            }
            
            /* Weapon weapon = other.GetComponent<Weapon>();
             if (weapon != null && !weapon.IsEquipped)
             {
                 equipmentManager.HandleWeaponPickup(weapon);

             }*/
        }
        else if (other.CompareTag("Health"))
        {
            HandleHealthPickup(other.gameObject);
        }
        else if (other.CompareTag("XP"))
        {
            HandleXPPickup(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (WeaponsInRange.Count > 0)
        {
            for (int i = 0; i < WeaponsInRange.Count; i++)
            {
                if (WeaponsInRange[i].gameObject == other.gameObject)
                {
                    WeaponsInRange.RemoveAt(i);
                }
            }
        }
    }
    public Weapon GetPickup()
    {
        // Return the first non-null pickup in range if available
        var PickupWeapon = WeaponsInRange.FirstOrDefault();
        if (PickupWeapon != null)
        {
            WeaponsInRange.Remove(PickupWeapon);
        }
        return PickupWeapon;
    }

    public bool IsThereWeaponInRange()
    {
        return WeaponsInRange.Count > 0;
    }
    private void HandleHealthPickup(GameObject healthPickup)
    {
        // Implement health pickup logic
        Debug.Log("Picked up health");
        // Add health to the player (assuming a PlayerHealth script or similar exists)
        // PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
        // playerHealth.AddHealth(healthAmount);
        Destroy(healthPickup);
    }

    private void HandleXPPickup(GameObject xpPickup)
    {
        // Implement XP pickup logic
        Debug.Log("Picked up XP");
        // Add XP to the player (assuming a PlayerXP script or similar exists)
        // PlayerXP playerXP = GetComponentInParent<PlayerXP>();
        // playerXP.AddXP(xpAmount);
        Destroy(xpPickup);
    }
}
