using UnityEngine;

public class NormalMode : GameModeBase
{
    private int[] healthValue;

    private EventConfig currentEventConfig;

    private readonly EventConfig[] nextEventConfigs = new EventConfig[2];

    private int turnCount;

    public override string Mode
    {
        get { return "Normal"; }
    }

    public override void Init()
    {
        Debug.Log("init game");

        healthValue = new int[4];
        for (int i = 0; i < healthValue.Length; i++)
        {
            healthValue[i] = 50;
        }

        turnCount = 0;
        int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
        currentEventConfig = EventConfigMng.EventDict[1][randIndex];
        UIManager.OpenPanel("GameView",false,healthValue,currentEventConfig,turnCount);
    }

    public override void RestartGame()
    {
        base.RestartGame();
        for (int i = 0; i < healthValue.Length; i++)
        {
            healthValue[i] = 50;
        }
        turnCount = 0;
        int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
        currentEventConfig = EventConfigMng.EventDict[1][randIndex];
        UIManager.DispatchMsg("RestartGame",healthValue,currentEventConfig,turnCount);
    }

    public void CheckGameOver()
    {
        for (int i = 0; i < healthValue.Length; i++)
        {
            if (healthValue[i] <= 0 || healthValue[i] >= 100)
            {
                GameStart.Game.GameOver();
                UIManager.OpenPanel("ResultView", false, healthValue, turnCount);
                return;
            }
        }
    }

    public void NextTurn()
    {
        turnCount++;
    }

    public EventConfig ChooseEvent(int dir)
    {
        int nextEventIndex = 0;

        if (dir < 0)
        {
            nextEventIndex = 0;
        }
        else
        {
            nextEventIndex = 1;
        }

        if (nextEventIndex == 0)
        {
            for (int i = 0; i < currentEventConfig.ChoiceOneSorce.Length; i++)
            {
                healthValue[i] += currentEventConfig.ChoiceOneSorce[i];
            }
        }
        else
        {
            for (int i = 0; i < currentEventConfig.ChoiceTwoSorce.Length; i++)
            {
                healthValue[i] += currentEventConfig.ChoiceTwoSorce[i];
            }
        }

        currentEventConfig = nextEventConfigs[nextEventIndex];

        turnCount++;

        return currentEventConfig;
    }

    public void UpdateNextEventConfig()
    {
        if (currentEventConfig.Needchoice == 1)
        {

            if (currentEventConfig.ChoiceOneid[0] != -1)
            {
                nextEventConfigs[0] = EventConfig.Get(currentEventConfig.ChoiceOneid[0]);
            }
            else
            {
                int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
                nextEventConfigs[0] = EventConfigMng.EventDict[1][randIndex];
            }

            if (currentEventConfig.ChoiceTwoid[0] != -1)
            {
                nextEventConfigs[1] = EventConfig.Get(currentEventConfig.ChoiceTwoid[0]);
            }
            else
            {
                int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
                nextEventConfigs[1] = EventConfigMng.EventDict[1][randIndex];
            }
        }
        else
        {

            if (currentEventConfig.ChoiceOneid[0] != -1)
            {
                nextEventConfigs[0] = nextEventConfigs[1] = EventConfig.Get(currentEventConfig.ChoiceOneid[0]);
            }
            else
            {
                int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
                nextEventConfigs[0] = nextEventConfigs[1] = EventConfigMng.EventDict[1][randIndex];
            }
        }
    }

}
