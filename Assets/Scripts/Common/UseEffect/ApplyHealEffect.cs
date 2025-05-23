using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHealEffect : ApplyUseEffect
{
    public ApplyHealEffect(BaseStat target)
    {
        this.target = target;
    }

    private BaseStat target;
    public override void ApplyEffect(UseEffect useEffect)
    {
        target.RecoverHealth(useEffect.amount);
    }
}
