using UnityEngine;

// Spear Script
// Role: Handles specific behavior for the spear weapon.
// Functionality: Implements the spear throwing logic.

public class Spear : Weapon
{
    [SerializeField]
    private float throwForce = 10f; // Recommended value: 10

    private Rigidbody rb;

    public override void Attack(Transform attackPoint, float attackForce)
    {
        // Detach the spear from the player
        transform.SetParent(null);
        transform.position = attackPoint.position;

        // Ensure the Rigidbody component is set up correctly
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        // Apply force to throw the spear
        rb.AddForce(attackPoint.forward * throwForce, ForceMode.Impulse);

        // Mark the spear as not equipped
        IsEquipped = false;
    }
}
