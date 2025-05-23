using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveStat
{
    float MoveSpeed { get; }
    float AddMoveSpeed { get; set; }

    float GetTotalMoveSpeed();
    void ApplyMoveEffect(float amount, float duration);

    void RegisterApplyMoveEffect();
}
