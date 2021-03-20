using System;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // Know what objects are clickable
    public LayerMask clickableLayer;

    // Swap cursors per object
    public Texture2D normalPointer;
    public Texture2D targetPointer;
    public Texture2D doorwayPointer;
    public Texture2D combatPointer;

    public EventVector3 OnClickEnvironment;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit, 50, clickableLayer.value))
        {
            bool door = false;
            bool item = false;
            if (raycastHit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorwayPointer, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if (raycastHit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combatPointer, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(targetPointer, new Vector2(16, 16), CursorMode.Auto);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (door)
                {
                    Transform doorway = raycastHit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                }
                else if (item)
                {
                    Transform itemTransform = raycastHit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(itemTransform.position);
                    Debug.Log("ITEM");
                }
                else
                {
                    OnClickEnvironment.Invoke(raycastHit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(normalPointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[Serializable]
public class EventVector3 : UnityEvent<Vector3> { }