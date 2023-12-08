using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class 升級系統視窗 : SingletonMonoBehaviour<升級系統視窗>
{
    [SerializeField] UnityEvent 升級成功音效 = null;
    [SerializeField] UnityEvent 升級失敗音效 = null;
    [SerializeField] UnityEvent 關閉頁面音效 = null;
    [SerializeField] UnityEvent 按下按鈕 = null;
    [SerializeField] UnityEvent 滑鼠經過音效 = null;

    public void 觸發升級成功音效()
    {
        升級成功音效.Invoke();
    }
    public void 觸發升級失敗音效()
    {
        升級失敗音效.Invoke();
    }
    public void 滑鼠經過()
    {
        滑鼠經過音效.Invoke();
    }
    public void 點擊按鈕()
    {
        按下按鈕.Invoke();
    }
}
