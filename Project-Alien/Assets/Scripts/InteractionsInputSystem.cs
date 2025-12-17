using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionsInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerController _player;

    void Awake()
    {
        SwitchActionMap("PC");
        SwitchActionMap("Cleaning");
        SwitchActionMap("Player");
    }

    public void EscFromPC(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SwitchActionMap("Player");
        }
    }

    public void EnterToPC(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_player.CheckInteraction() == "Scanner")
            {
                SwitchActionMap("PC");
            }
            else if (_player.CheckInteraction() == "Cleaning")
            {
                SwitchActionMap("Cleaning");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void SwitchActionMap(string name)
    {
        _playerInput.currentActionMap.Disable();
        _playerInput.SwitchCurrentActionMap(name);
        _playerInput.currentActionMap.Enable();
    }
}
