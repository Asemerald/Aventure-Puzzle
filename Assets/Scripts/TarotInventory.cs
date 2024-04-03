using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TarotInventory : MonoBehaviour
{
    public static TarotInventory Instance {  get; private set; }

    [SerializeField] int selectedCard = 0;
    [SerializeField] CardTemplate[] cards;

    [SerializeField] CardTemplate currendCard;

    bool selectingCard;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SelectCardHUD();
        currendCard = cards[selectedCard];
    }

    private void Update()
    {
        if (!GameManager.Instance.inTarotInventory)
            return;

        if (InputsBrain.Instance.move.ReadValue<Vector2>().x > 0 && !selectingCard)
            StartCoroutine(SelectCard(true));
        else if (InputsBrain.Instance.move.ReadValue<Vector2>().x < 0 && ! selectingCard)
            StartCoroutine(SelectCard(false));
    }

    public void SwitchCardState()
    {
        Debug.Log("Tarot Inventory : Change state");

        switch (currendCard.state)
        {
            case CardTemplate.CardState.None:
                currendCard.state = CardTemplate.CardState.Endroit;
                break;
            case CardTemplate.CardState.Endroit:
                currendCard.state = CardTemplate.CardState.Envers;
                break;
            case CardTemplate.CardState.Envers:
                currendCard.state = CardTemplate.CardState.None;
                break;
        }
    }

    public void ApplyCard()
    {

        for(int i = 0; i < cards.Length; i++)
        {
            currendCard = cards[selectedCard];
            switch (cards[selectedCard].state)
            {
                case CardTemplate.CardState.None:
                    cards[selectedCard].NoCard();
                    break;
                case CardTemplate.CardState.Endroit:
                    cards[selectedCard].CardUpside();
                    break;
                case CardTemplate.CardState.Envers:
                    cards[selectedCard].CardUpsideDown();
                    break;
            }
        }
    }

    void SelectCardHUD()
    {
        HUD.Instance.tarotSelect.position = HUD.Instance.cardsPos[selectedCard].position;
    }

    IEnumerator SelectCard(bool up)
    {
        selectingCard = true;

        if (up)
            selectedCard++;
        else 
            selectedCard--;

        if (selectedCard > 2)
            selectedCard = 0;
        else if (selectedCard < 0)
            selectedCard = 2;

        currendCard = cards[selectedCard];

        SelectCardHUD();

        yield return new WaitForSecondsRealtime(.25f);

        selectingCard = false;
    }
}
