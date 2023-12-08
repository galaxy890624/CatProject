using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 震動系統 : MonoBehaviour
{
    // 單例模式
    static public 震動系統 instance = null;
    private void Awake()
    {
        instance = this;
    }

    /// <summary>呼叫震動系統震動</summary>
    /// <param name="震動幅度">強度</param>
    public void 震動(float 震動幅度)
    {
        this.震動幅度 = 震動幅度;
    }

    float 震動幅度 = 0.1f;
    [SerializeField] float 震動衰減速度 = 0.1f;
    // LateUpdate 是在螢幕刷新之前最後執行的 Update
    private void LateUpdate()
    {
        float 隨機上下 = Random.Range(-1f * 震動幅度, 震動幅度);
        float 隨機左右 = Random.Range(-1f * 震動幅度, 震動幅度);
        // 修改攝影機本身的座標來達到震動的效果
        this.transform.localPosition = new Vector3(隨機左右, 隨機上下, 0f);

        // 震動一幀之後減少震動幅度
        震動幅度 -= Time.deltaTime * 震動衰減速度;
        // 震動幅度最小為零
        震動幅度 = Mathf.Max(震動幅度, 0f);
    }
}
