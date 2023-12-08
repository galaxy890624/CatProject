using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 閃爍系統 : MonoBehaviour
{
    // 單例
    static public 閃爍系統 instance = null;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Renderer 渲染器 = null;
    float 閃爍值 = 0f;
    [SerializeField] float 閃爍速度 = 10f;

    [SerializeField][Range(0f, 1f)] float 幅度 = 0f;
    [SerializeField] float 衰減速度 = 1f;

    // 刷新
    private void LateUpdate()
    {
        // 閃爍波
        閃爍值 = (Mathf.Sin(Time.time * 閃爍速度) + 1f) * 0.5f;

        // 控制渲染器當中的每一個材質球
        for (int i = 0; i < 渲染器.materials.Length; i++)
        {
            渲染器.materials[i].SetFloat("_Ctrl", Mathf.Lerp(0f, 閃爍值, 幅度) );
        }

        幅度 -= Time.deltaTime * 衰減速度;
        幅度 = Mathf.Max(幅度, 0f);
    }

    public void 閃爍()
    {
        幅度 = 1f;
    }
}
