using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 血條顯示器 : MonoBehaviour
{
    void Start()
    {
        SaveManager.instance.血量變化事件 += 刷新;
        刷新();
    }
    private void OnDisable()
    {
        SaveManager.instance.血量變化事件 -= 刷新;
    }
    [SerializeField] Image 血條 = null;
    [SerializeField] Text 血量文字 = null;
    void 刷新()
    {
        血條.fillAmount = SaveManager.instance.hp / SaveManager.instance.maxhp;
        血量文字.text = SaveManager.instance.hp.ToString("F0") + " / " + SaveManager.instance.maxhp.ToString("F0");
    }
}
