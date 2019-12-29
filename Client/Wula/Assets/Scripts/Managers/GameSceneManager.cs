using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameSceneManager : BaseManager<GameSceneManager>
{

    public override IEnumerator OnAwake()
    {
        yield return StartCoroutine(base.OnAwake());
    }

    public override IEnumerator OnStart()
    {
        yield return StartCoroutine(base.OnStart());
    }


    /// <summary>
    /// 加载场景并显示加载界面
    /// </summary>
    public void LoadSceneShowLoadingAsync(string sceneName,Action callBack = null)
    {
        if (string.IsNullOrEmpty(sceneName) || string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError(sceneName + "场景名字格式不正确");
            return;
        }
        UIManager.Instance.ShowPanel(UIPanelType.PanelLoading, PanelFrom.Mask, sceneName, callBack);
    }

    public override void OnUpdate()
    {

    }

}
