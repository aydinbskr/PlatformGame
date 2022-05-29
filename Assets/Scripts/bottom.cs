using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bottom : MonoBehaviour
{
    //ON GUI BUTTONS
    bool isPlayerDrop=false;
    public GUIStyle replay;
	public GUIStyle home;
	public GUIStyle gameOver;
	public GUIStyle score;
	public GUIStyle title;
	public GUIStyle victory;

    void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="Player")
		{
            isPlayerDrop=true;
            Time.timeScale=0;
			print("player drop");
        }
			
		Destroy(col.gameObject);
	}

     void OnGUI()
	{
		
		
		if(isPlayerDrop)
		{
			Time.timeScale=0;
			DataManager.Instance.LevelTime=40;
			if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.6f, Screen.width*.38f, Screen.height*.08f), "", replay))
			{
				
				Time.timeScale=1;
			
				
				DataManager.Instance.UserScore=0;
				DataManager.Instance.Speed=6;
				
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

				
			}
			if(GUI.Button(new Rect(Screen.width*.52f, Screen.height*.6f, Screen.width*.38f, Screen.height*.08f), "", home))
			{
				DataManager.Instance.UserScore=0;
				
				DataManager.Instance.Speed=6;
				SceneManager.LoadScene(0);
			}
			//Gameover background image
			if(GUI.Button(new Rect(Screen.width*.1f, Screen.height*.08f, Screen.width*.8f, Screen.height*.50f), "", gameOver))
			{
				
			}
			
			GUI.Label(new Rect(Screen.width*.45f, Screen.height*.42f, Screen.width*.1f, Screen.height*.08f), DataManager.Instance.UserScore.ToString(), score);

			
		}
		

	}
}