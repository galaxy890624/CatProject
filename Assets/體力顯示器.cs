using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class 體力顯示器 : MonoBehaviour
{
    private void Start()
    {
        // 在訂閱者當中加上我
        SaveManager.instance.體力變化事件 += 刷新;
        // 為了避免從來都沒人使用這個值 初始化時主動刷新一次
        刷新();
    }
    // 此物件被關閉或刪除的時候
    private void OnDisable()
    {
        // 當我被刪除的時候退訂這個事件
        SaveManager.instance.體力變化事件 -= 刷新;
    }

    [SerializeField] Image 體力條 = null;
    [SerializeField] Text 體力文字 = null;
    void 刷新()
    {
        // 用最大體力除與目前的體力來求出體力百分比並顯示
        體力條.fillAmount = SaveManager.instance.ap/SaveManager.instance.maxap;
        // 將目前的體力以及最大值顯示出來
        體力文字.text = SaveManager.instance.ap.ToString("F0") + " / " + SaveManager.instance.maxap.ToString("F0");
    }
}
