using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] protected float dragThreshold = 0.5f;
    [SerializeField] protected float doubleClickTime = 0.3f;

    protected bool isDragging = false;
    protected Vector2 dragStartPosition;
    protected float lastClickTime;
    protected GameObject currentDraggedObject;

    public delegate void InputEvent(Vector2 position, GameObject target);
    public event InputEvent OnPointerDownEvent;
    public event InputEvent OnPointerDragEvent;
    public event InputEvent OnPointerUpEvent;
    public event InputEvent OnDoubleClickEvent;

    protected virtual void Update()
    {
        HandleInput();
    }

    protected virtual void HandleInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            OnPointerDrag(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPointerUp(Input.mousePosition);
        }
    }

    protected virtual void OnPointerDown(Vector2 position)
    {
        dragStartPosition = position;
        isDragging = false;
        currentDraggedObject = GetUIElementUnderPointer(position);

        // Check for double click
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick < doubleClickTime && currentDraggedObject != null)
        {
            OnDoubleClickEvent?.Invoke(position, currentDraggedObject);
        }

        lastClickTime = Time.time;
        OnPointerDownEvent?.Invoke(position, currentDraggedObject);
    }

    protected virtual void OnPointerDrag(Vector2 position)
    {
        currentDraggedObject = GetUIElementUnderPointer(position);
        if (currentDraggedObject == null) return;

        if (!isDragging && Vector2.Distance(dragStartPosition, position) > dragThreshold)
        {
            isDragging = true;
        }

        if (isDragging)
        {
            OnPointerDragEvent?.Invoke(position, currentDraggedObject);
        }
    }

    protected virtual void OnPointerUp(Vector2 position)
    {
        GameObject releasedObject = GetUIElementUnderPointer(position);
        OnPointerUpEvent?.Invoke(position, releasedObject);
        isDragging = false;
        currentDraggedObject = null;
    }

    public bool IsDragging()
    {
        return isDragging;
    }
    
    protected GameObject GetUIElementUnderPointer(Vector2 position)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            return results[0].gameObject;
        }
        return null;
    }
    
    public GameObject GetCurrentDraggedObject()
    {
        return currentDraggedObject;
    }

}
