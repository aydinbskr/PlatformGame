using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator fireAnimator;

   
    void Start()
    {
        fireAnimator=GetComponent<Animator>();
        Destroy(gameObject,3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag =="Enemy")
        {
            print("carpisti");
            fireAnimator.SetBool("hit",true);
        }
        
    }
    
   
    
    
}
