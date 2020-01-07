using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BaseMovement : BaseSubModule
{
    /// <summary>
    /// 主相机
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// 转向速度
    /// </summary>
    public float turnSpeed = 10f;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 7f;


    public override void OnInit(BaseCharacter character)
    {
        base.OnInit(character);
        mainCamera = Camera.main;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public virtual void Move()
    {

    }

    public virtual void TurnTo()
    {

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
