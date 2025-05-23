using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMoveEffect : ApplyUseEffect
{
    public ApplyMoveEffect(IMoveStat target)
    {
        this.target = target;
    }

    private IMoveStat target;

    public override void ApplyEffect(UseEffect useEffect)
    {
        target.ApplyMoveEffect(useEffect.amount, useEffect.duration);
    }
}
