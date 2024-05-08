using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DevelopperMenu : MonoBehaviour
{
    
    public class MyCustomMenuItems
    {
    [MenuItem("MyMenu/Do Something")]
    private static void DoSomething()
    {
       AstralPocket.Instance.CastAstralPocket();
    }
    }

}
