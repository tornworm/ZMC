using System;
using System.Collections;
using System.Collections.Generic;


public class BaseController<T> where T : new()
{
   


    protected Queue<T> EventQueue;

    protected Dictionary<int, Action<T>> EventListeners;

    public virtual void OnInit() { }

    public virtual void OnUpdate() { }

    public virtual void AddListener(int eventID, Action<T> arg) { }

    public virtual void AddListener(Protocol_C2S protocol, Action<T> arg) { }

    public virtual void RemoveListener(int eventID, Action<T> arg) { }

    public virtual void RemoveListener(Protocol_C2S protocol, Action<T> arg) { }

    public virtual void PushEvent(T arg) { }
    public virtual void PushEvent(T arg,object ob) { }
    
}
