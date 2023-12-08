using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 適用於任何霸條的特效程式
/// </summary>
public class BarEffect : MonoBehaviour
{
    [SerializeField] Image 我的條 = null;
    [SerializeField] Image 目標條 = null;
    [SerializeField] float 反應速度 = 1f;
    // 畫面刷新之前 這一幀的末期
    private void LateUpdate()
    {
        // Mathf.MoveTowards 定額定量漸變法
        // Mathf.MoveTowards (A, B, 每一幀允許的量)

        我的條.fillAmount = Mathf.MoveTowards(我的條.fillAmount, 目標條.fillAmount, Time.deltaTime * 反應速度);
        if (我的條.fillAmount < 目標條.fillAmount)
            我的條.fillAmount = 目標條.fillAmount;
    }
}
