using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Text mafiatext;
    bool chk = true;
    GameObject Char_Sel;
    private void Awake()
    {
        if (PlayerManager.LocalPlayer.ismafia)
            mafiatext.text = "악인";
        else
            mafiatext.text = "시민";
        Invoke("StartCharacterSelect", 1.0f);
    }
    

    void StartCharacterSelect()
    {
        if (PlayerListManager.playernumber == 1)
        {
            Char_Sel = PhotonNetwork.Instantiate("CharacterSelect", Vector3.zero, Quaternion.identity);
        }
        Invoke("RemoveCharacterSelect", 3.0f);
    }

    void RemoveCharacterSelect()
    {
        if(Char_Sel == null)
        {
            Char_Sel = GameObject.FindGameObjectWithTag("CharacterSelect");
        }
        Char_Sel.transform.GetChild(0).gameObject.SetActive(false);
    }
}
