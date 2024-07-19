// EquipmentManager Script
// Role: Manages equipping and positioning of weapons.
// Functionality: Stores the equipped weapon, equips new weapons, and handles picking up and dropping weapons.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] Weapon EquippedWeapon;
    [SerializeField] Transform WeaponSocket;

    public void EquipWeapon(Weapon weapon)
    {
        if (EquippedWeapon != null)
        {
            // Logic to handle unequipping the current weapon, if necessary
            EquippedWeapon.transform.parent = null;
        }

        EquippedWeapon = weapon;
        weapon.transform.parent = WeaponSocket;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        // Add any additional logic for equipping the weapon (e.g., enabling weapon-specific scripts)
    }

    public void HandleWeaponPickup(Weapon weapon)
    {
        if (EquippedWeapon == null)
        {
            EquipWeapon(weapon);
        }
    }

   /* public void HandleWeaponDrop(Weapon weapon)
    {
        if (EquippedWeapon == weapon)
        {
            EquippedWeapon = null;
            // Logic to handle dropping the weapon, if necessary
            weapon.transform.parent = null;
            // Add any additional logic for dropping the weapon (e.g., disabling weapon-specific scripts)
        }
    }*/
}
