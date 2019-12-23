using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{ 
    [SerializeField]
    private GameObject playerUiPrefab;
    Rigidbody2D ribody;
    public static GameObject LocalPlayerInstance;
    public static PlayerManager LocalPlayer;
    public bool isReady = false;

    private void OnDestroy()
    {
        GameManager.playerlist.Remove(this);
    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayer = this;
            PlayerManager.LocalPlayerInstance = gameObject;
        }
        GameManager.playerlist.Add(this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(isReady);
        }
        else
        {
            isReady = (bool)stream.ReceiveNext();
        }
    }

    void Start()
    {
        ribody = GetComponent<Rigidbody2D>();

        if (playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(photonView.IsMine)
            ribody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public void Ready(bool chk)
    {
        isReady = chk;
    }
}
