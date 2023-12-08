using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// 玩家移動控制器

//公開  物件  命名       : 繼承Unity底層
public class PlayerMove : MonoBehaviour
{
    //單位 命名 = 預設值0

    // 物理引擎欄位
    // SerializeField = 標記要顯示在屬性面板中
    //               型別       命名
    [SerializeField] Rigidbody 物理引擎 = null; // player position
    [SerializeField] Animator 動畫系統 = null;
    [Range(0.1f, 10f)]
    [SerializeField] float 走路速度 = 2f;
    [Range(0.1f, 10f)]
    [SerializeField] float 跑步速度 = 4f;
    [Header("影響人物的加減速度")]
    [SerializeField] float 變化速度 = 10f;
    [Header("根據參照物來決定自身移動方向")]
    [SerializeField] Transform 方向參照物 = null;
    [Header("要求玩家面向某個方向")]
    [SerializeField] LocalFace 玩家面向 = null;
    [SerializeField] float 跳躍力 = 6f;
    [SerializeField] 地板檢測器 地板偵測器 = null;
    [SerializeField] float 翻滾消耗多少體力 = 4f;
    [SerializeField] float 走路音量 = 0.1f;
    [SerializeField] float 跑步音量 = 3f;
    [SerializeField] float 跳躍音量 = 5f;
    [SerializeField] Animator 鏡頭待機動畫 = null;
    public float 音量 = 0f;

    // 變數
    float ws = 0f;
    float ad = 0f;
    float sp = 0f;

    [SerializeField] UnityEvent 跳躍事件 = null;

    // 初始化 (物件被創造的瞬間執行一次)
    void Start()
    {
        // 初始化時要做的事情，只會執行一次。
        // 向遊戲管理器登記我的存在
        GamePlayManager.instance.玩家 = this.gameObject;
        GamePlayManager.instance.玩家的移動程式 = this;
    }
    // 每一個CPU幀都會執行一次
    void Update()
    {
        // 預設立場為無聲音
        音量 = 0f;


        // && 而且 及匣
        // if(MP > 10 && CD < 0) 魔力足夠且冷卻時間到了
        // || 或著 或匣
        // 血歸零 || 劇情殺

        // 如果按下了空白鍵的瞬間就跳躍
        if (Input.GetKeyDown(KeyCode.Space) && 地板偵測器.在地上 && GamePlayManager.instance.status == 遊戲狀態.遊戲中)
        {
            物理引擎.velocity = new Vector3(物理引擎.velocity.x, 跳躍力, 物理引擎.velocity.z);
            音量 = 跳躍音量;
            跳躍事件.Invoke();
        }

        // 把地板偵測器的結果傳送進動畫系統
        動畫系統.SetBool("onFloor", 地板偵測器.在地上);

        // 前翻
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftCommand))
        {
            // 如果體力足夠而且在遊戲中才翻滾
            if (SaveManager.instance.ap >= 翻滾消耗多少體力 && GamePlayManager.instance.status == 遊戲狀態.遊戲中)
            {
                動畫系統.SetTrigger("dodgeF");
                // 扣體力
                SaveManager.instance.ap -= 翻滾消耗多少體力;
            }
        }

        // 如果我按下了左邊
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // sp值逐漸向著1漸變
            sp = Mathf.Lerp(sp, 1f, Time.deltaTime * 變化速度);
        }
        else // 否則
        {
            sp = Mathf.Lerp(sp, 0f, Time.deltaTime * 變化速度);
        }

        // 每一幀都執行一次
        // 捕捉鍵盤的WSAD按鍵
        float 輸入ad = Input.GetAxisRaw("Horizontal"); // -1=A 1=D
        float 輸入ws = Input.GetAxisRaw("Vertical"); // -1=S 1=W

        // 如果遊戲狀態不是遊戲中
        if (GamePlayManager.instance.status != 遊戲狀態.遊戲中)
        {
            // 歸零WSAD數值
            輸入ad = 0;
            輸入ws = 0;
        }

        // 讀取動畫參數
        float 動畫ws = 動畫系統.GetFloat("animws");
        float 動畫ad = 動畫系統.GetFloat("animad");
        float 動畫百分比 = 動畫系統.GetFloat("animP");

        // 根據動畫的百分比來干涉wsad
        ws = Mathf.Lerp(ws, 動畫ws, 動畫百分比);
        ad = Mathf.Lerp(ad, 動畫ad, 動畫百分比);

        // 30 120 144
        // Time.deltaTime = 1/FPS

        // 讓WSAD進行漸變
        // 大約花一秒的時間將ws漸變到輸入的值
        ws = Mathf.Lerp(ws, 輸入ws, Time.deltaTime * 變化速度);
        ad = Mathf.Lerp(ad, 輸入ad, Time.deltaTime * 變化速度);

        // 設定動畫
        動畫系統.SetFloat("ws", ws);
        動畫系統.SetFloat("ad", ad);
        動畫系統.SetFloat("sp", sp);

        // 移動向量
        Vector3 移動向量 = new Vector3(ad, 0f, ws);

        // != null 不等於空 (有東西的意思)
        // 如果我有指定方向的參照物，才需要依據這個參照物來換算
        if (方向參照物 != null)
        {
            // 如果有參照物的話就將方向換算為該參照物的方向
            移動向量 = 方向參照物.TransformDirection(移動向量);
        }
        else
        {
            // 如果沒有參照物就使用自己當作參照物來決定方向
            移動向量 = this.transform.TransformDirection(移動向量);
        }
        
        // 限制向量的長度 使其不會超過1
        移動向量 = Vector3.ClampMagnitude(移動向量, 1f);

        // 根據sp值決定要走路還是跑步
        float 速度實際 = Mathf.Lerp(走路速度, 跑步速度, sp);

        // 計算動畫應有的速度
        Vector2 動畫wsad = new Vector2(動畫ad, 動畫ws);
        // 把向量長度當作動畫的整體速度使用
        float 動畫移動速度 = 動畫wsad.magnitude;
        // 根據動畫百分比來決定要用動畫還是原本的跑速or走速
        速度實際 = Mathf.Lerp(速度實際, 動畫移動速度, 動畫百分比);

        // 將移動向量乘上速度
        移動向量 = 移動向量 * 速度實際;

        // 如果有在移動
        if (移動向量.magnitude > 0.1f)
        {
            // 設定聲音
            if (sp > 0.9f)
            {
                音量 = 跑步音量;
            }
            else
            {
                音量 = 走路音量;
            }
        }

        // 為了不干涉物理引擎的重力作用所以將Y值改為原始的樣貌
        移動向量.y = 物理引擎.velocity.y;

        物理引擎.velocity = 移動向量;

        // if 如果 else 否則

        // 如果 WS的絕對值 + AD絕對值 > 0f
        if (Mathf.Abs(ws) + Mathf.Abs(ad) > 0.5f)
        {
            // 表示玩家正在移動
            // 將動畫系統中的Move值改成true
            動畫系統.SetBool("Move", true);
            // 在移動的時候要求對齊的物件對齊
            if (玩家面向 != null)
                玩家面向.對齊母物件 = true;
        }
        else
        {
            // 將動畫系統中的Move值改成true
            動畫系統.SetBool("Move", false);

            if (玩家面向 != null)
                玩家面向.對齊母物件 = false;
        }

        座標顯示器.text = " ( " + this.transform.position.x + ", " + this.transform.position.y + ", " + this.transform.position.z + " ) "; // 玩家位置


        // 如果在沒體力的狀態就開啟鏡頭待機動畫
        if (SaveManager.instance.hp < SaveManager.instance.maxhp * 0.25)
        {
            鏡頭待機動畫.SetBool("殘血", true);
        }
        else
        {
            鏡頭待機動畫.SetBool("殘血", false);
        }
    }

    [SerializeField] Text 座標顯示器 = null;

    

    // 畫開發用的參考線
    private void OnDrawGizmos()
    {
        // 繪製聲音範圍
        Gizmos.color = new Color(1f, 0f, 1f);
        Gizmos.DrawWireSphere(this.transform.position, 音量);
    }
}
