using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviour : BaseCharacter
{
    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
    }



    public override void Update()
    {
        base.Update();
        buffContainer.OnUpdate();

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

}
