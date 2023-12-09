using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using AYE;

public class 選項視窗 : Windows<選項視窗>
{
    [SerializeField] AudioMixer 音控 = null;
    [SerializeField] UnityEvent 開關視窗聲音 = null;
    [SerializeField] Text fps顯示 = null;
    float fps = 0f;
    int fpsCount = 0;
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

        // 每累計100次fps才顯示一次
        if (fpsCount >= 100)
        {
            fpsCount = 0;
            // 顯示FPS
            fps顯示.text = "FPS : " + (fps / 100f).ToString("F1");
            fps = 0f;
        }
        fps += 1f / Time.unscaledDeltaTime;
        fpsCount += 1;
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

    public void 全螢幕按鈕()
    {
        Screen.fullScreen = true;
    }
    public void 視窗化按鈕()
    {
        Screen.SetResolution((int)(Screen.width * 0.5f), (int)(Screen.height * 0.5f), false);
    }
    public void 全螢幕視窗化()
    {
        Screen.SetResolution(Screen.width, Screen.height, false);
    }
    public void 高畫質()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void 中畫質()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void 低畫質()
    {
        QualitySettings.SetQualityLevel(0);
    }
}
