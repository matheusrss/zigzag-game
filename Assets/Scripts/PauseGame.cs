using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private Text txtPause;

    public void Pausa()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            txtPause.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            txtPause.enabled = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
