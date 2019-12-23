using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Text playerNameText;

    PlayerManager target;
    Transform targetTransform;

    void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    public void SetTarget(PlayerManager _target)
    {
        if(_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        target = _target;
        if(playerNameText != null)
        {
            targetTransform = target.gameObject.transform;
            playerNameText.text = target.photonView.Owner.NickName;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            Destroy(gameObject);
        if(targetTransform != null)
            transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + Vector3.up * 1.0f);
    }
}
