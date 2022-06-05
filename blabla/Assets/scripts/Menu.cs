using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    LevelChanger levelChanger;
    public void Play()
    {
        levelChanger.FadeToLevel(1);
        DataHolder.player_1_lives = 3;
        DataHolder.player_2_lives = 3;
        DataHolder.was_fight = false;
    }

    public void Exit()
    {
      Application.Quit();
    }
}
