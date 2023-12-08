using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayManager : AYEStatusBehaviour<�C�����A>
{
    #region �n�O�C�����A
    static public GamePlayManager instance = null;
    public GameObject ���a = null;
    public PlayerMove ���a�����ʵ{�� = null;
    public Transform ���a���Y = null;
    public int �C���}�l�ɪ��_�ۼƶq = 0;
    private void Awake()
    {
        instance = this;
        AddStatus(�C�����A.��l��, �i�J��l��, ��l��);
        AddStatus(�C�����A.�˼ƭp��, �i�J�˼ƭp��, �˼ƭp��);
        AddStatus(�C�����A.�C����, �i�J�C����, �C����, ���}�C����);
        AddStatus(�C�����A.�C������, �i�J�C������, �C������);
    }
    #endregion
    #region ��l��

    [SerializeField] float �ͩR�ȨC�@�ŭ��h�� = 1.3f;
    [SerializeField] float ��O�ȨC�@�ŭ��h�� = 1.2f;

    void �i�J��l��() // Start
    {
        Cursor.lockState = CursorLockMode.Locked;
        // ��l���ݩ�, ����
        SaveManager.instance.maxhp = 100f * MathF.Pow(�ͩR�ȨC�@�ŭ��h��, SaveManager.instance.�ͩR�ȵ���);
        SaveManager.instance.hp = SaveManager.instance.maxhp;
        SaveManager.instance.maxap = 10f * MathF.Pow(��O�ȨC�@�ŭ��h��, SaveManager.instance.��O����);
        SaveManager.instance.ap = SaveManager.instance.maxap;
        SaveManager.instance.score = 0;
        SaveManager.instance.���ȹD�� = 0;
        // �C���O�_����
        SaveManager.instance.isOver = false;
        // �����_�ۦb�C����l�Ʈɪ��ƶq
        �C���}�l�ɪ��_�ۼƶq = SaveManager.instance.gem;

    }
    void ��l��() // Update
    {
        // �@����������A��u�˼ƭp�ɡv
        if (statusTime > 1f)
            status = �C�����A.�˼ƭp��;
    }
    #endregion
    #region �˼ƭp��
    [SerializeField] Text �˼ƭp�ɤ�r��� = null;
    void �i�J�˼ƭp��() // Start
    {
        �˼ƭp�ɤ�r���.text = "3";
    }
    void �˼ƭp��() // Update
    {
        float �˼Ʈɶ� = 3f - statusTime;
        �˼ƭp�ɤ�r���.text = �˼Ʈɶ�.ToString("F3");
        if (statusTime > 3f && statusTime < 4f)
        {
            �˼ƭp�ɤ�r���.text = "GO";
            status = �C�����A.�C����;
        }
    }
    #endregion
    #region  �C����
    [SerializeField] int �C��[�h�֤��� = 1;
    float �W���[�����ɶ� = 0;
    [SerializeField] List<GameObject> �Ҧ����_�� = new List<GameObject>();
    void �i�J�C����() // Start
    {

        // �q�\���`�ƥ�
        SaveManager.instance.���`�ƥ� += �C�������`;

        // ���éҦ��_��
        for (int i = 0; i < �Ҧ����_��.Count; i++)
        {
            �Ҧ����_��[i].SetActive(false);
        }
        // ��X�T�����
        for (int i = 0; i < 3; i++)
        {
            // ��X�@�ӼƦr
            int �n�Q�Ұʪ��_�͸��X = UnityEngine.Random.Range(0, �Ҧ����_��.Count);
            // �Ұʸ��_��
            �Ҧ����_��[�n�Q�Ұʪ��_�͸��X].SetActive(true);
            // �q�M�椤�������_��
            �Ҧ����_��.RemoveAt(�n�Q�Ұʪ��_�͸��X);
        }

    }
    void �C����() // Update
    {
        if(statusTime > 1f)
        {
            �˼ƭp�ɤ�r���.text = null;
        }

        // �C��W�[1��O
        SaveManager.instance.ap += Time.deltaTime;
        // �@���p�ɾ�
        if (statusTime > �W���[�����ɶ� + 1f)
        {
            �W���[�����ɶ� = statusTime;
            // �C��[�W�C��n�[������
            SaveManager.instance.score += �C��[�h�֤���;
        }
    }
    void ���}�C����()
    {
        // ���}�����A�ɰh�q���`�ƥ�
        SaveManager.instance.���`�ƥ� -= �C�������`;
    }
    void �C�������`()
    {
        status = �C�����A.�C������;
    }
    #endregion
    #region �C������
    void �i�J�C������() // Start
    {

    }
    void �C������() // Update
    {

    }
    #endregion
}

// �C�|��������¬O�Ʀr �u���L�i�H�\Ū
public enum �C�����A
{
    /// <summary>��}�l�٫ܶª��ɭ�</summary>
    ��l��,
    /// <summary>�˼ƤT��}�l�C��</summary>
    �˼ƭp��,
    /// <summary>�C����</summary>
    �C����,
    /// <summary>�C������</summary>
    �C������,
}