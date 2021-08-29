using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public static Movement instance;
    private Rigidbody rbPlayer;
    private Collider colPlayer;

    private float speed = 13.0f;
    private float jumpForce = 30.0f;
    private float gravityMultiplier = 5.0f;
    private float bottomBound = -5.0f;

    private void Awake()
    {
        instance = this;
        rbPlayer = GetComponent<Rigidbody>();
        colPlayer = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !PauseMenuManager.isGamePaused)
            Jump();

        //TD: reset level
        if (transform.position.y < bottomBound)
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

    public bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, colPlayer.bounds.extents.y + 0.1f))
            return true;

        return false;
    }
}
