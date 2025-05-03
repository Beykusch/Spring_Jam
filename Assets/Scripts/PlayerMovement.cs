using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    
    public float speed = 10f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    //Crouch
    public float crouchSpeed = 6f;
    public float crouchHeight = 0.8f;
    private float originalHeight;
    private Vector3 originalCenter;
    private bool isCrouching = false;

    public Transform playerCamera;
    public float crouchCameraOffset = 0.6f;
    public float cameraSmoothSpeed = 10f;

    private Vector3 cameraStandPosition;
    private Vector3 cameraCrouchPosition;

    public Transform playerBody; // assign this to your 3D cylinder in the Inspector

    public float bodyCrouchScale = 0.5f; // adjust how much you want it to shrink (Y axis)
    private Vector3 originalBodyScale;
    private Vector3 originalBodyPosition;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        originalCenter = controller.center;
        originalBodyScale = playerBody.localScale;
        originalBodyPosition = playerBody.localPosition;

        if (playerCamera != null)
        {
            cameraStandPosition = playerCamera.localPosition;
            cameraCrouchPosition = cameraStandPosition - new Vector3(0, crouchCameraOffset, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // slight downward force to keep grounded
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        float currentSpeed = isCrouching ? crouchSpeed : speed;
        Vector3 horizontalVelocity = move * currentSpeed;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalVelocity = new Vector3(horizontalVelocity.x, velocity.y, horizontalVelocity.z);

        controller.Move(finalVelocity * Time.deltaTime);

        // Movement detection logic
        if (lastPosition != transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = transform.position;

        //initiate crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchHeight;

            float centerOffset = (originalHeight - crouchHeight) / 2f;
            controller.center = originalCenter - new Vector3(0, centerOffset, 0);

            if (playerBody != null)
            {
                Vector3 newScale = new Vector3(originalBodyScale.x, originalBodyScale.y * bodyCrouchScale, originalBodyScale.z);
                playerBody.localScale = newScale;

                // Move body down by half the amount it shrank
                float heightDifference = originalBodyScale.y - newScale.y;
                playerBody.localPosition = originalBodyPosition - new Vector3(0, heightDifference / 2f, 0);
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = originalHeight;
            controller.center = originalCenter;

            if (playerBody != null)
            {
                playerBody.localScale = originalBodyScale;
                playerBody.localPosition = originalBodyPosition;
            }
        }

        if (playerCamera != null)
        {
            Vector3 targetPosition = isCrouching ? cameraCrouchPosition : cameraStandPosition;
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetPosition, Time.deltaTime * cameraSmoothSpeed);
        }

    }
}
