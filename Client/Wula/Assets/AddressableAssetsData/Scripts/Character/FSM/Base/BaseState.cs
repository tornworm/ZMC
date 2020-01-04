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

public class BaseState 
{

    public virtual void OnInit() { }

    public virtual void OnEnter() { }

    public virtual void OnUpdate() { }

    public virtual void OnExit() { }

}
