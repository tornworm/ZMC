using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息管理
/// </summary>
public class MessageManager : BaseManager<MessageManager>
{
    public Msg_C2S_Controller Msg_C2S_Controller;
    public Msg_S2C_Controller Msg_S2C_Controller;


    public override IEnumerator OnAwake()
    {
        yield return StartCoroutine(base.OnAwake());
        DontDestroyOnLoad(gameObject);
        Msg_C2S_Controller?.OnInit();
        Msg_S2C_Controller?.OnInit();
        yield break;
    }

    public override IEnumerator OnStart()
    {
        yield return StartCoroutine(base.OnStart());
        yield break;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Msg_C2S_Controller?.OnUpdate();
        Msg_S2C_Controller?.OnUpdate();
    }
}
