using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{
    public Vector2 rawMovementInput { get; private set; }
    public Vector2 mousePos { get; private set; }

    public bool attackInput { get; private set; }
    public bool dashInput { get; private set; }

    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dashInput = true;
        }
        else if (context.canceled)
        {
            dashInput = false;
        }
    }
    public void OnMousePos(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInput = true;
        }
        else if (context.canceled)
        {
            attackInput = false;
        }
    }
}
