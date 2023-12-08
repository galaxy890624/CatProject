using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class 分數顯示器 : MonoBehaviour
{
    void Start()
    {
        SaveManager.instance.分數變化事件 += 刷新;
        刷新();
    }
    private void OnDisable()
    {
        SaveManager.instance.分數變化事件 -= 刷新;
    }
    [SerializeField] Text 分數文字 = null;
    void 刷新()
    {
        // 將分數用000,000,000的方式顯示
        分數文字.text = SaveManager.instance.score.ToString("N0");

        /*if (SaveManager.instance.score >= 10)
        {
            分數文字.text = <Color="#ff00ff">SaveManager.instance.score.ToString("N0")</Color>;
        }*/
    }
}
