using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    private bool deal_damage = false;

    private float push_force = 300;
    private float push_radius = 5;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && !deal_damage)
        {
            StartCoroutine(DamageCd());
            Standoff.PushObj(unit.GetComponent<Rigidbody2D>(), push_force, gameObject.GetComponentInParent<Golem>().transform.position, push_radius);
            unit.ReciveDamage();
        }
    }


    private IEnumerator DamageCd()
    {
        deal_damage = true;
        yield return new WaitForSeconds(0.5f);
        deal_damage = false;

    }
}
