using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIK : MonoBehaviour
{
    public Transform target;
    public Transform aim;
    public Transform bone;

    private void LateUpdate()
    {
        Vector3 targetPos = target.position;

        AimAtTarget(bone, targetPos);

    }

    private void AimAtTarget(Transform bone, Vector3 targetPos)
    {
        Vector3 aimDirection = aim.forward;
        Vector3 targetDirection = targetPos - aim.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        bone.rotation = aimTowards * bone.rotation;
    }
}
