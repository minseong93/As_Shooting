using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerEvent : MonoBehaviour
{
    public delegate void InputHandler(Vector3 inputData);
    public delegate void MouseHandler();
    public delegate void MouseClickHandler();
    public event InputHandler OnInputMove;
    public event MouseHandler OnInputMouse;
    public event MouseClickHandler OnClickMouse;
    private Vector3 inputVec;
    private Vector3 currentMousePosition;
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        if (inputVec.x != 0 || inputVec.y != 0)
        {
            OnInputMove?.Invoke(inputVec);
        }
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentMousePosition != mouse)
        {
            OnInputMouse?.Invoke();
            currentMousePosition = mouse;
        }
        if (Input.GetMouseButtonDown(0))
            OnClickMouse?.Invoke();
    }
}
