using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActor : BaseModel
{
    /// <summary>
    /// buff管理器
    /// </summary>
    public BuffContainer buffContainer;


    /// <summary>
    /// 状态机
    /// </summary>
    public Hero_FSMMachine fsmMachine;

    public override void OnInit()      
    {
        base.OnInit();
        buffContainer = new BuffContainer(this as BaseCharacter);
        fsmMachine = new Hero_FSMMachine(this as HeroBehaviour);
    }

    public override void Update()
    {
        base.Update();
        buffContainer.OnUpdate();
        fsmMachine.OnUpdate();
    }


    public override void OnDestroy()
    {
        base.OnDestroy();
    }

}




