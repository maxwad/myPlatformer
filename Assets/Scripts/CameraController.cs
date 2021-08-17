using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float offset = 7;

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!target)
            return;
        
        transform.position = target.position + new Vector3(offset, offset, -offset * 3.5f);
    }
}
