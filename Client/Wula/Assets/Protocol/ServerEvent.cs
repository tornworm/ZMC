using Newtonsoft.Json;


public class ServerEvent
{
    public enum AnalysisType
    {
        NorString, JsonString
    }



    public ServerEvent() { }


    public ServerEvent(int EventID, string Data)
    {
        this.EventID = EventID;
        this.Data = Data;
    }

    public int EventID { get; set; }
    public string Data { get; set; }
    public AnalysisType analysisType { get; set; }


    /// <summary>
    /// 获取协议对象 方法自动判断序列化的是普通字符串还是Json并返回协议对象或字符串数组 使用时泛型一定要搞对
    /// </summary>
    public T GetObj<T>() where T : class
    {
        if (analysisType == AnalysisType.JsonString)
            return JsonConvert.DeserializeObject<T>(Data);
        if (analysisType == AnalysisType.NorString)
            return Data.Split('|') as T;

        return null;
    }
}
