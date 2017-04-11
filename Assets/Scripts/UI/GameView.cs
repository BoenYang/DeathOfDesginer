using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameView : UIBase
{
    public Image CurrentEvent;

    public Image[] NextEvents;

    public Text ChooseDesc;

    public List<Image> Bars; 

    public float EnsureDistance = 20;

    public float MaxRotAngle = 40;

    private Vector3 originPos;

    private bool isAnimating = false;

    private float xMoveDistance;

    private Sequence seq = null;

    private EventConfig currentEventConfig;

    private readonly EventConfig[] nextEventConfigs = new EventConfig[2];

    private int[] healthValue;

    public override void OnInit()
    {
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

        ChooseDesc.text = currentEventConfig.Event;

        GameStart.Game.UpdateNextEventConfig();
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        
        xMoveDistance = 0;
        if (isAnimating)
        {
            return;
        }
    }

    public void OnEndDrag(BaseEventData eventData)
    {
 
        if (isAnimating)
        {
            return;
        }
        
        if (Mathf.Abs(xMoveDistance) > EnsureDistance)
        {
            NextEventImage();
        }
        else
        {
            isAnimating = true;
            DisableTouch();

            ChooseDesc.text = currentEventConfig.Event;
            
            seq = DOTween.Sequence();
            seq.Insert(0, CurrentEvent.rectTransform.DOLocalMove(originPos, 1.0f));
            seq.Insert(0, CurrentEvent.transform.DORotate(Vector3.zero, 1.0f));
            seq.OnComplete(() =>
            {
                isAnimating = false;
                EnableTouch();
                for (int i = 0; i < NextEvents.Length; i++)
                {
                    NextEvents[i].gameObject.SetActive(false);
                }
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
        xMoveDistance += pointData.delta.x;
        float angleRatio = -xMoveDistance / EnsureDistance;

        Vector2 pos = originPos + new Vector3(xMoveDistance, 0,0);
        CurrentEvent.rectTransform.localPosition = pos;
        CurrentEvent.rectTransform.eulerAngles = new Vector3(0,0,MaxRotAngle * angleRatio);

        if (xMoveDistance < 0)
        {
            NextEvents[0].gameObject.SetActive(true);
            NextEvents[1].gameObject.SetActive(false);
        }
        else
        {
            NextEvents[0].gameObject.SetActive(false);
            NextEvents[1].gameObject.SetActive(true);
        }

        if (Mathf.Abs(xMoveDistance) > EnsureDistance)
        {
            NextEventImage();
        }
        else
        {
            if (currentEventConfig.Needchoice == 1)
            {
                if (xMoveDistance < 0)
                {
                    ChooseDesc.text = currentEventConfig.ChoiceOne;
                }
                else
                {
                    ChooseDesc.text = currentEventConfig.ChoiceTwo;
                }
            }
            else
            {
                ChooseDesc.text = currentEventConfig.ChoiceOne;
            }
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


    private void NextEventImage()
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
        UpdateFillAmount();
        ChooseDesc.text = currentEventConfig.Event;

        seq.Insert(0, DOTween.ToAlpha(() => CurrentEvent.color, (c) => CurrentEvent.color = c, 0, 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DORotate(new Vector3(0, 0, -60 * dir), 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DOLocalMove(originPos + new Vector3(360, 0, 0) * dir, 1.0f));
        seq.OnComplete(() =>
        {

            Image temp = CurrentEvent;
            CurrentEvent = NextEvents[nextEventIndex];
            NextEvents[nextEventIndex] = temp;
            NextEvents[nextEventIndex].rectTransform.localPosition = originPos;
            NextEvents[nextEventIndex].rectTransform.eulerAngles = Vector3.zero;
            CurrentEvent.rectTransform.SetAsLastSibling();

            Color c = NextEvents[nextEventIndex].color;
            c.a = 1.0f;
            NextEvents[nextEventIndex].color = c;

            Image image = null;
            for (int i = 0; i < NextEvents.Length; i++)
            {
                image = NextEvents[i];
                image.gameObject.SetActive(false);
            }


            EnableTouch();
            isAnimating = false;

            GameStart.Game.UpdateNextEventConfig();
            GameStart.Game.CheckGameOver();
        });
        seq.Play();
    }

    private void DisableTouch()
    {
        CurrentEvent.raycastTarget = false;
        for (int i = 0; i < NextEvents.Length; i++)
        {
            NextEvents[i].raycastTarget = false;
        }
    }

    private void EnableTouch()
    {
        CurrentEvent.raycastTarget = true;
        for (int i = 0; i < NextEvents.Length; i++)
        {
            NextEvents[i].raycastTarget = true;
        }
    }

    private void UpdateFillAmount()
    {
        DOTween.To(() => Bars[0].fillAmount, (f) => Bars[0].fillAmount = f, healthValue[0] / 100f, 0.5f).Play();
        DOTween.To(() => Bars[1].fillAmount, (f) => Bars[1].fillAmount = f, healthValue[1] / 100f, 0.5f).Play();
        DOTween.To(() => Bars[2].fillAmount, (f) => Bars[2].fillAmount = f, healthValue[2] / 100f, 0.5f).Play();
        DOTween.To(() => Bars[3].fillAmount, (f) => Bars[3].fillAmount = f, healthValue[3] / 100f, 0.5f).Play();
    }
}
