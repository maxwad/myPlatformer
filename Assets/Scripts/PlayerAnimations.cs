using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Collider colPlayer;
    private Animator animator;
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
        colPlayer = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimation();

        JumpAnimation();
    }

    private void MoveAnimation()
    {
        if (Mathf.Abs(rbPlayer.velocity.x) > 2)
            animator.SetBool(TagManager.A_RUN, true);
        else
            animator.SetBool(TagManager.A_RUN, false);

        if (rbPlayer.velocity.x > 0)
            transform.eulerAngles = new Vector3(0, 90, 0);

        if (rbPlayer.velocity.x < 0)
            transform.eulerAngles = new Vector3(0, -90, 0);


    }

    private void JumpAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Movement.instance.IsGrounded())
            animator.SetBool(TagManager.A_JUMP, true);

        animator.SetBool(TagManager.A_JUMP, !Movement.instance.IsGrounded());
    }
   
}
