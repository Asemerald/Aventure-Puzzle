using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicMarketStart : MonoBehaviour
{
    public Transform posToReach;
    public bool cinematicPlaying = false;

    public GameObject fisrtCam;

    private void Start()
    {
        cinematicPlaying = true;
        StartCoroutine(CinematicAstralPocket());
    }

    public float timeToWalk = 3;

    IEnumerator CinematicAstralPocket()
    {
        //Cinématique de suzie

        //Puis VFX Antonin et placer le joueur a un emplacement précis
        //PlayerController.Instance.transform.position = new Vector3();
        PlayerController.Instance.playerHasControl = false;
        fisrtCam.SetActive(true);
        PlayerController.Instance._playerAnimator.SetSpeed(0);
        HUD.Instance.whiteFade.SetActive(true);
        HUD.Instance.whiteFadeAnim.Play("FadeToScreen");
        yield return new WaitForSeconds(1);

        //Animation de se relever

        PlayerController.Instance._playerAnimator.SetSpeed(0);

        yield return new WaitForSeconds(1);
        fisrtCam.SetActive(false);


        //Fin de la cinématique le joueur reprend le contrôle
        PlayerController.Instance.playerHasControl = true;
        HUD.Instance.inGamePanel.SetActive(true);
    }
}
