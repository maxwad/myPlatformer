using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody rbPlayer;
    private Animator animator;
    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
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
        if (Mathf.Abs(rbPlayer.velocity.x) > 1 && Movement.instance.isBackWalk == false)
            animator.SetBool(TagManager.A_RUN, true);

        if (Mathf.Abs(rbPlayer.velocity.x) < 1 || Movement.instance.isBackWalk)
            animator.SetBool(TagManager.A_RUN, false);

        //enable/disable backwalk animation
        animator.SetBool(TagManager.A_BACKWALK, Movement.instance.isBackWalk);        
    }

    private void JumpAnimation()
    {

        if (Movement.instance.IsGrounded())
        {
            animator.SetBool(TagManager.A_JUMP, false);
            //animator.SetBool(TagManager.A_FALL, false);
        }
        else
        {
            animator.SetBool(TagManager.A_JUMP, true);

            //if (Movement.instance.isSpacePushed)
            //{
            //    animator.SetBool(TagManager.A_JUMP, true);
            //    //Debug.Log("Jumping");
            //}
            //else
            //{
            //    animator.SetBool(TagManager.A_FALL, true);
            //    //Debug.Log("Falling");
            //}
            
        }
    }
   
}
