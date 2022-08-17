using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformScript : MonoBehaviour
{
    public float xRot;
    public float yRot;
    public float zRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(xRot, yRot, zRot);
    }
}
