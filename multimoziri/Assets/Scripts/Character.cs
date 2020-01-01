using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class Character : MonoBehaviourPunCallbacks
{
    bool hasSpirit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !hasSpirit)
        {
            hasSpirit = true;
            transform.position = collision.gameObject.transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.parent = collision.gameObject.transform;
            collision.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
