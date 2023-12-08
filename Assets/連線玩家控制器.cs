using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;
public class 連線玩家控制器 : MonoBehaviourPunCallbacks
{
    [SerializeField] List<GameObject> 鏡像要關閉的物件 = new List<GameObject>();
    [SerializeField] PlayerMove playerMove = null;
    [SerializeField] MouseRotate mouseRotate = null;
    [SerializeField] NavMeshObstacle navMeshObstacle = null;
    [SerializeField] 閃爍系統 閃爍系統 = null;
    [SerializeField] IK控制 ik控制 = null;
    private void Awake()
    {
        if (photonView.IsMine)
        {
            // 如果我是本尊
        }
        else
        {
            // 如果我是鏡像
            for (int i = 0; i < 鏡像要關閉的物件.Count; i++)
            {
                鏡像要關閉的物件[i].SetActive(false);
            }
            playerMove.enabled = false;
            mouseRotate.enabled = false;
            navMeshObstacle.enabled = false;
            閃爍系統.enabled = false;
            ik控制.enabled = false;
        }
    }
}
