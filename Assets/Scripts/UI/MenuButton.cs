using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private string defaultText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonSelect);
        textMeshPro.SetText(">" + defaultText + "<");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshPro.SetText(defaultText);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.ButtonPress);
    }
}
