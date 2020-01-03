using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Function : Single<Function>
{
    public void AddListenner()
    {
        MessageManager.Instance.Msg_C2S_Controller.AddListener(Protocol_C2S.OnRequestLogin, OnLogin);
    }
    public void RemoveListenner()
    {
        MessageManager.Instance.Msg_C2S_Controller.RemoveListener(Protocol_C2S.OnRequestLogin, OnLogin);
    }
    private void SendAllClient()
    {
        DataManager.Instance.allUserList.ForEach(temp => {
            temp.serverUser.Send(DataBaseManager.Instance.Inquiry("USERS",temp.ID)[1]);
        });
    }
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="event"></param>
    private void OnLogin(Event @event)
    {
        OnRequestLogin_C2S lg = @event.GetObj<OnRequestLogin_C2S>();
        lg.password = 0;//---
        List<string> list = DataBaseManager.Instance.Inquiry("USERS", lg.accountID);
        
        if (DataManager.Instance.isOnlined(lg.accountID))
        {
            Request request = new Request(Protocol_S2C.LoginUserOnline, "");
        }
        else
        if (list.Count<=1)
        {
            Request request = new Request(Protocol_S2C.LoginUsernameNotExist, "");
        }
        else
        {
            DataManager.Instance.CreatUser(lg.accountID, lg.password, Server.Instance.allUserList[Server.Instance.allUserList.Count - 1]);
            //成功
            //服务器收到请求，处理请求后发给客户端
            OnRequestLogin_Success_S2C protoCol_s2c = new OnRequestLogin_Success_S2C();
            protoCol_s2c.name = list[1];
            protoCol_s2c.heroCount = int.Parse(list[3]);
            protoCol_s2c.vipLevel = int.Parse(list[4]);
            protoCol_s2c.goldCount = int.Parse(list[5]);
            string json = JsonTool.Serialize(protoCol_s2c);
            Request request = new Request(Protocol_S2C.OnLogin_Success, json);
            MessageManager.Instance.Msg_S2C_Controller.PushEvent(request);
        }

       
    }  
    
}
