using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTemplate : MonoBehaviour
{

    //All the children will herit of the variable and fonction of this script

    public string cardName;
    public enum CardState { None, Endroit, Envers}
    public CardState state;

    public virtual void NoCard()
    {
        Debug.Log("Tarot Inventory : no effects");
    }

    public virtual void CardUpside()
    {
        Debug.Log("Tarot Inventory : upside effect");
    }

    public virtual void CardUpsideDown()
    {
        Debug.Log("Tarot Inventory : upside down effect");
    }

}
