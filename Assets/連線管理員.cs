using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class 連線管理員 : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject 生成點 = null;
    void Start()
    {
        PhotonNetwork.Instantiate("連線玩家", 生成點.transform.position, Quaternion.identity);
    }

}
