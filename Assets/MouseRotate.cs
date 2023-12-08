using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    float mouseX = 0f;
    float mouseY = 0f;
    public float 滑鼠速度 = 1f;

    [SerializeField] Transform 水平旋轉物體 = null;
    [SerializeField] Transform 垂直旋轉物體 = null;

    private void Update()
    {
        // 只有在遊戲中才能旋轉
        if (GamePlayManager.instance.status == 遊戲狀態.遊戲中)
        {
            mouseX = mouseX + (Input.GetAxis("Mouse X") * 滑鼠速度);
            mouseY = mouseY + (Input.GetAxis("Mouse Y") * -1f * 滑鼠速度);
        }
        // 限制Y的上下角度
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // 把XYZ轉換成旋轉量
        Quaternion 水平旋轉量 = Quaternion.Euler(0f, mouseX, 0f);
        // 更換水平旋轉物體的旋轉值
        水平旋轉物體.localRotation = 水平旋轉量;

        Quaternion 垂直旋轉量 = Quaternion.Euler(mouseY, 0f, 0f);
        垂直旋轉物體.localRotation = 垂直旋轉量;
    }
}
