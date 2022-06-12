using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    
    public float walkSpeed;

    public Transform groundCheck;
    public LayerMask groundLayer;

    Animator enemyAnimator;

   
    bool mustPatrol;
    bool mustTurn;

    public Rigidbody2D rb;
    AudioSource a;
    void Awake()
	{
		a=gameObject.GetComponent<AudioSource>();
	}
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
    
        if(other.tag=="Fire")
        {
            a.Play();
            GetDamage(1);
            StartCoroutine(TakingDamage());
            Destroy(other.gameObject,0.5f);
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
            DataManager.Instance.KilledEnemies++;
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
