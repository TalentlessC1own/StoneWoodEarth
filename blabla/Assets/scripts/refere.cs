using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class refere : MonoBehaviour
{
    [SerializeField]
    private GameObject player1;

    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private arena arena;

    [SerializeField]
    private GameObject restart_button;

    [SerializeField]
    private GameObject menu_button;

    [SerializeField]
    private GameObject Cong1;

    [SerializeField]
    private GameObject Cong2;


    public void Winner(GameObject player)
    {
        StartCoroutine(Delay(player));
    }

    private IEnumerator Delay(GameObject player)
    {
        arena.enabled = false;
        yield return new WaitForSecondsRealtime(1.5f);
       // Time.timeScale = 0;
       
        yield return new WaitForSecondsRealtime(1.5f);
        if (player == player1)
            Cong2.SetActive(true);
        if (player == player2)
            Cong1.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        restart_button.SetActive(true);
        menu_button.SetActive(true);
    }
}
