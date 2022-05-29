using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused=false;
	
	public GUIStyle pause;
	public GUIStyle resume;
	public GUIStyle restart;
	public GUIStyle mainmenu;

    void Update()
	{
		
		if(paused)
		{
			Time.timeScale=0;
			
		}
		else
		{
			
			Time.timeScale=1;	
			
		}
	}
	
	void OnGUI()
	{
		 GUI.depth = 1;
		if(GUI.Button(new Rect(Screen.width*.9f,Screen.width*0f,Screen.width*.10f, Screen.width*.10f),"",pause))
		{
			paused=!paused;
			
		}
		if(paused)
			Pause();
	}
	
	void Pause()
	{
		
		if(GUI.Button(new Rect(Screen.width*.9f,Screen.width*.1f,Screen.width*.10f, Screen.width*.10f),"",resume))
		{
			paused=!paused;

		}
		
		if(GUI.Button(new Rect(Screen.width*.9f,Screen.width*.2f,Screen.width*.10f, Screen.width*.10f),"",restart))
		{
			Time.timeScale=1;
			DataManager.Instance.UserScore=0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		
		
		if(GUI.Button(new Rect(Screen.width*.9f,Screen.width*.3f,Screen.width*.10f, Screen.width*.10f),"",mainmenu))
		{
			DataManager.Instance.UserScore=0;
			SceneManager.LoadScene(0);
			
		}
		
	}
}
