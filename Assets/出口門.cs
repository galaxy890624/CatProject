using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 出口門 : MonoBehaviour
{
    [SerializeField] 地板檢測器 地板檢測器;
    [SerializeField] Animator 門的動畫控制器;
    bool 是否對話過 = false;
    [SerializeField] SayStuff 任務目標對話 = null;
    [SerializeField] int 需要幾把鑰匙 = 1;
    private void Update()
    {
        // 如果有偵測到玩家就開門
        if (地板檢測器.在地上)
        {
            // 如果鑰匙已經足夠了就爽快的開門
            if (SaveManager.instance.任務道具 >= 需要幾把鑰匙)
            {
                門的動畫控制器.SetBool("開關", true);
                return;
            }

            // 如果沒有對話過的話就開啟對話提醒玩家要找鑰匙
            if (是否對話過 == false)
            {
                是否對話過 = true;
                SaySystem.instance.StartSay(任務目標對話);
            }
        }
        else
        {
            門的動畫控制器.SetBool("開關", false);
        }
    }
}