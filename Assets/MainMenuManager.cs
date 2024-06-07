using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
   [SerializeField] private bool PlayButton;
   [SerializeField] private Material DeskMaterial;

   [Header("Emissive Settings")] 
   [SerializeField] private float duration;
   [SerializeField] private float targetIntensity = 1f;
   [SerializeField] private Color EmissiveColor;
   
   private float startTime;


   private void Start()
   {
      StartCoroutine(ChangeEmissionIntensity());
   }

   //make the intensity go from 0 to 1 over time
   private IEnumerator ChangeEmissionIntensity()
   {
      while (Time.time - startTime < duration)
      {
         float t = (Time.time - startTime) / duration;
         float currentIntensity = Mathf.Lerp(0f, targetIntensity, t);
         Color newEmissionColor = EmissiveColor * currentIntensity;
         DeskMaterial.SetColor("_EmissionColor", newEmissionColor);
         yield return null;
      }

      // Ensure the final intensity is set exactly
      DeskMaterial.SetColor("_EmissionColor", Color.yellow * targetIntensity);
   }

}
