using UnityEngine;

public static class UtilsClass
{
    public static float CrossProduct(Vector3 vectorA, Vector3 vectorB)
    {
        return vectorA.x * vectorB.y - vectorA.y * vectorB.x;
    }
}
