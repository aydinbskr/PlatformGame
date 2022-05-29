using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float damage;
    public float walkSpeed;

    public Transform groundCheck;
    public LayerMask groundLayer;

    Animator enemyAnimator;

    bool colliderBusy=false;
    bool mustPatrol;
    bool mustTurn;

    public Rigidbody2D rb;
    void Start()
    {
        enemyAnimator= GetComponent<Animator>();
        mustPatrol=true;
        transform.localScale=new Vector2(transform.localScale.x*-1,transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
           Patrol();
        }
        
    }
    private void FixedUpdate() {
        if(mustPatrol)
        {
            mustTurn=!Physics2D.OverlapCircle(groundCheck.position,0.1f,groundLayer);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player" && !colliderBusy)
        {
            colliderBusy=true;
            other.GetComponent<PlayerController>().TakeDamage(1);
        }
        else if(other.tag=="Fire")
        {
            GetDamage(1);
            StartCoroutine(TakingDamage());
            Destroy(other.gameObject,0.5f);
        }
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Player")
        {
            colliderBusy=true;
            other.GetComponent<PlayerController>().TakeDamage(1);
        }
       
    }
    private void OnTriggerExit2D(Collider2D other) {
         if(other.tag=="Player")
        {
            colliderBusy=false;
            
        }
       
    }

     public void GetDamage(float damage)
    {
        if ((health - damage) >= 0)
        {
            health -= damage;
           
        }
        else
        {
            health = 0;
        }
        
        AmIDead();
    }
    void AmIDead()
    {
        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
    }
    void Patrol()
    {
        if(mustTurn)
        {
            Flip();
        }
        rb.velocity=new Vector2(walkSpeed*Time.fixedDeltaTime,rb.velocity.y);

    }
    void Flip()
    {
        mustPatrol=false;
        transform.localScale=new Vector2(transform.localScale.x*-1,transform.localScale.y);
        walkSpeed*=-1;
        mustPatrol=true;
    }
    IEnumerator TakingDamage()
    {
        enemyAnimator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.5f);
        enemyAnimator.SetBool("Hit", false);
    }
    
}
