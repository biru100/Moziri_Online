using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject[] Characterlist;
    private void Start()
    {
        if (PlayerListManager.playernumber == 1)
        {
            for (int i = 0; i < PlayerListManager.currentplayer; i++)
            {
                Vector3 temp;
                float angle = 360 / PlayerListManager.currentplayer * i * Mathf.Deg2Rad;
                temp = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));
                PhotonNetwork.Instantiate("Character/" + Characterlist[Random.Range(0, Characterlist.Length)].name, temp * 5, Quaternion.Euler(0, 0, 0)).transform.parent = transform;
            }
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.back * 13.0f * Time.deltaTime);
    }
}
