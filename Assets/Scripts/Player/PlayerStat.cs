using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat, ICombatStat, IMoveStat
{
    public float AttackDamage => attackDamage;
    [SerializeField] private float attackDamage;

    public float AttackRate => attackRate;
    [SerializeField] private float attackRate;

    public float Defence => defence;
    [SerializeField] private float defence;

    public float MoveSpeed => moveSpeed;
    [SerializeField] private float moveSpeed;

    public float AddMoveSpeed { get; set; }

    private Coroutine addMoveEffectCoroutine;

    public override void RegisterUseEffect()
    {
        base.RegisterUseEffect();
        RegisterApplyMoveEffect();
    }

    public void ApplyUseItem(UseEffect useEffect)
    {
        if (registedUseEffect.TryGetValue(useEffect.useableType, out ApplyUseEffect applyEffect))
        {
            Debug.Log(useEffect.useableType);
            applyEffect.ApplyEffect(useEffect);
        }
    }

    public void Attack()
    {

    }

    public void TakeDamage(float damage)
    {
        currnethealth -= damage;
        UIManager.Instance.OnChangeHealth?.Invoke(currnethealth , maxHealth);
    }

    public override void RecoverHealth(float heal)
    {
        base.RecoverHealth(heal);
        UIManager.Instance.OnChangeHealth?.Invoke(currnethealth, maxHealth);
    }


    #region MoveStat

    public void RegisterApplyMoveEffect()
    {
        if(this is IMoveStat moveStat)
        {
            registedUseEffect[UseableType.MoveSpeed] = new ApplyMoveEffect(moveStat);
        }
        else
        {
            Debug.LogError("MoveEffect 등록 실패 : IMoveStat Interface를 확인해 주세요.");
        }   
    }

    public float GetTotalMoveSpeed()
    {
        return moveSpeed + AddMoveSpeed;
    }

    public void ApplyMoveEffect(float amount, float duration)
    {
        if(addMoveEffectCoroutine != null)
        {
            StopCoroutine(addMoveEffectCoroutine);
        }

        addMoveEffectCoroutine = StartCoroutine(AddMoveEfectCoroutine(amount, duration));
    }

    private IEnumerator AddMoveEfectCoroutine(float amount, float duration)
    {
        AddMoveSpeed = amount;
        yield return new WaitForSeconds(duration);
        AddMoveSpeed = 0;
        addMoveEffectCoroutine = null;
    }
    #endregion
}
