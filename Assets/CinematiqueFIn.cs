using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicFIn : MonoBehaviour
{
    public Transform posToReach;
    public bool cinematicPlaying = false;

    public GameObject fisrtCam;

    public GameObject EndUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cinematicPlaying)
        {
            cinematicPlaying = true;
            StartCoroutine(CinematicAstralPocket());
            //StartCoroutine(HUD.Instance.Tutorial());
        }
    }

    public float timeToWalk = 3;

    IEnumerator CinematicAstralPocket()
    {
        PlayerController.Instance.playerHasControl = false;
        fisrtCam.SetActive(true);
        PlayerController.Instance._playerAnimator.SetFall(false);
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

        yield return new WaitForSeconds(1);
        

        //Fin de la cinématique le joueur reprend le contrôle
        
        
        EndUI.SetActive(true);
        
        
    }

}
