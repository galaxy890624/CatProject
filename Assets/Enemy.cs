using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : AYEMonster<怪物狀態>
{
    #region 登記
    private void Awake()
    {
        AddStatus(怪物狀態.待機, 進入待機, 待機, 離開待機);
        AddStatus(怪物狀態.巡邏, 進入巡邏, 巡邏, 離開巡邏);
        AddStatus(怪物狀態.懷疑, 進入懷疑, 懷疑, 離開懷疑);
        AddStatus(怪物狀態.追逐, 進入追逐, 追逐, 離開追逐);
        AddStatus(怪物狀態.攻擊, 進入攻擊, 攻擊, 離開攻擊);
        AddStatus(怪物狀態.尋找, 進入尋找, 尋找, 離開尋找);
    }
    #endregion
    #region 待機
    [SerializeField] Vector2 待機多久巡邏 = new Vector2(1f, 3f);
    void 進入待機()
    {

    }
    void 待機()
    {
        if (IsTime(Random.Range(待機多久巡邏.x, 待機多久巡邏.y)))
        {
            status = 怪物狀態.巡邏;
        }
    }
    void 離開待機()
    {

    }
    #endregion
    #region 巡邏
    [SerializeField] Vector2 巡邏範圍 = new Vector2(5f, 20f);
    Vector3 巡邏目標位置 = Vector3.zero;
    void 進入巡邏()
    {
        // 決定目的地
        巡邏目標位置 = GetRandomAIPos(巡邏範圍.x, 巡邏範圍.y, GamePlayManager.instance.玩家.transform.position);
    }
    void 巡邏()
    {
        // 要求角色向該位置前進 (無位移)
        UpdateWay(巡邏目標位置);
        if (wayAngle > 20f)
        {
            // 離要去的方向角度還很大先不要走路
            anim.SetBool("跑", false);
        }
        else
        {
            // 如果已經面向要去的方向就播放動畫
            anim.SetBool("跑", true);
        }
        // 15秒為巡邏上限
        if (statusTime > 15f)
            status = 怪物狀態.待機;
        // 如果本已經到達目的地就提早離開
        if (IsClose(巡邏目標位置, true))
            status = 怪物狀態.待機;
    }
    void 離開巡邏()
    {
        // 取消動畫
        anim.SetBool("跑", false);
    }
    #endregion
    #region 懷疑
    [SerializeField] float 懷疑多久 = 0.5f;
    void 進入懷疑()
    {
        Face(GamePlayManager.instance.玩家的頭);
    }
    void 懷疑()
    {
        // 懷疑一小段時間, 假如
        if (statusTime > 懷疑多久)
        {
            Vector3 玩家方向 = GamePlayManager.instance.玩家的頭.position - 眼睛.position;
            bool 有障礙物 = Physics.Raycast(眼睛.position, 玩家方向, 玩家方向.magnitude, 視線障礙物圖層);

            if (有障礙物 == false)
            {
                status = 怪物狀態.追逐;
            }
            else
            {
                status = 怪物狀態.巡邏;
            }
        }
    }
    void 離開懷疑()
    {
        // 取消盯著玩家
        CancelFace();
    }
    #endregion
    #region 追逐
    float 持續追逐能量 = 0f;
    [SerializeField] float 追逐最大時間 = 4f;
    void 進入追逐()
    {
        持續追逐能量 = 追逐最大時間;
        anim.SetBool("跑", true);
    }
    void 追逐()
    {
        UpdateWay(GamePlayManager.instance.玩家.transform.position, 999f, 10f);
        // 如果追逐的能量耗盡了, 那就到尋找狀態
        持續追逐能量 -= Time.deltaTime;
        if (持續追逐能量 <= 0f)
        {
            status = 怪物狀態.尋找;
        }

        // 如果追到玩家, 就攻擊他
        if (IsClose(GamePlayManager.instance.玩家.transform.position, true, 2f))
        {
            status = 怪物狀態.攻擊;
        }

    }
    void 離開追逐()
    {
        anim.SetBool("跑", false);
    }
    #endregion
    #region 尋找
    Vector3 尋找目的地 = Vector3.zero;
    [SerializeField] Vector2 尋找範圍 = new Vector2(0f, 30f);
    [SerializeField] float 離玩家多近要重抽 = 10f;
    void 進入尋找()
    {
        // 抽出一個玩家附近的位置
        尋找目的地 = GetRandomAIPos(尋找範圍.x, 尋找範圍.y, GamePlayManager.instance.玩家.transform.position);

        // 當條件成立時, 會無限執行下去 
        while(Vector3.Distance(尋找目的地, GamePlayManager.instance.玩家.transform.position) < 離玩家多近要重抽)
        {
            尋找目的地 = GetRandomAIPos(尋找範圍.x, 尋找範圍.y, GamePlayManager.instance.玩家.transform.position);
        }
    }
    void 尋找()
    {
        // 要求角色向該位置前進 (無位移)
        UpdateWay(尋找目的地);
        if (wayAngle > 20f)
        {
            // 離要去的方向角度還很大先不要走路
            anim.SetBool("跑", false);
        }
        else
        {
            // 如果已經面向要去的方向就播放動畫
            anim.SetBool("跑", true);
        }
        // 15秒為巡邏上限
        if (statusTime > 15f)
            status = 怪物狀態.待機;
        // 如果本已經到達目的地就提早離開
        if (IsClose(尋找目的地, true))
            status = 怪物狀態.待機;
    }
    void 離開尋找()
    {
        // 取消動畫
        anim.SetBool("跑", false);
    }
    #endregion
    #region 攻擊
    [SerializeField] float 攻擊力 = 999f;
    void 進入攻擊()
    {
        SaveManager.instance.hp -= 攻擊力;
        震動系統.instance.震動(1f);
        噴血效果.instance.噴血();
        閃爍系統.instance.閃爍();
    }
    void 攻擊()
    {

    }
    void 離開攻擊()
    {

    }
    #endregion

    [SerializeField] float 視野距離 = 15f;
    [SerializeField] Transform 眼睛 = null;
    [SerializeField] float 視野角度 = 90f;
    [SerializeField] LayerMask 視線障礙物圖層 = 0;
    [SerializeField] float 絕對距離 = 4f;
    [SerializeField] float 聽力範圍 = 5f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float 玩家與我的距離 = Vector3.Distance(this.transform.position, GamePlayManager.instance.玩家.transform.position);

        // 判定玩家是不是在視野距離內
        if (Vector3.Distance(this.transform.position, GamePlayManager.instance.玩家的頭.position) < 視野距離)
        {
            // AB = B - A 沿著眼睛前方的向量
            Vector3 ab = 眼睛.forward;
            // AC = C - A 玩家位置 - 眼睛位置
            Vector3 ac = GamePlayManager.instance.玩家的頭.position - 眼睛.position;

            // 兩個向量的夾角
            float 眼睛與玩家的夾角 = Vector3.Angle(ab, ac);

            if (眼睛與玩家的夾角 < 視野角度 || 玩家與我的距離 < 絕對距離)
            {
                // 怪物的眼睛到玩家的頭部之間是否暢通無阻
                RaycastHit[] 所有擊中的對象 = Physics.RaycastAll(眼睛.position, ac, ac.magnitude, 視線障礙物圖層);
                // 如果雷射沒有擊中任何東西, 表示怪物的眼睛到玩家的頭部之間是否暢通無阻
                if (所有擊中的對象.Length <= 0 || 玩家與我的距離 < 絕對距離)
                {
                    // 看見玩家
                    // 如果是待機或巡邏時, 看見玩家就切換到懷疑狀態
                    if (status == 怪物狀態.待機 || status == 怪物狀態.巡邏 || status == 怪物狀態.尋找)
                    {
                        status = 怪物狀態.懷疑;
                    }

                    // 如果已經在追逐的狀態下, 依舊看見了玩家, 那就補滿追逐能量
                    if (status == 怪物狀態.追逐)
                    {
                        持續追逐能量 = 追逐最大時間;
                    }
                }
            }
        }

        // 如果玩家在聽力範圍內
        float 總音量 = GamePlayManager.instance.玩家的移動程式.音量 + 聽力範圍;

        // 如果玩家的音量是0 直接歸零總音量
        if (GamePlayManager.instance.玩家的移動程式.音量 == 0f)
        {
            總音量 = 0f;
        }

        // 如果離玩家的距離小於總音量
        if (玩家與我的距離 < 總音量)
        {
            // 怪物聽見了玩家
            if (status == 怪物狀態.待機 || status == 怪物狀態.巡邏 || status == 怪物狀態.尋找)
            {
                status = 怪物狀態.懷疑;
            }
        }

    }
}

public enum 怪物狀態
{
    待機,
    巡邏,
    懷疑,
    追逐, // 緊張音效
    尋找, // 失去目標到處逛街
    攻擊, // 抓到就死
}


