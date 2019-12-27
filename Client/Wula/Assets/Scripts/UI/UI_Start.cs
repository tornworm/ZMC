using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Start : UIBehaviour
{

    private Button randomBtn;
    private Button startBtn;
    private InputField account;

    public override void Start()
    {
        
         randomBtn =transform.Find("bg/RandomBtn").GetComponent<Button>();
         startBtn = transform.Find("bg/StartBtn").GetComponent<Button>();
         account = transform.Find("bg/account").GetComponent<InputField>();
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
            UIManager.Singleton.CreatHintBox("账号格式不对!");
            return;
        }
        //发送10001号协议
        Event @event = new Event();
        @event.EventID = 10001;
        @event.EventParamObj = int.Parse(account.text);
        SendController.Singleton.PushEvent(@event);
        //跳转加载场景
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
 
}
