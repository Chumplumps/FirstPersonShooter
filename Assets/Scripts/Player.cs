using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;
    private CharacterController controller; // Reference to character controller
    private Vector3 motion; // Is the movement offset per frame
    private bool isJumping = false;

    // Functions
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // W A S D / Right Left Up Down Arrow Input
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        // Left Shift Input
        bool inputRun = Input.GetKey(KeyCode.LeftShift);
        // Space Bar Input
        bool inputJump = Input.GetButtonDown("Jump");

        // Put Horizontal & Vertical input into vector
        Vector3 inputDir = new Vector3(inputH, 0f, inputV);
        // Rotate direction to Player's Direction
        inputDir = transform.TransformDirection(inputDir);
        // If input exceeds length of 1
        if (inputDir.magnitude > 1f)
        {
            // Normalize it to 1f
            inputDir.Normalize();
        }

        if (inputRun)
        {
            Run(inputDir.x, inputDir.z);
        }
        else
        {
            Walk(inputDir.x, inputDir.z);
        }

        // If Player is on the ground AND pressed "Jump"
        if (IsGrounded() && inputJump)
        {
            // Make the Player Jump
            Jump();
        }

        // If is NOT Grounded AND isJumping
        if (!IsGrounded() && isJumping)
        {
            // Set isJumping to false
            isJumping = false;
        }

        motion.y += gravity * Time.deltaTime;

        controller.Move(motion * Time.deltaTime);
    }
    private bool IsGrounded()
    {
        // Raycast below the player
        Ray groundRay = new Ray(transform.position, -transform.up);
        // If hitting something
        if (Physics.Raycast(groundRay, groundRayDistance))
        {
            return true;
        }
        return false;
    }
    private void Move(float inputH, float inputV, float speed)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        motion.x = direction.x * speed;
        motion.z = direction.z * speed;
    }
    public void Walk(float inputH, float inputV)
    {
        Move(inputH, inputV, walkSpeed);
    }
    public void Run(float inputH, float inputV)
    {
        Move(inputH, inputV, runSpeed);
    }
    public void Jump()
    {
        motion.y = jumpHeight;
        isJumping = true; // We are jumping
    }

}
