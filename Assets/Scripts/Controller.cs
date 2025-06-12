using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Controller : MonoBehaviour
{
    private float horizontalInput = 0;

    public static Controller Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of Controller exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

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

    public bool IsRotateRight()
    {
        return horizontalInput > 0;
    }

    public bool IsRotateLeft()
    {
        return horizontalInput < 0;
    }
}
