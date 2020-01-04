using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelfKeyCode
{
    Move,Attack
}

public class BaseCtrl : BaseObject
{
    private BaseCharacter _character;
    public BaseCharacter character
    {
        get
        {
            if (_character == null)
                _character = GetComponent<HeroBehaviour>();
            return _character;
        }
    }


    public override void Update()
    {
        base.Update();

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            OnCtrl(SelfKeyCode.Move);
        else if (Input.GetKey(KeyCode.J))
            OnCtrl(SelfKeyCode.Attack);
#endif

    }


    public virtual void OnCtrl(SelfKeyCode keyCode)
    {
        switch (keyCode)
        {
            case SelfKeyCode.Move: OnTryMove(); break; 
            case SelfKeyCode.Attack: OnTryNormalAttact();break;
            default: break;
        }
    }

    public virtual void OnTryMove()
    {
        
    }

    public virtual void OnTryNormalAttact()
    {

    }
}
