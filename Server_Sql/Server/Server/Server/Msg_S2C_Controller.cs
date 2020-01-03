using System.Collections.Generic;
using System;
using System.Text;
using Newtonsoft.Json;
using System.Net.Sockets;

public class Request
{
    public int requestID { get; private set; }
    public string data { get; private set; }
    public Request() { }
    public Request(Protocol_S2C requestID, string data)
    {
        this.requestID = (int)requestID;
        this.data = data;
    }

    public Request(Protocol_S2C requestID, System.Object jsonObj)
    {
        this.requestID = (int)requestID;
        data = JsonConvert.SerializeObject(jsonObj);
    }
}
/*服务器消息接收控制器，负责管理游戏中基于事件机制的所有逻辑*/
public class Msg_S2C_Controller : BaseController<Request>
{

    public StringBuilder stringBuilder = new StringBuilder();
    private ServerUser serverUser;
    public override void OnInit()
    {
        base.OnInit();
        
    }
    public void InitServerUser(ServerUser serverUser)
    {
        EventQueue = new Queue<Request>();
        this.serverUser = new ServerUser();
        this.serverUser = serverUser;
    }
    //向队尾追加一个事件
    public override void PushEvent(Request request)
    {
        EventQueue.Enqueue(request);
    }
    public override void PushEvent(Request request, object ob)
    {
        EventQueue.Enqueue(request);
        serverUser = (ServerUser)ob;
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

            //Server.Instance.Send(stringBuilder.ToString());
            serverUser.Send(stringBuilder.ToString());
            stringBuilder.Length = 0;
        }
    }
    
}

