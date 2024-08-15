using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

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
    private Vector2 rotateInput;

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
        Rotate();
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
        // Get the current control scheme
        var currentControlScheme = context.control.device;

        if (currentControlScheme is Mouse)
        {
            // Handle mouse aiming
            HandleMouseAiming(context);
        }
        else if (currentControlScheme is Gamepad)
        {
            // Handle controller aiming
            HandleControllerAiming(context);
        }
    }

    private void HandleMouseAiming(InputAction.CallbackContext context)
    {
        Vector3 mousePosition = context.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 lookDirection = hitPoint - transform.position;

            // Adjust rotation speed based on distance
            float distance = lookDirection.magnitude;
            float adjustedRotateSpeed = rotateSpeed * Mathf.Clamp(distance, 0.5f, 2f); // Adjust these values as needed

            lookDirection.y = 0f; // Ignore vertical difference
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * adjustedRotateSpeed);
        }
    }


    private void HandleControllerAiming(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
        if (rotateInput.magnitude > 0)
        {
            Vector3 lookDirection = new Vector3(rotateInput.x, 0, rotateInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
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
        }
    }
    void Rotate()
    {
        if (rotateInput.magnitude > 0)
        {
            Vector3 lookDirection = new Vector3(rotateInput.x, 0, rotateInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
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
