using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] [RequireComponent(typeof(PlayerInput))] [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerControllerConfig")]
    [SerializeField] private PlayerControllerConfig playerControllerConfig;

    [Header("Assigned Transforms")]
    [SerializeField] private Transform fpCamera;

    //Referenced Variables
    private CharacterController characterController;
    private PlayerInput playerInput;

    //Private Variables
    //Movement
    private float currentPlayerSpeed = 5f;
    private Vector2 playerInputVector  = Vector3.zero;
    private Vector3 movementVector = Vector3.zero;
    //Fall
    private Vector3 fallVector = Vector3.zero;
    //Look
    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        PlayerFall();
        PlayerMovement();
        PlayerLook();
    }

    private void PlayerFall()
    {
        fallVector.y -= playerControllerConfig.gravityMultiplier * Physics.gravity.sqrMagnitude * Time.deltaTime;
        characterController.Move(fallVector * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        playerInputVector = playerInput.actions["Move"].ReadValue<Vector2>();
        movementVector = ((transform.forward * playerInputVector.y) + (transform.right * playerInputVector.x)) * currentPlayerSpeed;
        characterController.Move(movementVector * Time.deltaTime);
    }

    private void PlayerLook()
    {
        rotationX += playerControllerConfig.isLookInvertedX * playerInput.actions["Look"].ReadValue<Vector2>().y * playerControllerConfig.lookSensitivity * Time.deltaTime;
        rotationY = playerControllerConfig.isLookInvertedY * playerInput.actions["Look"].ReadValue<Vector2>().x * playerControllerConfig.lookSensitivity * Time.deltaTime;
        fpCamera.localRotation = Quaternion.Euler(new Vector3(rotationX, 0f, 0f));
        transform.rotation *= Quaternion.Euler(new Vector3(0f,rotationY,0f));
    }
}