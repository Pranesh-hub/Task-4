using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb; // Reference to the Rigidbody component
    public GameManager gameManager;
    public float moveSpeed = 200f; // Speed of movement

    private bool canMove = true; // Flag to track if the player can move

    void Start()
    {
        // Lock the cursor to the game view
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void FixedUpdate()
    {
        // If player cannot move, return early
        if (!canMove) return;

        Vector3 movement = Vector3.zero;

        // Capture movement input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.z += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.z -= 1;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            movement.y += 10;
            Debug.Log("Space key pressed");
        }
        if (rb.position.z > 35.2)
        {
            gameManager.EndGame();
        }
        if (rb.position.x < 5)
        {
            gameManager.RestartGame();
        }
        // Normalize the movement vector to ensure consistent speed in all directions
        if (movement != Vector3.zero)
        {
            movement.Normalize();
        }

        // Apply the movement force to the Rigidbody
        rb.AddForce(movement * moveSpeed, ForceMode.Acceleration);
    }

    void Update()
    {
        // Allow unlocking the cursor for debugging or menu access
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        // }
    }
}
