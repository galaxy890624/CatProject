using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class 科技樹 : MonoBehaviour
{
    [SerializeField] UnityEvent 按鈕點擊音效 = null;

    /*
     * 消耗gem升級HP,MP
     * Lv 0 ~ 20
     * [i][0] = 等級
     * [i][1] = 升級目標
     * [i][2] = 消耗寶石
     */

    public List<CostData> cost = new List<CostData>() ;

    public void 升級HP()
    {
        Debug.Log("升級HP");
        升級系統視窗.ins.點擊按鈕();
    }

    public void 升級MP()
    {
        Debug.Log("升級MP");
        升級系統視窗.ins.點擊按鈕();
    }

    public void 離開()
    {
        Debug.Log("離開");
        SceneManager.LoadScene("Menu");
    }
}
// 呼叫過場動畫
// 過場動畫.instance.過場(切換場景到第一關);

/*
 * void 切換場景到第一關()
    {
        
    }
*/

/*
 * 每次升級
 * HP*=1.3
 * MP*=1.3
 * Lv+=1
 * 寶石消耗(Lv[0])=20
 * 寶石消耗(Lv[i])=寶石消耗(Lv[i-1])*=2
 */


public struct CostData
{
    public int hpLevel;
    public float maxHp;
    public float hpCost;

    public float mpLevel;
    public float maxMp;
    public float mpCost;
}