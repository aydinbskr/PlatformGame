using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float followSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            transform.position=Vector3.Slerp(transform.position,new Vector3(target.position.x,target.position.y,transform.position.z),followSpeed);
        }
        
    }
}
