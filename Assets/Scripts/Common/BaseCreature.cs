using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCreature : MonoBehaviour
{
    public BaseStat stat;

    protected Animator animator;

    public Rigidbody Rb => rb;
    protected Rigidbody rb;

    public virtual void Init() 
    { 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public virtual void RegisterState() { }
}
