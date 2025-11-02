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
    [SerializeField]
    private PlayerController OtherPlayer;


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
    private bool isOnPlayer = false;
    private int objectsUnderPlayer = 0;

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

    private void OnEnable()
    {
        playerInput.Enable();
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
        moveAction.Disable();
        jumpAction.Disable();
    }

    int GetState()
    {
        return (int)currentState;
    }

    public void SetGrounded(bool grounded, bool onPlayer, int objectCount)
    {
        isGrounded = grounded;
        isOnPlayer = onPlayer;
        objectsUnderPlayer = objectCount;
    }


    private void stateTransition(State newState)
    {
        if (newState != currentState)
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

    private void stateLogic()
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
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded && !(isOnPlayer && objectsUnderPlayer == 1 &&
        (OtherPlayer.GetState() == (int)State.jump || OtherPlayer.GetState() == (int)State.fall)))
        {
            stateTransition(State.jump);
            rigidbody.linearVelocityY = jumpSpeed;
        }
    }

    

}
