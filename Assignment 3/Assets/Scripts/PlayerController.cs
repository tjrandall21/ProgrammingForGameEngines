using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 10f;
    private Rigidbody rigidBody = null;
    [SerializeField] private GroundCheck groundCheck = null;
    [SerializeField] private Transform lookTarget = null;
    [SerializeField] private float lookSensitivity = 75f;
    [SerializeField] private float lookDeadZone = 0.1f;
    [SerializeField] private float maxLookUp = 70.0f;
    [SerializeField] private float maxLookDown = -70.0f;
    [SerializeField] private float deathZoneY = -5.0f;
    private float xRotation = 0.0f;
    private PlayerInput input = null;
    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    private InputAction lookAction = null;
    [SerializeField] private bool invertY = false;
    private Vector3 CheckPointPosition;


    bool jumpPlatformsEnabled = true;
    [SerializeField] private GameObject onPlatforms;
    private Collider[] onPlatformColliders;
    private Renderer[] onPlatformRenderers;
    [SerializeField] private GameObject offPlatforms;
    private Collider[] offPlatformColliders;
    private Renderer[] offPlatformRenderers;

    private bool reachedGoal = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        input = new PlayerInput();
        moveAction = input.Player.Move;
        jumpAction = input.Player.Jump;
        lookAction = input.Player.Look;

        onPlatformColliders = onPlatforms.GetComponentsInChildren<Collider>();
        onPlatformRenderers = onPlatforms.GetComponentsInChildren<Renderer>();

        offPlatformColliders = offPlatforms.GetComponentsInChildren<Collider>();
        offPlatformRenderers = offPlatforms.GetComponentsInChildren<Renderer>();


        jumpAction.performed += OnJump;
        Cursor.lockState = CursorLockMode.Locked;
        CheckPointPosition = transform.position;

        UpdatePlatforms();
    }

    void OnEnable()
    {
        input.Enable();
        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();
    }

        void OnDisable()
    {
        input.Disable();
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        Vector3 fwd = rigidBody.transform.forward;
        Vector3 right = rigidBody.transform.right;
        fwd.y = 0.0f;
        right.y = 0.0f;
        fwd.Normalize();
        right.Normalize();

        Vector3 moveVelocity = (fwd * moveInput.y * moveSpeed) + (right * moveInput.x * moveSpeed);
        moveVelocity.y = rigidBody.linearVelocity.y;

        rigidBody.linearVelocity = moveVelocity;
        rigidBody.angularVelocity = Vector3.zero;

        //Look Action
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        Vector2 lookDelta = Vector2.zero;
        if (lookInput.sqrMagnitude > lookDeadZone * lookDeadZone)
        {
            lookDelta = lookInput * lookSensitivity * Time.deltaTime;
        }

        Quaternion rotation = Quaternion.Euler(0.0f, lookDelta.x, 0.0f);
        rotation = rigidBody.rotation * rotation;
        rigidBody.MoveRotation(rotation);

        if (invertY)
        {
            xRotation += lookDelta.y;
        }
        else
        {
            xRotation -= lookDelta.y;
        }
        xRotation = Mathf.Clamp(xRotation, maxLookDown, maxLookUp);
        lookTarget.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (transform.position.y < deathZoneY)
        {
            Respawn();
        }
        if (transform.parent != null)
        {
            Collider collider = transform.parent.GetComponent<Collider>();
            if (!collider.enabled)
            {
                transform.parent = null;
            }
        }
    }
    
    void OnJump(InputAction.CallbackContext context)
    {
        if (groundCheck.IsGrounded() && rigidBody.linearVelocity.y < 0.1f)
        {
            Vector3 velocity = rigidBody.linearVelocity;
            velocity.y = jumpSpeed;
            rigidBody.linearVelocity = velocity;
            jumpPlatformsEnabled = !jumpPlatformsEnabled;
            UpdatePlatforms();
            groundCheck.OnJump();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
        }
        else if (other.tag == "Checkpoint")
        {
            CheckPointPosition = other.transform.position;
        }
        else if (other.tag == "Goal")
        {
            reachedGoal = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    void Respawn()
    {
        Vector3 respawnPoint = CheckPointPosition;
        respawnPoint.y += 2;
        transform.position = respawnPoint;
        rigidBody.linearVelocity = Vector3.zero;
    }

    void UpdatePlatforms()
    {
        foreach (Collider collider in onPlatformColliders)
        {
            collider.enabled = jumpPlatformsEnabled;
        }
        foreach (Renderer renderer in onPlatformRenderers)
        {
            renderer.enabled = jumpPlatformsEnabled;
        }

        foreach (Collider collider in offPlatformColliders)
        {
            collider.enabled = !jumpPlatformsEnabled;
        }
        foreach (Renderer renderer in offPlatformRenderers)
        {
            renderer.enabled = !jumpPlatformsEnabled;
        }
    }

    public bool HasReachedGoal()
    {
        return reachedGoal;
    }
}
