using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
   
   public static MainMenuManager Instance { get; private set; }
   
   [SerializeField] private Material DeskMaterial;
   [SerializeField] private GameObject Desk;
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
   [SerializeField] private Color EmissiveColor;
   [SerializeField] private float EmissiveIntensity;
   
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
      FadeOutQuit = false;
      
      DeskMaterial.SetColor("_EmissionColor", EmissiveColor * EmissiveIntensity);
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
      FadeOutOnQuit();
      
      if (InputsUIBrain.Instance.back.WasPressedThisFrame())
      {
         Debug.Log("Back Pressed");
         Back();
      }
      
   }
   
   private async void InitialFade()
   {
      if (!InitialfadeOut) return;
      if (blackScreen.alpha >= 0)
      {
         blackScreen.alpha -= Time.deltaTime / duration;
      }
      if (blackScreen.alpha == 0)
      {
         InitialfadeOut = false;
         StartCoroutine(SelectPlayButton());
         StartCoroutine(FadeEmissiveOut());
         await WaitForSeconds(2);
         Cards.SetActive(true);
      }
   }
   
   private IEnumerator FadeEmissiveOut()
   {
      Debug.LogWarning("Fading Emissive Out");
      Color startColor = DeskMaterial.GetColor("_EmissionColor");
      Color targetColor = new Color(0f, 0f, 0f); // Black (no emission)
      float elapsedTime = 0f;

      while (elapsedTime < 2f)
      {
         elapsedTime += Time.deltaTime;
         float t = Mathf.Clamp01(elapsedTime / 2f);
         Color newColor = Color.Lerp(startColor, targetColor, t);

         // Set the emissive color
         DeskMaterial.SetColor("_EmissionColor", newColor);
         yield return null; // Wait for the next frame
      }

      // Ensure the final color is exactly the target color
      DeskMaterial.SetColor("_EmissionColor", targetColor);
   }



   
   
   private IEnumerator SelectPlayButton()
   {
      yield return new WaitForSeconds(4);
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
            await WaitForSeconds(1);
            EventSystem.current.SetSelectedGameObject(MainMenuFirstButton.gameObject);
            break;
         case 1:
            OptionsPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitForSeconds(1);
            EventSystem.current.SetSelectedGameObject(OptionsFirstButton.gameObject);
            break;
         case 2:
            ChapterPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitForSeconds(1);
            EventSystem.current.SetSelectedGameObject(ChapterFirstButton.gameObject);
            break;
         case 3:
            MainMenuPanel.SetActive(false);
            await WaitForSeconds(1);
            StartGame();
            break;
         case 4:
            CreditsPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitForSeconds(1);
            EventSystem.current.SetSelectedGameObject(CreditsFirstButton.gameObject);
            break;
         case 5:
            QuitPanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            await WaitForSeconds(1);
            EventSystem.current.SetSelectedGameObject(QuitFirstButton.gameObject);
            break;
         
      }
   }
   
   
   private void StartGame()
   {
      SceneManager.LoadScene(SceneToLoad);
   }

   public async void Back()
   {
      if (OptionsPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitForSeconds(1);
         OptionsButton.BackPress();
      }
      else if (ChapterPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitForSeconds(1);
         ChapterButton.BackPress();
      }
      else if(CreditsPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitForSeconds(1);
         CreditsButton.BackPress();
      }
      else if(QuitPanel.activeInHierarchy)
      {
         OpenMenuPanel(0);
         await WaitForSeconds(1);
         QuitButton.BackPress();
      }
      else if(MainMenuPanel.activeInHierarchy)
      {
         OpenMenuPanel(5);
      }
   }
   
   private Task WaitForSeconds(int seconds)
   {
      return Task.Delay(seconds * 1000);
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
   
   [Header("Scene to Load")]
   [SerializeField] private int SceneToLoad;
   
   
   private bool FadeOutQuit;
   private void FadeOutOnQuit()
   {
      //make dark screen fade in
      
      if (FadeOutQuit)
      {
         if (blackScreen.alpha <= 1)
         {
            blackScreen.alpha += Time.deltaTime / 1;
         }
         if (blackScreen.alpha >= 1)
         {
            FadeOutQuit = false;
            //Quit
            Application.Quit();
            Debug.Log("Quit Game");
         }
      }
      
   }

   public void Quit()
   {
      //make dark screen fade in and then quit
      FadeOutQuit = true;
   }
}
