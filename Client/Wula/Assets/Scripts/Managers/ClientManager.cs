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
        IPAddress ipAddress = IPAddress.Parse("192.168.199.193");
        //服务器应用程序端口号
        IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 2345);
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
        //显示服务器给你发送的消息
       Debug.Log(System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count));

        //回调该方法
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, clientSocket);
    }

    // data:协议号&协议 用&分割
    public void Send(string data)
    {
        clientSocket.Send(System.Text.Encoding.UTF8.GetBytes(data));
    }
}
