using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;


   
    private int speed;

    private int levelTime;

    private int numberOfBricks;

    private int level;

    private string userName;
    private int bestScore;
    
    

    private bool gameover;

   

    void Awake()
    {
        if(Instance==null)
        {
            Instance=this;
            Time.timeScale = 1;
            
            
            speed=6;
            numberOfBricks=33;
            level=1;
            
            gameover=false;
            userName=null;
            levelTime=40;
            LoadData();
            
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

     public int UserScore{get;set;}

    public int Health{get;set;}
    public int BossHealth{get;set;}
    
    public string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName=value;
        }
    }

   
     public int Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed=value;
        }
    }
    public int NumberOfBricks
    {
        get
        {
            return numberOfBricks;
        }
        set
        {
            numberOfBricks=value;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level=value;
        }
    }
    public int LevelTime
    {
        get
        {
            return levelTime;
        }
        set
        {
            levelTime=value;
        }
    }
    public int BestScore
    {
        get
        {
            return bestScore;
        }
        set
        {
            bestScore=value;
        }
    }
    public bool Gameover
    {
        get
        {
            return gameover;
        }
        set
        {
            gameover=value;
        }
    }
     public void LoadData()
    {
        bestScore=PlayerPrefs.GetInt("BestScore");
    }
}
