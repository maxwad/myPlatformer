using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody rbPlayer;
    public Transform trTarget;
    private Animator animator;

    float speedLocomotion = 0.0f;

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
        //blendtree
        float animationStep = 0.01f;
        float bound = Movement.instance.speedLocomotion;

        //idle state
        if (rbPlayer.velocity.x == 0)
            bound = 0;

        //run/backwalk state
        if (bound > speedLocomotion)
            speedLocomotion += animationStep;
        else
            speedLocomotion -= animationStep;

        animator.SetFloat(TagManager.A_SPEED, speedLocomotion);
    }

    private void JumpAnimation()
    {
        animator.SetBool(TagManager.A_JUMP, !Movement.instance.IsGrounded());        
    }

    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, trTarget.position);
    }
}
