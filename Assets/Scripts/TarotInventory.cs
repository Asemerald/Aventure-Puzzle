using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TarotInventory : MonoBehaviour
{
    public static TarotInventory Instance {  get; private set; }

    [SerializeField] int selectedCard = 0;

    bool selectingCard;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SelectCardHUD();
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

        SelectCardHUD();

        yield return new WaitForSecondsRealtime(.3f);

        selectingCard = false;
    }
}
