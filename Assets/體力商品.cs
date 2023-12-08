using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 體力商品 : MonoBehaviour
{
    [SerializeField] SayStuff 錢不夠文本 = null;
    [SerializeField] SayStuff 升級成功文本_體力 = null;
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
    [SerializeField] int 幾等可以顯示 = 0;
    void 刷新商店()
    {
        // 當前的體力等級必須要和顯示等級相等才會顯示
        this.gameObject.SetActive(SaveManager.instance.體力等級 == 幾等可以顯示);
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
            SaySystem.instance.StartSay(錢不夠文本);
            return;
        }

        SaveManager.instance.gem -= 售價;
        SaveManager.instance.體力等級 += 1;
        SaySystem.instance.StartSay(升級成功文本_體力);
        SaveManager.instance.要求刷新商店();
    }
}
