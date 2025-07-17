using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public SoundType hoverSound = SoundType.HOVER;
    public SoundType clickSound = SoundType.START;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.PlaySfx(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.PlaySfx(clickSound);
    }
}
