using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))] [RequireComponent(typeof(PlayerInput))] [RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerControllerConfig")]
    [SerializeField] private PlayerControllerConfig playerControllerConfig;

    [Header("Assigned Transforms")]
    [SerializeField] private Transform cameraTX;
    [SerializeField] private Transform bobTX;

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
    //HeadBob
    private float headBobNormailizer = 0f;
    private float currentPlayerHeadBobAmplitude = 0.2f;
    private float currentPlayerHeadBobFrequency = 10f;
    private float currentPlayerHeadBobReset = 3f;
    private float initialBobTXPositionY = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        characterController.minMoveDistance = 0f;
        playerInput = GetComponent<PlayerInput>();

        initialBobTXPositionY = bobTX.localPosition.y;
    }

    private void Update()
    {
        PlayerFall();
        PlayerHeadBobAndFootStep();
        PlayerMovement();
        PlayerLook();
        PlayerJump();
    }

    private void PlayerJump()
    {
        if(playerInput.actions["Jump"].triggered)
        {
            fallVector.y += Mathf.Sqrt(2f * playerControllerConfig.gravityMultiplier * Physics.gravity.sqrMagnitude * playerControllerConfig.jumpHeight);
        }
    }

    private void PlayerFall()
    {
        if (characterController.isGrounded && fallVector.y<=0f)
        {
            fallVector.y = 0f;
        }
        else
        {
            fallVector.y -= playerControllerConfig.gravityMultiplier * Physics.gravity.sqrMagnitude * Time.deltaTime;
        }

       
        Debug.Log(fallVector);
        characterController.Move(fallVector * Time.deltaTime);
    }

    private void PlayerHeadBobAndFootStep()
    {
        headBobNormailizer += Time.deltaTime* currentPlayerHeadBobFrequency;
        if (Mathf.Abs(playerInputVector.x)>0f|| Mathf.Abs(playerInputVector.y)>0f)
        {
            bobTX.localPosition = new Vector3(0f, initialBobTXPositionY + currentPlayerHeadBobAmplitude * Mathf.Sin( headBobNormailizer ), 0f);
        }
        else
        {
            bobTX.localPosition = Vector3.Lerp(bobTX.localPosition, new Vector3(0f, initialBobTXPositionY, 0f), Time.deltaTime*currentPlayerHeadBobReset);
        }
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
        cameraTX.localRotation = Quaternion.Euler(new Vector3(rotationX, 0f, 0f));
        transform.rotation *= Quaternion.Euler(new Vector3(0f,rotationY,0f));
    }
}