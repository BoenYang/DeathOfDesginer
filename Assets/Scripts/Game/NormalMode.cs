using System.Collections.Generic;
using UnityEngine;

public class NormalMode : GameModeBase
{
    private int[] HealthValue;

    private Dictionary<int,List<EventConfig>> eventDict = new Dictionary<int, List<EventConfig>>();

    private EventConfig firstEvent;

    public override string Mode
    {
        get { return "Normal"; }
    }

    public override void Init()
    {
        HealthValue = new int[4];
        for (int i = 0; i < HealthValue.Length; i++)
        {
            HealthValue[i] = 100;
        }

        Dictionary<int,EventConfig> events = ConfigMng.Instance.GetAllConfigs<EventConfig>();

        foreach (EventConfig e in events.Values)
        {
            if (!eventDict.ContainsKey(e.Type))
            {
                eventDict.Add(e.Type, new List<EventConfig>());
            }
            eventDict[e.Type].Add(e);
        }

        int randIndex = Random.Range(0, eventDict[1].Count);
        firstEvent = eventDict[1][randIndex];

        UIManager.OpenPanel("GameView",false,HealthValue,firstEvent);
    }

}
