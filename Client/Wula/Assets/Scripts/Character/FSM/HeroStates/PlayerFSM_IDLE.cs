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

public class PlayerFSM_IDLE : HeroStateBase 
{
    public PlayerFSM_IDLE(HeroBehaviour behaviour)
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
        behaviour.animent.PlayAnim("idle", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
