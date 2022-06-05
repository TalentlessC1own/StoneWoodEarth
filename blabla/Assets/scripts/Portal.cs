using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
   public Animator portal_animator;

    private void Awake()
    {
        portal_animator = GetComponent<Animator>();
    }
}
