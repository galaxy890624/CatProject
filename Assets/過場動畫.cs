using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class 過場動畫 : MonoBehaviour
{
    #region 假單例
    // 假單例
    public static 過場動畫 instance = null;
    // 最早的初始化
    private void Awake()
    {
        // 向所有人宣告我就是instance
        instance = this;
    }
    #endregion

    [SerializeField] Animator 動畫系統 = null;

    Action 暫存變暗之後要做的事情 = null;

    /// <summary>要求播放離場動畫並且委託要做的事情</summary>
    public void 過場(Action 變暗之後要幹嘛)
    {
        // 記住待會兒要做什麼事情
        暫存變暗之後要做的事情 = 變暗之後要幹嘛;
        // 播放動畫
        動畫系統.SetTrigger("Run");
        // 1.1秒之後執行變暗要做的事情
        Invoke("變暗之後", 1.1f);
    }

    void 變暗之後()
    {
        // 如果有事情拜託我做 我就做
        if (暫存變暗之後要做的事情 != null)
        {
            暫存變暗之後要做的事情.Invoke();
        }
    }
}
