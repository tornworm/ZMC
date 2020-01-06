using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class ClientManager : BaseManager<ClientManager>
{
    static byte[] dataBuffer = new byte[1024];
    Socket clientSocket;

    public override IEnumerator OnAwake()
    {
        yield return StartCoroutine(base.OnAwake());

        //初始化sokect,选择tcp
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //服务器地址
        //IPAddress ipAddress = IPAddress.Parse("47.92.205.66");
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        //服务器应用程序端口号
        IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 8888);
        //绑定IP和端口号
        clientSocket.Connect(iPEndPoint);

        //byte[] data = new byte[1024];
        //int count = clientSocket.Receive(data);
        //string msg = System.Text.Encoding.UTF8.GetString(data, 0, count);
        //Debug.Log("收到的消息是：" + msg);
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, clientSocket);
        yield break;
    }


    static void ReceiveCallback(IAsyncResult ar)
    {
        Socket clientSocket = ar.AsyncState as Socket;
        int count = clientSocket.EndReceive(ar);
        //服务器给你发送的消息
        string data = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count);

        Debug.LogWarning("data" + data);
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
        clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(data));
    }


    public static void AnalysisAndSendClient(string data)
    {
        // 解析响应数据 返回响应数据包
        ServerEvent @event = AnalysisString(data);
        // 将解析好的数据包放入消息队列里
        MessageManager.Instance.Msg_S2C_Controller.PushEvent(@event);
    }

    /// <summary>
    /// 解析响应数据 返回响应封装类
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static ServerEvent AnalysisString(string data)
    {
        string[] dataArr = data.Split('&');
        int eventID = int.Parse(dataArr[0]);
        string content = dataArr[1];
        ServerEvent @event = new ServerEvent(eventID, content);
        if ('1' == dataArr[0][1])  // json
        {
            @event.analysisType = ServerEvent.AnalysisType.JsonString;
        }
        if ('0' == dataArr[0][1])  // 字符串
        {
            @event.analysisType = ServerEvent.AnalysisType.NorString;
        }
        return @event;

    }
}
