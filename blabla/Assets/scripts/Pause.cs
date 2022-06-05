using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool pause = false;
    [SerializeField]
    private GameObject pause_menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            
            ManagePause();
            pause = !pause;
            DataHolder.pause = pause;
        }
    }

    private void ManagePause()
    {
       if(pause)
            UnSetPause();
       else
            SetPause();
    }

    public void SetPause()
    {
        pause_menu.SetActive(true);
        Time.timeScale = 0;
       
    }

    public void UnSetPause()
    {
        pause_menu.SetActive(false);
        Time.timeScale = 1;
    }
}

