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
      
        Debug.LogWarning("您移动了");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        HeroMove.z = Input.GetAxis("Vertical") * 3 * Time.deltaTime;
        HeroMove.x = Input.GetAxis("Horizontal") * 3 * Time.deltaTime;
        behaviour.characterController.Move(behaviour.transform.TransformDirection(HeroMove));
        //解决朝向问题
        Vector3 newDir = new Vector3(HeroMove.x, 0, HeroMove.z).normalized;
        behaviour.transform.forward = behaviour.transform.TransformDirection(newDir);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
