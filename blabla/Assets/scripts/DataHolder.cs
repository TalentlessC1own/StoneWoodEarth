using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder 
{
    public static int player_1_lives = 3;
    public static int player_2_lives = 3;

    public static Golems golem_player_1 { get; set; }
    public static Golems golem_player_2 { get; set; }

    public static  players winner { get; set; }


    public static players pre_winner { get; set; }

    public static bool was_fight = false;


    public static bool pause = false;
}
