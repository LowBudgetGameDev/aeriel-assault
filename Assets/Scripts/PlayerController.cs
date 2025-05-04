using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private float movementSpeed = 10f;
    private float turnSpeed = 1f;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();

        inputActions.Player.Shoot.performed += (InputAction.CallbackContext obj) =>
        {
            Shoot();
        };
    }

    private void Update()
    {
        Vector3 movementInput = inputActions.Player.Move.ReadValue<Vector2>();

        transform.position += transform.up * movementSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, movementInput.x * turnSpeed);
    }

    private void Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(transform.up);
    }
}
