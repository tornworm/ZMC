using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonTool
{
    public static string Serialize(System.Object obj)
    {
        return  JsonConvert.SerializeObject(obj);
    }

    public static T Deserialize<T>(string jsonStr)
    {
        return JsonConvert.DeserializeObject<T>(jsonStr);
    }
}
