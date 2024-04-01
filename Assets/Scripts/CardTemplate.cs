using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTemplate : MonoBehaviour
{

    //All the children will herit of the variable and fonction of this script

    public string cardName;


    public virtual void CardUpside()
    {

    }

    public virtual void CardUpsideDown()
    {

    }

}
