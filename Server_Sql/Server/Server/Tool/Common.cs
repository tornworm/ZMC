using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

public class Debug
{
    public static void log(string str)
    {
        Console.WriteLine(str);
    }
}
public static class JsonTool
{
    public static string Serialize(System.Object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T Deserialize<T>(string jsonStr)
    {
        return JsonConvert.DeserializeObject<T>(jsonStr);
    }
    
}
public class Common 
{
   
}

/// <summary>
/// [0] ? s2c : c2s
/// [1] ? json : norString
/// </summary>
public enum Protocol_C2S
{
    OnRequestLogin = 0110001,  
}

public enum Protocol_S2C
{
    OnLogin_Success = 1110001,
    LoginUserOnline = 10001,//账号正在游戏中
    LoginUsernameNotExist = 10002,//账号不存在
    LoginUsernameOrPasswordWrong = 10003,//账号或密码错误
}


