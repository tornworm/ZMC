using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRequestLogin
{
    public int accountID;
}

public class UIBehaviour:MonoBehaviour,CtoC_EventListener,CtoS_EventListener,StoC_EventListener
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        MessageManager.Instance.Msg_S2C_Controller.AddListener(Protocol_S2C.OnLogin_Success, OnStartGame);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            MessageManager.Instance.Msg_S2C_Controller.PushEvent(new Event(1, "OnStartGame_s2c", null));
        if(Input.GetKeyDown(KeyCode.S))
        {
            OnRequestLogin obj = new OnRequestLogin();
            obj.accountID = 99999;
            Request request = new Request(Protocol_C2S.OnRequestLogin, JsonTool.Serialize(obj));
            NetworkManager.Instance.Send(request);
        }
            
    }
    public virtual void Init()
    {

    }
    public virtual void Enter()
    {

    }
    public virtual void Response()
    {

    }
    public virtual void OnDestroy()
    {

    }

    

    public void OnStartGame(Event arg)
    {
        Debug.LogWarning(arg.EventObj as string); 
    }

    public void OnEventTrigger(Event _Event)
    {
        throw new System.NotImplementedException();
    }
}
