using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed = 10f;
    private float turnSpeed = 1f;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        Vector3 movementInput = inputActions.Player.Move.ReadValue<Vector2>();

        transform.position += transform.up * movementSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, movementInput.x * turnSpeed);
    }
}
