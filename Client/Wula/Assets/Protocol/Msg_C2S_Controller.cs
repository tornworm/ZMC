using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Text;

public class Request
{
    public int requestID { get; private set; }
    public string data { get; private set; }

    public Request(Protocol_C2S requestID, string data)
    {
        this.requestID = (int)requestID;
        this.data = data;
    }

    public Request(Protocol_C2S requestID, System.Object jsonObj)
    {
        this.requestID = (int)requestID;
        data = JsonConvert.SerializeObject(jsonObj);
        Debug.Log(data);
    }
}

/*本地请求消息上传控制器，负责管理游戏中基于事件机制的所有逻辑*/
public class Msg_C2S_Controller : BaseController<Request>
{
    public StringBuilder stringBuilder = new StringBuilder();

    public override void OnInit()
    {
        base.OnInit();
        EventQueue = new Queue<Request>();
        DontDestroyOnLoad(gameObject);
    }

    //向队尾追加一个事件
    public override void PushEvent(Request request)
    {
        EventQueue.Enqueue(request);
    }

    public override void OnUpdate()
    {
        //判断是否有事件和是否有监听者
        if (EventQueue.Count > 0)
        {
            //每一帧都从队头取出一个事件
            Request request = EventQueue.Dequeue();
            //消息处理
            stringBuilder
            .Append(request.requestID.ToString())
            .Append("&")
            .Append(request.data);

            ClientManager.Instance.Send(stringBuilder.ToString());
            stringBuilder.Length = 0;
        }
    }
}

