using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private int killedEnemies;

    private int level;

    private string userName;
    private int bestScore;
    private int playerHealth;
    private int playerPotion;
    private int checkpoint;

    private bool gameover;
    private bool victory;
    private bool key;
    
    private int eggHitCounter;

    void Awake()
    {
        if(Instance==null)
        {
            Instance=this;
            Time.timeScale = 1;
            playerHealth = 4;
            playerPotion=10;
           
            killedEnemies=0;
            level=3;
            checkpoint=-7;
            gameover=false;
            victory=false;
            key=false;
            userName=null;
            
            eggHitCounter=5;
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
    
    public int PlayerHealth
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth=value;
        }
    }

    public int PlayerPotion
    {
        get
        {
            return playerPotion;
        }
        set
        {
            playerPotion=value;
        }
    }
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
   
    public int KilledEnemies
    {
        get
        {
            return killedEnemies;
        }
        set
        {
            killedEnemies=value;
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
    public bool Victory
    {
        get
        {
            return victory;
        }
        set
        {
            victory=value;
        }
    }
    public bool Key
    {
        get
        {
            return key;
        }
        set
        {
            key=value;
        }
    }
    public int Checkpoint
    {
        get
        {
            return checkpoint;
        }
        set
        {
            checkpoint=value;
        }
    }
     public int EggHitCounter
    {
        get
        {
            return eggHitCounter;
        }
        set
        {
            eggHitCounter=value;
        }
    }
     public void LoadData()
    {
        bestScore=PlayerPrefs.GetInt("BestScore");
    }
}
