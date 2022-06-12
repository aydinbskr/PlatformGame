using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color startingColor = Color.clear;
    
    AudioSource a;
    void Awake()
	{
		a=gameObject.GetComponent<AudioSource>();
	}
    void Start()
    {
        startingColor = spriteRenderer.color;
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Fire")
        {
            a.Play();
            DataManager.Instance.EggHitCounter--;
            StartCoroutine(FlickerAnimation());
            Destroy(col.gameObject);

            if(DataManager.Instance.EggHitCounter==0)
            {
                DataManager.Instance.UserScore+=100;
                Destroy(gameObject);
            }
            
        }
    }

    IEnumerator FlickerAnimation()
    {
        spriteRenderer.color = Color.black;
 
        yield return new WaitForSeconds(0.05f);
 
        spriteRenderer.color = startingColor;
    }
    
    
}
