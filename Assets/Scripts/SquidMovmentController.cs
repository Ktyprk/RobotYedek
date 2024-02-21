using UnityEngine;
using UnityEngine.InputSystem;

public class SquidMovmentController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 45.0f;  // Speed of rotation in degrees per second
    private Vector2 moveInput;
    private Vector3 rotationDirection = Vector3.zero;
    private float moveUp = 0;
    private float moveDown = 0;

    void Update()
    {
        // Move the cube based on the input
        transform.Translate(new Vector3(moveInput.x, moveUp - moveDown, moveInput.y) * moveSpeed * Time.deltaTime);

        // Rotate the cube based on the rotation direction
        transform.Rotate(rotationDirection * rotateSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRotateLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
            rotationDirection.y = -1;
        else if (context.canceled)
            rotationDirection.y = 0;
    }

    public void OnRotateRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            rotationDirection.y = 1;
        else if (context.canceled)
            rotationDirection.y = 0;
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveUp = 1;
        else if (context.canceled)
            moveUp = 0;
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveDown = 1;
        else if (context.canceled)
            moveDown = 0;
    }

    public void OnResetRotation(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            transform.rotation = Quaternion.identity; // Reset rotation to default
        }
    }
}

