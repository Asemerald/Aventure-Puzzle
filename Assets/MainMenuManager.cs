using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
   
   public static MainMenuManager Instance { get; private set; }
   [SerializeField] private Button PlayButton;
   [SerializeField] private Material DeskMaterial;
   [SerializeField] private GameObject Cards;

   [Header("Initial Fade")] 
   [SerializeField] private CanvasGroup blackScreen;
   [SerializeField] private float duration;
   
   private bool InitialfadeOut;
   private bool fadeOut;
   private bool fadeIn;
   
   [Header("Menu Panels")] 
   [SerializeField] private GameObject MainMenuPanel;
   [SerializeField] private GameObject OptionsPanel;
   [SerializeField] private GameObject ChapterPanel;
   [SerializeField] private GameObject CreditsPanel;
   [SerializeField] private GameObject QuitPanel;
   
   [SerializeField] private float FadeDuration;
   
   [Header("First Buttons")] 
   [SerializeField] private Button MainMenuFirstButton;
   [SerializeField] private Button OptionsFirstButton;
   [SerializeField] private Button ChapterFirstButton;
   [SerializeField] private Button CreditsFirstButton;
   [SerializeField] private Button QuitFirstButton;
   
   private CanvasGroup currentPanel;
   


   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      blackScreen.gameObject.SetActive(true);
      fadeOut = true; 
   }

   private void Update()
   {
      InitialFade();
      
      
   }
   
   private void InitialFade()
   {
      if (!InitialfadeOut) return;
      if (blackScreen.alpha >= 0)
      {
         blackScreen.alpha -= Time.deltaTime / duration;
      }
      if (blackScreen.alpha == 0)
      {
         fadeOut = false;
         Cards.SetActive(true);
         StartCoroutine(SelectPlayButton());
         //TODO Lights Candles
      }
   }
   
   private void FadeOutPanels()
   {
      if (currentPanel.alpha >= 0)
      {
         currentPanel.alpha -= Time.deltaTime / FadeDuration;
      }
      if (currentPanel.alpha == 0)
      {
         fadeOut = false;
      }
   }
   
   private void FadeInPanels()
   {
      if (currentPanel.alpha <= 1)
      {
         currentPanel.alpha += Time.deltaTime / FadeDuration;
      }
      if (currentPanel.alpha >= 1)
      {
         fadeIn = false;
      }
   }
   
   
   
   private IEnumerator SelectPlayButton()
   {
      yield return new WaitForSeconds(2);
      EventSystem.current.SetSelectedGameObject(PlayButton.gameObject);
   }

   public void OpenMenuPanel(float MenuId)
   {
      switch (MenuId)
      {
         case 0:
            MainMenuPanel.SetActive(true);
            OptionsPanel.SetActive(false);   
            ChapterPanel.SetActive(false);
            CreditsPanel.SetActive(false);
            QuitPanel.SetActive(false);
            break;
         case 1:
            OptionsPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            break;
         case 2:
            ChapterPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            break;
         case 3:
            MainMenuPanel.SetActive(false);
            StartGame();
            break;
         case 4:
            CreditsPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            break;
         case 5:
            QuitPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            break;
            
      }
   }
   
   
   private void StartGame()
   {
      //TODO Load Scene
      Debug.Log("Start Game");
   }
   
}
