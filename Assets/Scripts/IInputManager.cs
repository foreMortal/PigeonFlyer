using UnityEngine.EventSystems;

public interface IInputManager
{
    public void ManageInputEvent(IInputTaker taker, bool state);
    public void OnPointerDown(PointerEventData eventData);
    public void OnPointerUp(PointerEventData eventData);
}
