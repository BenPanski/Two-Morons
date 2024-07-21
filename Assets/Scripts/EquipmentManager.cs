using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] public Weapon CurrentWeapon;
    [SerializeField] PickupManager pickupManager;
    public Transform attackPoint; // The point from which the weapon will attack
    public float attackForce = 10f; // The force used in attacks

    public void HandleWeaponPickup()
    {
        if (!pickupManager.IsThereWeaponInRange())
        {
            return;
        }
        // Try to cast the pickup to a Weapon
        Weapon weapon = pickupManager.GetPickup();

        // Check if the cast was successful
        if (weapon == null)
        {
            return;
        }

        // Handle dropping the current weapon if it exists
        if (CurrentWeapon != null)
        {
            HandleWeaponDrop(CurrentWeapon);
        }

        // Equip the new weapon
        CurrentWeapon = weapon;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.Equip();
    }



    public void HandleWeaponDrop(Weapon weapon)
    {
        // Drop the weapon
        if (CurrentWeapon == weapon)
        {
            weapon.Drop();
            CurrentWeapon = null;
        }
    }

    public void Attack()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.Attack(attackPoint, attackForce);

            // If it should be dropped after attacking
            if (CurrentWeapon.DropAfterAttack)
            {
                HandleWeaponDrop(CurrentWeapon);
            }
        }
    }
}
