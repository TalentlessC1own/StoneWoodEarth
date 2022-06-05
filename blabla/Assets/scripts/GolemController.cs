using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemController : MonoBehaviour
{
    private Golem golem;

    private string move_control;
    private KeyCode jump_control;
    private KeyCode attack_control;

    public Controller controller;

    private void Awake()
    {
        golem = gameObject.GetComponent<Golem>();
    }

    private void Start()
    {
        SetControllers();
    }
    private void Update()
    {
        if (Input.GetButton(move_control)) golem.Run(Input.GetAxis(move_control));
        else if (golem.IsGrounded) golem.State = GolemStates.Idle;
        if (golem.IsGrounded && Input.GetKeyDown(jump_control)) golem.Jump();
        if (Input.GetKeyDown(attack_control)) golem.Attack();
       
    }

    private void SetControllers()
    {
        if (controller == Controller.player_1)
        {
            move_control = "Horizontal";
            jump_control = KeyCode.W;
            attack_control = KeyCode.Q;
        }
        if (controller == Controller.player_2)
        {
            move_control = "Horizontal1";
            jump_control = KeyCode.UpArrow;
            attack_control = KeyCode.P;
        }
    }
}

public enum Controller
{
    player_1,
    player_2
}