using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


// Panel功能界面
public class PanelLoading : BasePanel
{
    // panel数据结构
    [System.Serializable]
    public class Data : IUIData
    {
        public string loadSceneName;
        public AsyncOperation async;
        public int progress = 0;
        public Action loadDoneCallback = null;  // 场景加载成功后的回掉
        public void OnInit()
        {
        }
    }

    // panel节点图示
    [System.Serializable]
    public class View : IUIView
    {
        public Slider m_pProgress;

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
        base.OnEnter(args);
        if (args.Length > 0)
            data.loadSceneName = args[0] as string;
        if (args.Length > 1)
            data.loadDoneCallback = args[1] as Action;
        StartCoroutine(LoadScenes());
    }



    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScenes()
    {
        if(string.IsNullOrEmpty(data.loadSceneName)|| string.IsNullOrWhiteSpace(data.loadSceneName))
        {
            Debug.LogError("加载场景失败 场景名字不正确");
            while (true)
            {
                yield return null;
            }
        }

        int nDisPlayProgress = 0;
        data.async = SceneManager.LoadSceneAsync(data.loadSceneName);
        data.async.allowSceneActivation = false;
        while (data.async.progress < 0.9f)
        {
            data.progress = (int)data.async.progress * 100;
            while (nDisPlayProgress < data.progress)
            {
                ++nDisPlayProgress;
                view.m_pProgress.value = (float)nDisPlayProgress / 100;
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        data.progress = 100;
        while (nDisPlayProgress < data.progress)
        {
            ++nDisPlayProgress;
            view.m_pProgress.value = (float)nDisPlayProgress / 100;
            yield return new WaitForEndOfFrame();
        }
        // 加载成功回调
        data.loadDoneCallback?.Invoke();
        // 允许激活此场景
        data.async.allowSceneActivation = true;
        // 关闭此界面
        UIManager.Instance.ClosePanel(UIPanelType.PanelLoading);



    }


    public override void OnExit()
    {
        base.OnExit();
        Destroy(gameObject);
    }
}
