using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviour : BaseCharacter
{
    public HeroMovement movement;

    public HeroAniment animent;

    public HeroController heroController;
   

    private void Start()
    {
        OnInit();
    }

    public override void OnInit()
    {
        base.OnInit();
        movement = new HeroMovement(this);
        animent = new HeroAniment(this);
        heroController = GetComponent<HeroController>();

    }



    public override void Update()
    {
        base.Update();
        buffContainer.OnUpdate();
        movement.OnUpdate();

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

}
