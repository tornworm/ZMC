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


public class HeroController : BaseCtrl 
{

    /// <summary>
    /// 摇杆方向或键盘方向
    /// </summary>
    public Vector2 JoyStickDir;

    public void Start()
    {
    }

    public override void Update()
    {
        base.Update();
#if PC_CTRL_MODE
        JoyStickDir.x = Input.GetAxis("Horizontal");
        JoyStickDir.y = Input.GetAxis("Vertical");
#endif
    }

    public override void OnTryMove()
    {
        base.OnTryMove();
        character.fsmMachine.SetState(PlayerState.RUN);

    }

    public override void OnTryNormalAttact()
    {
        base.OnTryNormalAttact();
    }


    public override void OnNoOperations()
    {
        base.OnNoOperations();
        character.fsmMachine.SetState(PlayerState.IDLE);
    }


    public void SetJoyStickPos(Vector2 pos)
    {
#if PHONE_CTRL_MODE
        JoyStickDir = pos;
#endif
    }
}
