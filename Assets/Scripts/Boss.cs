using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
	Animator bossAnimator;
	public int health;
	public GameObject door;

    public HealthBar healthBar;

	public bool isFlipped = false;
	AudioSource a;
    void Awake()
	{
		a=gameObject.GetComponent<AudioSource>();
	}
    void Start()
    {
        bossAnimator= GetComponent<Animator>();
		health=4;
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

    public void Attack()
	{
		
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TakeDamage(1);
        
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="Fire")
		{
			a.Play();
            GetDamage(1);
			StartCoroutine(TakingDamage());
        }
        	
	}
	public void GetDamage(int damage)
    {
        if ((health - damage) >= 0)
        {
            health -= damage;
            healthBar.SetHealth(health);
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
            bossAnimator.SetBool("dead", true);
            Invoke("BossDead", 1);
        }
    }
    void BossDead()
    {
        Destroy(gameObject);
        DataManager.Instance.Victory=true;
		door.SetActive(true);
    }

	IEnumerator TakingDamage()
    {
        bossAnimator.SetBool("hit", true);
        yield return new WaitForSeconds(0.5f);
        bossAnimator.SetBool("hit", false);
    }

}
