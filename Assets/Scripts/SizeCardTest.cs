using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCardTest : CardTemplate
{

    //All the children will have access to the parent function and variable without needing to declare it

    public override void CardUpside()
    {
        base.CardUpside();
    }

    public override void CardUpsideDown()
    {
        base.CardUpside();
    }

    public void ExempleFunction()
    {
        //You don't need to declare a new string var for the name, it already exist in the parent
        cardName = "Exemple";
    }

}
