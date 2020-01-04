using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 加速buff  (buff实例类名要和枚举名一至！)
/// </summary>
public class BUff_MoveSpeed_Up : Buff
{
    public override void OnInit(BaseCharacter character, float[] effectTimePointArray, float[] effectValueArray, float maxTime)
    {
        buffType = BuffType.BUff_MoveSpeed_Up;
        base.OnInit(character, effectTimePointArray, effectValueArray, maxTime);
    }

    public override void OnTriggerEffect(float value)
    {
        base.OnTriggerEffect(value);
    }
}
