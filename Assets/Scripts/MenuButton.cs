using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private string defaultText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMeshPro.SetText(">" + defaultText + "<");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.SetText(defaultText);
    }
}
