using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rbPlayer;

    private float speed = 15.0f;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
        float sideDirection = Input.GetAxis("Horizontal");

        //transform.Translate(Vector3.right * sideDirection * speed * Time.deltaTime);
        rbPlayer.velocity = new Vector3(sideDirection * speed, rbPlayer.velocity.y, 0);
    }
}
