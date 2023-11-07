using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
   
    public float moveSpeed = 10.0f; // ���������ƶ��ٶ�
    private bool isControlling = false; // �Ƿ����ڿ�������ı�־
    private Vector3 lastMousePosition; // ��һ֡�����λ��
    private Transform controlledObject; // �����Ƶ�����

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���������
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null) // ��������������
                {
                    isControlling = true;
                    lastMousePosition = Input.mousePosition;
                    controlledObject = hit.collider.gameObject.transform;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isControlling) // �������ͷ�
        {
            isControlling = false;
            controlledObject = null;
        }

        if (isControlling && controlledObject != null)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) // �������Ƿ��ƶ�
            {
                float deltaX = (Input.mousePosition.x - lastMousePosition.x) * moveSpeed * Time.deltaTime;
                float deltaY = (Input.mousePosition.y - lastMousePosition.y) * moveSpeed * Time.deltaTime;

                Vector3 movement = new Vector3(deltaX, 0.0f, deltaY);

                controlledObject.Translate(movement);
            }
            lastMousePosition = Input.mousePosition; // �������λ��
        }
    }
}