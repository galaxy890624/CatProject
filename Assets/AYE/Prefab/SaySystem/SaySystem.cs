using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class SaySystem : MonoBehaviour
{
    public static SaySystem instance = null;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] SayStuff testData = null;
    private void Start()
    {
        // 播放測試文件
        if (testData != null)
        {
            SaySystem.instance.StartSay(testData);
        }
    }

    SayStuff current;
    public bool isPlay = false;

    /// <summary>開始對話</summary>
    public void StartSay(SayStuff sayStuff)
    {
        // 如果正在對話就忽略這個命令
        if (isPlay == true)
        {
            Debug.LogError("已經在對話了，不能加入新的命令。");
            return;
        }
        // 取得文本
        current = sayStuff;
        // 開始異步執行對話
        StartCoroutine(對話());
    }
    [SerializeField] Animator anim = null;
    [SerializeField] Text leftTitle = null;
    [SerializeField] Text rightTitle = null;
    [SerializeField] Text mainBox = null;
    [SerializeField] Transform continueObj = null;
    IEnumerator 對話()
    {
        isPlay = true;
        // 啟動動畫
        anim.SetBool("Play", true);
        // 如果是左邊的人名 就顯示人名在左邊的文字方塊中 如果不是就填入空白
        leftTitle.text = current.list[0].isLeft ? current.list[0].npcName : "";
        rightTitle.text = current.list[0].isLeft ? "" : current.list[0].npcName;
        // 先不顯示內容 關閉提示
        mainBox.text = "";
        // 關閉右下角E提示
        continueObj.localScale = Vector3.zero;
        // 等待0.5秒
        yield return new WaitForSeconds(0.5f);

        // 對話總表
        for (int j = 0; j < current.list.Count; j++)
        {

            // 開始這句話之前設定好人名並且關閉提示
            leftTitle.text = current.list[j].isLeft ? current.list[j].npcName : "";
            rightTitle.text = current.list[j].isLeft ? "" : current.list[j].npcName;
            continueObj.localScale = Vector3.zero;
            // 逐步顯示每一個字到畫面上
            string 最終顯示的內容 = "";
            for (int i = 0; i < current.list[j].info.Length; i++)
            {
                // 有幾個字就會跑幾圈
                最終顯示的內容 = 最終顯示的內容 + current.list[j].info[i];
                // 顯示到畫面上
                mainBox.text = 最終顯示的內容;

                if (AudioManager.instance != null)
                    AudioManager.instance.Play("打字");

                // 每顯示一個字等待0.05秒
                yield return new WaitForSeconds(0.05f);
            }
            // 顯示繼續提示 讓玩家按了繼續
            continueObj.localScale = Vector3.one;
            isStop = true;
            // 如果還沒按E 就卡死在這邊等待
            while (needContinue == false)
            {
                // 等待0.1秒
                yield return new WaitForSeconds(0.1f);
            }
            isStop = false;
            needContinue = false;
        }

        // 所有的迴圈都結束了 關閉動畫並且等0.5秒後表示對話結束
        anim.SetBool("Play", false);
        yield return new WaitForSeconds(0.5f);
        isPlay = false;
    }

    bool needContinue = false;
    bool isStop = false;
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.Mouse0)) && isStop)
        {
            needContinue = true;
        }
    }
}