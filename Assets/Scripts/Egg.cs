using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color startingColor = Color.clear;
    public GUIStyle message;
    
    AudioSource[] a;
    void Awake()
	{
		a=gameObject.GetComponents<AudioSource>();
	}
    void Start()
    {
        startingColor = spriteRenderer.color;
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag=="Fire")
        {
            a[0].Play();
            DataManager.Instance.EggHitCounter--;
            StartCoroutine(FlickerAnimation());
            Destroy(col.gameObject);

            if(DataManager.Instance.EggHitCounter==0)
            {
                a[1].Play();
                GetComponent<SpriteRenderer>().enabled=false;
                Destroy(gameObject,3f);
            }
            
        }
    }

    IEnumerator FlickerAnimation()
    {
        spriteRenderer.color = Color.black;
 
        yield return new WaitForSeconds(0.05f);
 
        spriteRenderer.color = startingColor;
    }
    void OnGUI()
	{	
		

        if(DataManager.Instance.EggHitCounter==0)
        {
		    GUI.Label(new Rect(Screen.width*.4f, Screen.height*.4f, Screen.width*.1f, 
						   Screen.height*.08f), "You won 100 coins", message);
        }

	}
}
