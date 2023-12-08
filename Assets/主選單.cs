using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class 主選單 : MonoBehaviourPunCallbacks
{
    [SerializeField] UnityEvent 按鈕點擊音效 = null;

    public void 開始遊戲()
    {
        Debug.Log("開始遊戲");

        // 呼叫過場動畫
        過場動畫.instance.過場(切換場景到第一關);
    }

    void 切換場景到第一關()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void 升級角色()
    {
        Debug.Log("升級角色");
        SceneManager.LoadScene("TechnologyTree");

    }
    public void 選項()
    {
        Debug.Log("選項");
    }
    public void 離開()
    {
        Debug.Log("離開");
    }

    public void 多人遊戲()
    {
        // 阻止操作
        連線等待視窗.ins.Open();
        // 顯示資訊
        連線等待視窗.ins.資訊.text = "連線中...";
        // 要求所有房間成員跟隨房主到同一個遊戲場景
        PhotonNetwork.AutomaticallySyncScene = true;
        // 如果遊戲是開發版就強制進入亞服
        PhotonNetwork.PhotonServerSettings.DevRegion = "asia";
        // 使用預設值連線到機房
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // 顯示資訊
        連線等待視窗.ins.資訊.text = "伺服器 : " + PhotonNetwork.CloudRegion + " 延遲 : " + PhotonNetwork.GetPing() + "ms";
        Invoke("隨機加入房間", 1f);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        // 關閉等待視窗
        連線等待視窗.ins.資訊.text = "網路異常...";
        Invoke("關閉等待視窗", 2f);
    }

    void 關閉等待視窗()
    {
        連線等待視窗.ins.Close();
    }
    void 隨機加入房間()
    {
        連線等待視窗.ins.資訊.text = "等待對手...";
        PhotonNetwork.JoinRandomRoom();
    }

    // 自己加入房間
    public override void OnJoinedRoom()
    {
        顯示房間內容();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        連線等待視窗.ins.資訊.text = "等待對手...";
        // 找不到房間就自己開一個
        int 隨機號碼 = UnityEngine.Random.Range(0, 2147483647);
        PhotonNetwork.CreateRoom(隨機號碼.ToString("N0"), new RoomOptions { MaxPlayers = 2 });
    }

    // 當有玩家進入我的房間的時候
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        顯示房間內容();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        顯示房間內容();
    }

    void 顯示房間內容()
    {
        連線等待視窗.ins.資訊.text = "等待對手 (" + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + ")";
        連線等待視窗.ins.資訊.text += "\n<size=30>房間號碼 : " + PhotonNetwork.CurrentRoom.Name + "</size>";
        // 當發現人數滿的時候就可以開始遊戲
        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // 鎖定房間讓其他人不能加入
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Invoke("開始多人遊戲", 1f);
        }

    }

    void 開始多人遊戲()
    {
        // 開始遊戲的時候如果我是房主就主動切換場景讓其他人跟上我
        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("GamePlay");
        }
    }

}
