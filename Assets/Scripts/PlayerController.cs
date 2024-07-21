using System;
using UnityEngine;
using UnityEngine.InputSystem;

// PlayerController Script
// Role: Manages player movement, rotation, and player actions such as attacking.
// Functionality: Reads input, moves the player character, rotates it towards the direction of movement, and handles attacking via EquipmentManager.

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public EquipmentManager equipmentManager; // Reference to the EquipmentManager

    private Vector2 moveInput;

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
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.deltaTime;
        if (movement.magnitude > 0)
        {
            transform.Translate(movement, Space.World);
            RotateTowardsMovementDirection(movement * rotateSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsMovementDirection(Vector3 movement)
    {
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
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
