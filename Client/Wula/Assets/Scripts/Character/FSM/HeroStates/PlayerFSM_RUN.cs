/*  _    _
   (o)--(o)
  /.______.\
  \________/    代码神宠
 ./        \.
( .        , )
 \ \_\\//_/ /
  ~~  ~~  ~~
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM_RUN : HeroStateBase 
{
    Vector3 HeroMove;
    public PlayerFSM_RUN(HeroBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public override void OnInit()
    {
        base.OnEnter();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        behaviour.animent.SetAllBoolToFalse();
        behaviour.animent.PlayAnim("running", true);
        Debug.LogWarning("您移动了");

    }

    public override void OnUpdate()
    {
        behaviour.movement.TurnTo();
        behaviour.movement.Move();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
