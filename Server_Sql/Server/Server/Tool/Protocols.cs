using System.Collections;
using System.Collections.Generic;



#region 登录模块
public class OnRequestLogin_C2S
{
    public int accountID;
    public int password;
}

public class OnRequestLogin_Success_S2C
{
    public int heroCount;
    public int goldCount;
    public string name;
    public int vipLevel;
} 
#endregion