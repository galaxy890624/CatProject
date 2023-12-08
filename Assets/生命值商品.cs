using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 生命值商品 : MonoBehaviour
{
    [SerializeField] SayStuff 錢不夠文本 = null;
    [SerializeField] SayStuff 升級成功文本 = null;

    private void Start()
    {
        刷新商店();
        // 訂閱刷新商店
        SaveManager.instance.商店刷新事件 += 刷新商店;
    }
    private void OnDestroy()
    {
        // 退訂刷新商店
        SaveManager.instance.商店刷新事件 -= 刷新商店;
    }

    [SerializeField] int HP幾等可以顯示 = 0;
    public void 刷新商店()
    {
        // 如果玩家當前的生命值等級 等同於我的等級才顯示
        this.gameObject.SetActive(SaveManager.instance.生命值等級 == HP幾等可以顯示);
    }
    [SerializeField] int 售價 = 100;
    public void 升級()
    {
        if (SaySystem.instance.isPlay == true)
        {
            return;
        }

        if (SaveManager.instance.gem < 售價)
        {
            Debug.Log("錢不夠用");
            SaySystem.instance.StartSay(錢不夠文本);
            升級系統視窗.ins.觸發升級失敗音效();
            return;
        }

        SaySystem.instance.StartSay(升級成功文本);
        SaveManager.instance.gem -= 售價;
        SaveManager.instance.生命值等級 += 1;
        SaveManager.instance.要求刷新商店();
        升級系統視窗.ins.觸發升級成功音效();
    }

}