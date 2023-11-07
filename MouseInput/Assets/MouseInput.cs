using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
   
    public float moveSpeed = 10.0f; // 控制物体移动速度
    private bool isControlling = false; // 是否正在控制物体的标志
    private Vector3 lastMousePosition; // 上一帧的鼠标位置
    private Transform controlledObject; // 被控制的物体

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键点击
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != null) // 如果点击到了物体
                {
                    isControlling = true;
                    lastMousePosition = Input.mousePosition;
                    controlledObject = hit.collider.gameObject.transform;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && isControlling) // 鼠标左键释放
        {
            isControlling = false;
            controlledObject = null;
        }

        if (isControlling && controlledObject != null)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) // 检查鼠标是否移动
            {
                float deltaX = (Input.mousePosition.x - lastMousePosition.x) * moveSpeed * Time.deltaTime;
                float deltaY = (Input.mousePosition.y - lastMousePosition.y) * moveSpeed * Time.deltaTime;

                Vector3 movement = new Vector3(deltaX, 0.0f, deltaY);

                controlledObject.Translate(movement);
            }
            lastMousePosition = Input.mousePosition; // 更新鼠标位置
        }
    }
}