using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultView : UIBase
{

    public Text Result;

    public Text GameTime;

    public Button RestartBtn;

    public override void OnInit()
    {
        RestartBtn.onClick.AddListener(OnRestartClick);
    }

    public override void OnRefresh()
    {
        int[] healthVal = (int[])Args[0];
        int turnCount = (int)Args[1];

        int zeroIndex = 1;
        int full = 0;
        for (int i = 0; i < healthVal.Length; i++)
        {

            if (healthVal[i] < 0 || healthVal[i] >= 100)
            {
                full = healthVal[i] >= 100 ? 1 : 0;
                zeroIndex = i + 1;
                break;
            }
        }

        int randIndex = Random.Range(0, ResultConfigMng.ResultTypeDict[zeroIndex][full].Count);
        ResultConfig resultConfig = ResultConfigMng.ResultTypeDict[zeroIndex][full][randIndex];

        Result.text = resultConfig.Describe;
        GameTime.text = "你坚持了" + turnCount + "个月";
    }

    private void OnRestartClick()
    {
        ClosePanel();
        GameStart.Game.RestartGame();   
    }
}
