using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MainMenu : MonoBehaviour
{
    AudioSource a;
    void Awake()
	{
		a=gameObject.GetComponent<AudioSource>();
	}
    
    public void PlayButton()
    {
        a.Play();
        if(!String.IsNullOrWhiteSpace(DataManager.Instance.UserName))
        {
             SceneManager.LoadScene(1);
        }
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
