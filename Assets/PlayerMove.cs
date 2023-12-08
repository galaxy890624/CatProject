using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// ���a���ʱ��

//���}  ����  �R�W       : �~��Unity���h
public class PlayerMove : MonoBehaviour
{
    //��� �R�W = �w�]��0

    // ���z�������
    // SerializeField = �аO�n��ܦb�ݩʭ��O��
    //               ���O       �R�W
    [SerializeField] Rigidbody ���z���� = null; // player position
    [SerializeField] Animator �ʵe�t�� = null;
    [Range(0.1f, 10f)]
    [SerializeField] float �����t�� = 2f;
    [Range(0.1f, 10f)]
    [SerializeField] float �]�B�t�� = 4f;
    [Header("�v�T�H�����[��t��")]
    [SerializeField] float �ܤƳt�� = 10f;
    [Header("�ھڰѷӪ��ӨM�w�ۨ����ʤ�V")]
    [SerializeField] Transform ��V�ѷӪ� = null;
    [Header("�n�D���a���V�Y�Ӥ�V")]
    [SerializeField] LocalFace ���a���V = null;
    [SerializeField] float ���D�O = 6f;
    [SerializeField] �a�O�˴��� �a�O������ = null;
    [SerializeField] float ½�u���Ӧh����O = 4f;
    [SerializeField] float �������q = 0.1f;
    [SerializeField] float �]�B���q = 3f;
    [SerializeField] float ���D���q = 5f;
    [SerializeField] Animator ���Y�ݾ��ʵe = null;
    public float ���q = 0f;

    // �ܼ�
    float ws = 0f;
    float ad = 0f;
    float sp = 0f;

    [SerializeField] UnityEvent ���D�ƥ� = null;

    // ��l�� (����Q�гy����������@��)
    void Start()
    {
        // ��l�Ʈɭn�����Ʊ��A�u�|����@���C
        // �V�C���޲z���n�O�ڪ��s�b
        GamePlayManager.instance.���a = this.gameObject;
        GamePlayManager.instance.���a�����ʵ{�� = this;
    }
    // �C�@��CPU�V���|����@��
    void Update()
    {
        // �w�]�߳����L�n��
        ���q = 0f;


        // && �ӥB �ΧX
        // if(MP > 10 && CD < 0) �]�O�����B�N�o�ɶ���F
        // || �ε� �ΧX
        // ���k�s || �@����

        // �p�G���U�F�ť��䪺�����N���D
        if (Input.GetKeyDown(KeyCode.Space) && �a�O������.�b�a�W && GamePlayManager.instance.status == �C�����A.�C����)
        {
            ���z����.velocity = new Vector3(���z����.velocity.x, ���D�O, ���z����.velocity.z);
            ���q = ���D���q;
            ���D�ƥ�.Invoke();
        }

        // ��a�O�����������G�ǰe�i�ʵe�t��
        �ʵe�t��.SetBool("onFloor", �a�O������.�b�a�W);

        // �e½
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftCommand))
        {
            // �p�G��O�����ӥB�b�C�����~½�u
            if (SaveManager.instance.ap >= ½�u���Ӧh����O && GamePlayManager.instance.status == �C�����A.�C����)
            {
                �ʵe�t��.SetTrigger("dodgeF");
                // ����O
                SaveManager.instance.ap -= ½�u���Ӧh����O;
            }
        }

        // �p�G�ګ��U�F����
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // sp�ȳv���V��1����
            sp = Mathf.Lerp(sp, 1f, Time.deltaTime * �ܤƳt��);
        }
        else // �_�h
        {
            sp = Mathf.Lerp(sp, 0f, Time.deltaTime * �ܤƳt��);
        }

        // �C�@�V������@��
        // ������L��WSAD����
        float ��Jad = Input.GetAxisRaw("Horizontal"); // -1=A 1=D
        float ��Jws = Input.GetAxisRaw("Vertical"); // -1=S 1=W

        // �p�G�C�����A���O�C����
        if (GamePlayManager.instance.status != �C�����A.�C����)
        {
            // �k�sWSAD�ƭ�
            ��Jad = 0;
            ��Jws = 0;
        }

        // Ū���ʵe�Ѽ�
        float �ʵews = �ʵe�t��.GetFloat("animws");
        float �ʵead = �ʵe�t��.GetFloat("animad");
        float �ʵe�ʤ��� = �ʵe�t��.GetFloat("animP");

        // �ھڰʵe���ʤ���Ӥz�Awsad
        ws = Mathf.Lerp(ws, �ʵews, �ʵe�ʤ���);
        ad = Mathf.Lerp(ad, �ʵead, �ʵe�ʤ���);

        // 30 120 144
        // Time.deltaTime = 1/FPS

        // ��WSAD�i�溥��
        // �j����@���ɶ��Nws���ܨ��J����
        ws = Mathf.Lerp(ws, ��Jws, Time.deltaTime * �ܤƳt��);
        ad = Mathf.Lerp(ad, ��Jad, Time.deltaTime * �ܤƳt��);

        // �]�w�ʵe
        �ʵe�t��.SetFloat("ws", ws);
        �ʵe�t��.SetFloat("ad", ad);
        �ʵe�t��.SetFloat("sp", sp);

        // ���ʦV�q
        Vector3 ���ʦV�q = new Vector3(ad, 0f, ws);

        // != null ������� (���F�誺�N��)
        // �p�G�ڦ����w��V���ѷӪ��A�~�ݭn�̾ڳo�ӰѷӪ��Ӵ���
        if (��V�ѷӪ� != null)
        {
            // �p�G���ѷӪ����ܴN�N��V���⬰�ӰѷӪ�����V
            ���ʦV�q = ��V�ѷӪ�.TransformDirection(���ʦV�q);
        }
        else
        {
            // �p�G�S���ѷӪ��N�ϥΦۤv��@�ѷӪ��ӨM�w��V
            ���ʦV�q = this.transform.TransformDirection(���ʦV�q);
        }
        
        // ����V�q������ �Ϩ䤣�|�W�L1
        ���ʦV�q = Vector3.ClampMagnitude(���ʦV�q, 1f);

        // �ھ�sp�ȨM�w�n�����٬O�]�B
        float �t�׹�� = Mathf.Lerp(�����t��, �]�B�t��, sp);

        // �p��ʵe�������t��
        Vector2 �ʵewsad = new Vector2(�ʵead, �ʵews);
        // ��V�q���׷�@�ʵe������t�רϥ�
        float �ʵe���ʳt�� = �ʵewsad.magnitude;
        // �ھڰʵe�ʤ���ӨM�w�n�ΰʵe�٬O�쥻���]�tor���t
        �t�׹�� = Mathf.Lerp(�t�׹��, �ʵe���ʳt��, �ʵe�ʤ���);

        // �N���ʦV�q���W�t��
        ���ʦV�q = ���ʦV�q * �t�׹��;

        // �p�G���b����
        if (���ʦV�q.magnitude > 0.1f)
        {
            // �]�w�n��
            if (sp > 0.9f)
            {
                ���q = �]�B���q;
            }
            else
            {
                ���q = �������q;
            }
        }

        // ���F���z�A���z���������O�@�ΩҥH�NY�ȧאּ��l���˻�
        ���ʦV�q.y = ���z����.velocity.y;

        ���z����.velocity = ���ʦV�q;

        // if �p�G else �_�h

        // �p�G WS������� + AD����� > 0f
        if (Mathf.Abs(ws) + Mathf.Abs(ad) > 0.5f)
        {
            // ��ܪ��a���b����
            // �N�ʵe�t�Τ���Move�ȧ令true
            �ʵe�t��.SetBool("Move", true);
            // �b���ʪ��ɭԭn�D�����������
            if (���a���V != null)
                ���a���V.��������� = true;
        }
        else
        {
            // �N�ʵe�t�Τ���Move�ȧ令true
            �ʵe�t��.SetBool("Move", false);

            if (���a���V != null)
                ���a���V.��������� = false;
        }

        �y����ܾ�.text = " ( " + this.transform.position.x + ", " + this.transform.position.y + ", " + this.transform.position.z + " ) "; // ���a��m


        // �p�G�b�S��O�����A�N�}�����Y�ݾ��ʵe
        if (SaveManager.instance.hp < SaveManager.instance.maxhp * 0.25)
        {
            ���Y�ݾ��ʵe.SetBool("�ݦ�", true);
        }
        else
        {
            ���Y�ݾ��ʵe.SetBool("�ݦ�", false);
        }
    }

    [SerializeField] Text �y����ܾ� = null;

    

    // �e�}�o�Ϊ��Ѧҽu
    private void OnDrawGizmos()
    {
        // ø�s�n���d��
        Gizmos.color = new Color(1f, 0f, 1f);
        Gizmos.DrawWireSphere(this.transform.position, ���q);
    }
}
