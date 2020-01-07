using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAniment : BaseAniment
{
    protected Animator animator;

    public HeroAniment(HeroBehaviour behaviour)
    {
        OnInit(behaviour);
    }

    public override void OnInit(BaseCharacter character)
    {
        base.OnInit(character);
        animator = character.GetComponent<Animator>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public virtual void PlayAnim(string codition, int value, float speed = 1)
    {
        animator.speed = speed;
        animator.SetInteger(codition, value);
    }

    public virtual void PlayAnim(string codition, float value, float speed = 1)
    {
        animator.speed = speed;
        animator.SetFloat(codition, value);
    }

    public virtual void PlayAnim(string codition, bool value, float speed = 1)
    {
        animator.speed = speed;
        animator.SetBool(codition, value);
    }

    public virtual void PlayAnimTrigger(string codition, float speed = 1)
    {
        animator.speed = speed;
        animator.SetTrigger(codition);
    }

    public virtual void SetAllBoolToFalse()
    {
        foreach (var param in animator.parameters)
        {
            if(param.type ==  AnimatorControllerParameterType.Bool)
            {
                PlayAnim(param.name, false);
            }
        } 
    }


    public override void OnExit()
    {
        base.OnExit();
    }
}
