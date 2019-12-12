using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Com.MyCompany.MyGame {
    public class PlayerManager : MonoBehaviourPunCallbacks {

        [Tooltip ("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
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
#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, loadingMode) => {
                CalledOnLevelWasLoaded (scene.buildIndex);
            };
#endif
        }
        void FixedUpdate () {
            if(photonView.IsMine)
                transform.Translate (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));
        }
#if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded (int level) {
            CalledOnLevelWasLoaded (level);
        }
#endif

        void CalledOnLevelWasLoaded (int level) {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast (transform.position, -Vector3.up, 5f)) {
                transform.position = new Vector3 (0f, 5f, 0f);
            }
        }
    }
}