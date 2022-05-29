using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator playerAnimator;

    Transform namlu;
    public Transform fire;

    public HealthBar healthBar;
    public PotionBar potionBar;
    bool facingRight = true;
    public bool isGrounded = false;

    public int maxHealth = 4;
    public int currentHealth;
    public int currentPotion;
    public Transform groundCheckPosition;

    public float groundCheckRadius;
    public LayerMask grounCheckLayer;
    public float moveSpeed = 4f;
    public float jumpSpeed = 4f;

    public float damagedTime;

    
    AudioSource[] a;
    void Awake()
	{
		a=gameObject.GetComponents<AudioSource>();
	}
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentPotion = 10;
        healthBar.SetMaxHealth(maxHealth);
        potionBar.SetPotion(10);
        namlu = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        OnGroundCheck();

        if (playerRB.velocity.x < 0 && facingRight)
        {
            FlipFace();
        }
        else if (playerRB.velocity.x > 0 && !facingRight)
        {
            FlipFace();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();

        }
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            ShootFire();
        }

        if (damagedTime > 0)
        {
            damagedTime -= Time.deltaTime;
        }

    }

    void Move()
    {

        playerRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRB.velocity.y);
        playerAnimator.SetFloat("playerSpeed", Mathf.Abs(playerRB.velocity.x));
    }
    void FlipFace()
    {
        facingRight = !facingRight;
        Vector3 temp = transform.localScale;

        temp.x *= -1;
        transform.localScale = temp;
    }
    void Jump()
    {
         a[2].Play();
        playerRB.AddForce(new Vector2(0, jumpSpeed));
    }
    void OnGroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, grounCheckLayer);
        playerAnimator.SetBool("isGrounded", isGrounded);
        if(transform.position.y<-5)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        if (damagedTime <= 0)
        {
            damagedTime = 0.7f;
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {

                playerAnimator.SetBool("dead", true);
                Destroy(gameObject, 1f);

            }
        }

    }
    void ShootFire()
    {

        a[1].Play();
        if (currentPotion > 0)
        {
            currentPotion -= 1;
            potionBar.SetPotion(currentPotion);
            Transform temp;
            temp = Instantiate(fire, namlu.position, Quaternion.identity);
            if (facingRight)
            {

                temp.GetComponent<Rigidbody2D>().velocity = fire.transform.right * 5f;
            }
            else
            {
                temp.GetComponent<Rigidbody2D>().velocity = fire.transform.right * -5f;
            }

        }


    }
    private void OnTriggerEnter2D(Collider2D other) {
      
        if(other.tag=="Potion")
        {   
            a[3].Play();
            currentPotion+=5;
            potionBar.SetPotion(currentPotion);
            Destroy(other.gameObject);
        }
        if(other.tag=="Coin")
        {
            a[3].Play();
            DataManager.Instance.UserScore+=1;
            Destroy(other.gameObject);
        }
        
    }
   

   
}
