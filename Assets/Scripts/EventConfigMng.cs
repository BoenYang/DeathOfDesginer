
using System.Collections.Generic;

public class EventConfigMng
{
    public static Dictionary<int, List<EventConfig>> EventDict = new Dictionary<int, List<EventConfig>>();

    static EventConfigMng()
    {
        Dictionary<int, EventConfig> events = ConfigMng.Instance.GetAllConfigs<EventConfig>();

        foreach (EventConfig e in events.Values)
        {
            if (!EventDict.ContainsKey(e.Type))
            {
                EventDict.Add(e.Type, new List<EventConfig>());
            }
            EventDict[e.Type].Add(e);
        }
    }
}

