using Assets.Scripts.Managers;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PanelLogin : BasePanel
{
    public Button loginBtn;
    public Button rigsterBtn;
    public InputField inputID;
    public InputField password;
    private readonly string strID = @"/^[1-9]{1}[0-9]{4,11}$/";
    private readonly string strPw = @"/^[0-9a-zA-Z]{6,12}$/";
    public override void OnEnter(object[] args)
    {
        base.OnEnter(args);
        MessageManager.Instance.Msg_S2C_Controller.AddListener(Protocol_S2C.OnRequestRegister_Success, RegisterSuccess);
        MessageManager.Instance.Msg_S2C_Controller.AddListener(Protocol_S2C.OnLogin_Success, OnLoginSuccess);
        rigsterBtn.onClick.AddListener(OnClickBtnRegister);
        loginBtn.onClick.AddListener(signIn);
        inputID.onValueChanged.AddListener((msg) =>
        {
            Regex regex = new Regex(strID);
            if (regex.IsMatch(msg))
            {
                UIManager.Instance.CreatHintBox("账号只能为数字");
            }
        });
        password.onValueChanged.AddListener(msg =>
        {
            Regex regex = new Regex(strPw);
            if (regex.IsMatch(msg))
            {
                UIManager.Instance.CreatHintBox("密码只能为数字或字母");
            }
        });
    }

    /// <summary>
    /// 注册
    /// </summary>
    private void OnClickBtnRegister()
    {
       
        if (inputID.text==""&& password.text==""&& password.text=="")
        {
            UIManager.Instance.CreatHintBox("账号密码不能为空!!!!");
            return;
        }      
        AdimBase adimBase = new AdimBase();
        adimBase.accountID = inputID.text;
        adimBase.password = password.text;
        string json = JsonTool.Serialize(adimBase);
        Request request = new Request(Protocol_C2S.OnRequestRegister, json);
        NetworkManager.Instance.Send(request);
    }
    private void RegisterSuccess(ServerEvent @event)
    {
        //0 注册失败 1 成功
        AdimBase adimBase = @event.GetObj<AdimBase>();
        if (adimBase.password=="0")
        {
            UIManager.Instance.CreatHintBox("账号已存在!!!!");
        }
        else if (adimBase.password=="1")
        {
            PlayerData.Instance.UserID = int.Parse(adimBase.accountID);
            GameSceneManager.Instance.LoadSceneShowLoadingAsync("Main", () => UIManager.Instance.ShowPanel(UIPanelType.PanelMain, PanelFrom.Normal));
            UIManager.Instance.ClosePanel(UIPanelType.PanelLogin);
            UIManager.Instance.CreatHintBox("注册成功!!!");
        }
    }
    private void signIn()
    {

        //如果账号规格不符
        if (inputID.text == "" && password.text == "" && password.text == "")
        {
            UIManager.Instance.CreatHintBox("账号密码不能为空!!!!");
            return;
        }
        // 客户端发送登陆请求
        AdimBase adimBase = new AdimBase();
        adimBase.accountID = inputID.text;
        adimBase.password = password.text;
        string json = JsonTool.Serialize(adimBase);
        Request request = new Request(Protocol_C2S.OnRequestLogin, json);
        NetworkManager.Instance.Send(request);
    }


    void OnLoginSuccess(ServerEvent @event)
    {
        AdimBase adimBase = @event.GetObj<AdimBase>();
        if (adimBase.password==Protocol_S2C.LoginUserOnline.ToString())
        {
            UIManager.Instance.CreatHintBox("账号已在线!!!");
            return;
        }
        else if (adimBase.password== Protocol_S2C.LoginUsernameNotExist.ToString())
        {
            UIManager.Instance.CreatHintBox("账号不存在，请重新注册!!!");
            return;
        }
        else if (adimBase.password == Protocol_S2C.LoginUsernameOrPasswordWrong.ToString())
        {
            UIManager.Instance.CreatHintBox("密码错误!!!");
            return;
        }
        PlayerData.Instance.UserID = int.Parse(adimBase.accountID);
        GameSceneManager.Instance.LoadSceneShowLoadingAsync("Main", () => UIManager.Instance.ShowPanel(UIPanelType.PanelMain, PanelFrom.Normal));
        UIManager.Instance.ClosePanel(UIPanelType.PanelLogin);
        UIManager.Instance.CreatHintBox("登陆成功!!!");
    }

    public override void OnExit()
    {
        base.OnExit();
        MessageManager.Instance.Msg_S2C_Controller.RemoveListener(Protocol_S2C.OnLogin_Success, RegisterSuccess);
        MessageManager.Instance.Msg_S2C_Controller.RemoveListener(Protocol_S2C.OnLogin_Success, OnLoginSuccess);
        Destroy(gameObject);
    }
}
