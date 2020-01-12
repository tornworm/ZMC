using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreatActor : BasePanel
{
    public Button creatActorBtn;
   
    public InputField inputNameB,inputNameG;
    public Transform content;
    public GameObject creatActorPanel;
    public Button boy, girl;

     public override void OnEnter(object[] args)
    {
        base.OnEnter(args);
        MessageManager.Instance.Msg_S2C_Controller.AddListener(Protocol_S2C.CreatActor, CreatActorSever);
        MessageManager.Instance.Msg_S2C_Controller.AddListener(Protocol_S2C.IsHaveActor, IsHaveActorServer);
        isHaveActor();

        creatActorBtn.gameObject.SetActive(true);
        creatActorPanel.SetActive(false);
        if (content.childCount>3)
        {
            creatActorBtn.gameObject.SetActive(false);
        }     
        ;
        creatActorBtn.onClick.AddListener(OnClickBtnCreatActor);
        boy.onClick.AddListener(CreatBoy);
        boy.onClick.AddListener(CreatGirl);
    }

    private void isHaveActor()
    {
        Actor actor = new Actor();
        actor.accountID = PlayerData.Instance.UserID;
        string json = JsonTool.Serialize(actor);
        Request request = new Request(Protocol_C2S.IsHaveActor, json);
        NetworkManager.Instance.Send(request);
    }
    private void IsHaveActorServer(ServerEvent @event)
    {
        List<Actor> actors = @event.GetObj<List<Actor>>();
        if (actors.Count==0)
        {
            creatActorBtn.gameObject.SetActive(true);
            return;
        }
        GameObject go = Resources.Load<GameObject>("Actor/actor");
        for (int i = 0; i < actors.Count; i++)
        {
           
            GameObject ins = Instantiate(go, content);
            ins.GetComponent<ActorItme>().Init(actors[i],ins);
        }
    }
    private void OnClickBtnCreatActor()
    {
        content.gameObject.SetActive(false);
        creatActorPanel.gameObject.SetActive(true);
        creatActorBtn.gameObject.SetActive(false);      
    }
    private void CreatBoy()
    {
        if (inputNameB.text == ""|| inputNameB.text.Length>8)
        {
            UIManager.Instance.CreatHintBox("请输入正确的名字");
            return;
        }
        Actor actor = new Actor();
        actor.accountID = PlayerData.Instance.UserID;
        actor.name = inputNameB.text;
        actor.roleID = 1;
        actor.lv = 0;
        actor.gender = "男";
        string json = JsonTool.Serialize(actor);
        Request request = new Request(Protocol_C2S.CreatActor, json);
        NetworkManager.Instance.Send(request);
    }
    private void CreatGirl()
    {
        if (inputNameG.text == null || inputNameG.text.Length > 8)
        {
            UIManager.Instance.CreatHintBox("请输入正确的名字");
            return;
        }
        Actor actor = new Actor();
        actor.accountID = PlayerData.Instance.UserID;
        actor.name = inputNameG.text;
        actor.roleID = 1;
        actor.lv = 0;
        actor.gender = "女";
        string json = JsonTool.Serialize(actor);
        Request request = new Request(Protocol_C2S.CreatActor, json);
        NetworkManager.Instance.Send(request);
    }
    private void CreatActorSever(ServerEvent @event)
    {
        Actor actor = @event.GetObj<Actor>();
        if (actor.name==Protocol_C2S.CreatActor.ToString())
        {
            UIManager.Instance.CreatHintBox("已存在此名字");
            return;
        }
        PlayerData.Instance.Name = actor.name;
        PlayerData.Instance.gender = actor.gender;
        PlayerData.Instance.Lv = actor.lv;
        PlayerData.Instance.roleId = actor.roleID;
        GameSceneManager.Instance.LoadSceneShowLoadingAsync("Main", () => UIManager.Instance.ShowPanel(UIPanelType.PanelMain, PanelFrom.Normal));
        UIManager.Instance.ClosePanel(UIPanelType.PanelActor);
        
    }
    public override void OnExit()
    {
        base.OnExit();
        MessageManager.Instance.Msg_S2C_Controller.RemoveListener(Protocol_S2C.CreatActor, CreatActorSever);
        Destroy(gameObject);
    }

   
}
