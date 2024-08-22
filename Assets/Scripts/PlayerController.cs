using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

// PlayerController Script
// Role: Manages player movement, rotation, and player actions such as attacking in a 2D environment.
// Functionality: Reads input, moves the player character, rotates it towards the direction of movement, and handles attacking via EquipmentManager.

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public EquipmentManager equipmentManager; // Reference to the EquipmentManager

    private Vector2 moveInput;
    private Vector2 aimInput;

    void Start()
    {
        // Ensure references are set
        if (equipmentManager == null)
        {
            equipmentManager = GetComponentInChildren<EquipmentManager>();
        }
    }

    void Update()
    {
        Move();
        Aim();
    }

    // This method must match the signature expected by the PlayerInput component
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // This method must match the signature expected by the PlayerInput component
    public void OnButton(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PressButton();
        }
        else if (context.canceled)
        {
            ReleaseButton();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }

    private void Aim()
    {
        if (aimInput.magnitude > 0)
        {
            Vector2 aimDirection = aimInput.normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void ReleaseButton()
    {
        Attack();
    }

    private void PressButton()
    {
        equipmentManager.HandleWeaponPickup();
    }

    void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        if (movement.magnitude > 0)
        {
            transform.Translate(movement, Space.World);
        }
    }

    void Attack()
    {
        if (equipmentManager.CurrentWeapon)
        {
            equipmentManager.Attack();
        }
    }

    void Shoot()
    {
        print("pew");
        // Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
