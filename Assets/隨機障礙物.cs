using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 隨機障礙物 : MonoBehaviour
{
    // 基礎的紙箱列表
    [SerializeField] GameObject[] 紙箱 = new GameObject[0];

    private void Start()
    {
        // 迴圈檢查每個物件
        for(int i = 0; i < 紙箱.Length; i++)
        {
            // 在使用紙箱之前將之啟用
            紙箱[i].SetActive(true);

            // 80%的機率會被關起來
            float 機率 = Random.Range(0f, 100f);
            if (機率 <= 100f)
            {
                紙箱[i].SetActive(false);
            }
            else
            {
                // 倖存的紙箱隨便轉一轉顯得自己隨意
                Quaternion 旋轉值 = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                紙箱[i].transform.rotation = 旋轉值;
                // 倖存的紙箱有很低的低率重疊
                float 重疊機率 = Random.Range(0f, 100f);
                if (重疊機率 <= 20f)
                {
                    // 複製自己, 在自己的位置向上+1, 旋轉方向隨意
                    Instantiate(紙箱[i], 紙箱[i].transform.position + new Vector3(0f, 1f, 0f), Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
                }
            }
        }
    }
}
