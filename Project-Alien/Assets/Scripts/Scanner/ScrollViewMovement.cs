using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScrollViewMovement : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private float _speed = .5f;
    private bool shouldMove = false;
    private float xValue;
    private float yValue;

    void Update()
    {
        if (!shouldMove) return;

        MoveScreen();
    }

    public void MoveScreen(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            shouldMove = true;
        }

        if (context.canceled)
        {
            shouldMove = false;
        }

        xValue = context.ReadValue<Vector2>().x;
        yValue = context.ReadValue<Vector2>().y;
    }

    private void MoveScreen()
    {
        _scrollRect.verticalNormalizedPosition += yValue * _speed * Time.deltaTime;
        _scrollRect.horizontalNormalizedPosition += xValue * _speed * Time.deltaTime;
    }
}
