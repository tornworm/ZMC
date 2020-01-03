using System;

public class MessageManager : BaseController<MessageManager>
{
    private static MessageManager _instance;

    public static MessageManager Instance {
        get {
            if (_instance == null)
            {
                _instance = new MessageManager();
            }
            return _instance;
        }
    }
    public Msg_C2S_Controller Msg_C2S_Controller;
    public Msg_S2C_Controller Msg_S2C_Controller;


    public override void OnInit()
    {
        base.OnInit();
        Msg_C2S_Controller = new Msg_C2S_Controller();
        Msg_S2C_Controller = new Msg_S2C_Controller();
        Msg_C2S_Controller?.OnInit();
        Msg_S2C_Controller?.OnInit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Msg_C2S_Controller?.OnUpdate();
        Msg_S2C_Controller?.OnUpdate();
    }
}

