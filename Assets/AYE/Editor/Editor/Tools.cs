using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

public class Tools : MonoBehaviour
{
    [MenuItem("工具/清除所有資料")]
    static public void 清除資料()
    {
        // 移除所有關於這個遊戲的資料
        PlayerPrefs.DeleteAll();
        // 要求Unity立刻做事, 避免在遊戲未執行時不做事(燈光,...)
        PlayerPrefs.Save();
    }
    [MenuItem("工具/添加100M寶石")]
    static public void 加100M寶石()
    {
        SaveManager.instance.gem += 100000000;
    }

    [MenuItem("工具/立刻自爆")]
    static public void 立刻自爆()
    {
        //如果是在執行當中, 才可以殺掉玩家

        if (Application.isPlaying)
        SaveManager.instance.hp -= float.MaxValue;
    }
}