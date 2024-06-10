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

    // Private fields
    [SerializeField] private float elapsedTime = 0f;
    private ParticleSystem PS;
    private MeshRenderer renderer;
    #endregion

    private void Start()
    {
        if (GetComponent<ParticleSystem >() != null) { PS = GetComponent<ParticleSystem>(); }
        if (GetComponent<MeshRenderer>() != null)    { renderer = GetComponent<MeshRenderer>(); }
    }

    public void Play()
    {
        if (PS != null) { PS.Play(); }
        if (renderer != null) { renderer.enabled = true; }
        StartCoroutine(PlayShaderAnim());
        

    }

    IEnumerator PlayShaderAnim()
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
        if (renderer != null) { renderer.enabled = false; }
    }
}
