using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCard : CardTemplate
{

    //All the children will have access to the parent function and variable without needing to declare it

    public override void NoCard()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Size, CardState.None);

        base.NoCard();
    }

    public override void CardUpside()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Size, CardState.Endroit);

        base.CardUpside();
    }

    public override void CardUpsideDown()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Size, CardState.Envers);

        base.CardUpsideDown();
    }

    public void ExempleFunction()
    {
        //You don't need to declare a new string var for the name, it already exist in the parent
        cardName = "Exemple";
    }

}
