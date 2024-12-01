using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IInputManager
{
    private bool held;
    public event Action<bool> heldDownInput;

    public void OnPointerDown(PointerEventData eventData)
    {
        held = true;
        heldDownInput?.Invoke(held);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        held = false;
        heldDownInput?.Invoke(held);
    }

    public void ManageInputEvent(IInputTaker taker, bool state)    
    {
        if (state) heldDownInput += taker.GetInputAction();
        else heldDownInput -= taker.GetInputAction();
    }
}
