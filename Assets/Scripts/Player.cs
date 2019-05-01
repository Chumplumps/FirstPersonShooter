using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = 10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;

    private CharacterController controller; // Reference to character controller
    private Vector3 motion; // Is the movement offset per frame

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        Move(inputH, inputV);

        if (IsGrounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                motion.y = jumpHeight;
            }
        }

        motion.y -= gravity * Time.deltaTime;

        controller.Move(motion * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Raycast below the player
        Ray groundRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        // If hitting something
        if (Physics.Raycast(groundRay, out hit, groundRayDistance))
        {
            return true;
        }
        return false;
    }

    void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        // Convert local direction to world space direction (relative to Player's transform)
        direction = transform.TransformDirection(direction);

        motion.x = direction.x * walkSpeed;
        motion.z = direction.z * walkSpeed;
    }
}
