// PlayerMovement Script
// Role: Manages player movement and rotation.
// Functionality: Reads input, moves the player character, and rotates it towards the direction of movement.
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Vector2 moveInput;

    void Update()
    {
        Move();
    }

    // This method must match the signature expected by the PlayerInput component
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // This method must match the signature expected by the PlayerInput component
    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            Shoot();
        }
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

    void Shoot()
    {
        print("pew");
        // Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
