using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> : MonoBehaviour where T : BaseManager<T>
{
    public static T Instance;
    // 受游戏流程控制的初始化

    public virtual IEnumerator OnAwake()
    {
        Instance = (T)this;
        yield break;
    }

    public virtual IEnumerator OnStart()
    {
        yield break;
    }

    public virtual void OnUpdate()
    {

    }
}
