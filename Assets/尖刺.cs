using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 尖刺 : MonoBehaviour
{
    [SerializeField] float 攻擊力 = 10f;
    [SerializeField] float 震動幅度 = 0.2f;
    // Trigger的進入事件
    private void OnTriggerEnter(Collider other)
    {
        // other = 碰到我的
        // 如果我碰到玩家了
        if (other.tag == "Player")
        {
            // 遇到玩家就扣血
            SaveManager.instance.hp -= 攻擊力;
            // 呼叫震動系統
            震動系統.instance.震動(震動幅度);
            // 呼叫閃爍系統
            閃爍系統.instance.閃爍();
            // 呼叫噴血
            噴血效果.instance.噴血();
        }
    }
}
