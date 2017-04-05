using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameView : UIBase
{
    public Image CurrentEvent;

    public Image NextEvent;

    public Text ChooseDesc;

    public List<Image> Bars; 

    public float EnsureDistance = 20;

    public float MaxRotAngle = 40;

    private Vector3 originPos;

    private bool isAnimating = false;

    private float xMoveDistance;

    private Vector2 touchDownPos;

    private Sequence seq = null;

    private EventConfig currentEventConfig;

    private int[] healthValue;

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
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
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

        xMoveDistance = pointData.position.x - touchDownPos.x;
        Vector2 pos = originPos + new Vector3(xMoveDistance, 0,0);
        CurrentEvent.rectTransform.localPosition = pos;
        float angleRatio = -xMoveDistance/EnsureDistance;
        CurrentEvent.rectTransform.eulerAngles = new Vector3(0,0,40 * angleRatio);

        if (Mathf.Abs(xMoveDistance) > EnsureDistance)
        {
            NextEventImage();
        }

        if (currentEventConfig.Needchoice == 1)
        {
            if (xMoveDistance > 0)
            {
                ChooseDesc.text = currentEventConfig.ChoiceOne;
            }
            else
            {
                ChooseDesc.text = currentEventConfig.ChoiceTwo;
            }
        }
    }

    private void NextEventImage()
    {
        isAnimating = true;
        DisableTouch();

        seq = DOTween.Sequence();
        float dir = Mathf.Sign(xMoveDistance);

        seq.Insert(0, DOTween.ToAlpha(() => CurrentEvent.color, (c) => CurrentEvent.color = c, 0, 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DORotate(new Vector3(0, 0, -60*dir), 1.0f));
        seq.Insert(0, CurrentEvent.rectTransform.DOLocalMove(originPos + new Vector3(360, 0, 0) * dir, 1.0f));
        seq.OnComplete(() =>
        {
            isAnimating = false;
            EnableTouch();
            Image temp = CurrentEvent;
            CurrentEvent = NextEvent;
            NextEvent = temp;
            NextEvent.rectTransform.localPosition = originPos;
            NextEvent.rectTransform.eulerAngles = Vector3.zero;

            CurrentEvent.rectTransform.SetAsLastSibling();

            Color c = NextEvent.color;
            c.a = 1.0f;
            NextEvent.color = c;


            if (currentEventConfig.Needchoice == 1)
            {
                if (xMoveDistance > 0)
                {
                    currentEventConfig = EventConfig.Get(currentEventConfig.ChoiceOneid[0]);
                }
                else
                {
                    currentEventConfig = EventConfig.Get(currentEventConfig.ChoiceOneid[1]);
                }

                ChooseDesc.text = currentEventConfig.Event;
            }
            else
            {
                currentEventConfig = EventConfig.Get(currentEventConfig.ChoiceOneid[0]);
                ChooseDesc.text = currentEventConfig.Event;
            }

        });
        seq.Play();
    }

    private void DisableTouch()
    {
        CurrentEvent.raycastTarget = false;
        NextEvent.raycastTarget = false;
    }

    private void EnableTouch()
    {
        CurrentEvent.raycastTarget = true;
        NextEvent.raycastTarget = true;
    }


}
