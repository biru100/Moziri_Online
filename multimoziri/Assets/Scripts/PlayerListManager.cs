using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerListManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static List<PlayerManager> playerlist = new List<PlayerManager>();
    public static bool[] mafialist;

    public static int mafiacnt;

    public static int currentplayer;

    public static int playernumber;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public static void SetMafia()
    { 
        if (mafialist == null)
        {
            currentplayer = playerlist.Count;
            int[] temp = { 0, 0, 0, 1, 1, 2, 2, 3 };
            int cnt = 0;
            bool[] mafiaarr = new bool[currentplayer];
            while (cnt < temp[currentplayer - 1])
            {
                int rantemp = Random.Range(0, currentplayer);
                if (!mafiaarr[rantemp])
                {
                    mafiaarr[rantemp] = true;
                    cnt++;
                }
            }
            mafialist = mafiaarr;
        }
        else
        {
            for(int i = 0;i < currentplayer; i++)
            {
                Debug.Log(i.ToString() + " : " + mafialist[i]);
            }
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(mafialist);
        }
        else
        {
            mafialist = (bool[])stream.ReceiveNext();
        }
    }
}
