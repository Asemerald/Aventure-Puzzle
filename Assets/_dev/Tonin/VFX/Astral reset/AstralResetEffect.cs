using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralResetEffect : MonoBehaviour
{
    #region Fields
    public Material waveMat;
    public float duration = 1f;
    public AnimationCurve amplitudeCurve;
    public AnimationCurve frequencyCurve;
    public bool play;

    // Private fields
    [SerializeField] private float elapsedTime = 0f;
    #endregion

    void Update()
    {
        if (play)
        {
            StartCoroutine(Play());
            play = false;
        }
    }

    IEnumerator Play()
    {
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            waveMat.SetFloat("_Amplitude", amplitudeCurve.Evaluate(elapsedTime/duration));
            waveMat.SetFloat("_Frequency", frequencyCurve.Evaluate(elapsedTime / duration));
            waveMat.SetFloat("_ElapsedTime", elapsedTime);
            yield return null;
        }
    }
}
