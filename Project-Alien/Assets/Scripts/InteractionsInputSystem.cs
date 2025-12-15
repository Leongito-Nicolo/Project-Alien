using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionsInputSystem : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

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
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(name);
        playerInput.currentActionMap.Enable();
    }
}
