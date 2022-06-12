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
        Destroy(gameObject,2f);
    }

    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag =="Enemy")
        {
            fireAnimator.SetBool("hit",true);
            Destroy(gameObject,0.5f); 
        }
        if(other.gameObject.tag =="Boss")
        {
            fireAnimator.SetBool("hit",true);
            Destroy(gameObject,0.5f); 
        }
    
    }
    
    
    
}
