using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCardTest : CardTemplate
{

    //All the children will have access to the parent function and variable without needing to declare it

    public override void NoCard()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Size");
        foreach (var item in objects)
        {
            item.transform.localScale = Vector3.one * 2;
        }

        base.NoCard();
    }

    public override void CardUpside()
    {
        //Using base dot the name of the original function, u play the original function
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Size");
        foreach (var item in objects)
        {
            item.transform.localScale = Vector3.one * 3;
        }

        base.CardUpside();
    }

    public override void CardUpsideDown()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Size");
        foreach (var item in objects)
        {
            item.transform.localScale = Vector3.one * 1;
        }

        base.CardUpside();
    }

    public void ExempleFunction()
    {
        //You don't need to declare a new string var for the name, it already exist in the parent
        cardName = "Exemple";
    }

}
