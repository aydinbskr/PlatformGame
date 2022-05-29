using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    public Text Username_field;
    void Start()
    {
        if(DataManager.Instance.UserName!=null)
        {
            gameObject.SetActive(false);
        }
    }
    public void GetPlayerName()
    {
        DataManager.Instance.UserName = Username_field.text.ToString();

    }
}
