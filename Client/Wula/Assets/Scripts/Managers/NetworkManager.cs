using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class NetworkManager : BaseManager<NetworkManager>
{
    public StringBuilder stringBuilder = new StringBuilder();


    public override IEnumerator OnAwake()
    {
        yield return StartCoroutine(base.OnAwake());
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
    }

    public void Send(Request request)
    {
        // 添加请求到消息队列
        MessageManager.Instance.Msg_C2S_Controller.PushEvent(request);
    }
}
