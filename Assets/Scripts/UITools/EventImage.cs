using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventImage : MonoBehaviour
{

    public Image Image;

    public Text ChooseOne;

    public Text ChooseTwo;

    public RectTransform rectTransform;

    private EventConfig config;

    public void DisableTouch()
    {
        Image.raycastTarget = false;
    }

    public void EnableTouch()
    {
        Image.raycastTarget = true;
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetEvent(EventConfig eventConfig)
    {
        config = eventConfig;
        Image.sprite = Resources.Load<Sprite>("Texture/UI/Role/" + config.Hero);
        ChooseOne.text = config.ChoiceOne;
        ChooseTwo.text = config.ChoiceTwo;

        ChooseOne.gameObject.SetActive(false);
        ChooseTwo.gameObject.SetActive(false);
    }
}
