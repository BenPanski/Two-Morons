using UnityEngine;

// Weapon Script
// Role: Base class for all weapons.
// Functionality: Defines common attributes and methods for all weapons, including equipped state and drop behavior.

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    [SerializeField] private bool dropAfterAttack; // Indicates if the weapon should be dropped after attacking
    private bool isEquipped; // Backing field for IsEquipped property
    private int firingPlayer;
    public bool DropAfterAttack
    {
        get { return dropAfterAttack; }
        set { dropAfterAttack = value; }
    }

    public bool IsEquipped
    {
        get { return isEquipped; }
        protected set { isEquipped = value; }
    }

    //  method for attacking, assignes the firing player by checking for the closest player
    public virtual void Attack(Transform attackPoint, float attackForce) 
    {
        firingPlayer = PlayerManager.Instance.GetClosestPlayerNumber(this.transform);
    }

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
