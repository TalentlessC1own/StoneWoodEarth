using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private int level_to_load;

    private void Start()
    {
        animator = GetComponent<Animator>();

        DataHolder.pause = false;
    }

    public void FadeToLevel(int level )
    {
        level_to_load = level;
        animator.SetTrigger("Fade");
       
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(level_to_load);
    }
}
