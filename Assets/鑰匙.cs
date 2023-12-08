using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 鑰匙 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SaveManager.instance.任務道具++;
            AudioManager.instance.Play("鑰匙");
            Destroy(gameObject);
        }
    }
}
