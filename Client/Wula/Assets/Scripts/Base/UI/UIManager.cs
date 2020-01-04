using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPanelType
{
    None,
    PanelLoading,
    PanelLogin,
    PanelMain,
    PanelChooseRole
}

public enum PanelFrom
{
    Normal,
    TOP,
    Message,
    Mask
}


/// <summary>
/// UI管理器  (当前全部UI类型为透视效果3DUI)
/// </summary>
public class UIManager : BaseManager<UIManager> 
{
    [System.Serializable]
    public class View
    {
        public Transform _Normal;
        public Transform _TOP;
        public Transform _Message;
        public Transform _Mask;
    }
    [System.Serializable]
    public class Data
    {
        public Dictionary<UIPanelType, BasePanel> PanelCacheDic = new Dictionary<UIPanelType, BasePanel>();  // 加载过的UI 保存在内存中的预设集合
        public List<BasePanel> CurPanels = new List<BasePanel>();  // 当前打开的所有panel
        public Stack<BasePanel> messageBoxStack = new Stack<BasePanel>();   // 所有消息盒子
    }

    public Data data;
    public View view;

    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <param name="name"></param>
    public void ShowPanel(UIPanelType panelType, PanelFrom panelFrom, params object[] args)
    {
        BasePanel prefab;
        data.PanelCacheDic.TryGetValue(panelType,out prefab);
        #region  获取内存中加载预设
        if (prefab == null)
        {
            ResourcesManager.Instance.LoadAssetsByKey<GameObject>(panelType.ToString(), (result) =>
            {
                BasePanel basePanel = result.GetComponent<BasePanel>();
                data.PanelCacheDic.Add(panelType, basePanel);
                LoadPanel(basePanel, panelFrom, args);
            });
        }
        #endregion
        else
        {
            LoadPanel(prefab, panelFrom, args);
        }


        void LoadPanel(BasePanel panelPrefab,PanelFrom panelFrom1, object[] args_)
        {
            BasePanel panel = GameObject.Instantiate(panelPrefab, GetFromParent(panelFrom), false);
            panel.transform.localScale = Vector3.one;
            data.CurPanels.Add(panel);
            panel.OnEnter(args_);
        }
    }
   
    /// <summary>
    ///  关闭UI界面
    /// </summary>
    /// <param name="name"></param>
    public void ClosePanel(UIPanelType panelType)
    {
        BasePanel panel = GetPanel(panelType);
        if(panel)
        {
            panel.OnExit();
            data.CurPanels.Remove(panel);
        }
        else
        {
            Debug.LogError("ClosePanel 找不到此panel");
        }
    }

    /// <summary>
    /// 根据PanelFrom枚举得到具体挂载父物体对象
    /// </summary>
    public Transform GetFromParent(PanelFrom panelFrom)
    {
        switch (panelFrom)
        {
            case PanelFrom.Normal: return view._Normal;
            case PanelFrom.TOP: return view._TOP;
            case PanelFrom.Message: return view._Message;
            case PanelFrom.Mask: return view._Mask;
            default: return null;
        }
    }

    /// <summary>
    ///  隐藏UI界面
    /// </summary>
    /// <param name="name"></param>
    public void HintPanel(UIPanelType panelType)
    {
        BasePanel panel = GetPanel(panelType);
        if (panel)
        {
            panel.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("HintPanel 找不到此panel");
        }

    }
    /// <summary>
    ///  打开隐藏的UI界面
    /// </summary>
    /// <param name="name"></param>
    public void ActivePanel(UIPanelType panelType)
    {
        BasePanel panel = GetPanel(panelType);
        if (panel)
        {
            panel.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("ActivePanel 找不到此panel");
        }
    }


    /// <summary>
    /// 获取当前正在Hierarchy面板的panel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public BasePanel GetPanel<T>()
    {
        foreach (BasePanel panel in data.CurPanels)
        {
            if(panel is T)
            {
                return panel;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取当前正在Hierarchy面板的panel
    /// </summary>
    public BasePanel GetPanel(UIPanelType panelType)
    {
        foreach (BasePanel panel in data.CurPanels)
        {
            if (panel.thisUIType == panelType)
            {
                return panel;
            }
        }
        return null;
    }

    /// <summary>
    /// 改变文字大小
    /// </summary>
    /// <param name="text"></param>
    /// <param name="Size"></param>
    /// <returns></returns>
    public string SetFont(string text, int Size)
    {
        
        string newText = "<Size=" + Size + ">" + text + "</Size>";

        return newText;

    }
    /// <summary>
    /// 改变文字颜色
    /// </summary>
    /// <param name="text"></param>
    /// <param name="colorCode"></param>
    /// <returns></returns>
    public string SetFont(string text, string colorCode)
    {
        
        string newText = "<color=" + colorCode + ">" + text + "</color>";
        return newText;
    }
    /// <summary>
    ///  改变文字大小和颜色
    /// </summary>
    /// <param name="text"></param>
    /// <param name="Size"></param>
    /// <param name="colorCode"></param>
    /// <returns></returns>
    public string SetFont(string text, int Size, string colorCode)
    {
      
        string newText = "<color=" + colorCode + ">" + "<Size=" + Size + ">" + text + "</Size>" + "</color>";

        return newText;
    }
    public void CreatHintBox(string text)
    {
        //气泡提示弹窗
        if (Resources.Load("Prefab/UI/UI_TextBox") == null)
        {
            Debug.LogError("Resources文件夹下没有UI_TextBox预制体");
            return;
        }
        GameObject tmp = Object.Instantiate(Resources.Load("Prefab/UI/UI_TextBox")) as GameObject;
        tmp.GetComponent<UI_TextBox>().textStr = text;
    }

    /// <summary>
    /// 播放序列帧动画
    /// </summary>
    /// <param name="sprites"></param>
    /// <param name="image"></param>
    /// <param name="OnceTime"></param>
    /// <returns></returns>
    public IEnumerator SequenceImage(Sprite[] sprites, Image image, float OnceTime)
    {
        
        for (int i = 0; i < sprites.Length; i++)
        {


            image.sprite = sprites[i];
            if (i == sprites.Length - 1)
                i = -1;
            yield return new WaitForSeconds(OnceTime);
        }
        yield return null;


    }
}
