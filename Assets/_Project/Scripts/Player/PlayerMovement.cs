using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f * 2;
    [SerializeField] private float jumpHeight = 3f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving = false;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // creating move vector
        Vector3 move = transform.right * x + transform.forward * z;

        // moving player
        controller.Move(move * speed * Time.deltaTime);

        // check if player can jump
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isMoving = true;
        }

        // Falling down
        velocity.y += gravity * Time.deltaTime;

        // Executing the jump action
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition!= gameObject.transform.position && isGrounded) {
            isMoving = true;
        } else {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;
    }
}
