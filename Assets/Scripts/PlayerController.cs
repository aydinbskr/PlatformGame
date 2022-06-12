using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    private float damagedTime;
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

        gameObject.transform.position=new Vector2(DataManager.Instance.Checkpoint,1);
        
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
           StartCoroutine(ShootFire());
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
                Invoke("PlayerDead", 2);

            }
            
        }
        

    }
    void PlayerDead()
    {
        Destroy(gameObject);
        DataManager.Instance.Gameover=true;
    }

    IEnumerator PlayerHurtAnimate()
    {
        playerAnimator.SetBool("hurt", true);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("hurt", false);
    }
    IEnumerator ShootFire()
    {
        
        playerAnimator.SetBool("attack", true);
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

        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("attack", false);

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
        if(other.tag=="Apple")
        {
            a[3].Play();
            DataManager.Instance.PlayerHealth+=3;
            Destroy(other.gameObject);
        }
        
        if(other.tag=="Checkpoint")
        {
            a[3].Play();
            DataManager.Instance.Checkpoint=13;
            Destroy(other.gameObject);
        }
        if(other.tag=="Key")
        {
            a[3].Play();
            DataManager.Instance.Key=true;
            print("Key"+DataManager.Instance.Key);
            Destroy(other.gameObject);
        }
        if(other.tag=="Door" && DataManager.Instance.Key && DataManager.Instance.Level!=3)
        {
            
            a[6].Play();
            DataManager.Instance.Level+=1;
            DataManager.Instance.Checkpoint=-7;
            SceneManager.LoadScene(DataManager.Instance.Level);
        }
        if(other.tag=="Door" && DataManager.Instance.Level==3)
        {
            
            a[6].Play();
            DataManager.Instance.Victory=true;
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
