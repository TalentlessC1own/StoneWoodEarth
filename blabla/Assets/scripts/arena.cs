using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arena : MonoBehaviour
{
    [SerializeField]
    LevelChanger changer;

    [SerializeField]
    private SpawnGolem spawn1;


    [SerializeField]
    private SpawnGolem spawn2;
   
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    private Golems player1_Choice;

    private Golems player2_Choice;

    Golem player1_golem;
    Golem player2_golem;


    

    [SerializeField]
    private GameObject timer;

    private bool end = false;
    private bool golems_fight = false;


    private void Update()
    {
      
        if (player1.GetComponent<player>().player_ready && player2.GetComponent<player>().player_ready )
        {
            golems_fight = true;
            player1.GetComponent<player>().player_ready = false;
            player2.GetComponent<player>().player_ready = false;
            StartCoroutine(FightDealay());
        }
        CheckGolems();

    }
    private void Start()
    {
        if (DataHolder.was_fight)
        {
            golems_fight = true;

            CreatePortals();
            
            StartCoroutine(WinnerMoveDelay());
           
        }
       
    }

    private IEnumerator WinnerMoveDelay()
    {
        yield return new WaitForSeconds(2);
        if (DataHolder.winner == players.player1)
        {
            player1_golem = spawn1.CreateGolem(DataHolder.golem_player_1);
            player1_golem.GetComponent<GolemController>().enabled = true;
            spawn2.CreateGolemHead(DataHolder.golem_player_2);
        }

        if (DataHolder.winner == players.player2)
        {
            player2_golem = spawn2.CreateGolem(DataHolder.golem_player_2);
            player2_golem.GetComponent<GolemController>().enabled = true;
            spawn1.CreateGolemHead(DataHolder.golem_player_1);
        }
        golems_fight = false;
    }
    private IEnumerator FightDealay()
    {
        DeleteAllHeads();

        timer.GetComponent<Animator>().SetInteger("state", 1);
        yield return new WaitForSeconds(4);
        timer.GetComponent<Animator>().SetInteger("state", 0);

        player1.GetComponent<player>().Cast();
        player2.GetComponent<player>().Cast();
        yield return new WaitForSeconds(1);

        DataHolder.golem_player_1 = player1_Choice;
        DataHolder.golem_player_2 = player2_Choice;

        CreateGolems();


        CreatePortals();
        yield return new WaitForSeconds(1.5f);
       
        DefineWinner();
        golems_fight = false;
        changer.FadeToLevel(2);

    }

    private void CreatePortals()
    {
        spawn1.GetComponentInChildren<Portal>().portal_animator.SetTrigger("Portal");
        spawn2.GetComponentInChildren<Portal>().portal_animator.SetTrigger("Portal");
    }
    private void CheckGolems()
    {
        List<Golem> golems = new List<Golem>();
        foreach (Golem golem in FindObjectsOfType<Golem>())
        {
            golems.Add(golem);
        }
        if (golems.Count != 0 || golems_fight)
        {
            player1.GetComponent<player>().Fight(true);
            player2.GetComponent<player>().Fight(true);
        }
        else if(golems.Count == 0 && !golems_fight)
        {
            player1.GetComponent<player>().Fight(false);
            player2.GetComponent<player>().Fight(false);
        }

    }
    public void KillAllGolem()
    {
        foreach(Golem golem in FindObjectsOfType<Golem>())
        {
           
            StartCoroutine(KillAllGolemDelay(golem));

        }
    }

    IEnumerator KillAllGolemDelay(Golem golem)
    {
        yield return new WaitForSeconds(0.5f);
        golem.Die();
        yield return new WaitForSeconds(2f);
        Destroy(golem.gameObject);

    }

    private void DeleteAllHeads()
    {
        foreach (Head head in FindObjectsOfType<Head>())
        {

            Destroy(head.gameObject);

        }
    }
    public void SetGolemType(Golems golem,players player)
    {
        switch(player)
        {
            case players.player1:
                player1_Choice = golem; 
                break;
            case players.player2:
                player2_Choice = golem;
                break;
        }
    }
    private void CreateGolems()
    {
       player1_golem = spawn1.CreateGolem(DataHolder.golem_player_1);
       player2_golem = spawn2.CreateGolem(DataHolder.golem_player_2);
    }

    private void DefineWinner()
    {
        int result = (player1_Choice - player2_Choice) % 3;
        switch (result < 0 ? result + 3 : result)
        {

            case 0:
                DataHolder.pre_winner = players.none;
                break;

            case 1:
                DataHolder.pre_winner = players.player2;
                break;

            case 2:
                DataHolder.pre_winner = players.player1;
                break;

        }

    }

}

public enum Golems
{
    StoneGolem = 1,
    EarthGolem,
    WoodGolem
}