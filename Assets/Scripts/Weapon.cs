using UnityEngine;

// Weapon Script
// Role: Base class for all weapons.
// Functionality: Defines common attributes and methods for all weapons.

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public abstract void Attack(Transform attackPoint, float attackForce);
}
