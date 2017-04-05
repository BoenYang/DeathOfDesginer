using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins;
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

    public bool isAnimating = false;

    public float xMoveDistance;

    private Vector2 touchDownPos;

    private Sequence seq = null;

    private EventConfig currentEventConfig;

    private EventConfig[] nextEventConfigs = new EventConfig[2];

    public int[] healthValue;

    public override void OnInit()
    {
        originPos = CurrentEvent.rectTransform.localPosition;
    }

    public override void OnRefresh()
    {
        healthValue = (int[])Args[0];
        currentEventConfig = Args[1] as EventConfig;
        ChooseDesc.text = currentEventConfig.Event;

        for (int i = 0; i < healthValue.Length; i++)
        {
            Bars[i].fillAmount = healthValue[i]/100f;
        }

        UpdateNextEventConfig();

    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        
        xMoveDistance = 0;
        if (isAnimating)
        {
            return;
        }

        PointerEventData pointData = eventData as PointerEventData;
        touchDownPos = pointData.position;
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
        Vector2 pos = originPos + new Vector3(xMoveDistance, 0,0);
        CurrentEvent.rectTransform.localPosition = pos;
        float angleRatio = -xMoveDistance/EnsureDistance;
        CurrentEvent.rectTransform.eulerAngles = new Vector3(0,0,40 * angleRatio);

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

    private void UpdateNextEventConfig()
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
                nextEventConfigs[0]= EventConfigMng.EventDict[1][randIndex];
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
                nextEventConfigs[0] = nextEventConfigs[1]  = EventConfigMng.EventDict[1][randIndex];
            }
        }
    }

    private void NextEventImage()
    {

        isAnimating = true;
        DisableTouch();

        seq = DOTween.Sequence();
        float dir = Mathf.Sign(xMoveDistance);
        int nextEventIndex = 0;


        if (xMoveDistance < 0)
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
            UpdateFillAmount();
        }
        else
        {
            for (int i = 0; i < currentEventConfig.ChoiceTwoSorce.Length; i++)
            {
                healthValue[i] += currentEventConfig.ChoiceTwoSorce[i];
            }
            UpdateFillAmount();
        }

        currentEventConfig = nextEventConfigs[nextEventIndex];
        ChooseDesc.text = currentEventConfig.Event;

        seq.Insert(0, DOTween.ToAlpha(() => CurrentEvent.color, (c) => CurrentEvent.color = c, 0, 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DORotate(new Vector3(0, 0, -60*dir), 1.0f));
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

            UpdateNextEventConfig();
            isAnimating = false;
            EnableTouch();
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
