using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody2D ribody;

    private void Awake()
    {
        ribody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ribody.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    }
}
