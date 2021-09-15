using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * 100, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider)
            {
                laserLine.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
        }
        else
        {
            //laserLine.SetPosition(1, new Vector3(0, 0, 100));
        }
    }
}
