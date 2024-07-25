using UnityEngine;

public class Spear : Weapon
{
    [SerializeField]
    private float throwForce = 10f; // Recommended value: 10
    public Rigidbody rb;
    private int firingPlayer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on this GameObject.");
        }
        SetPhysicsState(isKinematic: true, useGravity: false);
    }

    public override void Attack(Transform attackPoint, float attackForce)
    { firingPlayer = PlayerManager.Instance.GetClosestPlayerNumber(this.transform);
        if (!IsEquipped)
        {
            Debug.LogWarning("Attempting to attack with an unequipped weapon.");
            return;
        }
        firingPlayer = PlayerManager.Instance.GetClosestPlayerNumber(this.transform);
        // Detach the spear from the player and enable physics
        transform.SetParent(null);
        transform.position = attackPoint.position;

        SetPhysicsState(isKinematic: false, useGravity: true);

        // Apply force to throw the spear
        rb.AddForce(attackPoint.forward * throwForce, ForceMode.Impulse);

        // Mark the spear as not equipped
        IsEquipped = false;
    }
    public void SetFiringPlayer(int player)
    {
        firingPlayer = player;
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

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object implements IDamageable
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Apply damage to the enemy
            damageable.TakeDamage(damage);

            // Bounce towards the other player
            Vector3 otherPlayer = PlayerManager.Instance.GetPlayerPosition((3- firingPlayer));
            if (otherPlayer != null)
            {
                Vector3 directionToOtherPlayer = (otherPlayer - transform.position).normalized;
                rb.velocity = directionToOtherPlayer * throwForce; // You can adjust throwForce as needed
            }
            else
            {
                print("No other player found");
            }

            // Increase bounce count and check if it exceeds maxBounces
            /*currentBounces++;
            if (currentBounces >= maxBounces)
            {
                Destroy(gameObject);
            }*/
        }
    }
}
