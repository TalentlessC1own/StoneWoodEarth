using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnGolem : MonoBehaviour
{
    private Golem StoneGolem;
    private Golem WoodGolem;
    private Golem EarthGolem;

    private GameObject WoodHead;
    private GameObject EarthHead;
    private GameObject StoneHead;

    

    Vector3 position_golem_1 = new Vector3(-2.13f, -1.15f, 1);
    Vector3 position_golem_2 = new Vector3(1.82f, -1.15f, 0);

    Vector3 position;

    private Golem selected_golem;

    private GameObject selected_head;
    private void Awake()
    {
        StoneGolem = Resources.Load<Golem>("StoneGolem");
        WoodGolem = Resources.Load<Golem>("WoodGolem");
        EarthGolem = Resources.Load<Golem>("EarthGolem");
        WoodHead = Resources.Load<GameObject>("WoodHead");
        EarthHead = Resources.Load<GameObject>("EarthHead");
        StoneHead = Resources.Load<GameObject>("StoneHead");
    }
    private void Start()
    {
        position = transform.localPosition;
    }

    private void Update()
    {
        position = transform.localPosition;
        Debug.Log(position);
    }
    private void  SelectGolem(Golems golem_type)
    {
        switch (golem_type)
        {
            case Golems.StoneGolem:
                selected_head = StoneHead;
                selected_golem = StoneGolem;
                break;
            case Golems.WoodGolem:
                selected_head = WoodHead;
                selected_golem = WoodGolem;
                break;
            case Golems.EarthGolem:
                selected_head = EarthHead;
                selected_golem = EarthGolem;
                break;
        }
    }

    public void CreateGolemHead(Golems golem_type)
    {
        SelectGolem(golem_type);
        if (gameObject.name == "SpawnGolemPlayer2" && selected_head != null)
        {
            Instantiate(selected_head, position_golem_2, Quaternion.identity);
        }
        if (gameObject.name == "SpawnGolemPlayer1" && selected_head != null)
        {
            Instantiate(selected_head, position_golem_1, Quaternion.identity);
        }
    }
   public Golem CreateGolem(Golems golem_type)
   {
        SelectGolem(golem_type);
        
        Golem newGolem = Instantiate(selected_golem, position,Quaternion.identity) as Golem;

        if (gameObject.name == "SpawnGolemPlayer2")
        {
            newGolem.GolemRotate();
            newGolem.GetComponent<GolemController>().controller = Controller.player_2;
            
            newGolem.transform.position = position_golem_2;
        }
        else
        {
            newGolem.transform.position = position_golem_1;
        }
       
       
        return newGolem;
    }
}
