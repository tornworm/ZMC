using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class Server : BaseController<Server>
{
    public List<ServerUser> allUserList = new List<ServerUser>();  
    public Socket socket;
    private static Server _instance;

    public static Server Instance {
        get {
            if (_instance == null)
            {
                _instance = new Server();
            }
            return _instance;
        }
    }
    public void Init()
    {
        MessageManager.Instance.OnInit();
        Function.Instance.AddListenner();
        InitSever();  
    }
   
    private void InitSever()
    {
        //TCP 初始化
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //服务器地址
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        //服务器应用程序端口号
        IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 2345);
        //绑定IP和端口号     
        serverSocket.Bind(iPEndPoint);
        serverSocket.Listen(100);
        while (true)
        {
            Debug.log(allUserList.Count+"dd");
            socket = serverSocket.Accept();
            ServerUser uc = new ServerUser(socket);
            allUserList.Add(uc);
            Debug.log("新用户");
        }          

    }   
}

public class ServerUser
{

    public Socket socket;
    static byte[] dataBuffer = new byte[1024];    
    Thread thread;
    public ServerUser() { }
    public ServerUser(Socket socket)
    {     
        MessageManager.Instance.Msg_S2C_Controller.InitServerUser(this);
        this.socket = socket;
        this.socket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, this.socket);
        thread = new Thread(OnUpData);
        thread.Start();
    }
    private void OnUpData()
    {
        while (true)
        {
            MessageManager.Instance.OnUpdate();
            Thread.Sleep(10);
        }
    }
    void ReceiveCallback(IAsyncResult ar)
    {

        Socket clientSocket = ar.AsyncState as Socket;

        int count = clientSocket.EndReceive(ar);

        //服务器给你发送的消息
        string data = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count);
        //客户端解析服务器响应数据，并发送到主线程消息队列
        AnalysisAndSendClient(data);
        //回调重新监听该方法
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, clientSocket);
    }
    /// <summary>
    /// 向服务器发送请求
    /// </summary>
    // data:协议号&协议数据 用&分割
    public void Send(string data)
    {
        socket.Send(Encoding.UTF8.GetBytes(data));
    }

    public static void AnalysisAndSendClient(string data)
    {
        // 解析响应数据 返回响应数据包
        Event @event = AnalysisString(data);

        // 将解析好的数据包放入消息队列里
        MessageManager.Instance.Msg_C2S_Controller.PushEvent(@event);
    }

    /// <summary>
    /// 解析响应数据 返回响应封装类
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static Event AnalysisString(string data)
    {

        string[] dataArr = data.Split('&');
        int eventID = int.Parse(dataArr[0]);
        string content = dataArr[1];
        Event @event = new Event(eventID, content);
        if ('1' == dataArr[0][1])  // json
        {
            @event.analysisType = Event.AnalysisType.JsonString;
        }
        if ('0' == dataArr[0][1])  // 字符串
        {
            @event.analysisType = Event.AnalysisType.NorString;
        }
        return @event;
    }

    public void Close()
    {       
        thread.Abort();
        socket.Close();
    }
}
