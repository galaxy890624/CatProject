using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �A�Ω�����Q�����S�ĵ{��
/// </summary>
public class BarEffect : MonoBehaviour
{
    [SerializeField] Image �ڪ��� = null;
    [SerializeField] Image �ؼб� = null;
    [SerializeField] float �����t�� = 1f;
    // �e����s���e �o�@�V������
    private void LateUpdate()
    {
        // Mathf.MoveTowards �w�B�w�q���ܪk
        // Mathf.MoveTowards (A, B, �C�@�V���\���q)

        �ڪ���.fillAmount = Mathf.MoveTowards(�ڪ���.fillAmount, �ؼб�.fillAmount, Time.deltaTime * �����t��);
        if (�ڪ���.fillAmount < �ؼб�.fillAmount)
            �ڪ���.fillAmount = �ؼб�.fillAmount;
    }
}
