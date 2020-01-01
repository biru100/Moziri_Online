using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Text playercnttext;
    public Text readytext;

    int readycnt = 0;
    int Startcnt = 5;
    float starttime;
    private void Start()
    {
        if (PlayerManager.LocalPlayerInstance == null)
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            PhotonNetwork.Instantiate("PlayerListManager", Vector3.zero, Quaternion.identity);
        SetPlayerCnt();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void LoadGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }


    public void Ready(bool chk)
    {
        PlayerManager.LocalPlayer.Ready(chk);
    }

    private void LateUpdate()
    {
        SetPlayerCnt();
        if (playercnttext != null)
            playercnttext.text = readycnt.ToString() + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
        ReadyCheck();
    }

    void ReadyCheck()
    {
        if (readycnt >= 4 && readycnt == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (!readytext.gameObject.activeSelf)
            {
                Startcnt = 5;
                starttime = Time.time;
                readytext.gameObject.SetActive(true);
                PlayerListManager.SetMafia();
            }
            if (starttime + 1 <= Time.time)
            {
                starttime = Time.time;
                Startcnt--;
            }
            readytext.text = "게임 시작 중..." + Startcnt.ToString();
            if (Startcnt < 1)
            {
                if (PlayerListManager.mafialist[PlayerListManager.playernumber - 1])
                    PlayerManager.LocalPlayer.ismafia = true;
                float angle = 360 / PlayerListManager.currentplayer * PlayerListManager.playernumber * Mathf.Deg2Rad;
                PlayerManager.LocalPlayerInstance.transform.position = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0) * 10f;
                PhotonNetwork.LoadLevel("GameScene");
            }
        }
        else if(readytext.gameObject.activeSelf)
        {
            readytext.gameObject.SetActive(false);
        }
    }

    void SetPlayerCnt()
    {
        readycnt = 0;
        Debug.Log(PlayerListManager.playerlist.Count);
        for (int i = 0; i < PlayerListManager.playerlist.Count; i++)
        {
            if (PlayerListManager.playerlist[i].isReady)
                readycnt++;
        }
    }
}
