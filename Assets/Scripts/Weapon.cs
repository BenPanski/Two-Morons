using UnityEngine;

// Weapon Script
// Role: Base class for all weapons.
// Functionality: Defines common attributes and methods for all weapons, including equipped state.

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public bool IsEquipped { get; set; } // Indicates if the weapon is currently equipped

    public abstract void Attack(Transform attackPoint, float attackForce);
}
