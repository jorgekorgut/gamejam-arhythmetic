using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Controller : MonoBehaviour
{
    private float horizontalInput = 0;

    public void Move(CallbackContext context)
    {
        if (context.performed)
        {
            horizontalInput = context.ReadValue<Vector2>().x;
        }
        else if (context.canceled)
        {
            horizontalInput = 0f;
        }
    }

    void FixedUpdate()
    {
        if (horizontalInput > 0)
        {
            GlobalHandler.Instance.sceneHandler.circlesRing.RotateRight();
        }
        else if (horizontalInput < 0)
        {
            GlobalHandler.Instance.sceneHandler.circlesRing.RotateLeft();
        }
    }
    
}
