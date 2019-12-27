﻿using System.Collections.Generic;
using UnityEngine;

/*消息上传控制器，负责管理游戏中基于事件机制的所有逻辑*/
public class SendController : BaseController
{
    //唯一实例
    public static SendController Singleton = null;
    //事件队列
    Queue<Event> EventQueue;
    //监听者列表
    List<CtoSEventListener> EventListeners;

    private void Awake()
    {
        OnLoad();
    }
    //自定义初始化方法
    public void OnLoad()
    {
        Singleton = this;
        EventQueue = new Queue<Event>();
        EventListeners = new List<CtoSEventListener>();
        DontDestroyOnLoad(gameObject);
    }

    //向事件控制器追加一个监听者
    public void AddListener(CtoSEventListener _Listener)
    {
        EventListeners.Add(_Listener);
    }

    //从事件控制器中删除一个监听者
    public void RemoveListener(CtoSEventListener _Listener)
    {
        EventListeners.Remove(_Listener);
    }

    //向队尾追加一个事件
    public void PushEvent(Event _Event)
    {
        EventQueue.Enqueue(_Event);
    }

    void Update()
    {
        //判断是否有事件和是否有监听者
        if (EventQueue.Count != 0 && EventListeners.Count != 0)
        {
            //每一帧都从队头取出一个事件
            Event Current = EventQueue.Dequeue();
            //派发给所有监听者进行处理
            foreach (CtoSEventListener Listener in EventListeners)
            {

                Listener.OnEventTrigger(Current);

            }
        }
    }
}

