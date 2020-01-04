using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMachine
{

    public virtual void OnInit() { }

    public virtual void OnUpdate() { }

    public virtual void OnDestroy() { }

    public virtual void SetState(int state)
    {

    }

}
