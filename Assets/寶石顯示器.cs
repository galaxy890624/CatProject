using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class 寶石顯示器 : MonoBehaviour
{
    private void Start()
    {
        刷新();
        SaveManager.instance.寶石變化事件 += 刷新;
    }

    private void OnDisable()
    {
        SaveManager.instance.寶石變化事件 -= 刷新;
    }

    [SerializeField] Text 寶石文字方塊 = null;

    // Update is called once per frame
    void 刷新()
    {
        寶石文字方塊.text = SaveManager.instance.gem.ToString("N0");
    }
}
