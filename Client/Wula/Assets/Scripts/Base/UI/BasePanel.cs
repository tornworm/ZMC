using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUIData
{
    void OnInit();
}

public interface IUIView
{
    void OnInit();
}

public class BasePanel : MonoBehaviour
{
    public UIPanelType thisUIType = UIPanelType.None;

    public virtual void OnInit()
    {

    }

    public virtual void OnEnter(object[] args)
    {

    }

    public virtual void OnExit()
    {

    }
}
