using UnityEngine;

public class Spear : Weapon
{
    [SerializeField]
    private float throwForce = 10f; // Recommended value: 10
    public Rigidbody rb;
    public Material MyMat;
   public Color MyOriginalColor;
 
  
    private void Awake()
    {
        MyOriginalColor = MyMat.color;
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on this GameObject.");
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
        SwapMyPlayer();
        ColorSpear();
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
        MyPlayer = player;
    }
    public override void Equip()
    {
        base.Equip();
        MyPlayer = PlayerManager.Instance.GetClosestPlayerNumber(this.transform);
        ColorSpear();
        SetPhysicsState(isKinematic: true, useGravity: false);
    }
    public void SwapMyPlayer() 
    {
        if (MyPlayer == 0)
        {
            return;
        }
        MyPlayer = 3 - MyPlayer;
    }
    private void ColorSpear()
    {
        if (MyPlayer != 0)
        {
            MyMat.color = PlayerManager.Instance.GetPlayerColor(MyPlayer);
        }
        else
        {
            MyMat.color = MyOriginalColor;
        }
    }
    private void OnDisable()
    {
        MyMat.color = MyOriginalColor;
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
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        // Check if the collided object implements IDamageable
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Apply damage to the enemy
            damageable.TakeDamage(damage);

            // Bounce towards the other player
            Vector3 otherPlayer = PlayerManager.Instance.GetPlayerPosition((MyPlayer));
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
