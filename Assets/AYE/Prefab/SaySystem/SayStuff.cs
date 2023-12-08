using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject 自訂物件

[CreateAssetMenu(fileName = "新的文本", menuName = "建立新文本")]
public class SayStuff : ScriptableObject
{
    // 表單
    public List<SayData> list;
}
[System.Serializable]
public struct SayData
{
    public string npcName;
    public bool isLeft;
    [TextArea(2, 5)] public string info;
}



