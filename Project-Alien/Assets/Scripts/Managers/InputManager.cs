using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        SwitchActionMap("PC");
        SwitchActionMap("Cleaning");
        SwitchActionMap("Player");
    }

    public void SwitchActionMap(string name)
    {
        _playerInput.currentActionMap.Disable();
        _playerInput.SwitchCurrentActionMap(name);
        _playerInput.currentActionMap.Enable();
    }


}
