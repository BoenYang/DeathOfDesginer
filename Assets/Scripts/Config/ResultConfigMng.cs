
using System.Collections.Generic;

public class ResultConfigMng
{

    public static Dictionary<int,Dictionary<int, List<ResultConfig>>> ResultTypeDict = new Dictionary<int, Dictionary<int, List<ResultConfig>>>();

    static ResultConfigMng()
    {
        Dictionary<int, ResultConfig> results = ConfigMng.Instance.GetAllConfigs<ResultConfig>();

        foreach (ResultConfig e in results.Values)
        {
            if (!ResultTypeDict.ContainsKey(e.Type))
            {
                ResultTypeDict.Add(e.Type, new Dictionary<int, List<ResultConfig>>());

                if (!ResultTypeDict[e.Type].ContainsKey(e.Finish))
                {
                    ResultTypeDict[e.Type].Add(e.Finish, new List<ResultConfig>());
                }
                ResultTypeDict[e.Type][e.Finish].Add(e);
            }
            else
            {
                if (!ResultTypeDict[e.Type].ContainsKey(e.Finish))
                {
                    ResultTypeDict[e.Type].Add(e.Finish, new List<ResultConfig>());
                }
                ResultTypeDict[e.Type][e.Finish].Add(e);
            }
        }
    }

}
