using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 主角動畫音效 : MonoBehaviour
{
    [SerializeField] Animator 動畫系統 = null;
    [SerializeField] List<AudioSource> 腳步聲陣列 = new List<AudioSource>();
    [SerializeField] float 腳步冷卻時間 = 0.2f;
    float 上一個腳步時間 = 0f;
    public void OnFoot()
    {
        // 如果不在地上就不執行腳步聲
        bool 是否在地上 = 動畫系統.GetBool("onFloor");
        if (是否在地上 == false)
        {
            return;
        }

        // 離上次腳步時間太靠近 取消這個回音
        if (Time.time < 上一個腳步時間 + 腳步冷卻時間)
        {
            return;
        }

        上一個腳步時間 = Time.time;
        int 籤 = Random.Range(0, 腳步聲陣列.Count);
        腳步聲陣列[籤].Play();
    }

    [SerializeField] AudioSource 落地音效 = null;
    public void OnFloor()
    {
        落地音效.Play();
    }
}