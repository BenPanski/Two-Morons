using UnityEngine;

// EquipmentManager Script
// Role: Manages equipping and positioning of weapons, and handles weapon-specific attacks.
// Functionality: Stores the currently equipped weapon, equips new weapons, positions them correctly, and handles attacking with the current weapon.

public class EquipmentManager : MonoBehaviour
{
    public Weapon CurrentWeapon { get; private set; }
    public Transform attackPoint; // The point from which the weapon will attack
    public float attackForce = 10f; // The force used in attacks

    public void HandleWeaponPickup(Weapon weapon)
    {
        if (CurrentWeapon != null)
        {
            HandleWeaponDrop(CurrentWeapon);
        }

        // Equip the weapon
        CurrentWeapon = weapon;
        weapon.IsEquipped = true;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public void HandleWeaponDrop(Weapon weapon)
    {
        // Drop the weapon
        if (CurrentWeapon == weapon)
        {
            CurrentWeapon.IsEquipped = false;
            CurrentWeapon = null;
        }
        weapon.transform.SetParent(null);
    }

    // Method to attack with the current weapon
    public void Attack()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.Attack(attackPoint, attackForce);
        }
    }
}
