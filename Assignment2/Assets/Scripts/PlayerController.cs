using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
    private Animator animator = null;

    private bool isGrounded = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        playerInput = new PlayerInput();
        animator = GetComponent<Animator>();
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
        animator.Play("Fall");
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


    void stateTransition(State newState)
    {
        if (newState!=currentState)
        {
            State oldState = currentState;
            currentState = newState;
            switch (newState)
            {
                case State.idle:
                    animator.Play("Idle");
                    break;
                case State.run:
                    animator.Play("Run");
                    break;
                case State.fall:
                    animator.Play("Fall");
                    break;
                case State.jump:
                    animator.Play("Jump");
                    break;
            }
        }
    }

    void stateLogic()
    {
        if (currentState == State.idle)
        {
            if (rigidbody.linearVelocityX != 0)
            {
                stateTransition(State.run);
            }
            if (!isGrounded)
            {
                stateTransition(State.fall);
            }
        }
        else if (currentState == State.run)
        {
            if (rigidbody.linearVelocityX == 0)
            {
                stateTransition(State.idle);
            }
            if (!isGrounded)
            {
                stateTransition(State.fall);
            }
        }
        else if (currentState == State.jump)
        {
            if (rigidbody.linearVelocityY < 0)
            {
                stateTransition(State.fall);
            }
        }
        else if (currentState == State.fall)
        {
            if (isGrounded)
            {
                stateTransition(State.idle);
            }
        }
    }

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
        stateLogic();
        GroundCheck();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            stateTransition(State.jump);
            rigidbody.linearVelocityY = jumpSpeed;
        }
    }


    void GroundCheck()
    {
        float rayLength = 0.02f;
        float rayDistanceFromCenter = 0.48f;
        float rayYOffset = 0.01f;
        Vector3 leftPos = transform.position;
        leftPos.x -= rayDistanceFromCenter;
        leftPos.y -= rayYOffset;
        Vector3 rightPos = transform.position;
        rightPos.x += rayDistanceFromCenter;
        rightPos.y -= rayYOffset;


        bool leftSide = Physics2D.Raycast(leftPos, Vector2.down, rayLength, LayerMask.GetMask("CanStandOn"));
        Debug.DrawRay(leftPos, Vector2.down * rayLength, Color.red);
        bool rightSide = Physics2D.Raycast(rightPos, Vector2.down, rayLength, LayerMask.GetMask("CanStandOn"));
        Debug.DrawRay(rightPos, Vector2.down * rayLength, Color.red);

        if (rightSide || leftSide)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

}
