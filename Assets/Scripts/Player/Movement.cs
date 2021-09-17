using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public static Movement instance;
    private Rigidbody rbPlayer;

    public Transform trTarget;
    public LayerMask mouseAimMask;
    private Camera mainCamera;

    //groundchecking
    public GameObject lLeg;
    public GameObject rLeg;
    private bool isGrL;
    private bool isGrR;

    public  float speedLocomotion = 0.0f;
    private float speed;
    private float speedRun = 13.0f;
    private float speedBackWalk = 4.0f;
    private float speedRotation = 15.0f;    
    private float jumpForce = 40.0f;
    private float gravityMultiplier = 7.0f;
    private float bottomBound = -5.0f;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter = 0.0f;

    private void Awake()
    {
        instance = this;
        rbPlayer = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !PauseMenuManager.isGamePaused)
            Jump();

        MouseAiming();

        //TD: reset level
        if (transform.position.y < bottomBound)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void Move()
    {
        //switch run/backwalk
        if (transform.forward.x * rbPlayer.velocity.x > 0)
        {
            //back direction
            speedLocomotion = -1;
            speed = speedBackWalk;            
        } else
        {
            //straight direction
            speedLocomotion = 1;
            speed = speedRun;
        }


        //side movement
        float sideDirection = Input.GetAxis("Horizontal");
        rbPlayer.velocity = new Vector3(sideDirection * speed, rbPlayer.velocity.y, 0);


        //more natural jump
        if (rbPlayer.velocity.y != 0)
            rbPlayer.velocity += Vector3.up * Physics.gravity.y * gravityMultiplier * Time.deltaTime;


        //character rotation 
        float lookDirection =  rbPlayer.transform.position.x - trTarget.position.x;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(lookDirection, 0, 0));
        rbPlayer.transform.rotation = Quaternion.Lerp(rbPlayer.transform.rotation, rotation, speedRotation * Time.deltaTime);

    }

    private void MouseAiming()
    {
        //mouse AIMing
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            trTarget.position = hit.point;
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
        {
            coyoteTimeCounter = coyoteTime;
            return true;
        }

        if (coyoteTimeCounter >= 0)
        {            
            coyoteTimeCounter -= Time.deltaTime;
            return true;
        } else
            return false;
    }
    
}
