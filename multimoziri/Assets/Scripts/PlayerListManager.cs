using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerListManager : MonoBehaviourPunCallbacks
{
    public static List<PlayerManager> playerlist = new List<PlayerManager>();

    public static int currentplayer;

    public static int playernumber;
}
