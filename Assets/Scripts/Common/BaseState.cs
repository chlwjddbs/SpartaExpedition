using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState<T> where T : BaseCreature
{
    public BaseState() { }

    protected float elapsedTime;

    protected T owner;

    public virtual void Init(T owner) 
    {
        this.owner = owner;
        elapsedTime = 0;
    }

    public virtual void OnEnter() 
    {
        //Debug.Log(this.GetType().Name);
        elapsedTime = 0;
    }

    public virtual void OnUpdate(float deltaTime)
    {
        elapsedTime += deltaTime;
    }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit() 
    {
        elapsedTime = 0;
    }
}
