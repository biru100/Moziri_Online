using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    bool chk = true;
    private void Awake()
    {
        if (PlayerManager.LocalPlayerInstance == null)
        {
            float angle = (360 / PlayerListManager.currentplayer * PlayerListManager.playernumber) * Mathf.Deg2Rad;
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f), Quaternion.identity, 0);
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }
}
