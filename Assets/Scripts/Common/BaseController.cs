using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController<T> where T : BaseCreature
{
    public Dictionary<string, BaseState<T>> registedState = new Dictionary<string, BaseState<T>>();

    public BaseState<T> CurrentState => currnetState;
    protected BaseState<T> currnetState;
    protected BaseState<T> previousState;

    protected T owner;

    public BaseController(T owner, BaseState<T> initState) 
    {
        this.owner = owner;
        RegisterState(initState);
        currnetState = initState;
        currnetState.OnEnter();
    }

    public virtual void OnUpdate(float deltaTime)
    {
        currnetState.OnUpdate(deltaTime);
    }

    public virtual void OnFixedUpdate()
    {
        currnetState.OnFixedUpdate();
    }

    public virtual void RegisterState(BaseState<T> state)
    {
        state.Init(owner);
        registedState[state.GetType().Name] = state;
    }

    public virtual void ChangeState(string newState)
    {
        if (currnetState.ToString() == newState) return;

        currnetState?.OnExit();
        previousState = currnetState;
        
        currnetState = registedState[newState];
        currnetState.OnEnter();
    }

    public virtual void LookRotat() { }
}
