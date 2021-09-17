using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer laserLine;
    public Transform target;
    private Vector3 laserEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //laserLine.SetPosition(0, transform.position);
        int LayerMaskAiming = 1 << 6;
        LayerMaskAiming = ~ LayerMaskAiming;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * 100, out hit, 40f, LayerMaskAiming))
            laserEnd = hit.point;
        else
            laserEnd = transform.position + transform.forward * 40;

        //laserLine.SetPosition(1, laserEnd);

    }

    private void LateUpdate()
    {
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, laserEnd);
    }
}
