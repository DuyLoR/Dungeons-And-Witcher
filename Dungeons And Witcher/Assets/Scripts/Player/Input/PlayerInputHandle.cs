using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{
    public static PlayerInputHandle Instance;
    public Vector2 rawMovementInput { get; private set; }
    public Vector2 mousePos { get; private set; }

    public int ChangeWeaponInput { get; private set; }
    public bool isWeaponInput { get; private set; }
    public bool attackInput { get; private set; }
    public bool dashInput { get; private set; }
    public bool collectorInput { get; private set; }
    public bool inventoryInput { get; private set; }
    public bool escapeInput { get; set; }
    public bool rightMouse { get; private set; }
    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
    private void Awake()
    {
        Instance = this;
        ChangeWeaponInput = 0;
    }
    public void OnMovementInput(InputAction.CallbackContext context)
    {
        rawMovementInput = context.ReadValue<Vector2>();
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started) { dashInput = true; }
        else if (context.canceled) { dashInput = false; }
    }
    public void OnMousePos(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started) { attackInput = true; }
        else if (context.canceled) { attackInput = false; }
    }
    public void OnChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isWeaponInput = true;
            char keyPressed = context.control.path[context.control.path.Length - 1];
            var bindings = context.action.bindings.ToArray();
            for (int i = 0; i < bindings.Length; i++)
            {
                char bindingPath = bindings[i].path[bindings[i].path.Length - 1];
                if (keyPressed == bindingPath)
                {
                    ChangeWeaponInput = i;
                    break;
                }
            }
        }
        else if (context.canceled)
        {
            isWeaponInput = false;
        }
    }
    public void OnCollectorInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            collectorInput = true;
        }
        else if (context.canceled)
        {
            collectorInput = false;
        }
    }
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inventoryInput = !inventoryInput;
        }
    }
    public void OnEscapeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            escapeInput = !escapeInput;
        }
    }
    public void OnRightMouseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rightMouse = true;
        }
        else if (context.canceled)
        {
            rightMouse = false;
        }
    }
}
