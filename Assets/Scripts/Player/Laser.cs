using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer laserLine;
    public Transform target;
    private Vector3 laserEnd;

    private bool laserEnabled = true;
    

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            laserEnabled = !laserEnabled;
    }

    void FixedUpdate()
    {
        Targeting();
    }

    private void LateUpdate()
    {
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, laserEnd);
    }

    private void Targeting()
    {
        if (laserEnabled)
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, transform.position);
            int LayerMaskAiming = 1 << 6;
            LayerMaskAiming = ~LayerMaskAiming;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward * 100, out hit, 40f, LayerMaskAiming))
                laserEnd = hit.point;
            else
                laserEnd = transform.position + transform.forward * 40;

            laserLine.SetPosition(1, laserEnd);
        } else
        {
            laserLine.enabled = false;
        }
        
    }
}
