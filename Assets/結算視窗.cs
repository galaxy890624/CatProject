using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class 結算視窗 : Windows<結算視窗>
{
    [SerializeField] Text 目前分數 = null;
    [SerializeField] Text 最高分 = null;
    [SerializeField] Animator 自我超越最高分動畫 = null;
    [SerializeField] Text 此局增加的寶石 = null;
    [SerializeField] GameObject 逃脫成功獎勵UI = null;
    public bool 逃脫成功 = false;

    // 我主動覆蓋底層的Start
    protected override void Start()
    {
        // 既然我消滅了底層的Start就要負責幫底層做他要做的事情
        base.Start();
        // 訂閱死亡這件事情
        SaveManager.instance.死亡事件 += 死亡;

        // 預設值為為逃脫
        逃脫成功 = false;
        // 預設關閉成功逃脫獎勵
        逃脫成功獎勵UI.SetActive(false);

    }
    private void OnDisable()
    {
        // 退訂
        SaveManager.instance.死亡事件 -= 死亡;
    }
    void 死亡()
    {
        Open();
    }

    // 視窗完全打開時顯示滑鼠
    public override void OnOpen()
    {
        base.OnOpen();
        Cursor.lockState = CursorLockMode.None;
        // 視窗開好之後如果已經自我超越就撥放動畫
        if (自我超越)
        {
            自我超越最高分動畫.SetTrigger("Play");
        }

        // 如果成功逃脫就顯示逃脫獎勵
        if (逃脫成功)
        {
            SaveManager.instance.gem += 50;
            逃脫成功獎勵UI.SetActive(true);
        }
    }

    // 視窗關閉時鎖定滑鼠
    public override void OnClose()
    {
        base.OnClose();
        Cursor.lockState = CursorLockMode.Locked;
    }

    bool 自我超越 = false;
    // 打開的一瞬間
    public override void Open()
    {
        base.Open();
        // 顯示當前分數 000,000,000
        目前分數.text = SaveManager.instance.score.ToString("N0"); // + "<color=#FFD306><size=3>分</size></color>"

        // 如果我當前的分數大於最高分 那就刷新最高分 (突破)
        if (SaveManager.instance.score >= SaveManager.instance.highScore)
        {
            SaveManager.instance.highScore = SaveManager.instance.score;
            最高分.text = "";
            自我超越 = true;
        }
        else //(未突破)
        {
            // 顯示最高分
            最高分.text = "High score " + SaveManager.instance.highScore.ToString("N0");
        }

        // 顯示此局增加了多少寶石
        此局增加的寶石.text = "+" + (SaveManager.instance.gem - GamePlayManager.instance.遊戲開始時的寶石數量).ToString("N0");
    }


    public void 重新開始()
    {
        過場動畫.instance.過場(切換場景);
    }

    void 切換場景()
    {
        SceneManager.LoadScene(0);
    }
}