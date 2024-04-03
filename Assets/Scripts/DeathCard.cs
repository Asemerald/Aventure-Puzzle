using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCard : CardTemplate
{
    //All the children will have access to the parent function and variable without needing to declare it

    public override void NoCard()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Death, CardState.None);

        base.NoCard();
    }

    public override void CardUpside()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Death, CardState.Endroit);

        base.CardUpside();
    }

    public override void CardUpsideDown()
    {
        InterractiblesManager.Instance.ApplyChanges(InteractibleTemplate.CardDirectModification.Death, CardState.Envers);

        base.CardUpsideDown();
    }
}
