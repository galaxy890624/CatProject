using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 地板檢測器 : MonoBehaviour
{
    public bool 在地上 = false;
    [SerializeField] float 檢定半徑 = 0.2f;
    [SerializeField] LayerMask 所謂地板的圖層;
    // 物理引擎刷新的時候
    private void FixedUpdate()
    {
        // 用物理引擎瞬間畫一個球 並且捕捉球當中的東西
        Collider[] 範圍內的碰撞器陣列 = Physics.OverlapSphere(this.transform.position, 檢定半徑, 所謂地板的圖層);
        // 如果碰撞器碰到的東西的數量不是零表示在地上
        if (範圍內的碰撞器陣列.Length > 0)
        {
            在地上 = true;
        }
        else
        {
            在地上 = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 檢定半徑);
    }

}
