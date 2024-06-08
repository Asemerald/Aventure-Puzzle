using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
   [SerializeField] private Button PlayButton;
   [SerializeField] private Material DeskMaterial;
   [SerializeField] private GameObject Cards;

   [Header("Initial Fade")] 
   [SerializeField] private CanvasGroup blackScreen;
   [SerializeField] private float duration;

   private bool fadeIn;
   private bool fadeOut;


   private void Start()
   {
      blackScreen.gameObject.SetActive(true);
      fadeOut = true; 
   }

   private void Update()
   {
      if (fadeOut)
      {
         if (blackScreen.alpha >= 0)
         {
            blackScreen.alpha -= Time.deltaTime / duration;
         }
         if (blackScreen.alpha == 0)
         {
            fadeOut = false;
            Cards.SetActive(true);
            //TODO Lights Candles
         }
      }
      if (fadeIn)
      {
         if (blackScreen.alpha < 1)
         {
            blackScreen.alpha += Time.deltaTime / duration;
         }
         if (blackScreen.alpha >= 1)
         {
            fadeIn = false;
         }
      }
   }
}
