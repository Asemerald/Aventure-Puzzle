using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMarketStart : MonoBehaviour
{
    public bool cinematicPlaying = false;

    public GameObject fisrtCam;
    public ParticleSystem crashParticles;
    
    public Transform StartPos;

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
        transform.position = StartPos.position + new Vector3(-37.5f, -5.5f, 0f);
        //Cin�matique de suzie

        fisrtCam.SetActive(true);
        //Puis VFX Antonin et placer le joueur a un emplacement pr�cis
        HUD.Instance.whiteFade.SetActive(true);
        HUD.Instance.whiteFadeAnim.Play("FadeToScreen");

        PlayerController.Instance.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(.5f);
        crashParticles.Play();

        yield return new WaitForSeconds(4.5f);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ChuteMeteore, this.transform.position);
        PlayerController.Instance.transform.position = StartPos.position;
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
