using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class 選項視窗 : Windows<選項視窗>
{
    [SerializeField] AudioMixer 音控 = null;
    [SerializeField] UnityEvent 開關視窗聲音 = null;

    protected override void Update()
    {
        base.Update(); // 先讓Windows底層做事

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 如果視窗已經打開了就關閉，否則就打開
            if (isOpen)
            {
                Close();
                開關視窗聲音.Invoke();
                // 如果我在Menu就不鎖定滑鼠
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    // 不鎖定滑鼠
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    // 遊戲中要鎖定
                    Cursor.lockState = CursorLockMode.Locked;
                }
                Time.timeScale = 1f;
            }
            else
            {
                Open();
                開關視窗聲音.Invoke();
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.001f;
            }
        }
    }

    [SerializeField] Slider 主音量拉桿 = null;
    [SerializeField] Slider 背景音樂拉桿 = null;
    [SerializeField] Slider 音效拉桿 = null;
    [SerializeField] Text 主音量顯示 = null;
    [SerializeField] Text 背景音量顯示 = null;
    [SerializeField] Text 音效音量顯示 = null;

    protected override void Start()
    {
        base.Start();

        // 從記錄檔中尋找音量大小
        float 主音量 = PlayerPrefs.GetFloat("MASTER", 0);
        float 背景音量 = PlayerPrefs.GetFloat("BG", 0);
        float 音效音量 = PlayerPrefs.GetFloat("FX", 0);
        // 修改拉桿值
        主音量拉桿.value = 主音量;
        背景音樂拉桿.value = 背景音量;
        音效拉桿.value = 音效音量;
        // 修改音量顯示
        主音量顯示.text = 主音量.ToString("F1") + "db";
        背景音量顯示.text = 背景音量.ToString("F1") + "db";
        音效音量顯示.text = 音效音量.ToString("F1") + "db";

        更新音控();
    }

    public void 數值異動()
    {
        // 從拉桿讀取數值
        float 主音量 = 主音量拉桿.value;
        float 背景音量 = 背景音樂拉桿.value;
        float 音效音量 = 音效拉桿.value;
        // 修改音量顯示
        主音量顯示.text = 主音量.ToString("F1") + "db";
        背景音量顯示.text = 背景音量.ToString("F1") + "db";
        音效音量顯示.text = 音效音量.ToString("F1") + "db";
        // 存檔
        PlayerPrefs.SetFloat("MASTER", 主音量);
        PlayerPrefs.SetFloat("BG", 背景音量);
        PlayerPrefs.SetFloat("FX", 音效音量);

        更新音控();
    }

    public void 重置()
    {
        主音量拉桿.value = 0f;
        背景音樂拉桿.value = 0f;
        音效拉桿.value = 0f;
        數值異動();
    }

    void 更新音控()
    {
        音控.SetFloat("主音量", 主音量拉桿.value);
        音控.SetFloat("背景音量", 背景音樂拉桿.value);
        音控.SetFloat("音效音量", 音效拉桿.value);
    }
}




