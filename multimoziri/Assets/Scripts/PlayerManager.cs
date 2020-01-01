using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{ 
    [SerializeField]
    private GameObject playerUiPrefab;
    [SerializeField]
    private float speed;
    GameObject ui;
    Rigidbody2D ribody;
    public static GameObject LocalPlayerInstance;
    public static PlayerManager LocalPlayer;
    public bool isReady = false;
    public bool ismafia = false;
    private void OnDestroy()
    {
        PlayerListManager.playerlist.Remove(this);
    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayer = this;
            PlayerManager.LocalPlayerInstance = gameObject;
            PlayerListManager.playernumber = photonView.Owner.ActorNumber;
        }
        PlayerListManager.playerlist.Add(this);
        DontDestroyOnLoad(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
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
        SetUI();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        if (ui == null)
            SetUI();
    }

    void Move()
    {
        if(photonView.IsMine)
            ribody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    }

    public void Ready(bool chk)
    {
        isReady = chk;
    }

    void SetUI()
    {
        if (playerUiPrefab != null)
        {
            ui = Instantiate(playerUiPrefab);
            ui.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }
}
