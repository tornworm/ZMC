
public class Event
{
    public Event() { }

    public Event(int EventID, object EventObj, object EventParamObj)
    {
        this.EventID = EventID;
        this.EventObj = EventObj;
        this.EventParamObj = EventParamObj;
    }

    public int EventID { get; set; }
    public object EventObj { get; set; }
    public object EventParamObj { get; set; }
}
