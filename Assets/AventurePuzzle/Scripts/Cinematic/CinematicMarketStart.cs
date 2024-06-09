using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMarketStart : MonoBehaviour
{
    public bool cinematicPlaying = false;

    public GameObject fisrtCam;

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

        //Puis VFX Antonin et placer le joueur a un emplacement pr�cis
        HUD.Instance.whiteFade.SetActive(true);
        HUD.Instance.whiteFadeAnim.Play("FadeToScreen");
        fisrtCam.SetActive(true);

        PlayerController.Instance.transform.position = new Vector3(37,5.5f,0);

        PlayerController.Instance._playerAnimator._animator.Play("ANIM_StandingUp");
        yield return new WaitForSeconds(5);
        fisrtCam.SetActive(false);

        //Fin de la cin�matique le joueur reprend le contr�le
        PlayerController.Instance.playerHasControl = true;
        HUD.Instance.inGamePanel.SetActive(true);
    }
}
