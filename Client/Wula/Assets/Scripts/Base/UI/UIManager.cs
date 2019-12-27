using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager {

    private volatile static UIManager singleton;
    public static UIManager Singleton
    {
        get { return singleton ?? (singleton = new UIManager()); }
    }
    private UIManager() { }
    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <param name="name"></param>
    public void OpenWindow(string name)
    {
              
        if (GameObject.Find(name) != null)
        {
            return;

        }
        GameObject tmp = Object.Instantiate(Resources.Load("Prefabs/UI/" + name)) as GameObject;
    }
    /// <summary>
    /// 打开UI界面,可关闭其他界面
    /// </summary>
    /// <param name="name"></param>
    /// <param name="closeAll"></param>
    public void OpenWindow(string name, bool closeAll)
    {
     
        if (!closeAll)
            return;

        else
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("UI"))
            {
                Object.Destroy(item);
            }
        }
        if (GameObject.Find(name) != null)
            return;
        GameObject tmp = Object.Instantiate(Resources.Load("Prefabs/UI/" + name)) as GameObject;
    }
    /// <summary>
    ///  关闭UI界面
    /// </summary>
    /// <param name="name"></param>
    public void CloseWindow(string name)
    {
       
        if (GameObject.Find(name + "(Clone)") == null)
            return;
        Object.Destroy(GameObject.Find(name + "(Clone)"));
    }
    /// <summary>
    ///  关闭当前UI界面
    /// </summary>
    /// <param name="name"></param>
    public void CloseSelf(string name)
    {
       
        if (GameObject.Find(name) == null)
            return;
        Object.Destroy(GameObject.Find(name));
    }
    /// <summary>
    ///  隐藏UI界面
    /// </summary>
    /// <param name="name"></param>
    public void HintWindow(string name)
    {

     
        if (GameObject.Find(name + "(Clone)") == null)
            return;
        GameObject.Find(name + "(Clone)").SetActive(false);
    }
    /// <summary>
    ///  打开隐藏的UI界面
    /// </summary>
    /// <param name="name"></param>
    public void ShowWindow(string name)
    {
       

        if (GameObject.Find(name + "(Clone)") == null)
            return;
        GameObject.Find(name + "(Clone)").SetActive(true);
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
    /// 得到品质图片
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public Sprite GetLevelImage(int level)
    {
        
        return Resources.Load<Sprite>("Textures/UI/common/item_color_0" + level);
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
