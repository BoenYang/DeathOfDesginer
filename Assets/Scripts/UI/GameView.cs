using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameView : UIBase
{
    public EventImage CurrentEvent;

    public List<EventImage> NextEvents;

    public List<Image> Bars;

    public List<Image> ChangeIndicator; 

    public Text ChooseDesc;

    public Text RoleName;

    public float EnsureDistance = 20;

    public float MaxRotAngle = 40;

    private Vector3 originPos;

    private bool isAnimating = false;

    private float xMoveDistance;

    private Sequence seq = null;

    public EventConfig currentEventConfig;

    private EventConfig[] nextEventConfigs;

    private int[] healthValue;

    private Vector2 startPos;

    private Dictionary<int, string> roleNameDict = new Dictionary<int, string>
    {
        {1,"老板"},
        {2,"记着"},
        {3,"程序"},
        {4,"美术"},
        {5,"其他策划"},
        {6,"玩家"},
    }; 

    public override void OnInit()
    {
        Debug.Log("Game View show");
        CurrentEvent.Init();
        for (int i = 0; i < NextEvents.Count; i++)
        {
            NextEvents[i].Init();
        }

        originPos = CurrentEvent.rectTransform.localPosition;
        AddMsgListener("RestartGame", OnRestartGame);
    }

    public override void OnRefresh()
    {
        healthValue = (int[])Args[0];
        currentEventConfig = Args[1] as EventConfig;

        for (int i = 0; i < healthValue.Length; i++)
        {
            Bars[i].fillAmount = healthValue[i]/100f;
        }

        CurrentEvent.SetEvent(currentEventConfig);

        ChooseDesc.text = currentEventConfig.Event;
        RoleName.text = roleNameDict[currentEventConfig.Hero];

        ChangeIndicator.ForEach((i)=>i.transform.localScale = Vector3.zero);

        nextEventConfigs = GameStart.Game.UpdateNextEventConfig();
        SetNextEventImage();
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        
        xMoveDistance = 0;
        if (isAnimating)
        {
            return;
        }
        PointerEventData pointData = eventData as PointerEventData;
        startPos = pointData.position;
    }

    public void OnEndDrag(BaseEventData eventData)
    {
 
        if (isAnimating)
        {
            return;
        }

       

        if (Mathf.Abs(xMoveDistance) > EnsureDistance)
        {
            NextEvent();
        }
        else
        {
            ChangeIndicator.ForEach((i) => i.transform.localScale = Vector3.zero);
            isAnimating = true;
            DisableTouch();

            CurrentEvent.HideChooseText();

            seq = DOTween.Sequence();
            seq.Insert(0, CurrentEvent.rectTransform.DOLocalMove(originPos, 1.0f));
            seq.Insert(0, CurrentEvent.transform.DORotate(Vector3.zero, 1.0f));
            seq.OnComplete(() =>
            {
                isAnimating = false;
                EnableTouch();
            });

            seq.Play();
        }
    }

    public void OnDrag(BaseEventData eventData)
    {
        if (isAnimating)
        {
            return;
        }

        PointerEventData pointData = eventData as PointerEventData;
        xMoveDistance = pointData.position.x - startPos.x;
        float angleRatio = -xMoveDistance / EnsureDistance;

        Vector2 pos = originPos + new Vector3(xMoveDistance*0.5f, 0,0);
        CurrentEvent.rectTransform.localPosition = pos;
        CurrentEvent.rectTransform.eulerAngles = new Vector3(0,0,MaxRotAngle * angleRatio);

        if (xMoveDistance < 0)
        {
            CurrentEvent.ChooseOne.gameObject.SetActive(true);
            CurrentEvent.ChooseTwo.gameObject.SetActive(false);

            NextEvents[0].gameObject.SetActive(true);
            NextEvents[1].gameObject.SetActive(false);

            int[] score = currentEventConfig.ChoiceOneSorce;
            for (int i = 0; i < score.Length; i++)
            {
                float percentage = Mathf.Abs(score[i]) / 20f;
                ChangeIndicator[i].transform.localScale = Vector3.one * Mathf.Clamp01(Mathf.Abs(xMoveDistance)/EnsureDistance) * percentage;
            }
        }
        else
        {
            CurrentEvent.ChooseOne.gameObject.SetActive(false);
            CurrentEvent.ChooseTwo.gameObject.SetActive(true);


            NextEvents[0].gameObject.SetActive(false);
            NextEvents[1].gameObject.SetActive(true);

            int[] score = currentEventConfig.ChoiceOneSorce;
            for (int i = 0; i < score.Length; i++)
            {
                float percentage = Mathf.Abs(score[i]) / 20f;
                ChangeIndicator[i].transform.localScale = Vector3.one * Mathf.Clamp01(Mathf.Abs(xMoveDistance) / EnsureDistance) * percentage;
            }

        }

        if (Mathf.Abs(xMoveDistance) > EnsureDistance)
        {
            NextEvent();
        }
    }

    private void OnRestartGame(UIMsg msg)
    {
        healthValue = (int[])msg.args[0];
        currentEventConfig = msg.args[1] as EventConfig;

        for (int i = 0; i < healthValue.Length; i++)
        {
            Bars[i].fillAmount = healthValue[i] / 100f;
        }
        ChooseDesc.text = currentEventConfig.Event;
    }

    private void SetNextEventImage()
    {
        for (int i = 0; i < nextEventConfigs.Length; i++)
        {
            NextEvents[i].SetEvent(nextEventConfigs[i]);
        }
    }


    private void NextEvent()
    {

        isAnimating = true;
        DisableTouch();

        seq = DOTween.Sequence();
        int dir = Math.Sign(xMoveDistance);

        int nextEventIndex = 0;

        if (dir < 0)
        {
            nextEventIndex = 0;
        }
        else
        {
            nextEventIndex = 1;
        }

        currentEventConfig = GameStart.Game.ChooseEvent(dir);
        ChooseDesc.text = currentEventConfig.Event;
        RoleName.text = roleNameDict[currentEventConfig.Hero];
        ChangeIndicator.ForEach((i) => i.transform.localScale = Vector3.zero);

        UpdateFillAmount();

        seq.Insert(0, DOTween.ToAlpha(() => CurrentEvent.Image.color, (c) => CurrentEvent.Image.color = c, 0, 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DORotate(new Vector3(0, 0, -60 * dir), 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DOLocalMove(originPos + new Vector3(360, 0, 0) * dir, 1.0f));
        seq.OnComplete(() =>
        {

            EventImage temp = CurrentEvent;
            CurrentEvent = NextEvents[nextEventIndex];
            NextEvents[nextEventIndex] = temp;
            NextEvents[nextEventIndex].rectTransform.localPosition = originPos;
            NextEvents[nextEventIndex].rectTransform.eulerAngles = Vector3.zero;
            CurrentEvent.rectTransform.SetAsLastSibling();

            Color c = NextEvents[nextEventIndex].Image.color;
            c.a = 1.0f;
            NextEvents[nextEventIndex].Image.color = c;

            EnableTouch();
            isAnimating = false;

            nextEventConfigs = GameStart.Game.UpdateNextEventConfig();
            SetNextEventImage();
            GameStart.Game.CheckGameOver();
        });
        seq.Play();
    }

    private void DisableTouch()
    {
        CurrentEvent.DisableTouch();
        NextEvents.ForEach((i)=>i.DisableTouch());
       
    }

    private void EnableTouch()
    {
        CurrentEvent.EnableTouch();
        NextEvents.ForEach((i) => i.EnableTouch());
    }

    private void UpdateFillAmount()
    {
        DOTween.To(() => Bars[0].fillAmount, (f) => Bars[0].fillAmount = f, healthValue[0] / 100f, 0.5f).Play();
        DOTween.To(() => Bars[1].fillAmount, (f) => Bars[1].fillAmount = f, healthValue[1] / 100f, 0.5f).Play();
        DOTween.To(() => Bars[2].fillAmount, (f) => Bars[2].fillAmount = f, healthValue[2] / 100f, 0.5f).Play();
        DOTween.To(() => Bars[3].fillAmount, (f) => Bars[3].fillAmount = f, healthValue[3] / 100f, 0.5f).Play();
    }
}
