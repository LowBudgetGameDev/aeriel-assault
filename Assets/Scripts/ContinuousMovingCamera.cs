using UnityEngine;

public class ContinuousMovingCamera : MonoBehaviour
{
    [SerializeField] private Vector2 speedVector;

    [SerializeField] private Vector2 maxCoordinates;

    private void Update()
    {
        transform.position += (Vector3) speedVector * Time.deltaTime;

        if (transform.position.x > maxCoordinates.x) transform.position = new Vector3(-maxCoordinates.x, -transform.position.y, transform.position.z);

        if (transform.position.x < -maxCoordinates.x) transform.position = new Vector3(maxCoordinates.x, -transform.position.y, transform.position.z);

        if (transform.position.y > maxCoordinates.y) transform.position = new Vector3(transform.position.x, -maxCoordinates.y, transform.position.z);

        if (transform.position.y < -maxCoordinates.y) transform.position = new Vector3(transform.position.x, maxCoordinates.y, transform.position.z);
    }
}
