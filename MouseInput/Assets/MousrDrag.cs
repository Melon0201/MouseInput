using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousrDrag : MonoBehaviour
{
    private bool isDragging = false;
    private Transform dragableObject;
    private Plane dragPlane;
    private Vector3 offsetToDragPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Draggable"))
                {
                    isDragging = true;
                    dragableObject = hitInfo.transform;
                    dragPlane = new Plane(Vector3.up, dragableObject.position);
                    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float distance;
                    dragPlane.Raycast(mouseRay, out distance);
                    offsetToDragPoint = dragableObject.position - mouseRay.GetPoint(distance);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && dragableObject != null)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (dragPlane.Raycast(mouseRay, out distance))
            {
                Vector3 newPosition = mouseRay.GetPoint(distance) + offsetToDragPoint;
                newPosition.y = dragableObject.position.y;
                dragableObject.position = newPosition;
            }
        }
    }
}