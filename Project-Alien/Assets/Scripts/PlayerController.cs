using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public HandleCleaning cleaningObject;
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float gravity;

    [Header("Mouse Look")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _maxLookAngle;


    [Header("Reach Distance")]
    [SerializeField] private int maxDistance;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalVelocity;
    private float xRotation = 0f;
    private bool isOnPc = false;
    private bool canSell = false;
    private int playerMoney = 0;
    private int moneyToAdd = 0;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -_maxLookAngle, _maxLookAngle);

        _cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * _speed + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnEnable()
    {
        EventManager.OnEndCleaning += EnableSelling;
    }

    void OnDisable()
    {
        EventManager.OnEndCleaning -= EnableSelling;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
            verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * gravity);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isOnPc)
            {
                string interaction = CheckInteraction();
                if (interaction == "Scanner")
                {
                    InputManager.Instance.SwitchActionMap("PC");
                    isOnPc = true;
                }
                else if (interaction == "Cleaning")
                {
                    InputManager.Instance.SwitchActionMap("Cleaning");
                    if(cleaningObject)cleaningObject.EnableBarMovement(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    isOnPc = true;
                }
                else if (interaction == "Table")
                {
                    if (!canSell) return;

                    playerMoney += moneyToAdd;
                    UIManager.Instance.UpdateMoney(playerMoney);
                    moneyToAdd = 0;
                    EventManager.DestroyObject();

                    canSell = false;
                    EventManager.SetTarget(null);
                }
            }
            else
            {
                InputManager.Instance.SwitchActionMap("Player");
                if(cleaningObject)cleaningObject.EnableBarMovement(false);
                isOnPc = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
    }

    public string CheckInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, maxDistance))
        {
            return hit.transform.tag;
        }

        return null;
    }

    public void EnableSelling(int moneyEarned)
    {
        canSell = true;
        moneyToAdd = moneyEarned;
    }
}

