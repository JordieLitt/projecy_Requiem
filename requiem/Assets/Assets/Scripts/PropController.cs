using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{

    Animator animator;
    public int propState;
    void start()
    {
        animator = GetComponent<Animator>();
    }

    void Update ()
    {
        animator.SetInteger("State", propState);
    }
}
