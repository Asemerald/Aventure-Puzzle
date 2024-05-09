using UnityEditor;
using UnityEngine;

public class DeveloperMenu : MonoBehaviour
{
    

    [MenuItem("Dev/Show Astral Pocket", true)]
    private static bool ShowAstralPocketGoldValidation()
    {
        //if play mode is active, set to true
        return Application.isPlaying;
    }
    
    [MenuItem("Dev/Show Astral Pocket")]
    private static void ShowAstralPocket()
    {
        AstralPocket.Instance.ShowAstralPocket = !AstralPocket.Instance.ShowAstralPocket;
    }
    
}