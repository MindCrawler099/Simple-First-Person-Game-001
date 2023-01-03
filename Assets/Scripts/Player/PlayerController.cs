using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] [RequireComponent(typeof(PlayerInput))] [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerControllerConfig")]
    [SerializeField] private PlayerControllerConfig playerControllerConfig;

    //Referenced Variables
    private CharacterController characterController;
    private PlayerInput playerInput;

    //Private Variables
    private float currentPlayerSpeed = 5f;
    private Vector2 playerInputVector  = Vector3.zero;
    private Vector3 movementVector = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        playerInputVector = playerInput.actions["Move"].ReadValue<Vector2>();
        movementVector = ((transform.forward * playerInputVector.y) + (transform.right * playerInputVector.x)) * currentPlayerSpeed;
        characterController.Move(movementVector * Time.deltaTime);
    }
}