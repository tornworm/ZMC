using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



/// <summary>
/// 全部buff类型(通过枚举值进行判断 >0为正向buff <0为逆向buff)
/// </summary>
public enum BuffType
{
    None = 0,

    #region 正向buff
    // 移动速度增加buff
    BUff_MoveSpeed_Up = 1,
    // 攻击速度增加buff
    BUFF_AttackSpeed_Up = 2,
    #endregion


    #region 逆向buff
    // 移动速度减少buff
    BUff_MoveSpeed_Cut = -1,
    // 攻击速度减少buff
    BUFF_AttackSpeed_Cut = -2,
    #endregion
}

public class BaseBuff 
{
    private int ID { get; set; }  // buffID 区分同一buff的多个实例。
    private float[] effectTimePointArray { get; set; }  // buff生效时间点（一般用于buff持续时间内 多次触发） 数组元素为normalTime
    private float[] effectValueArray { get; set; }  // buff生效value值（与effectTime每个元素相对应 时间=》buff效果） 
    private BaseCharacter character { get; set; }  // buff挂载实体
    private float curTime { get; set; }  // 当前时间
    private float maxTime { get; set; }  // 最大时间
    protected BuffType buffType { get; set; } = BuffType.None;  // 当前buff类型


    public Action OnStart; // buff开始启动的回调(此回调适合触发特效或音效)
    public Action OnComplete; // buff完成后回调(此回调适合删除特效或音效 重置角色等)

    /// <summary>
    /// buff初始化
    /// </summary>
    public virtual void OnInit(BaseCharacter character, float[] effectTimePointArray, float[] effectValueArray, float maxTime)
    {
        this.character = character;
        this.effectTimePointArray = effectTimePointArray;
        this.effectValueArray = effectValueArray;
    }

    public virtual void OnEnter()
    {
        OnStart?.Invoke();
    }

    /// <summary>
    /// 更新
    /// </summary>

    public virtual void OnUpdate()
    {
        int timeQuantum = 0; // 当前时间段
        float norTime = GetNormalTime();
        if (norTime < 0)
            return;

        if (norTime >= 1)
        {
            OnComplete?.Invoke();
            OnExit();
        }

        if (effectTimePointArray.Length < 0 || effectValueArray.Length < 0)
        {
            Debug.LogError("buff生效时间点或buff生效value值数量<0");
            return;
        }

        if (effectTimePointArray.Length!= effectValueArray.Length)
        {
            Debug.LogError("buff生效时间点与buff生效value值数量不同！！");
            return;
        }
        curTime += Time.deltaTime;


        for (int i = 0; i < effectTimePointArray.Length; i++)
        {
            //  如果当前buff时间到达效果触发点
            if (effectTimePointArray[timeQuantum] > norTime) 
            {
                // 触发一次效果
                OnTriggerEffect(effectValueArray[timeQuantum]);
                timeQuantum++;
            }
        }
    }


    /// <summary>
    /// 时间点触发后的调用
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnTriggerEffect(float value)
    {

    }

    /// <summary>
    /// 当重复使用buff
    /// </summary>
    public virtual void OnRepetition()
    {

    }


    /// <summary>
    /// 退出运行
    /// </summary>
    public virtual void OnExit()
    {
        // character.buffContainer.RemoveBuff(buffType, ID);
    }


    /// <summary>
    /// 获取buff类型
    /// </summary>
    public virtual BuffType GetBuffType()
    {
        return buffType;
    }


    /// <summary>
    /// 获取buff类型
    /// </summary>
    public virtual int GetBuffID()
    {
        return ID;
    }

    /// <summary>
    /// 获取当前buff运行时间
    /// </summary>
    public virtual float GetCurTime()
    {
        return curTime;
    }

    /// <summary>
    /// 获取当前buff最大时间
    /// </summary>
    public virtual float GetMaxTime()
    {
        return maxTime;
    }


    /// <summary>
    /// 获取buff进度 单位化时间
    /// </summary>
    /// <returns></returns>
    public virtual float GetNormalTime()
    {
        return curTime / maxTime;
    }

   



}
