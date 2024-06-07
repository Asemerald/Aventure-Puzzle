using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralPocketEnabler : MonoBehaviour
{
    public Transform posToReach;
    public bool cinematicPlaying = false;

    public GameObject fisrtCam;
    public GameObject secondCam;

    public Animator cardAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cinematicPlaying)
        {
            cinematicPlaying=true;
            StartCoroutine(CinematicAstralPocket());
            //StartCoroutine(HUD.Instance.Tutorial());
        }
    }

    public float timeToWalk = 3;

    IEnumerator CinematicAstralPocket()
    {
        PlayerController.Instance.playerHasControl = false;
        fisrtCam.SetActive(true);
        PlayerController.Instance._playerAnimator.SetSpeed(0);
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

        fisrtCam.SetActive(false);
        secondCam.SetActive(true);
        yield return new WaitForSeconds(1);

        //Déclencher la cinematique de la caméra qui suit la carte

        cardAnimation.Play("ReceiveCard");
        yield return new WaitForSeconds(2.5f);
        //Puis les 3 cartes qui tourne autour du joueur
        PlayerController.Instance.hasAstralPocket = true;
        RotationCards.Instance.SetAngle(3);

        yield return new WaitForSeconds(1.5F);

        secondCam.SetActive(false);

        //Fin de la cinématique le joueur reprend le contrôle
        PlayerController.Instance.playerHasControl = true;
        HUD.Instance.inGamePanel.SetActive(true);
    }
}
