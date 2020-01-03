﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PanelLogin : BasePanel
{

    public Button randomBtn;
    public Button startBtn;
    public InputField account;

    public override void OnEnter(object[] args)
    {
        base.OnEnter(args);
        MessageManager.Instance.Msg_S2C_Controller.AddListener(Protocol_S2C.OnLogin_Success, OnLoginSuccess);
        randomBtn.onClick.AddListener(random);
        startBtn.onClick.AddListener(signIn);
    }

    /// <summary>
    /// 随机账号
    /// </summary>
    private void random()
    {
        account.text = Random.Range(10000000, 99999999).ToString();
       
    }
    
    private void signIn()
    {
        
        //如果账号规格不符
        if (account.text.Length != 8)
        {
            UIManager.Instance.CreatHintBox("账号格式不对!");
            return;
        }
        // 客户端发送登陆请求
        OnRequestLogin_C2S protoCol_c2s = new OnRequestLogin_C2S();
        protoCol_c2s.accountID = int.Parse(account.text);
        Request request = new Request(Protocol_C2S.OnRequestLogin, protoCol_c2s);
        NetworkManager.Instance.Send(request);

        //服务器收到请求，处理请求后发给客户端
        OnRequestLogin_Success_S2C protoCol_s2c = new OnRequestLogin_Success_S2C();
        protoCol_s2c.name = "橐文";
        protoCol_s2c.heroCount = 10;
        protoCol_s2c.vipLevel = 1;
        protoCol_s2c.goldCount = 99999;
        string json = JsonTool.Serialize(protoCol_s2c);
        string response = (int)Protocol_S2C.OnLogin_Success+"&" + json;
        ClientManager.AnalysisAndSendClient(response);
    }

    void OnLoginSuccess(Event @event)
    {
        OnRequestLogin_Success_S2C protoCol = @event.GetObj<OnRequestLogin_Success_S2C>();
        Debug.LogWarning("    name:" + protoCol.name + "   goldCount:" + protoCol.goldCount + "    heroCount:" + protoCol.heroCount + "    vipLevel:" + protoCol.vipLevel);
        GameSceneManager.Instance.LoadSceneShowLoadingAsync("Main", () => UIManager.Instance.ShowPanel(UIPanelType.PanelMain, PanelFrom.Normal));
        UIManager.Instance.ClosePanel(UIPanelType.PanelLogin);
        UIManager.Instance.CreatHintBox("登陆成功!!!");
    }

    public override void OnExit()
    {
        base.OnExit();
        Destroy(gameObject);
    }
}