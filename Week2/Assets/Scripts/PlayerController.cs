using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    float jumpSpeed = 30.0f;

    private PlayerInput playerInput = null;
    private Rigidbody2D rigidbody = null;
    private InputAction moveAction = null;
    private InputAction jumpAction = null;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
        moveAction = playerInput.Player.Move;
        jumpAction = playerInput.Player.Jump;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.linearVelocityX = moveAction.ReadValue<Vector2>().x * moveSpeed;
        Debug.Log(moveAction.ReadValue<Vector2>().x);
    }
}
