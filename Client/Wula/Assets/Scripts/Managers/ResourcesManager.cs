using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ResourcesManager : BaseManager<ResourcesManager>
{

    public override IEnumerator OnAwake()
    {
        bool loadDone = false;
        yield return StartCoroutine(base.OnAwake());
        Addressables.InitializeAsync().Completed +=
            (arg) =>
            {
                loadDone = true;
            };
        yield return loadDone;
    }


    #region 可寻址系统AB加载
    /// <summary>
    /// 直接通过key寻址加载(此方法只能返回一个结果 如果寻址key大于一个寻址模糊) 
    /// /// <summary>
    public Tresult LoadAssetsByKey<Tresult>(string key, Action<Tresult> callBack = null)
    {
        Tresult tresult = default(Tresult);
        Addressables.LoadAssetAsync<Tresult>(key).Completed +=
            (obj) =>
            {
                tresult = obj.Result;
                callBack?.Invoke(obj.Result);
            };
        return tresult;
    }


    /// <summary>
    /// 通过多个key寻址加载
    /// /// <summary>
    public IList<Tresult> LoadAssetsByKeys<Tresult>(List<object> keys, Action<IList<Tresult>> callBack = null)
    {
        IList<Tresult> tresult = null;
        Addressables.LoadAssetsAsync<Tresult>((keys as IList<object>), null, Addressables.MergeMode.Union).Completed +=
            (obj) =>
            {
                tresult = obj.Result;
                callBack?.Invoke(obj.Result);
            };
        return tresult;
    }

    /// <summary>
    /// 通过key和lable进行精确寻址加载    
    /// /// <summary>
    public IList<Tresult> LoadAssetsByKeyLable<Tresult>(string key, string lable, Action<IList<Tresult>> callBack = null)
    {
        IList<Tresult> tresult = null;
        Addressables.LoadAssetsAsync<Tresult>(new List<object> { key, lable }, null, Addressables.MergeMode.Intersection).Completed +=
            (obj) =>
            {
                tresult = obj.Result;
                callBack?.Invoke(obj.Result);
            };
        return tresult;
    }


    /// <summary>
    /// 通过lable获取所有标记为此lable的资源进行寻址加载    
    /// /// <summary>
    public IList<Tresult> LoadAssetsByLable<Tresult>(AssetLabelReference lable, Action<IList<Tresult>> callBack = null)
    {
        IList<Tresult> tresult = null;
        Addressables.LoadAssetsAsync<Tresult>(lable, null).Completed +=
            (obj) =>
            {
                tresult = obj.Result;
                callBack?.Invoke(obj.Result);
            };
        return tresult;
    }



    #endregion


}
