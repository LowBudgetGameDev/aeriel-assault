using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera;

    public static Camera GetMainCamera()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        return mainCamera;
    }

    public static float CrossProduct(Vector3 vectorA, Vector3 vectorB)
    {
        return vectorA.x * vectorB.y - vectorA.y * vectorB.x;
    }

    public static Vector3 RandomUnitVector()
    {
        return new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
    }
}
