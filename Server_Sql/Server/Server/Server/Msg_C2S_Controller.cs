using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


/*本地请求消息上传控制器，负责管理游戏中基于事件机制的所有逻辑*/
public class Msg_C2S_Controller : BaseController<Event>
{
    
    public override void OnInit()
    {
        base.OnInit();
        EventQueue = new Queue<Event>();
        EventListeners = new Dictionary<int, Action<Event>>();
    }

    //向事件控制器追加一个监听者 例：Msg_S2C_Controller.AddListener(10001,OnGoldChanged);
    public override void AddListener(int eventID, Action<Event> _Listener)
    {
        if (!EventListeners.ContainsKey(eventID))
        { EventListeners.Add(eventID, _Listener); return; }

        EventListeners[eventID] += _Listener;
    }

    public override void AddListener(Protocol_C2S protocol, Action<Event> _Listener)
    {
        AddListener((int)protocol, _Listener);
    }


    //从事件控制器中删除一个监听者
    public override void RemoveListener(int eventID, Action<Event> _Listener)
    {
        if (EventListeners.ContainsKey(eventID))
            EventListeners[eventID] -= _Listener;
    }

    public override void RemoveListener(Protocol_C2S protocol, Action<Event> _Listener)
    {
        RemoveListener((int)protocol, _Listener);
    }

    //向队尾追加一个事件
    public override void PushEvent(Event _Event)
    {
        EventQueue.Enqueue(_Event);
    }

    public override void OnUpdate()
    {
        //判断是否有事件和是否有监听者
        if (EventQueue.Count > 0)
        {
            //每一帧都从队头取出一个事件
            Event Current = EventQueue.Dequeue();
            //消息处理
            if (EventListeners.ContainsKey(Current.EventID))
            {
                EventListeners[Current.EventID]?.Invoke(Current);
            }
        }
    }

}


