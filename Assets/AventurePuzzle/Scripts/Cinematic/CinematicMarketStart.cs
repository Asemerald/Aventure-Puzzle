using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMarketStart : MonoBehaviour
{
    public bool cinematicPlaying = false;

    public GameObject fisrtCam;
    public ParticleSystem crashParticles;

    public bool testAnim;

    private void Start()
    {
        cinematicPlaying = true;
        StartCoroutine(CinematicAstralPocket());
    }

    IEnumerator CinematicAstralPocket()
    {
        PlayerController.Instance.playerHasControl = false;
        PlayerController.Instance._playerAnimator.SetFall(false);
        //Cin�matique de suzie

        fisrtCam.SetActive(true);
        //Puis VFX Antonin et placer le joueur a un emplacement pr�cis
        HUD.Instance.whiteFade.SetActive(true);
        HUD.Instance.whiteFadeAnim.Play("FadeToScreen");

        PlayerController.Instance.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(.5f);
        crashParticles.Play();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ChuteMeteore, this.transform.position);

        yield return new WaitForSeconds(4.5f);
        
        PlayerController.Instance.transform.position = new Vector3(37,5.5f,0);
        //AudioManager.instance.PlayOneShot(FMODEvents.instance.Crash, this.transform.position);
        
        
        PlayerController.Instance._playerAnimator._animator.Play("ANIM_StandingUp");

        yield return new WaitForSeconds(5);
        fisrtCam.SetActive(false);

        //Fin de la cin�matique le joueur reprend le contr�le
        PlayerController.Instance.playerHasControl = true;
        HUD.Instance.inGamePanel.SetActive(true);
    }

    private void Update()
    {
        if (testAnim)
        {
            testAnim = false;
            PlayerController.Instance._playerAnimator._animator.Play("ANIM_StandingUp");
        }
    }
}
