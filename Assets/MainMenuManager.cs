using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
   [SerializeField] private GameObject PlayButton;
   [SerializeField] private Material DeskMaterial;
   [SerializeField] private GameObject Cards;
   [SerializeField] private float waitForBeforeSelect;

   [Header("Emissive Settings")] 
   [SerializeField] private float duration;
   [SerializeField] private float targetIntensity = 1f;
   [SerializeField] private float startIntensity = 0f;
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
         float currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, t);
         Color newEmissionColor = EmissiveColor * currentIntensity;
         DeskMaterial.SetColor("_EmissionColor", newEmissionColor);
         yield return null;
      }

      // Ensure the final intensity is set exactly
      DeskMaterial.SetColor("_EmissionColor", EmissiveColor * targetIntensity);
      Cards.SetActive(true);
      
   }

   private IEnumerator SelectCard()
   {
      yield return new WaitForSeconds(waitForBeforeSelect);
      PlayButton.GetComponent<MainMenuButton>().ButtonClicked();
   }
   
   

}
