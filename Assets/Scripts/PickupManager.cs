// PickupManager Script
// Role: Manages detection and handling of player pickups.
// Functionality: Detects items in range, identifies item types, and communicates with the appropriate script to handle pickups.
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public EquipmentManager equipmentManager;

    void Start()
    {
        FindRefrences();
    }

    private void FindRefrences()
    {
        // Find the EquipmentManager on the player
        equipmentManager = GetComponentInParent<EquipmentManager>();
        if (equipmentManager == null)
        {
            Debug.LogError("EquipmentManager not found on parent.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                equipmentManager.HandleWeaponPickup(weapon);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                equipmentManager.HandleWeaponDrop(weapon);
            }
        }
    }
}
