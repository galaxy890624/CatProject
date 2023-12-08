using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : AYEMonster<�Ǫ����A>
{
    #region �n�O
    private void Awake()
    {
        AddStatus(�Ǫ����A.�ݾ�, �i�J�ݾ�, �ݾ�, ���}�ݾ�);
        AddStatus(�Ǫ����A.����, �i�J����, ����, ���}����);
        AddStatus(�Ǫ����A.�h��, �i�J�h��, �h��, ���}�h��);
        AddStatus(�Ǫ����A.�l�v, �i�J�l�v, �l�v, ���}�l�v);
        AddStatus(�Ǫ����A.����, �i�J����, ����, ���}����);
        AddStatus(�Ǫ����A.�M��, �i�J�M��, �M��, ���}�M��);
    }
    #endregion
    #region �ݾ�
    [SerializeField] Vector2 �ݾ��h�[���� = new Vector2(1f, 3f);
    void �i�J�ݾ�()
    {

    }
    void �ݾ�()
    {
        if (IsTime(Random.Range(�ݾ��h�[����.x, �ݾ��h�[����.y)))
        {
            status = �Ǫ����A.����;
        }
    }
    void ���}�ݾ�()
    {

    }
    #endregion
    #region ����
    [SerializeField] Vector2 ���޽d�� = new Vector2(5f, 20f);
    Vector3 ���ޥؼЦ�m = Vector3.zero;
    void �i�J����()
    {
        // �M�w�ت��a
        ���ޥؼЦ�m = GetRandomAIPos(���޽d��.x, ���޽d��.y, GamePlayManager.instance.���a.transform.position);
    }
    void ����()
    {
        // �n�D����V�Ӧ�m�e�i (�L�첾)
        UpdateWay(���ޥؼЦ�m);
        if (wayAngle > 20f)
        {
            // ���n�h����V�����٫ܤj�����n����
            anim.SetBool("�]", false);
        }
        else
        {
            // �p�G�w�g���V�n�h����V�N����ʵe
            anim.SetBool("�]", true);
        }
        // 15�����ޤW��
        if (statusTime > 15f)
            status = �Ǫ����A.�ݾ�;
        // �p�G���w�g��F�ت��a�N�������}
        if (IsClose(���ޥؼЦ�m, true))
            status = �Ǫ����A.�ݾ�;
    }
    void ���}����()
    {
        // �����ʵe
        anim.SetBool("�]", false);
    }
    #endregion
    #region �h��
    [SerializeField] float �h�æh�[ = 0.5f;
    void �i�J�h��()
    {
        Face(GamePlayManager.instance.���a���Y);
    }
    void �h��()
    {
        // �h�ä@�p�q�ɶ�, ���p
        if (statusTime > �h�æh�[)
        {
            Vector3 ���a��V = GamePlayManager.instance.���a���Y.position - ����.position;
            bool ����ê�� = Physics.Raycast(����.position, ���a��V, ���a��V.magnitude, ���u��ê���ϼh);

            if (����ê�� == false)
            {
                status = �Ǫ����A.�l�v;
            }
            else
            {
                status = �Ǫ����A.����;
            }
        }
    }
    void ���}�h��()
    {
        // �����n�۪��a
        CancelFace();
    }
    #endregion
    #region �l�v
    float ����l�v��q = 0f;
    [SerializeField] float �l�v�̤j�ɶ� = 4f;
    void �i�J�l�v()
    {
        ����l�v��q = �l�v�̤j�ɶ�;
        anim.SetBool("�]", true);
    }
    void �l�v()
    {
        UpdateWay(GamePlayManager.instance.���a.transform.position, 999f, 10f);
        // �p�G�l�v����q�ӺɤF, ���N��M�䪬�A
        ����l�v��q -= Time.deltaTime;
        if (����l�v��q <= 0f)
        {
            status = �Ǫ����A.�M��;
        }

        // �p�G�l�쪱�a, �N�����L
        if (IsClose(GamePlayManager.instance.���a.transform.position, true, 2f))
        {
            status = �Ǫ����A.����;
        }

    }
    void ���}�l�v()
    {
        anim.SetBool("�]", false);
    }
    #endregion
    #region �M��
    Vector3 �M��ت��a = Vector3.zero;
    [SerializeField] Vector2 �M��d�� = new Vector2(0f, 30f);
    [SerializeField] float �����a�h��n���� = 10f;
    void �i�J�M��()
    {
        // ��X�@�Ӫ��a���񪺦�m
        �M��ت��a = GetRandomAIPos(�M��d��.x, �M��d��.y, GamePlayManager.instance.���a.transform.position);

        // ����󦨥߮�, �|�L������U�h 
        while(Vector3.Distance(�M��ت��a, GamePlayManager.instance.���a.transform.position) < �����a�h��n����)
        {
            �M��ت��a = GetRandomAIPos(�M��d��.x, �M��d��.y, GamePlayManager.instance.���a.transform.position);
        }
    }
    void �M��()
    {
        // �n�D����V�Ӧ�m�e�i (�L�첾)
        UpdateWay(�M��ت��a);
        if (wayAngle > 20f)
        {
            // ���n�h����V�����٫ܤj�����n����
            anim.SetBool("�]", false);
        }
        else
        {
            // �p�G�w�g���V�n�h����V�N����ʵe
            anim.SetBool("�]", true);
        }
        // 15�����ޤW��
        if (statusTime > 15f)
            status = �Ǫ����A.�ݾ�;
        // �p�G���w�g��F�ت��a�N�������}
        if (IsClose(�M��ت��a, true))
            status = �Ǫ����A.�ݾ�;
    }
    void ���}�M��()
    {
        // �����ʵe
        anim.SetBool("�]", false);
    }
    #endregion
    #region ����
    [SerializeField] float �����O = 999f;
    void �i�J����()
    {
        SaveManager.instance.hp -= �����O;
        �_�ʨt��.instance.�_��(1f);
        �Q��ĪG.instance.�Q��();
        �{�{�t��.instance.�{�{();
    }
    void ����()
    {

    }
    void ���}����()
    {

    }
    #endregion

    [SerializeField] float �����Z�� = 15f;
    [SerializeField] Transform ���� = null;
    [SerializeField] float �������� = 90f;
    [SerializeField] LayerMask ���u��ê���ϼh = 0;
    [SerializeField] float ����Z�� = 4f;
    [SerializeField] float ť�O�d�� = 5f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float ���a�P�ڪ��Z�� = Vector3.Distance(this.transform.position, GamePlayManager.instance.���a.transform.position);

        // �P�w���a�O���O�b�����Z����
        if (Vector3.Distance(this.transform.position, GamePlayManager.instance.���a���Y.position) < �����Z��)
        {
            // AB = B - A �u�۲����e�誺�V�q
            Vector3 ab = ����.forward;
            // AC = C - A ���a��m - ������m
            Vector3 ac = GamePlayManager.instance.���a���Y.position - ����.position;

            // ��ӦV�q������
            float �����P���a������ = Vector3.Angle(ab, ac);

            if (�����P���a������ < �������� || ���a�P�ڪ��Z�� < ����Z��)
            {
                // �Ǫ��������쪱�a���Y�������O�_�Z�q�L��
                RaycastHit[] �Ҧ���������H = Physics.RaycastAll(����.position, ac, ac.magnitude, ���u��ê���ϼh);
                // �p�G�p�g�S����������F��, ��ܩǪ��������쪱�a���Y�������O�_�Z�q�L��
                if (�Ҧ���������H.Length <= 0 || ���a�P�ڪ��Z�� < ����Z��)
                {
                    // �ݨ����a
                    // �p�G�O�ݾ��Ψ��ޮ�, �ݨ����a�N�������h�ê��A
                    if (status == �Ǫ����A.�ݾ� || status == �Ǫ����A.���� || status == �Ǫ����A.�M��)
                    {
                        status = �Ǫ����A.�h��;
                    }

                    // �p�G�w�g�b�l�v�����A�U, ���¬ݨ��F���a, ���N�ɺ��l�v��q
                    if (status == �Ǫ����A.�l�v)
                    {
                        ����l�v��q = �l�v�̤j�ɶ�;
                    }
                }
            }
        }

        // �p�G���a�bť�O�d��
        float �`���q = GamePlayManager.instance.���a�����ʵ{��.���q + ť�O�d��;

        // �p�G���a�����q�O0 �����k�s�`���q
        if (GamePlayManager.instance.���a�����ʵ{��.���q == 0f)
        {
            �`���q = 0f;
        }

        // �p�G�����a���Z���p���`���q
        if (���a�P�ڪ��Z�� < �`���q)
        {
            // �Ǫ�ť���F���a
            if (status == �Ǫ����A.�ݾ� || status == �Ǫ����A.���� || status == �Ǫ����A.�M��)
            {
                status = �Ǫ����A.�h��;
            }
        }

    }
}

public enum �Ǫ����A
{
    �ݾ�,
    ����,
    �h��,
    �l�v, // ��i����
    �M��, // ���h�ؼШ�B�}��
    ����, // ���N��
}


