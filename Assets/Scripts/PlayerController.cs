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

    public Transform groundCheckPosition;

    public float groundCheckRadius;
    public LayerMask grounCheckLayer;
    public float moveSpeed = 4f;
    public float jumpSpeed = 4f;

    public float damagedTime;
    public GUIStyle score;
    
    AudioSource[] a;
    void Awake()
	{
		a=gameObject.GetComponents<AudioSource>();
	}
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        
        healthBar.SetMaxHealth(DataManager.Instance.PlayerHealth);
        potionBar.SetPotion(DataManager.Instance.PlayerPotion);
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
            a[4].Play();
            damagedTime = 1f;
            DataManager.Instance.PlayerHealth -= damage;
            healthBar.SetHealth(DataManager.Instance.PlayerHealth);

            StartCoroutine(PlayerHurtAnimate());

            if (DataManager.Instance.PlayerHealth <= 0)
            {

                playerAnimator.SetBool("dead", true);
                Destroy(gameObject, 1f);
                DataManager.Instance.Gameover=true;

            }
            
        }
        

    }
    IEnumerator PlayerHurtAnimate()
    {
        playerAnimator.SetBool("hurt", true);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("hurt", false);
    }
    void ShootFire()
    {

        a[1].Play();
        if (DataManager.Instance.PlayerPotion > 0)
        {
            DataManager.Instance.PlayerPotion -= 1;
            potionBar.SetPotion(DataManager.Instance.PlayerPotion);
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
            DataManager.Instance.PlayerPotion+=5;
            potionBar.SetPotion(DataManager.Instance.PlayerPotion);
            Destroy(other.gameObject);
        }
        if(other.tag=="Coin")
        {
            a[3].Play();
            DataManager.Instance.UserScore+=1;
            Destroy(other.gameObject);
        }
        if(other.tag=="Enemy")
        {
            TakeDamage(1);
        }
        
    }
   
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag=="Enemy")
        {
            TakeDamage(1);
        }
       
    }
    void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="Trap")
		{
            playerAnimator.SetBool("dead", true);
            a[5].Play();
            Invoke("KillPlayer",1f);  
        }	
	}
    void KillPlayer()
    {
    
        DataManager.Instance.Gameover=true;
        Destroy(gameObject);
    }
    void OnGUI()
	{	
		//SCORE
		GUI.Label(new Rect(Screen.width*.74f, Screen.height*.0f, Screen.width*.1f, 
						   Screen.height*.08f), " "+DataManager.Instance.UserScore, score);

	}
   

   
}
