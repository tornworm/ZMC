using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/// <summary>
/// [0] ? c2s : s2c
/// [1] ? norString : json
/// </summary>
public enum Protocol_C2S
{
    OnRequestLogin = 0110001,
    OnRequestRegister = 0110002,
    CreatActor = 0110006,
    IsHaveActor = 0110007, 
    OnClientExit = 0099999,
}

public enum Protocol_S2C
{
    OnLogin_Success = 1110001,
    LoginUserOnline = 1110002,//账号正在游戏中
    LoginUsernameNotExist = 1110003,//账号不存在
    LoginUsernameOrPasswordWrong = 1110004,//密码错误
    OnRequestRegister_Success = 1110005, //注册
    CreatActor = 1110006,// 创建角色
    IsHaveActor=1110007, //是否存在角色
}



