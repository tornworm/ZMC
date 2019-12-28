using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnRequestLogin_C2S
{
    public int accountID;
}

public class OnRequestLogin_Success_S2C
{
    public int heroCount;
    public int goldCount;
    public string name;
    public int vipLevel;
}