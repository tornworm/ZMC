using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


// Panel功能界面
public class UI_PanelChooseRole : BasePanel
{
    // panel数据结构
    [System.Serializable]
    public class Data : IUIData
    {
    
        public void OnInit()
        {
        }
    }

    // panel节点图示
    [System.Serializable]
    public class View : IUIView
    {
      

        public void OnInit()
        {
        }
    }

    public Data data;
    public View view;

    public override void OnInit()
    {
        base.OnInit();
    }

    public override void OnEnter(object[] args)
    {
      
    }




}


