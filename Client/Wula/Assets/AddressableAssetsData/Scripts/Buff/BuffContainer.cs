using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// buff管理器（容器） buff模块
/// </summary>
public class BuffContainer
{

    public BaseCharacter character;

    /// <summary>
    /// 增益buff列表
    /// </summary>
    public List<Buff> buffList;  

    /// <summary>
    /// 减益buff列表
    /// </summary>
    public List<DeBuff> deBuffList;


    public BuffContainer(BaseCharacter baseCharacter)
    {
        character = baseCharacter;
        OnInit();
    }

    public void OnInit()
    {
        buffList = new List<Buff>();
        deBuffList = new List<DeBuff>();
    }

    public void OnUpdate()
    {
        for (int i = buffList.Count-1; i >=0; i--)
        {
            buffList[i].OnUpdate();
        }
        for (int i = deBuffList.Count - 1; i >= 0; i--)
        {
            deBuffList[i].OnUpdate();
        }
    }


    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="character"></param>
    /// <param name="effectTimePointArray"></param>
    /// <param name="effectValueArray"></param>
    /// <param name="maxTime"></param>
    /// <param name="OnComplete"></param>
    public void AddBuff(Buff buff, BaseCharacter character, float[] effectTimePointArray, float[] effectValueArray, float maxTime)
    {
        BaseBuff curBuff = this.buffList.Find(x => x.GetBuffType() == buff.GetBuffType());
        if (curBuff != null)
        {
            curBuff.OnInit(character, effectTimePointArray, effectValueArray, maxTime);
            curBuff.OnRepetition();
        }
        else
        {
            buff.OnEnter();
            buff.OnInit(character, effectTimePointArray, effectValueArray, maxTime);
            buffList.Add(buff);
        };
    }


    /// <summary>
    /// 添加减益buff
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="character"></param>
    /// <param name="effectTimePointArray"></param>
    /// <param name="effectValueArray"></param>
    /// <param name="maxTime"></param>
    /// <param name="OnComplete"></param>
    public void AddDeBuff(DeBuff buff, BaseCharacter character, float[] effectTimePointArray, float[] effectValueArray, float maxTime, Action OnComplete = null)
    {
        BaseBuff curBuff = this.deBuffList.Find(x => x.GetBuffType() == buff.GetBuffType());
        if (buff != null)
        {
            buff.OnInit(character, effectTimePointArray, effectValueArray, maxTime);
        }
        else
        {
            buff.OnInit(character, effectTimePointArray, effectValueArray, maxTime);
            deBuffList.Add(buff);
        };
    }



    /// <summary>
    /// 精准移除一个buff（通过ID）
    /// </summary>
    /// <param name="buffType"></param>
    /// <param name="id"></param>
    public void RemoveBuff(BuffType buffType, int id)
    {

        //if ((int)buffType > 0)
        //{
        //    var buff = buffList.Find(buff => buff.GetBuffType() == buffType && id == buff.GetBuffID());
        //    buffList.Remove(buff);
        //}
        //if ((int)buffType < 0)
        //{
        //    var buff = deBuffList.Find(buff => buff.GetBuffType() == buffType && id == buff.GetBuffID());
        //    deBuffList.Remove(buff);
        //}

    }

    


    /// <summary>
    /// 移除所有buff
    /// </summary>
    public void RemoveAllBuff()
    {
        RemoveAllRiseBuff();
        RemoveAllDeBuff();
    }


    /// <summary>
    /// 移除所有正向buff
    /// </summary>
    public void RemoveAllRiseBuff()
    {
        for (int i = buffList.Count - 1; i >= 0; i--)
        {
            buffList[i].OnExit();
            buffList.RemoveAt(i);
        }
    }

    /// <summary>
    /// 移除所有debuff
    /// </summary>
    public void RemoveAllDeBuff()
    {
        for (int i = deBuffList.Count - 1; i >= 0; i--)
        {
            deBuffList[i].OnExit();
            deBuffList.RemoveAt(i);
        }
    }


    /// <summary>
    /// 检查是否含有某类buff
    /// </summary>
    public bool HasBuff(BuffType buffType)
    {
        if ((int)buffType > 0)
        {
            return buffList.Find(buff => buff.GetBuffType() == buffType) != null;
        }
        if ((int)buffType < 0)
        {
            return deBuffList.Find(buff => buff.GetBuffType() == buffType) != null;
        }
        Debug.LogError("没有找到此buff:" + buffType.ToString());
        return false;
    }



}
