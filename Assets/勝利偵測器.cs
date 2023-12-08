using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 勝利偵測器 : MonoBehaviour
{
    // 當碰撞器被設為Trigger且碰到物件時
    private void OnTriggerEnter(Collider other)
    {
        // 如果碰到玩家
        if (other.tag == "Player")
        {
            // 打開結算視窗之前先定義為成功逃脫
            結算視窗.ins.逃脫成功 = true;
            // 啟動結算視窗
            結算視窗.ins.Open();
        }
    }
}
