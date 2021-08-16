using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Collider colPlayer;

    private float speed = 15.0f;
    private float jumpForce = 30.0f;
    private float gravityMultiplier = 5f;

    // TODO: if it will not use for animation - delete
    private bool _isGrounded;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
        colPlayer = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();

        //TD: reset level
        if (transform.position.y < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Move()
    {
        float sideDirection = Input.GetAxis("Horizontal");
        rbPlayer.velocity = new Vector3(sideDirection * speed, rbPlayer.velocity.y, 0);

        //it makes jumping more natural
        if (rbPlayer.velocity.y != 0)
            rbPlayer.velocity += Vector3.up * Physics.gravity.y * gravityMultiplier * Time.deltaTime;
    }

    private void Jump()
    {
        rbPlayer.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, colPlayer.bounds.extents.y + 0.1f))
            return _isGrounded = true;

        return _isGrounded = false;
    }
}
