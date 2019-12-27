using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

public class Clien : MonoBehaviour
{
    static byte[] dataBuffer = new byte[1024];
    Socket clientSocket;
    // Start is called before the first frame update
    void Start()
    {
        //初始化sokect,选择tcp
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //服务器地址
        IPAddress ipAddress = IPAddress.Parse("192.168.1.100");
        //服务器应用程序端口号
        IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, 2345);
        //绑定IP和端口号
        clientSocket.Connect(iPEndPoint);


        //byte[] data = new byte[1024];
        //int count = clientSocket.Receive(data);
        //string msg = System.Text.Encoding.UTF8.GetString(data, 0, count);
        //Debug.Log("收到的消息是：" + msg);
        clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallback, clientSocket);


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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            clientSocket.Send(System.Text.Encoding.UTF8.GetBytes("客户端给你发了一条消息"));
        }
    }
}
