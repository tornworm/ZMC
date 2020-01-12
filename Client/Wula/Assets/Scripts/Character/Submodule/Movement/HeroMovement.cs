using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : BaseMovement
{
    public  HeroBehaviour heroBehaviour;

    private float turnSpeedMultiplier;
    private Vector3 targetDirection;
    private Quaternion freeRotation;

    public HeroMovement(BaseCharacter character)
    {
        OnInit(character);
    }


    public override void OnInit(BaseCharacter character)
    {
        base.OnInit(character);
        heroBehaviour = character as HeroBehaviour;
    }


    public override void OnUpdate()
    {
        base.OnUpdate();


    }

    public override void Move()
    {
        base.Move();
        if (heroBehaviour.heroController.JoyStickDir != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            heroBehaviour.characterController.Move(heroBehaviour.transform.forward * Time.deltaTime * moveSpeed);
        }
    }


    /// <summary>
    /// 角色转向
    /// </summary>
    public override void TurnTo()
    {
        base.TurnTo();
        UpdateTargetDirection();
        if (heroBehaviour.heroController.JoyStickDir != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            freeRotation = Quaternion.LookRotation(targetDirection.normalized, heroBehaviour.transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - heroBehaviour.transform.eulerAngles.y;
            var eulerY = heroBehaviour.transform.eulerAngles.y;

            if (diferenceRotation < 0 || diferenceRotation > 0)
                eulerY = freeRotation.eulerAngles.y;

            var euler = new Vector3(0, eulerY, 0);
            heroBehaviour.transform.rotation = Quaternion.Slerp(heroBehaviour.transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
        }
    }


    /// <summary>
    /// 转向计算
    /// </summary>
    public virtual void UpdateTargetDirection()
    {
        turnSpeedMultiplier = 1f;
        var forward = mainCamera.transform.TransformDirection(Vector3.forward * 10);
        forward.y = 0;

        var right = mainCamera.transform.TransformDirection(Vector3.right);

        targetDirection = heroBehaviour.heroController.JoyStickDir.x * right + heroBehaviour.heroController.JoyStickDir.y * forward;
    }



    public override void OnExit()
    {
        base.OnExit();
    }

}
