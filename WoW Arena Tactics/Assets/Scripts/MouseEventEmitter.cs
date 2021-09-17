using UnityEngine;
using UnityEngine.Events;

public class MouseEventEmitter : MonoBehaviour
{
    public event UnityAction MouseEntered;
    public event UnityAction MouseExited;
    public event UnityAction MouseLeftClicked;
    public event UnityAction MouseRightClicked;
    public event UnityAction MouseMiddleClicked;

    private bool leftDown;
    private bool rightDown;
    private bool midDown;

    private void OnMouseEnter()
    {
        MouseEntered?.Invoke();
    }

    private void OnMouseExit()
    {
        MouseExited?.Invoke();
        leftDown = false;
        rightDown = false;
        midDown = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0) && leftDown) MouseLeftClicked?.Invoke();
        if (Input.GetMouseButtonUp(1) && rightDown) MouseRightClicked?.Invoke();
        if (Input.GetMouseButtonUp(2) && midDown) MouseMiddleClicked?.Invoke();

        if (Input.GetMouseButtonDown(0)) leftDown = true;
        if (Input.GetMouseButtonDown(1)) rightDown = true;
        if (Input.GetMouseButtonDown(2)) midDown = true;
    }
}
