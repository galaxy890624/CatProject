using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    float mouseX = 0f;
    float mouseY = 0f;
    public float �ƹ��t�� = 1f;

    [SerializeField] Transform �������ફ�� = null;
    [SerializeField] Transform �������ફ�� = null;

    private void Update()
    {
        // �u���b�C�����~�����
        if (GamePlayManager.instance.status == �C�����A.�C����)
        {
            mouseX = mouseX + (Input.GetAxis("Mouse X") * �ƹ��t��);
            mouseY = mouseY + (Input.GetAxis("Mouse Y") * -1f * �ƹ��t��);
        }
        // ����Y���W�U����
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // ��XYZ�ഫ������q
        Quaternion ��������q = Quaternion.Euler(0f, mouseX, 0f);
        // �󴫤������ફ�骺�����
        �������ફ��.localRotation = ��������q;

        Quaternion ��������q = Quaternion.Euler(mouseY, 0f, 0f);
        �������ફ��.localRotation = ��������q;
    }
}
