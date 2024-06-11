using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
   
   public static MainMenuManager Instance { get; private set; }
   
   [SerializeField] private Material DeskMaterial;
   [SerializeField] private GameObject Cards;
   
   [Header("MainMenu Buttons")]
   [SerializeField] private MainMenuButton PlayButton;
   [SerializeField] private MainMenuButton OptionsButton;
   [SerializeField] private MainMenuButton ChapterButton;
   [SerializeField] private MainMenuButton CreditsButton;
   [SerializeField] private MainMenuButton QuitButton;

   [Header("Initial Fade")] 
   [SerializeField] private CanvasGroup blackScreen;
   [SerializeField] private float duration;
   
   private bool InitialfadeOut;
   
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
   
   [Header("Additionals Buttons")]
   [SerializeField] private Toggle FullScreenToggle;
   [SerializeField] private Slider MusicVolumeSlider;
   [SerializeField] private Slider SFXVolumeSlider;
   [SerializeField] private TMP_Dropdown ResolutionDropdown;
   
   private CanvasGroup currentPanel;
   


   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      //Hide and lock cursor
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      
      blackScreen.gameObject.SetActive(true);
      InitialfadeOut = true; 
   }
   
   //on application focus, lock cursor
   private void OnApplicationFocus(bool hasFocus)
   {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }

   private void Update()
   {
      InitialFade();
      
      if (InputsUIBrain.Instance.back.WasPressedThisFrame())
      {
         Debug.Log("Back Pressed");
         Back();
      }
      
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
         InitialfadeOut = false;
         Cards.SetActive(true);
         StartCoroutine(SelectPlayButton());
         //TODO Lights Candles
      }
   }
   
   
   
   
   private IEnumerator SelectPlayButton()
   {
      yield return new WaitForSeconds(2);
      EventSystem.current.SetSelectedGameObject(MainMenuFirstButton.gameObject);
   }

   public async void OpenMenuPanel(float MenuId)
   {
      switch (MenuId)
      {
         case 0:
            MainMenuPanel.SetActive(true);
            if (OptionsPanel.activeInHierarchy)
            {
               OptionsPanel.GetComponent<PanelFader>().Hide();
            }

            if (ChapterPanel.activeInHierarchy)
            {
               ChapterPanel.GetComponent<PanelFader>().Hide();
            }
            
            if (CreditsPanel.activeInHierarchy)
            {
               CreditsPanel.GetComponent<PanelFader>().Hide();
            }
            
            if (QuitPanel.activeInHierarchy)
            {
               QuitPanel.GetComponent<PanelFader>().Hide();
            }
            await WaitFor1Second();
            EventSystem.current.SetSelectedGameObject(MainMenuFirstButton.gameObject);
            break;
         case 1:
            OptionsPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitFor1Second();
            EventSystem.current.SetSelectedGameObject(OptionsFirstButton.gameObject);
            break;
         case 2:
            ChapterPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitFor1Second();
            EventSystem.current.SetSelectedGameObject(ChapterFirstButton.gameObject);
            break;
         case 3:
            MainMenuPanel.SetActive(false);
            StartGame();
            break;
         case 4:
            CreditsPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitFor1Second();
            EventSystem.current.SetSelectedGameObject(CreditsFirstButton.gameObject);
            break;
         case 5:
            QuitPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitFor1Second();
            EventSystem.current.SetSelectedGameObject(QuitFirstButton.gameObject);
            break;
         
      }
   }
   
   
   private void StartGame()
   {
      //TODO Load Scene
      Debug.Log("Start Game");
   }

   public async void Back()
   {
      if (OptionsPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitFor1Second();
         OptionsButton.BackPress();
      }
      else if (ChapterPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitFor1Second();
         ChapterButton.BackPress();
      }
      else if(CreditsPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitFor1Second();
         CreditsButton.BackPress();
      }
      else if(QuitPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitFor1Second();
         QuitButton.BackPress();
      }
      else if(MainMenuPanel.activeInHierarchy)
      {
         OpenMenuPanel(5);
      }
   }
   
   private Task WaitFor1Second()
   {
      return Task.Delay(1000);
   }
   
   public void ToggleFullScreen()
   {
      Screen.fullScreen = FullScreenToggle.isOn;
   }
   
   public void SetMusicVolume()
   {
      //TODO Set Music Volume
   }
   
   public void SetSFXVolume()
   {
      //TODO Set SFX Volume
   }
   
   public void SetResolution()
   {
      if (ResolutionDropdown.value == 0)
      {
         Screen.SetResolution(1920, 1080, Screen.fullScreen);
      }
      else if (ResolutionDropdown.value == 1)
      {
         Screen.SetResolution(1280, 720, Screen.fullScreen);
      }
      else if (ResolutionDropdown.value == 2)
      {
         Screen.SetResolution(800, 600, Screen.fullScreen);
      }
   }
   
}
