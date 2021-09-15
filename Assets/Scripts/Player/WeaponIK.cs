using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    [System.Serializable]
    public class HumanBone
    {
        public HumanBodyBones bone;
        public float weight = 1.0f;
    }

    public Transform target;
    public Transform aim;

    public int iterations = 10;
    private float weight = 0.6f;
    private float angleLimit = 60.0f;
    private float distanceLimit = 3f;

    public HumanBone[] humanBones;
    public Transform[] boneTransforms;

    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for (int i = 0; i < boneTransforms.Length; i++)
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
    }

    private void LateUpdate()
    {
        Vector3 targetPos = GetTargetPositions();
        for (int i = 0; i < iterations; i++)
        {
            for (int b = 0; b < boneTransforms.Length; b++)
            {
                Transform bone = boneTransforms[b];
                float boneweight = humanBones[b].weight * weight;
                AimAtTarget(bone, targetPos, boneweight);
            }            
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPos, float weight)
    {
        Vector3 aimDirection = aim.forward;
        Vector3 targetDirection = targetPos - aim.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }

    private Vector3 GetTargetPositions()
    {
        Vector3 targetDirection = target.position - aim.position;
        Vector3 aimDirection = aim.forward;
        float blendOut = 0.0f;


        //angle limit
        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit)
            blendOut += (targetAngle - angleLimit) / 50.0f;

        //distance limit
        float targetDistance = targetDirection.magnitude;
        if (targetDistance < distanceLimit)
            blendOut += distanceLimit - targetDistance;

        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);

        return aim.position + direction;
    }
}
