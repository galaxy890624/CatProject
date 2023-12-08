using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 寶石 : MonoBehaviour
{
    // 穿透類型的碰撞器被碰了
    private void OnTriggerEnter(Collider other)
    {
        // 如果玩家碰到我
        if (other.tag == "Player")
        {
            SaveManager.instance.gem++;
            AudioManager.instance.Play("寶石");
            SaveManager.instance.hp += 1;
            Destroy(this.gameObject); // 刪除自己的遊戲物件
        }

    }
}
