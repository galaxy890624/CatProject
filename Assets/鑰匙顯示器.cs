using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 鑰匙顯示器 : MonoBehaviour
{
    private void Start()
    {
        SaveManager.instance.任務道具刷新 += 刷新顯示;
        刷新顯示();
    }
    private void OnDisable()
    {
        SaveManager.instance.任務道具刷新 -= 刷新顯示;
    }
    [SerializeField] List<GameObject> 鑰匙UI = new List<GameObject>();
    void 刷新顯示()
    {
        for (int i = 0; i < 鑰匙UI.Count; i++)
        {
            鑰匙UI[i].SetActive(false);
        }

        for (int i = 0; i < 鑰匙UI.Count; i++)
        {
            // 如果i比道具數量小就顯示
            // 0 < 2 O
            // 1 < 2 O
            // 2 < 2 X
            if (i < SaveManager.instance.任務道具)
            {
                鑰匙UI[i].SetActive(true);
            }
        }
    }
}
