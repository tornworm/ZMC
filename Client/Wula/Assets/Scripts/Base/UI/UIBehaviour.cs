using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour:MonoBehaviour,CtoCEventListener,CtoSEventListener,StoCEventListener
{
    // Start is called before the first frame update
    public virtual void Start()
    {

    }
    public virtual void Init()
    {

    }
    public virtual void Enter()
    {

    }
    public virtual void Response()
    {

    }
    public virtual void OnDestroy()
    {

    }

    public void OnEventTrigger(Event _Event)
    {
        throw new System.NotImplementedException();
    }
}
