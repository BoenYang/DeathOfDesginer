﻿using System.Collections.Generic;
using UnityEngine;

public class NormalMode : GameModeBase
{
    private int[] HealthValue;

    private EventConfig firstEvent;

    private int turnCount;

    public override string Mode
    {
        get { return "Normal"; }
    }

    public override void Init()
    {
        HealthValue = new int[4];
        for (int i = 0; i < HealthValue.Length; i++)
        {
            HealthValue[i] = 50;
        }

        turnCount = 0;
        int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
        firstEvent = EventConfigMng.EventDict[1][randIndex];
        UIManager.OpenPanel("GameView",false,HealthValue,firstEvent,turnCount);
    }

    public override void RestartGame()
    {
        base.RestartGame();
        for (int i = 0; i < HealthValue.Length; i++)
        {
            HealthValue[i] = 50;
        }
        turnCount = 0;
        int randIndex = Random.Range(0, EventConfigMng.EventDict[1].Count);
        firstEvent = EventConfigMng.EventDict[1][randIndex];
        UIManager.DispatchMsg("RestartGame",HealthValue,firstEvent,turnCount);
    }

}
