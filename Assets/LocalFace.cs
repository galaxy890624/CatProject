using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalFace : MonoBehaviour
{
    // 自己記住自己的方向
    Quaternion 自我方向;

    [SerializeField] Transform 我的母物件 = null;

    /// <summary>要求我對齊我的母物件</summary>
    public bool 對齊母物件 = false;

    [SerializeField] float 對齊速度 = 10f;

    private void Start()
    {
        // 初始化的時候記住自己的方向
        自我方向 = this.transform.rotation;
    }
    private void Update()
    {
        if (對齊母物件 == true)
            自我方向 = Quaternion.Lerp(自我方向, 我的母物件.rotation, Time.deltaTime * 對齊速度);

        // 自己等於自己的方向
        this.transform.rotation = 自我方向;
    }
}
