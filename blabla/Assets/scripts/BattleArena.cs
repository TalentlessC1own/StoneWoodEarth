using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArena : MonoBehaviour
{

    private bool golems_exist = false;

    [SerializeField]
    LevelChanger changer;

    [SerializeField]
    private GameObject lightning_spawn_1;

    [SerializeField]
    private GameObject lightning_spawn_2;

    private bool define_winner = false;

    [SerializeField]
    private SpawnGolem spawn1;

    [SerializeField]
    private SpawnGolem spawn2;

    Golem player1_golem;
    Golem player2_golem;
    private void Start()
    {
        CreatePortals();
        StartCoroutine(BattleDelay());
    }
    private void Update()
    {
        if(golems_exist)
            DefineWinner(); 
    }

    IEnumerator BattleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        CreateGolems();
        yield return new WaitForSeconds(1f);
        DamageLoser();
        yield return new WaitForSeconds(1.5f);
        player1_golem.GetComponent<GolemController>().enabled = true;
        player2_golem.GetComponent<GolemController>().enabled = true;
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(1.2f);
        DataHolder.was_fight = true;
        changer.FadeToLevel(1);
    }
    private void CreateGolems()
    {
        player1_golem = spawn1.CreateGolem(DataHolder.golem_player_1);
        player2_golem = spawn2.CreateGolem(DataHolder.golem_player_2);
        player1_golem.OnHealthBar();
        player2_golem.OnHealthBar();
        golems_exist = true;
        
       
    }
    private void CreatePortals()
    {
        spawn1.GetComponentInChildren<Portal>().portal_animator.SetTrigger("Portal");
        spawn2.GetComponentInChildren<Portal>().portal_animator.SetTrigger("Portal");
    }
    private void DamageLoser()
    {
        if (DataHolder.pre_winner == players.none)
            return;
        if (DataHolder.pre_winner == players.player1)
        {
            lightning_spawn_2.SetActive(true);
            player2_golem.ReciveDamage();
        }
        if (DataHolder.pre_winner == players.player2)
        {
            lightning_spawn_1.SetActive(true);
            player1_golem.ReciveDamage();
        }
    }
    public void DefineWinner()
    {
        if (define_winner ) return;
        if (player1_golem.IsDie())
        {
            DataHolder.winner = players.player2;
            StartCoroutine(EndDelay());
        }
        if (player2_golem.IsDie())
        {
            DataHolder.winner = players.player1;
            StartCoroutine(EndDelay());
        }
        

       
    }
}
