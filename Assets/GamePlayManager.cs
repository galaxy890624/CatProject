using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayManager : AYEStatusBehaviour<遊戲狀態>
{
    #region 登記遊戲狀態
    static public GamePlayManager instance = null;
    public GameObject 玩家 = null;
    public PlayerMove 玩家的移動程式 = null;
    public Transform 玩家的頭 = null;
    public int 遊戲開始時的寶石數量 = 0;
    private void Awake()
    {
        instance = this;
        AddStatus(遊戲狀態.初始化, 進入初始化, 初始化);
        AddStatus(遊戲狀態.倒數計時, 進入倒數計時, 倒數計時);
        AddStatus(遊戲狀態.遊戲中, 進入遊戲中, 遊戲中, 離開遊戲中);
        AddStatus(遊戲狀態.遊戲結束, 進入遊戲結束, 遊戲結束);
    }
    #endregion
    #region 初始化

    [SerializeField] float 生命值每一級乘多少 = 1.3f;
    [SerializeField] float 體力值每一級乘多少 = 1.2f;

    void 進入初始化() // Start
    {
        Cursor.lockState = CursorLockMode.Locked;
        // 初始化屬性, 分數
        SaveManager.instance.maxhp = 100f * MathF.Pow(生命值每一級乘多少, SaveManager.instance.生命值等級);
        SaveManager.instance.hp = SaveManager.instance.maxhp;
        SaveManager.instance.maxap = 10f * MathF.Pow(體力值每一級乘多少, SaveManager.instance.體力等級);
        SaveManager.instance.ap = SaveManager.instance.maxap;
        SaveManager.instance.score = 0;
        SaveManager.instance.任務道具 = 0;
        // 遊戲是否結束
        SaveManager.instance.isOver = false;
        // 紀錄寶石在遊戲初始化時的數量
        遊戲開始時的寶石數量 = SaveManager.instance.gem;

    }
    void 初始化() // Update
    {
        // 一秒之後切換狀態到「倒數計時」
        if (statusTime > 1f)
            status = 遊戲狀態.倒數計時;
    }
    #endregion
    #region 倒數計時
    [SerializeField] Text 倒數計時文字方塊 = null;
    void 進入倒數計時() // Start
    {
        倒數計時文字方塊.text = "3";
    }
    void 倒數計時() // Update
    {
        float 倒數時間 = 3f - statusTime;
        倒數計時文字方塊.text = 倒數時間.ToString("F3");
        if (statusTime > 3f && statusTime < 4f)
        {
            倒數計時文字方塊.text = "GO";
            status = 遊戲狀態.遊戲中;
        }
    }
    #endregion
    #region  遊戲中
    [SerializeField] int 每秒加多少分數 = 1;
    float 上次加分的時間 = 0;
    [SerializeField] List<GameObject> 所有的鑰匙 = new List<GameObject>();
    void 進入遊戲中() // Start
    {

        // 訂閱死亡事件
        SaveManager.instance.死亡事件 += 遊戲中死亡;

        // 隱藏所有鑰匙
        for (int i = 0; i < 所有的鑰匙.Count; i++)
        {
            所有的鑰匙[i].SetActive(false);
        }
        // 抽出三把顯示
        for (int i = 0; i < 3; i++)
        {
            // 抽出一個數字
            int 要被啟動的鑰匙號碼 = UnityEngine.Random.Range(0, 所有的鑰匙.Count);
            // 啟動該鑰匙
            所有的鑰匙[要被啟動的鑰匙號碼].SetActive(true);
            // 從清單中移除該鑰匙
            所有的鑰匙.RemoveAt(要被啟動的鑰匙號碼);
        }

    }
    void 遊戲中() // Update
    {
        if(statusTime > 1f)
        {
            倒數計時文字方塊.text = null;
        }

        // 每秒增加1體力
        SaveManager.instance.ap += Time.deltaTime;
        // 一秒的計時器
        if (statusTime > 上次加分的時間 + 1f)
        {
            上次加分的時間 = statusTime;
            // 每秒加上每秒要加的分數
            SaveManager.instance.score += 每秒加多少分數;
        }
    }
    void 離開遊戲中()
    {
        // 離開此狀態時退訂死亡事件
        SaveManager.instance.死亡事件 -= 遊戲中死亡;
    }
    void 遊戲中死亡()
    {
        status = 遊戲狀態.遊戲結束;
    }
    #endregion
    #region 遊戲結束
    void 進入遊戲結束() // Start
    {

    }
    void 遊戲結束() // Update
    {

    }
    #endregion
}

// 列舉的本質依舊是數字 只不過可以閱讀
public enum 遊戲狀態
{
    /// <summary>剛開始還很黑的時候</summary>
    初始化,
    /// <summary>倒數三秒開始遊戲</summary>
    倒數計時,
    /// <summary>遊戲中</summary>
    遊戲中,
    /// <summary>遊戲結束</summary>
    遊戲結束,
}