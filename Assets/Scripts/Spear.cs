using UnityEngine;

// Spear Script
// Role: Handles specific behavior for the spear weapon.
// Functionality: Implements the spear throwing logic.

public class Spear : Weapon
{
    public override void Attack(Transform attackPoint, float attackForce)
    {
        // Detach the spear from the player
        transform.SetParent(null);
        transform.position = attackPoint.position;

        // Enable physics on the spear
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        // Apply force to throw the spear
        rb.AddForce(attackPoint.forward * attackForce, ForceMode.Impulse);
    }
}
