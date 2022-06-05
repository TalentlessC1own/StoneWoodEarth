using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Unit
{

    private bool taking_damage = false;
    private bool die = false;
    private bool casting = false;

    [SerializeField]
    refere referee;

    [SerializeField]
    private IconScript IconPanel;

    [SerializeField]
    players player_number;

    [SerializeField]
    private arena _arena;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private HealthBar healthBar;

    private Animator animator;

    public bool player_ready { get; set; }

    private bool fighting = false;
    
    Golems user_choice;
    private PlayerState state
    {
        get { return (PlayerState)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    private void Start()
    {
        player_ready = false;
        if (player_number == players.player1)
        {
            lives = DataHolder.player_1_lives;
            
        }
        if (player_number == players.player2)
        {
            lives = DataHolder.player_2_lives;
        }
        healthBar.SetMaxHealth(3);
        healthBar.SetHealth(lives);

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (fighting)
            IconPanel.IconOff();
        if (!fighting && !player_ready)
            IconPanel.IconOn();
       

        if (!taking_damage && !die && !casting)
            state = PlayerState.idle;
       
        if(lives <= 0)
            Die();
        if (player_number == players.player1 && !player_ready && !die && !fighting && !DataHolder.pause)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
               _arena.SetGolemType(Golems.StoneGolem,player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _arena.SetGolemType(Golems.WoodGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _arena.SetGolemType(Golems.EarthGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
        }

        if (player_number == players.player2 && !player_ready && !die && !fighting && !DataHolder.pause)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                _arena.SetGolemType(Golems.StoneGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                _arena.SetGolemType(Golems.WoodGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                _arena.SetGolemType(Golems.EarthGolem, player_number);
                player_ready = true;
                IconPanel.IconOff();
            }
        }

    }

    private IEnumerator TakeDamageAnim()
    {
        taking_damage = true;
        lives--;
        healthBar.SetHealth(lives);
        if (player_number == players.player1)
        {
            DataHolder.player_1_lives = lives; 

        }
        if (player_number == players.player2)
        {
            DataHolder.player_2_lives = lives;
        }
        state = PlayerState.takedamage;
        _arena.KillAllGolem();
        yield return new WaitForSeconds(1f);
        taking_damage = false;
    }
    public void Fight(bool fighting_ )
    {
        fighting = fighting_;
    }
   
    private IEnumerator CastAnim()
    {
        casting = true;
        state = PlayerState.cast;
        yield return new WaitForSeconds(1.2f);
        casting = false;
    }

    public void Cast()
    {
        StartCoroutine(CastAnim());
    }

    private void Die()
    {
        die = true;
        state = PlayerState.die;
        referee.Winner(gameObject);
        Destroy(gameObject,1);
    }

    override public void ReciveDamage()
    {
        StartCoroutine(TakeDamageAnim());
       
    }

    
}

public enum players { player1, player2,none };
public enum PlayerState
{
    idle,
    cast,
    takedamage,
    die
}