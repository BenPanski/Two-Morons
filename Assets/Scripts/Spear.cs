using UnityEngine;

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
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        SetPhysicsState(isKinematic: true, useGravity: false);
    }

    public override void Attack(Transform attackPoint, float attackForce)
    {
        if (!IsEquipped)
        {
            Debug.LogWarning("Attempting to attack with an unequipped weapon.");
            return;
        }

        // Detach the spear from the player and enable physics
        transform.SetParent(null);
        transform.position = attackPoint.position;

        SetPhysicsState(isKinematic: false, useGravity: true);

        // Apply force to throw the spear
        rb.AddForce(attackPoint.forward * throwForce, ForceMode.Impulse);

        // Mark the spear as not equipped
        IsEquipped = false;
    }

    public override void Equip()
    {
        base.Equip();
        SetPhysicsState(isKinematic: true, useGravity: false);
    }

    public override void Drop()
    {
        base.Drop();
        SetPhysicsState(isKinematic: false, useGravity: true);
    }

    private void SetPhysicsState(bool isKinematic, bool useGravity)
    {
        rb.isKinematic = isKinematic;
        rb.useGravity = useGravity;
    }
}
