using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionsInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    void Awake()
    {
        SwitchActionMap("PC");
        SwitchActionMap("Player");
    }

    public void EscFromPC(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchActionMap("Player");
        }
    }

    public void EnterToPC(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwitchActionMap("PC");
        }
    }

    public void SwitchActionMap(string name)
    {
        _playerInput.currentActionMap.Disable();
        _playerInput.SwitchCurrentActionMap(name);
        _playerInput.currentActionMap.Enable();
    }
}
