using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController<T> : MonoBehaviour
{

    protected Queue<T> EventQueue;

    protected Dictionary<int, Action<T>> EventListeners;

    public virtual void OnInit() { }

    public virtual void OnUpdate() { }

    public virtual void AddListener(int eventID, Action<T> arg) { }

    public virtual void AddListener(Protocol_S2C protocol, Action<T> arg) { }

    public virtual void RemoveListener(int eventID, Action<T> arg) { }

    public virtual void RemoveListener(Protocol_S2C protocol, Action<T> arg) { }

    public virtual void PushEvent(T arg) { }
}
