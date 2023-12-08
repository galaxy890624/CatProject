using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Reset()
    {
        // ��X�ۤv���Ҧ��l����
        // �ھڦۤv���X�Ӥl����]�X���j��
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // ���զb�o�Ӥl���󨭤W�䭵�ĵ{��
            AudioSource ��쪺�{�� = this.transform.GetChild(i).GetComponent<AudioSource>();
            // �p�G����쪺��
            if (��쪺�{�� != null)
            {
                // �إ߷s���ɮ�
                AudioBag �s�����ĥ] = new AudioBag();
                �s�����ĥ].���� = ��쪺�{��;
                �s�����ĥ].�W�� = this.transform.GetChild(i).name;
                // ��i�C���ݨϥ�
                ���ĦC��.Add(�s�����ĥ]);
            }
        }
    }

    public List<AudioBag> ���ĦC�� = new List<AudioBag>();

    [System.Serializable]
    public struct AudioBag
    {
        public string �W��;
        public AudioSource ����;
    }

    public void Play(string �W��)
    {
        for (int i = 0; i < ���ĦC��.Count; i++)
        {
            if (���ĦC��[i].�W��.Contains(�W��))
            {
                ���ĦC��[i].����.Play();
                return;
            }
        }
    }

}
