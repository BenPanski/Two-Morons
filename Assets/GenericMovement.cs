using UnityEngine;

public class GenericMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed of the movement
    public float rotationSpeed = 720f; // Speed of rotation

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Get input from keyboard or controller
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        // Move the capsule
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the capsule to face the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

