using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame {
    public class PlayerUI : MonoBehaviour {

        [Tooltip ("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3 (0f, 30f, 0f);
        #region Private Fields
        [SerializeField]
        private float playerHeight = 1f;
        Transform targetTransform;
        Vector3 targetPosition;
        PlayerManager target;

        [Tooltip ("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        #endregion

        #region MonoBehaviour CallBacks
        private void Awake () {
            transform.SetParent (GameObject.Find ("Canvas").GetComponent<Transform> (), false);
        }
        void Update () {
            if (target == null) {
                Destroy (this.gameObject);
                return;
            }
        }
        #endregion

        #region Public Methods
        public void SetTarget (PlayerManager _target) {
            if (_target == null) {
                Debug.LogError ("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }
            // Cache references for efficiency
            target = _target;
            targetTransform = _target.gameObject.transform;
            if (playerNameText != null) {
                playerNameText.text = target.photonView.Owner.NickName;
            }
        }
        void LateUpdate () {
            // #Critical
            // Follow the Target GameObject on screen.
            if (targetTransform != null) {
                targetPosition = targetTransform.position;
                targetPosition.y += playerHeight;
                transform.position = Camera.main.WorldToScreenPoint (targetPosition) + screenOffset;
            }
        }
        #endregion

    }
}