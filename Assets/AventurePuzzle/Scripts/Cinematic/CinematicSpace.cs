using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicSpace : MonoBehaviour
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
        PlayerController.Instance.playerHasControl = false;
        fisrtCam.SetActive(true);
        PlayerController.Instance.transform.position = new Vector3(-56.5f, -6, 0);
        PlayerController.Instance._playerAnimator.SetFall(false);
        PlayerController.Instance._playerAnimator.SetSpeed(0);
        HUD.Instance.whiteFade.SetActive(true);
        HUD.Instance.whiteFadeAnim.Play("FadeToScreen");
        yield return new WaitForSeconds(1);

        //Le joueur perd le contrôle
        //Cinématique Caméra qui suit lejoueur a la statue
        float elapsedTime = 0;
        float distanceFromPos = Vector3.Distance(PlayerController.Instance.transform.position, posToReach.position);
        float speed = (distanceFromPos / timeToWalk) / 14.5f;

        Vector3 startPos = PlayerController.Instance.transform.position;

        PlayerController.Instance.transform.LookAt(posToReach.position);

        while (elapsedTime < timeToWalk)
        {
            elapsedTime += Time.deltaTime;

            Vector3 newPos = Vector3.Lerp(startPos, posToReach.position, elapsedTime / timeToWalk);
            PlayerController.Instance.transform.position = newPos;
            PlayerController.Instance._playerAnimator.SetSpeed(speed);

            yield return null;
        }

        PlayerController.Instance._playerAnimator.SetSpeed(0);

        yield return new WaitForSeconds(1);
        fisrtCam.SetActive(false);


        //Fin de la cinématique le joueur reprend le contrôle
        PlayerController.Instance.playerHasControl = true;
        HUD.Instance.inGamePanel.SetActive(true);
    }
}
