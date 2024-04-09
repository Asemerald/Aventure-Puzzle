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
        if (!GameManager.Instance.inTarotInventory || GameManager.Instance.gameIsPause)
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

        UpdateHUDState();
    }

    public void UpdateHUDState()
    {
        for (int i = 0; i < cards.Length; i++)
            HUD.Instance.UpdateHUDCard(i, cards[i].state);
    }

    public void ApplyCard()
    {

        for(int i = 0; i < cards.Length; i++)
        {
            switch (cards[i].state)
            {
                case CardTemplate.CardState.None:
                    cards[i].NoCard();
                    break;
                case CardTemplate.CardState.Endroit:
                    cards[i].CardUpside();
                    break;
                case CardTemplate.CardState.Envers:
                    cards[i].CardUpsideDown();
                    break;
            }
        }
    }

    void SelectCardHUD()
    {
        HUD.Instance.tarotSelect.position = HUD.Instance.cardsPos[selectedCard].transform.position;
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
