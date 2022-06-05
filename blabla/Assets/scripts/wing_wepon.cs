using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wing_wepon : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private Vector3 direction;

    public Vector3 Direction { set { direction = value; } }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       Unit unit = collision.GetComponent<Unit>();

        if(unit)
        {
            unit.ReciveDamage();
            Destroy(gameObject);
        }
    }
}
