using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFader : MonoBehaviour
{
    
    public static PanelFader Instance { get; private set; }

    private CanvasGroup Panel;
    private bool InitialfadeIn;

    private bool FadeOut;
    private void Awake()
    {
        Panel = GetComponent<CanvasGroup>();
        Panel.alpha = 0;
    }
    

    private void OnEnable()
    {
        Panel.alpha = 0;
        StartCoroutine(WaitFor1Second());
        FadeOut = false;
    }

    private IEnumerator WaitFor1Second()
    {
        yield return new WaitForSeconds(1);
        InitialfadeIn = true;
    }

    private void Update()
    {
        
        if (InitialfadeIn)
        {
            if (Panel.alpha <= 1)
            {
                Panel.alpha += Time.deltaTime / 1;
            }
            if (Panel.alpha >= 1)
            {
                InitialfadeIn = false;
            }
        }
        
        if (FadeOut)
        {
            if (Panel.alpha >= 0)
            {
                Panel.alpha -= Time.deltaTime / 1;
            }
            if (Panel.alpha == 0)
            {
                FadeOut = false;
            }
        }
    }

    public void Hide()
    {
        StartCoroutine(HidePanel());
    }
    
    private IEnumerator HidePanel()
    {
        FadeOut = true;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
