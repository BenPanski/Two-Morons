using UnityEngine;

// Spear Script
// Role: Handles specific behavior for the spear weapon.
// Functionality: Implements the spear throwing logic and manages its physics states.

public class Spear : Weapon
{
    [SerializeField]
    private float throwForce = 10f; // Recommended value: 10

    private Rigidbody rb;

    void Awake()
    {
        InitializeSpear();
    }

    private void InitializeSpear()
    {
        // Ensure the Rigidbody component is set up correctly
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Ensure the spear is stationary initially
        SetPhysicsState(isKinematic: true, useGravity: false);
    }

    public override void Attack(Transform attackPoint, float attackForce)
    {
        // Detach the spear from the player and enable physics
        transform.SetParent(null);
        transform.position = attackPoint.position;

        SetPhysicsState(isKinematic: false, useGravity: true);

        // Apply force to throw the spear
        rb.AddForce(attackPoint.forward * throwForce, ForceMode.Impulse);

        // Mark the spear as not equipped
        IsEquipped = false;
    }

    public void Equip()
    {
        // Disable physics when equipped
        SetPhysicsState(isKinematic: true, useGravity: false);
        IsEquipped = true;
    }

    public void Drop()
    {
        // Enable physics when dropped
        SetPhysicsState(isKinematic: false, useGravity: true);
        IsEquipped = false;
    }

    private void SetPhysicsState(bool isKinematic, bool useGravity)
    {
        rb.isKinematic = isKinematic;
        rb.useGravity = useGravity;
    }
}
