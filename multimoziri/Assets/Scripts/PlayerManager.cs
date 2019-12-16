using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Com.MyCompany.MyGame {
    public class PlayerManager : MonoBehaviourPunCallbacks {

        [Tooltip ("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        [Tooltip ("The Player's UI GameObject Prefab")]
        [SerializeField]
        private GameObject playerUiPrefab;
        private void Awake () {
            if (photonView.IsMine) {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            transform.position = Vector2.zero;
            DontDestroyOnLoad (this.gameObject);
        }
        private void Start () {
            if (playerUiPrefab != null) {
                GameObject _uiGo = Instantiate (playerUiPrefab);
                _uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
            } else {
                Debug.LogWarning ("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }
        void FixedUpdate () {
            if (photonView.IsMine)
                transform.Translate (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")) * Time.deltaTime);
        }
    }
}