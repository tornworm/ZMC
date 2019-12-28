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
}


