using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public static Movement instance;
    private Rigidbody rbPlayer;
    private Collider colPlayer;

    public GameObject lLeg;
    public GameObject rLeg;

    private bool isGrL;
    private bool isGrR;

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
        // we have to check both legs grounded because the joints make the other way wrong

        Ray rayLeftLeg = new Ray(lLeg.transform.position, Vector3.down);
        Ray rayRightLeg = new Ray(rLeg.transform.position, Vector3.down);
        float distance = 0.3f;

        isGrL = Physics.Raycast(rayLeftLeg, distance);
        isGrR = Physics.Raycast(rayRightLeg, distance);        

        if (isGrL || isGrR)
            return true;

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.green, 0.5f);
    }
}
