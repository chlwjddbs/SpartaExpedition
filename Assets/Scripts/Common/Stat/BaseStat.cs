using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class BaseStat : MonoBehaviour
{
    protected Dictionary<UseableType,ApplyUseEffect> registedUseEffect = new Dictionary<UseableType,ApplyUseEffect>();

    [SerializeField] protected float maxHealth;
    [SerializeField] protected float baseHealth;
    [SerializeField] protected float currnethealth;

    protected int exp;

    public bool isDeath;

    public virtual void Init()
    {
        maxHealth = baseHealth;
        currnethealth = maxHealth;
        RegisterUseEffect();
    }

    public virtual void RegisterUseEffect() 
    {
        registedUseEffect[UseableType.HP] = new ApplyHealEffect(this);
    }

    public virtual void RecoverHealth(float heal)
    {
        currnethealth = Mathf.Clamp(currnethealth + heal, currnethealth + heal, maxHealth);    
    }

    public virtual void Death() { }
}
