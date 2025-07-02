using UnityEngine;

public class ObjectDropShadow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector2 offset = new Vector2(-1, -1);

    private Transform shadowTransform;

    private void Awake()
    {
        GameObject shadow = new GameObject("Shadow", typeof(SpriteRenderer));

        shadowTransform = shadow.transform;

        shadowTransform.parent = transform;

        SpriteRenderer shadowSpriteRenderer = shadowTransform.GetComponent<SpriteRenderer>();

        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        shadowSpriteRenderer.color = new Color(0f, 0f, 0f, 0.5f);
        shadowSpriteRenderer.sortingOrder = -100;

        shadowTransform.localPosition = new Vector3(offset.x, offset.y, 0f);
        shadowTransform.localScale = spriteRenderer.transform.localScale;
    }

    private void Update()
    {
        shadowTransform.position = transform.position + new Vector3(offset.x, offset.y, 0f);
    }
}
