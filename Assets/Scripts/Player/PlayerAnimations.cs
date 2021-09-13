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
        //enable/disable run animation
        if (Mathf.Abs(rbPlayer.velocity.x) > 1)
            animator.SetBool(TagManager.A_RUN, true);
        else
            animator.SetBool(TagManager.A_RUN, false);


        //enable/disable backwalk animation
        animator.SetBool(TagManager.A_BACKWALK, Movement.instance.isBackWalk);
        Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name + " - " + Movement.instance.isBackWalk);
    }

    private void JumpAnimation()
    {

        if (Movement.instance.IsGrounded())
        {
            animator.SetBool(TagManager.A_JUMP, false);
        }
        else
        {
            animator.SetBool(TagManager.A_JUMP, true);
        }
    }
   
}
