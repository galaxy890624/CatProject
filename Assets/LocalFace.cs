using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFace : MonoBehaviour
{
    // �ۤv�O��ۤv����V
    Quaternion �ۧڤ�V;

    [SerializeField] Transform �ڪ������� = null;

    /// <summary>�n�D�ڹ���ڪ�������</summary>
    public bool ��������� = false;

    [SerializeField] float ����t�� = 10f;

    private void Start()
    {
        // ��l�ƪ��ɭ԰O��ۤv����V
        �ۧڤ�V = this.transform.rotation;
    }
    private void Update()
    {
        if (��������� == true)
            �ۧڤ�V = Quaternion.Lerp(�ۧڤ�V, �ڪ�������.rotation, Time.deltaTime * ����t��);

        // �ۤv����ۤv����V
        this.transform.rotation = �ۧڤ�V;
    }
}
