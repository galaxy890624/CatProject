using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Reset()
    {
        // 找出自己的所有子物件
        // 根據自己有幾個子物件跑幾次迴圈
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // 嘗試在這個子物件身上找音效程式
            AudioSource 找到的程式 = this.transform.GetChild(i).GetComponent<AudioSource>();
            // 如果有找到的話
            if (找到的程式 != null)
            {
                // 建立新的檔案
                AudioBag 新的音效包 = new AudioBag();
                新的音效包.音效 = 找到的程式;
                新的音效包.名稱 = this.transform.GetChild(i).name;
                // 放進列表中待使用
                音效列表.Add(新的音效包);
            }
        }
    }

    public List<AudioBag> 音效列表 = new List<AudioBag>();

    [System.Serializable]
    public struct AudioBag
    {
        public string 名稱;
        public AudioSource 音效;
    }

    public void Play(string 名稱)
    {
        for (int i = 0; i < 音效列表.Count; i++)
        {
            if (音效列表[i].名稱.Contains(名稱))
            {
                音效列表[i].音效.Play();
                return;
            }
        }
    }

}
