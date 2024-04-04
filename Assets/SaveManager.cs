using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveManager
{
    #region ��ҳ]�p�Ҧ�
    public static SaveManager instance
    {
        get // ���HŪ���ڡA���H�ݭn�ڡB�ϥΧ�
        {
            // �p�G�ڤ��s�b��O���餤
            if (_instance == null)
            {
                // ����ڴN�ۤv�гy�ۤv
                _instance = new SaveManager();
            }
            // �^�ǵ����
            return _instance;
        }
    }
    static SaveManager _instance = null;
    #endregion

    /// <summary>��O</summary>
    public float ap
    {
        get // ���HŪ����O��
        {
            return _ap;
        }
        set // ���H�g�J��O��
        {
            // ����d��b0~�̤j����
            _ap = Mathf.Clamp(value, 0f, maxap);
            // ��H�ק���O�ɳq���Ҧ��q�\�ڪ��Τ�
            if (��O�ܤƨƥ� != null)
            {
                ��O�ܤƨƥ�.Invoke();
            }
        }
    }
    float _ap = 10f;
    public float maxap = 10f;
    public Action ��O�ܤƨƥ� = null;

    /// <summary> �ͩR�� </summary>
    public float hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = Mathf.Clamp(value, 0f, maxhp);
            if (��q�ܤƨƥ� != null)
            {
                ��q�ܤƨƥ�.Invoke();
            }
            // �p�G�S���� �ӥB �C���|������
            if (hp <= 0f && isOver == false || GamePlayManager.instance.���a.transform.position.y <= -100 && isOver == false) // GamePlayManager.instance.���a.transform.position.y <= -100 ���`
            {
                isOver = true;
            }
        }
    }
    float _hp = 100f;
    public float maxhp = 100f;
    public Action ��q�ܤƨƥ� = null;

    /// <summary>�O�_����</summary>
    public bool isOver
    {
        get
        {
            return _isOver;
        }
        set
        {
            _isOver = value;
            // �p�G���H�q�\���`�ƥ� �ӥB �T��w�g�C�������F
            if (���`�ƥ� != null && _isOver == true)
            {
                ���`�ƥ�.Invoke();
            }
        }
    }
    bool _isOver = false;
    public Action ���`�ƥ� = null;

    /// <summary> ���� </summary>
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (�����ܤƨƥ� != null)
            {
                �����ܤƨƥ�.Invoke();
            }
        }
    }
    int _score = 0; // �C���s���C�����k�s, ���ݭn���m�����μg�o��
    public Action �����ܤƨƥ� = null;

    /// <summary>�̰���(�sŪ��)</summary>
    public int highScore
    {
        // ���HŪ���̰������ɭ� �����h�w�Ьd��highScore��
        // �p�G�d����N��0
        get { return PlayerPrefs.GetInt("highScore", 0); }
        // ���H�g�J�̰������ɭ� �N�o�ӭȦs��w�Ъ�highScore�ȷ�
        set { PlayerPrefs.SetInt("highScore", value); }
    }

    /// <summary>
    /// �_��
    /// </summary>
    public int gem
    {
        get
        {
            return PlayerPrefs.GetInt("gem", 0);
        }
        set
        {
            PlayerPrefs.SetInt("gem", value);
            if (�_���ܤƨƥ� != null)
            {
                �_���ܤƨƥ�.Invoke();
            }
        }
    }
    public Action �_���ܤƨƥ� = null;

    /// <summary>�ͩR�ȵ���(�۰ʦs��)</summary>
    public int �ͩR�ȵ���
    {
        get
        {
            return PlayerPrefs.GetInt("HPLEVEL", 0);
        }

        set
        {
            PlayerPrefs.SetInt("HPLEVEL", value);
        }

    }

    /// <summary>��O����(�۰ʦs��)</summary>
    public int ��O����
    {
        get
        {
            return PlayerPrefs.GetInt("APLEVEL", 0);
        }

        set
        {
            PlayerPrefs.SetInt("APLEVEL", value);
        }

    }


    public Action �ө���s�ƥ� = null;
    public void �n�D��s�ө�()
    {
        if (�ө���s�ƥ� != null)
        {
            �ө���s�ƥ�.Invoke(); // ��s
        }
    }
    /// <summary>
    /// �_����ܾ�
    /// </summary>
    public int ���ȹD��
    {
        get { return _���ȹD��; }
        set
        {
            _���ȹD�� = value;
            if (���ȹD���s != null)
                ���ȹD���s.Invoke();
        }
    }
    int _���ȹD�� = 0;
    public Action ���ȹD���s = null;


}
