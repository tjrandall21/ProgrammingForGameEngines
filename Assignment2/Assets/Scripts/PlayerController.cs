using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private bool isPlayerOne = true;

    private enum State
    {
        idle,
        run,
        jump,
        fall
    }
    private State currentState = State.idle;

    private bool facingLeft = false;

    private PlayerInput playerInput = null;
    private Rigidbody2D rigidbody = null;
    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    private SpriteRenderer sprite = null;

    private bool isGrounded = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerInput = new PlayerInput();
        if (isPlayerOne)
        {
            moveAction = playerInput.Player.Move;
            jumpAction = playerInput.Player.Jump;
        }
        else
        {
            moveAction = playerInput.PlayerTwo.Move;
            jumpAction = playerInput.PlayerTwo.Jump;
        }
        jumpAction.performed += OnJump;
    }

    void OnEnable()
    {
        playerInput.Enable();
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
        moveAction.Disable();
        jumpAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        rigidbody.linearVelocityX = moveInput.x * moveSpeed;
        if (!facingLeft && moveInput.x == -1)
        {
            facingLeft = true;
        }
        else if (facingLeft && moveInput.x == 1)
        {
            facingLeft = false;
        }
        sprite.flipX = facingLeft;

    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rigidbody.linearVelocityY = jumpSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject != null)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isGrounded = false;
            }
        }
    }
}
