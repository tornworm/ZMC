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
    public float posX, posY;

   



    public void Start()
    {
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



    public void SetJoyStickPos(Vector2 pos)
    {
        posX = pos.x;
        posY = pos.y;
    }
}
