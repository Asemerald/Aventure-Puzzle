using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCard : CardTemplate
{
    //All the children will have access to the parent function and variable without needing to declare it

    public override void NoCard()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Fire, CardState.None);

        base.NoCard();
    }

    public override void CardUpside()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Fire, CardState.Endroit);

        base.CardUpside();
    }

    public override void CardUpsideDown()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Fire, CardState.Envers);

        base.CardUpsideDown();
    }
}
