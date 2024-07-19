using UnityEngine;

// Weapon Script
// Role: Base class for all weapons.
// Functionality: Defines common attributes and methods for all weapons, including equipped state and drop behavior.

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public bool DropAfterAttack { get; set; } // Indicates if the weapon should be dropped after attacking
    private bool isEquipped; // Backing field for IsEquipped property

    public bool IsEquipped
    {
        get { return isEquipped; }
        protected set { isEquipped = value; }
    }

    // Abstract method for attacking, to be implemented by subclasses
    public abstract void Attack(Transform attackPoint, float attackForce);

    // Method to equip the weapon
    public virtual void Equip()
    {
        IsEquipped = true;
        // Additional logic for equipping the weapon, if needed
    }

    // Method to drop the weapon
    public virtual void Drop()
    {
        IsEquipped = false;
        // Additional logic for dropping the weapon, if needed
    }
}
