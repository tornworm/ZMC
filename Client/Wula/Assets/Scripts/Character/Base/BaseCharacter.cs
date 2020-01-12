using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : BaseActor
{
    /// <summary>
    /// 碰撞体
    /// </summary>
    protected CapsuleCollider m_CapsuleCol;


    /// <summary>
    /// 人物控制器
    /// </summary>
    [HideInInspector] public CharacterController characterController;


    /// <summary>        
    /// 移动朝向        
    /// </summary>        
    protected Vector3 moveDirection;

    /// <summary>        
    /// 移动至向量位置        
    /// </summary>        
    protected Vector3 moveTargetPos;



    public override void OnInit()
    {
        base.OnInit();
        characterController = GetComponent<CharacterController>();
    }

    public override void Update()
    {
        base.Update();
        ImitationGravity();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }


    /// <summary>       
    ///  获取英雄角色的身体半径      
    ///  </summary>        
    ///  <returns></returns>        
    public float GetBodyRadius()
    {
        if (this.m_CapsuleCol != null)
            return this.m_CapsuleCol.radius;
        return 0f;
    }


    /// <summary>       
    /// 是否可以被攻击      
    /// </summary>       
    /// <returns></returns>       
    public virtual bool IsPossibleAttacked()
    {
        return true;
    }

    /// <summary>       
    /// 根据当前状态判定是否在Move  
    /// </summary>      
    /// <returns></returns>        
    public virtual bool IsPossibleMoveTo()
    {
        return true;
    }


    /// <summary>       
    /// 是否在使用技能        
    /// </summary>        
    /// <returns></returns>       
    public virtual bool IsUsingSkill()
    {
        return false;
    }


    /// <summary>
    /// 是否死亡
    /// </summary>
    /// <returns></returns>
    public bool IsDead()
    {
        return false;
    }

    /// <summary>        
    ///  播放受击效果        
    /// </summary>      
    /// <param name="attackerPos"></param>  
    /// <param name="effId"></param>       
    public void PlayAttackedEff(Vector3 attackerPos)
    {

    }


    /// <summary>      
    ///  自残  
    /// 一般用在Debuff状态下      
    /// </summary>        
    /// <param name="damage"></param>      
    /// <param name="resultHp"></param>  
    /// <param name="effId"></param>      
    /// <param name="numEffId"></param>        
    public virtual void SelfDamage(float damage, float resultHp, int effId, int numEffId)
    {

    }


    /// <summary>
    /// 模拟重力
    /// </summary>
    public virtual void ImitationGravity()
    {
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down);
        }

    }
}







