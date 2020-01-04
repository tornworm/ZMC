using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel : BaseObject
{
    public Material material;
    protected Animator animator;

    public override void OnInit()
    {
        base.OnInit();
        animator = GetComponent<Animator>();
    }

    public void PlayAnim(string codition, int value, float speed = 1)
    {
        animator.speed = speed;
        animator.SetInteger(codition, value);
    }

    public void PlayAnim(string codition, float value, float speed = 1)
    {
        animator.speed = speed;
        animator.SetFloat(codition, value);
    }

    public void PlayAnim(string codition, bool value, float speed = 1)
    {
        animator.speed = speed;
        animator.SetBool(codition, value);
    }

    public void PlayAnimTrigger(string codition, float speed = 1)
    {
        animator.speed = speed;
        animator.SetTrigger(codition);
    }
}

